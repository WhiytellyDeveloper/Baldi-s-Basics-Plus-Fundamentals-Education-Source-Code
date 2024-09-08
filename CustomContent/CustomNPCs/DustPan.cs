using bbpfer.Extessions;
using UnityEngine;
using bbpfer.CustomLoaders;
using bbpfer.CustomRooms.CustomObjects;
using System.Collections.Generic;
using bbpfer.FundamentalManagers;
using PixelInternalAPI.Extensions;
using System.Linq;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class DustPan : NPC, CustomDataNPC
    {
        public void Setup()
        {
            audMan = this.getAudMan("#EC3D41");
            startVoiceline = AssetsCreator.CreateSound("DP_Start", "Characters", "Vfx_DP_Start", SoundType.Voice, "#EC3D41", 1);
            endVoiceline = AssetsCreator.CreateSound("DP_End", "Characters", "Vfx_DP_End", SoundType.Voice, "#EC3D41", 1);
            placeVoiceline = AssetsCreator.CreateSound("DP_PlaceTrashBag", "Characters", "Vfx_DP_Bag", SoundType.Voice, "#EC3D41", 1);
            findVoiceline = AssetsCreator.CreateSound("DP_Find", "Characters", "Vfx_DP_AnotherBag", SoundType.Voice, "#EC3D41", 1);

            Sprite[] sprites = new Sprite[8];
            for (int i = 0; i <= 7; i++)
                sprites[i] = AssetsCreator.Get<Sprite>($"DustPan_{i}");

            renderer = this.CreateAnimatedSpriteRotator(new SpriteRotationMap[] {
                GenericExtensions.CreateRotationMap(8, sprites.ToArray()),
            });

            renderer.targetSprite = sprites[0];
        }

        public void InGameSetup() =>
            cooldown = new Cooldown(28, 0, false, CooldownEnd);

        //-------------------------------------------------------------------

        public override void Initialize()
        {
            base.Initialize();
            home = ec.CellFromPosition(transform.position)?.FloorWorldPosition ?? transform.position;
            behaviorStateMachine.ChangeState(new DustPan_Wait(this));
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();

            cooldown.UpdateCooldown();
            cooldown.timeScale = ec.NpcTimeScale;

            /*
            if (Input.GetKeyDown(KeyCode.P))
                behaviorStateMachine.ChangeState(new DustPan_Find(this));
            */
        }

        public void CooldownEnd() => 
            behaviorStateMachine.ChangeState(new DustPan_Find(this));
        

        public TrashBag GetRandomTrashBag()
        {
            TrashBag[] tb = FindObjectsOfType<TrashBag>();
            List<TrashBag> availableTrashBags = new List<TrashBag>();
            foreach (var bag in tb)
            {
                if (!collectedTrashBags.Contains(bag))
                    availableTrashBags.Add(bag);        
            }

            if (availableTrashBags.Count == 0) return null;

            return availableTrashBags[Random.Range(0, availableTrashBags.Count)];
        }

        public Vector3 home;
        public Cooldown cooldown;
        public AudioManager audMan;
        public SoundObject startVoiceline, endVoiceline, placeVoiceline, findVoiceline;
        public TrashBag trbag;
        public int trashBags = 0, trashBagsMax = 5;
        public List<TrashBag> collectedTrashBags = new List<TrashBag>();
        public AnimatedSpriteRotator renderer;
    }

    public class DustPan_BaseState : NpcState { protected DustPan dp; public DustPan_BaseState(DustPan npc) : base(npc) { this.npc = npc; dp = npc; } }

    public class DustPan_Wait : DustPan_BaseState
    {
        public DustPan_Wait(DustPan npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(0, 0);
            ChangeNavigationState(new NavigationState_DoNothing(npc, 0));
            dp.cooldown.Restart();
            dp.trashBags = 0;
        }
    }

    public class DustPan_Find : DustPan_BaseState
    {
        public DustPan_Find(DustPan npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            dp.trbag = dp.GetRandomTrashBag();

            if (dp.trbag == null)
            {
                dp.behaviorStateMachine.ChangeState(new DustPan_Wait(dp));
                return;
            }

            dp.trbag.home = dp.transform.position;

            if (dp.trashBags == 0)
            dp.audMan.QueueAudio(dp.startVoiceline);
            else
                dp.audMan.QueueAudio(dp.findVoiceline);

            dp.trbag.GetComponent<BoxCollider>().isTrigger = true;
            npc.Navigator.SetSpeed(7, 12);
            ChangeNavigationState(new NavigationState_TargetPosition(npc, 0, dp.trbag.transform.position));
        }

        public override void Update()
        {
            base.Update();
            if (Vector2.Distance(new Vector2(dp.transform.position.x, dp.transform.position.z), new Vector2(dp.trbag.transform.position.x, dp.trbag.transform.position.z)) <= 3)
            {
                dp.behaviorStateMachine.ChangeState(new DustPan_TrowBag(dp, dp.trbag));
            }
        }
    }

    public class DustPan_TrowBag : DustPan_BaseState
    {
        protected TrashBag tb;
        protected Cell cell;

        public DustPan_TrowBag(DustPan npc, TrashBag trbag) : base(npc) { tb = trbag; }

        public override void Initialize()
        {
            base.Initialize();
            tb.dust = dp;
            tb.exploded = false;
            List<Cell> hall = dp.ec.FindHallways()[Random.Range(0, dp.ec.FindHallways().Count - 1)];
            cell = hall[Random.Range(0, hall.Count - 1)];
            ChangeNavigationState(new NavigationState_TargetPosition(npc, 300, cell.FloorWorldPosition));
        }

        public override void Update()
        {
            base.Update();
            tb.transform.position = new Vector3(dp.transform.position.x, 1, dp.transform.position.z);

            if (Vector2.Distance(new Vector2(dp.transform.position.x, dp.transform.position.z), new Vector2(cell.FloorWorldPosition.x, cell.FloorWorldPosition.z)) <= 3)
            {
                dp.audMan.QueueAudio(dp.placeVoiceline);
                tb.transform.position = new Vector3(dp.transform.position.x, 0, dp.transform.position.z);
                dp.collectedTrashBags.Add(tb);
                dp.trashBags++;

                if (dp.trashBags == dp.trashBagsMax)
                    dp.behaviorStateMachine.ChangeState(new DustPan_Return(dp));

                dp.behaviorStateMachine.ChangeState(new DustPan_Find(dp));
            }
        }
    }

    public class DustPan_Return : DustPan_BaseState
    {
        public DustPan_Return(DustPan npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(40, 80);
            ChangeNavigationState(new NavigationState_TargetPosition(npc, 0, dp.home));
        }

        public override void Update()
        {
            base.Update();
            if (dp.transform.position == dp.home)
                dp.behaviorStateMachine.ChangeState(new DustPan_Wait(dp));

        }

    }
}
