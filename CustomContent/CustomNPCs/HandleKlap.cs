using bbpfer.Extessions;
using UnityEngine;
using PixelInternalAPI.Classes;
using bbpfer.FundamentalManagers;
using bbpfer.CustomLoaders;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class HandleKlap : NPC, IClickable<int>, CustomDataNPC
    {
        public void Setup()
        {
            audMan = this.getAudMan("#231921");
            clapVoiceline = AssetsCreator.CreateSound("HandleKlap_ClapHere", "Characters", "Vfx_HK_Clap", SoundType.Voice, "#231921", 1);
            happyVoiceline = AssetsCreator.CreateSound("HandleKlap_InTime", "Characters", "Vfx_HK_InTime", SoundType.Voice, "#231921", 1);
            sadVoiceline = AssetsCreator.CreateSound("HandleKlap_OutTime", "Characters", "Vfx_HK_OutTime", SoundType.Voice, "#231921", 1);
            clapSound = AssetsCreator.CreateSound("HandleKlap_ClapSound", "Characters", "Sfx_HK_Clap", SoundType.Effect, "#231921", 1);

            idleSprite = AssetsCreator.Get<Sprite>("Klip1");
            waitClapSprite = AssetsCreator.Get<Sprite>("HandleKlap_Prepare");
            waitClapSpriteHighlight = AssetsCreator.Get<Sprite>("HandleKlap_Prepare_Selected");
            happySprite = AssetsCreator.Get<Sprite>("HandleKlap_Happy");
            sadSprite = AssetsCreator.Get<Sprite>("HandleKlap_Sad");
        }

        public void InGameSetup() {
            cooldown = new Cooldown(20, 0, false, null, desactiveClap);
            clapTime = new Cooldown(2, 0, false, SadClap);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------

        public override void Initialize()
        {
            base.Initialize();
            gameObject.layer = LayerStorage.iClickableLayer;
            behaviorStateMachine.ChangeState(new HandleKlap_Wandering(this));
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();
            cooldown.UpdateCooldown();
            cooldown.timeScale = ec.NpcTimeScale;
        }

        public void Clicked(int player)
        {
            if (waitingForClaping && cooldown.cooldownIsEnd)     
                Clap(true, players[player]);
            
        }

        public void ClickableUnsighted(int player)
        {
            if (waitingForClaping && cooldown.cooldownIsEnd)
                spriteRenderer[0].sprite = waitClapSprite;
        }

        public void ClickableSighted(int player)
        {
            if (waitingForClaping && cooldown.cooldownIsEnd)
                spriteRenderer[0].sprite = waitClapSpriteHighlight;
        }

        public void desactiveClap() =>
                waitingForClaping = false;

        public void Clap(bool vaule, PlayerManager player)
        {
            cooldown.Restart();
            behaviorStateMachine.ChangeState(new HandleKlap_Wandering(this));

            if (vaule)
            {
                audMan.PlaySingle(clapSound);
                audMan.QueueAudio(happyVoiceline);
                Singleton<CoreGameManager>.Instance.AddPoints(15, player.playerNumber, true);
                spriteRenderer[0].sprite = happySprite;
            }
            else
            {
                spriteRenderer[0].sprite = sadSprite;
                audMan.QueueAudio(sadVoiceline);
            }
        }

        public void SadClap() =>   
            Clap(false, lastPlayer);
        

        public bool ClickableRequiresNormalHeight() { return true; }
        public bool ClickableHidden() { return !waitingForClaping; }
        public bool waitingForClaping;
        public Cooldown cooldown, clapTime;
        public Sprite idleSprite, waitClapSprite, waitClapSpriteHighlight, happySprite, sadSprite;
        public PlayerManager lastPlayer;
        public AudioManager audMan;
        public SoundObject clapVoiceline, happyVoiceline, sadVoiceline, clapSound;
    }

    public class HandleKlap_BaseState : NpcState { protected HandleKlap hk; public HandleKlap_BaseState(HandleKlap npc) : base(npc) { this.npc = npc; hk = npc; } }

    public class HandleKlap_Wandering : HandleKlap_BaseState
    {
        public HandleKlap_Wandering(HandleKlap npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(14, 14);
            ChangeNavigationState(new NavigationState_WanderRandom(hk, 0));
        }

        public override void Update()
        {
            base.Update();
        }

        public override void PlayerInSight(PlayerManager player)
        {
            base.PlayerInSight(player);


            if (Vector3.Distance(player.transform.position, hk.transform.position) <= 10 && hk.cooldown.cooldownIsEnd)
            {
                hk.lastPlayer = player;
                hk.behaviorStateMachine.ChangeState(new HandleKlap_WaitForClap(hk));
            }
        }
    }

    public class HandleKlap_WaitForClap : HandleKlap_BaseState
    {
        public HandleKlap_WaitForClap(HandleKlap npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            hk.spriteRenderer[0].sprite = hk.waitClapSprite;
            hk.waitingForClaping = true;
            npc.Navigator.SetSpeed(16, 16);
            ChangeNavigationState(new NavigationState_WanderRandom(hk, 0));
            hk.audMan.QueueAudio(hk.clapVoiceline);
            hk.clapTime.Restart();
        }

        public override void Update()
        {
            base.Update();
            hk.clapTime.UpdateCooldown();
            hk.clapTime.timeScale = hk.ec.EnvironmentTimeScale * hk.ec.NpcTimeScale;
        }

        public override void InPlayerSight(PlayerManager player)
        {
            base.InPlayerSight(player);
            hk.spriteRenderer[0].sprite = hk.waitClapSpriteHighlight;
        }
    }
}
