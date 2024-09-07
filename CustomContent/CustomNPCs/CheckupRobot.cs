using bbpfer.Extessions;
using UnityEngine;
using System.Collections;
using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;
using PixelInternalAPI.Extensions;
using System.Linq;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class CheckupRobot : NPC, CustomDataNPC
    {
        public void Setup()
        {
            audMan = this.getAudMan("#36303F");
            TurningSound = AssetsCreator.CreateSound("CR_Turning", "Characters", "Sfx_CR_Turning", SoundType.Effect, "#36303F", 1);
            InstructionsVoiceline = AssetsCreator.CreateSound("CR_StartCheckup", "Characters", "Vfx_CR_StartCheckup1", SoundType.Voice, "#36303F", 1, new SubtitleTimedKey { key = "Vfx_CR_StartCheckup2", time = 3.952f });
            for (int i = 1; i <= 2; i++)
                winVoicelines[i] = AssetsCreator.CreateSound($"CR_CompleteWin{i}", "Characters", $"Vfx_CR_CompleteWin{i}", SoundType.Voice, "#36303F", 1);

            for (int i = 1; i <= 4; i++)
                loseVoicelines[i] = AssetsCreator.CreateSound($"CR_CompleteLose{i}", "Characters", $"Vfx_CR_Lose{i}", SoundType.Voice, "#36303F", 1);

            for (int i = 1; i <= 3; i++)
                followingVoicelines[i] = AssetsCreator.CreateSound($"CR_Following{i}", "Characters", $"Vfx_CR_Following{i}", SoundType.Voice, "#36303F", 1);

            for (int i = 1; i <= 4; i++)
                wanderingVoicelines[i] = AssetsCreator.CreateSound($"CR_Wandering{i}", "Characters", $"Vfx_CR_Wandering{i}", SoundType.Voice, "#36303F", 1);

            Sprite[] sprites = new Sprite[8];

            for (int i = 0; i <= 7; i++)
                sprites[i] = AssetsCreator.Get<Sprite>($"CheckupRobot_{i}");

            renderer = this.CreateAnimatedSpriteRotator(new SpriteRotationMap[] {
                GenericExtensions.CreateRotationMap(8, sprites.ToArray()),
            });

            idle = sprites[0];
            check = AssetsCreator.Get<Sprite>("CheckupRobot_Checkup1");
            check2 = AssetsCreator.Get<Sprite>("CheckupRobot_Checkup2");
        }

        public void InGameSetup()
        {
            cooldown = new Cooldown(42, 0, false, null, null, 1, false, true);
            spinningCooldown = new Cooldown(20, 0, false, DesactiveSpinningCamera);
            wanderVoicelineCooldown = new Cooldown(10, 0, false, PlayWanderingVoiceline);
            timeScaleMod = new TimeScaleModifier { playerTimeScale = 1, npcTimeScale = 0.4f, environmentTimeScale = 0.4f };
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------

        public override void Initialize()
        {
            base.Initialize();
            renderer.targetSprite = idle;
            behaviorStateMachine.ChangeState(new CheckupRobot_Wandering(this));
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();

            if (spinningCamera)
            players[0].transform.RotateContinuously(15f);

            cooldown.UpdateCooldown();
            spinningCooldown.UpdateCooldown();

            cooldown.timeScale = ec.EnvironmentTimeScale * ec.NpcTimeScale;
            spinningCooldown.timeScale = ec.EnvironmentTimeScale * ec.NpcTimeScale;

            spinningCooldown.Pause(!spinningCamera);
        }

        public IEnumerator Checkup(PlayerManager pm)
        {
            audMan.FlushQueue(true);
            audMan.QueueAudio(InstructionsVoiceline);
            yield return new WaitForSeconds(InstructionsVoiceline.subDuration);
            forcePlayerLookingAtBot = false;
            playerCameraRot = ((int)pm.transform.rotation.eulerAngles.y);
            startCheckup = true;
            spriteRenderer[0].sprite = check;
            audMan.PlaySingle(TurningSound);
            yield return new WaitForSeconds(1f);
            spriteRenderer[0].sprite = check2;
            audMan.PlaySingle(TurningSound);
            yield return new WaitForSeconds(1f);
            spriteRenderer[0].sprite = check;
            audMan.PlaySingle(TurningSound);
            yield return new WaitForSeconds(0.1f);
            audMan.PlaySingle(TurningSound);
            renderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
            Complete(!cameraMoved, pm);
        }

        public void Complete(bool win, PlayerManager pm)
        {
            cooldown.Restart();
            cooldown.Pause(false);
            startCheckup = false;
            if (win)
            {
                audMan.QueueRandomAudio(winVoicelines);
                Singleton<CoreGameManager>.Instance.AddPoints(30, pm.playerNumber, true);
            }
            else
            {
                audMan.QueueRandomAudio(loseVoicelines);
                spinningCamera = true;
            }
            

            pm.plm.Entity.SetFrozen(false);
            cameraMoved = false;
            playerCameraRot = 0;
            behaviorStateMachine.ChangeState(new CheckupRobot_Wandering(this));
            pm.ec.RemoveTimeScale(timeScaleMod);
        }

        public void DesactiveSpinningCamera()
        {
            spinningCamera = false;
            spinningCooldown.Restart();
        }
        
        public void PlayRandomWanderingVoiceline() =>       
            audMan.QueueRandomAudio(wanderingVoicelines);
        

        public void PlayWanderingVoiceline()
        {
            if (Random.value > 1f && !audMan.AnyAudioIsPlaying)
                PlayRandomWanderingVoiceline();
        }

        public AnimatedSpriteRotator renderer;
        public Sprite idle, check, check2;
        public int playerCameraRot;
        public bool forcePlayerLookingAtBot;
        public bool cameraMoved;
        public bool spinningCamera;
        public bool startCheckup;
        public Cooldown cooldown;
        public Cooldown spinningCooldown;
        public Cooldown wanderVoicelineCooldown;
        public AudioManager audMan;
        public SoundObject[] wanderingVoicelines = new SoundObject[5];
        public SoundObject[] winVoicelines = new SoundObject[3];
        public SoundObject[] loseVoicelines = new SoundObject[5];
        public SoundObject[] followingVoicelines = new SoundObject[4];
        public SoundObject InstructionsVoiceline, TurningSound;
        public TimeScaleModifier timeScaleMod;
    }

    public class CheckupRobot_BaseState : NpcState
    {
        public CheckupRobot cr;
        public CheckupRobot_BaseState(CheckupRobot checkupRobot) : base(checkupRobot) { cr = checkupRobot; npc = checkupRobot; }
    }

    public class CheckupRobot_Wandering : CheckupRobot_BaseState
    {
        public CheckupRobot_Wandering(CheckupRobot checkupRobot) : base(checkupRobot) { }

        public override void Initialize()
        {
            base.Initialize();
            cr.cooldown.Pause(false);
            cr.behaviorStateMachine.ChangeNavigationState(new NavigationState_WanderRandom(cr, 0));
            cr.Navigator.SetSpeed(10, 10);
        }

        public override void Update()
        {
            base.Update();
            cr.wanderVoicelineCooldown.UpdateCooldown();
        }

        public override void PlayerInSight(PlayerManager player)
        {
            base.InPlayerSight(player);

            if (cr.cooldown.cooldownIsEnd && !player.Tagged && !player.Invisible)
                cr.behaviorStateMachine.ChangeState(new CheckupRobot_Following(cr, player));
        }
    }

    public class CheckupRobot_Following : CheckupRobot_BaseState
    {
        protected PlayerManager pm;
        public CheckupRobot_Following(CheckupRobot checkupRobot, PlayerManager player) : base(checkupRobot) { pm = player; }

        public override void Initialize()
        {
            base.Initialize();
            cr.Navigator.SetSpeed(14, 14);
        }

        public override void Update()
        {
            base.Update();

            if (!pm.Tagged || !pm.Invisible)
                cr.behaviorStateMachine.ChangeNavigationState(new NavigationState_TargetPlayer(cr, 0, pm.transform.position));
            else if (pm.Tagged || pm.Invisible)
                cr.behaviorStateMachine.ChangeState(new CheckupRobot_Wandering(cr));
        }

        public override void OnStateTriggerEnter(Collider other)
        {
            base.OnStateTriggerEnter(other);

            if (other.CompareTag("Player"))
                cr.behaviorStateMachine.ChangeState(new CheckupRobot_Checkup(cr, other.GetComponent<PlayerManager>()));
        }

        public override void PlayerSighted(PlayerManager player)
        {
            base.PlayerSighted(player);

            if (cr.cooldown.cooldownIsEnd && !player.Tagged && !player.Invisible)
                cr.audMan.QueueRandomAudio(cr.followingVoicelines);
        }
    }

    public class CheckupRobot_Checkup : CheckupRobot_BaseState
    {
        protected PlayerManager pm;
        public CheckupRobot_Checkup(CheckupRobot checkupRobot, PlayerManager player) : base(checkupRobot) { pm = player; }

        public override void Initialize()
        {
            base.Initialize();
            pm.ec.AddTimeScale(cr.timeScaleMod);
            cr.cooldown.Pause(true);
            cr.cooldown.Restart();
            cr.Navigator.SetSpeed(0, 0);
            pm.plm.Entity.SetFrozen(true);
            cr.renderer.enabled = false;
            cr.forcePlayerLookingAtBot = true;
            cr.StartCoroutine(cr.Checkup(pm));
        }

        public override void Update()
        {
            base.Update();

            if (cr.playerCameraRot != ((int)pm.transform.rotation.eulerAngles.y) && cr.startCheckup)
                cr.cameraMoved = true;

            if (Singleton<InputManager>.Instance.GetDigitalInput("LookBack", false) && cr.startCheckup)
                cr.cameraMoved = true;

            if (cr.forcePlayerLookingAtBot)
                pm.transform.RotateSmoothlyToNextPoint(cr.transform.position, 0.8f);
        }
    }
}
