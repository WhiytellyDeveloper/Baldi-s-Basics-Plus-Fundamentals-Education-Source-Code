using bbpfer.Extessions;
using bbpfer.CustomRooms.CustomObjects;
using UnityEngine;
using System.Collections;
using System.Linq;
using bbpfer.CustomLoaders;
using PixelInternalAPI.Classes;
using PixelInternalAPI.Extensions;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class SecretAdmirer : NPC, CustomDataNPC
    {
        public void Setup()
        {
            var itemHolder = ObjectCreationExtensions.CreateSpriteBillboard(null).AddSpriteHolder(new Vector3(0f, 2.29f, 0f), LayerStorage.billboardLayer);
            itemHolder.transform.localScale = new Vector3(1.8f, 1.8f, 1);
            itemHolder.transform.parent.SetParent(transform);
            itemHolder.transform.parent.localPosition = Vector3.zero;
            spriteHolder = itemHolder;

            idleSprite = AssetsCreator.Get<Sprite>("SecretAdmirer_Idle");
            getItemSprite = AssetsCreator.Get<Sprite>("SecretAdmirer_GetItem");
            upsetSprite = AssetsCreator.Get<Sprite>("SecretAdmirer_Upste");
            helloSprite = AssetsCreator.Get<Sprite>("SecretAdmirer_Hello");
            stunSprite = AssetsCreator.Get<Sprite>("SecretAdmirer_Stun");

            audMan = this.getAudMan("#C7C7EA");

            spotVoicelines[0] = AssetsCreator.CreateSound("SCR_SpotPlayer1", "Characters", "Vfx_SCR_Spot1", SoundType.Voice, "#C7C7EA", 1);
            spotVoicelines[1] = AssetsCreator.CreateSound("SCR_SpotPlayer2", "Characters", "Vfx_SCR_Spot2", SoundType.Voice, "#C7C7EA", 1);
            getItemVoiceline = AssetsCreator.CreateSound("SCR_GetItemFormPlayer", "Characters", "Vfx_SCR_WithItem", SoundType.Voice, "#C7C7EA", 1);
            upsetVoiceline = AssetsCreator.CreateSound("SCR_GetItemFormPlayer", "Characters", "Vfx_SCR_RemovedItem", SoundType.Voice, "#C7C7EA", 1);
        }

        public void InGameSetup() =>
            cooldown = new Cooldown(25, 0, false, null, null, 1, false, true);
        

        //-------------------------------------------------------------------------------------------

        public ItemPilar pilar;
        public SpriteRenderer spriteHolder;
        public ItemObject item;
        public Sprite idleSprite, getItemSprite, upsetSprite, helloSprite, stunSprite;
        public Cooldown cooldown;
        public AudioManager audMan;
        public SoundObject[] spotVoicelines = new SoundObject[2];
        public SoundObject getItemVoiceline, upsetVoiceline;

        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new SecretAdmirer_Wandering(this));
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();
            cooldown.UpdateCooldown();
            cooldown.timeScale = ec.NpcTimeScale;
            if (navigator.Entity.CurrentRoom != null)
            {
                if (navigator.Entity.CurrentRoom.category == spawnableRooms[0])
                    FindPilar();
            }

        }

        public IEnumerator helloAnim(SoundObject voiceline)
        {
            spriteRenderer[0].sprite = helloSprite;
            yield return new WaitForSeconds(voiceline.subDuration);
            spriteRenderer[0].sprite = idleSprite;
        }

        public void FindPilar() =>
            pilar = navigator.Entity.CurrentRoom.objectObject.GetComponentInChildren<ItemPilar>();

        public ItemObject GetItem(PlayerManager player)
        {
            if (player.itm.items[player.itm.selectedItem] != Singleton<CoreGameManager>.Instance.NoneItem)
                return player.itm.items[player.itm.selectedItem];
            else
                for (int i = 0; i < player.itm.items.Length; i++)
                    if (player.itm.items[i].itemType != Items.None)
                return player.itm.items[i];
            return null;
        }
    }

    public class SecretAdmirer_BaseState : NpcState {
        protected SecretAdmirer scr;  
        public SecretAdmirer_BaseState(SecretAdmirer npc) : base(npc) { this.npc = npc; scr = npc; } 
    }

    public class SecretAdmirer_Wandering : SecretAdmirer_BaseState
    {
        public SecretAdmirer_Wandering(SecretAdmirer npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(7, 10);
            ChangeNavigationState(new NavigationState_WanderRandom(npc, 0));
        }

        public override void PlayerSighted(PlayerManager player)
        {
            base.PlayerSighted(player);

            if (scr.cooldown.cooldownIsEnd && player.itm.HasItem())
            {
                SoundObject voiceline = scr.spotVoicelines[Random.Range(0, 1)];
                scr.StartCoroutine(scr.helloAnim(voiceline));
                scr.audMan.PlaySingle(voiceline);
                scr.behaviorStateMachine.ChangeState(new SecretAdmirer_FindPlayer(scr, player));
            }
        }
    }

    public class SecretAdmirer_FindPlayer : SecretAdmirer_BaseState
    {
        protected PlayerManager followingPlayer;
        public SecretAdmirer_FindPlayer(SecretAdmirer npc, PlayerManager player) : base(npc) { followingPlayer = player; }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(10, 14);
        }

        public override void Update()
        {
            if (!followingPlayer.itm.HasItem())
                scr.behaviorStateMachine.ChangeState(new SecretAdmirer_Wandering(scr));

            ChangeNavigationState(new NavigationState_TargetPlayer(npc, 100, followingPlayer.transform.position));
        }

        public override void OnStateTriggerEnter(Collider other)
        {
            base.OnStateTriggerEnter(other);

            if (other.CompareTag("Player"))
            {
                var playerManager = other.GetComponent<PlayerManager>();
                bool itemFound = false;

                while (!itemFound)
                {
                    scr.item = scr.GetItem(playerManager);

                    if (playerManager.itm.items.Any(i => i.itemType == scr.item.itemType))
                    {
                        if (!playerManager.itm.HasItem())
                            npc.behaviorStateMachine.ChangeState(new SecretAdmirer_Wandering(scr));

                        playerManager.itm.Remove(scr.item.itemType);
                        scr.spriteHolder.sprite = scr.item.itemSpriteLarge;
                        scr.audMan.PlaySingle(scr.getItemVoiceline);
                        npc.behaviorStateMachine.ChangeState(new SecretAdmirer_Pilar(scr));
                        itemFound = true;
                    }
                }
            }
        }

    }
    public class SecretAdmirer_Pilar : SecretAdmirer_BaseState
    {
        public SecretAdmirer_Pilar(SecretAdmirer npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            scr.spriteRenderer[0].sprite = scr.getItemSprite;
            npc.Navigator.SetSpeed(14, 22);
            ChangeNavigationState(new NavigationState_TargetPosition(npc, 0, scr.pilar.transform.position));
        }

        public override void Update()
        {
            base.Update();
            if (((int)Vector3.Distance(new Vector3(scr.pilar.transform.position.x, 0, scr.pilar.transform.position.z), new Vector3(scr.transform.position.x, 0, scr.transform.position.z))) <= 8)
            {
                scr.pilar.AssingItem(scr.item);
                scr.spriteHolder.sprite = null;
                scr.cooldown.Pause(true);
                scr.cooldown.Restart();
                scr.behaviorStateMachine.ChangeState(new SecretAdmirer_WanderingInRoom(scr));
            }
        }

        public class SecretAdmirer_WanderingInRoom : SecretAdmirer_BaseState
        {
            public SecretAdmirer_WanderingInRoom(SecretAdmirer npc) : base(npc) { }

            public override void Initialize()
            {
                base.Initialize();
                ChangeNavigationState(new NavigationState_PartyEvent(npc, 0, scr.Navigator.Entity.CurrentRoom));
            }

            public override void Update()
            {
                base.Update();

                if (scr.pilar.item == Singleton<CoreGameManager>.Instance.NoneItem)
                {
                    scr.audMan.PlaySingle(scr.upsetVoiceline);
                    scr.spriteRenderer[0].sprite = scr.upsetSprite;
                    scr.cooldown.Pause(false);
                    npc.behaviorStateMachine.ChangeState(new SecretAdmirer_Wandering(scr));
                }

            }
        }
    }
}
