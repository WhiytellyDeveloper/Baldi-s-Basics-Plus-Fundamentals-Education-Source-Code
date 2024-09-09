using bbpfer.Extessions;
using UnityEngine;
using bbpfer.CustomLoaders.CustomNpcsObjects;
using bbpfer.CustomLoaders;
using PixelInternalAPI.Extensions;
using PixelInternalAPI.Classes;
using bbpfer.FundamentalManagers;
using MTM101BaldAPI;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class Duwalio : NPC, CustomDataNPC
    {
        public void Setup() {
            var presentSprite = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.Get<Sprite>("PresentIcon_Large"));
            var presentHolder = presentSprite.AddSpriteHolder(0, LayerStorage.ignoreRaycast);
            var present = new GameObject("DuwalioPresent").AddComponent<DuwalioPresent>();
            present.gameObject.layer = LayerStorage.iClickableLayer;
            presentHolder.transform.SetParent(present.transform);
            present.entity = present.gameObject.CreateEntity(1, 1, presentHolder.transform);
            present.gameObject.ConvertToPrefab(true);
            presentPref = present;
        }

        public void InGameSetup() { }

        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new Duwalio_Wandering(this));
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();

            if (Input.GetKeyDown(KeyCode.L)) {
                behaviorStateMachine.ChangeState(new Duwalio_Throwing(this, 0));
            }
        }

        public DuwalioPresent presentPref;
    }

    public class Duwalio_BaseState : NpcState { protected Duwalio dw; public Duwalio_BaseState(Duwalio npc) : base(npc) { this.npc = npc; dw = npc; } }

    public class Duwalio_Wandering : Duwalio_BaseState
    {
        public Duwalio_Wandering(Duwalio npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(15, 15);
            ChangeNavigationState(new NavigationState_WanderRandom(npc, 0));
        }
    }

    public class Duwalio_Throwing : Duwalio_BaseState
    {
        protected int player;
        public Duwalio_Throwing(Duwalio npc, int player) : base(npc) { this.player = player; }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(0, 0);
            GameObject.Instantiate<DuwalioPresent>(dw.presentPref).Initilalize(-Singleton<CoreGameManager>.Instance.GetCamera(player).transform.forward, dw.ec, dw.transform.position);
        }
    }

}
