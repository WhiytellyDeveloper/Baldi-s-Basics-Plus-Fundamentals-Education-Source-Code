using bbpfer.Extessions;
using UnityEngine;
using bbpfer.CustomLoaders;
using bbpfer.CustomRooms.CustomObjects;
using System.Collections.Generic;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class DustPan : NPC, CustomDataNPC
    {
        public void Setup() { }

        public void InGameSetup() =>  
            cooldown = new Cooldown(40, 0, false);

        //-------------------------------------------------------------------

        public override void Initialize()
        {
            base.Initialize();
            home = ec.CellFromPosition(transform.position).FloorWorldPosition;
            behaviorStateMachine.ChangeState(new DustPan_Wait(this));
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();
            if (Input.GetKeyDown(KeyCode.P))
                behaviorStateMachine.ChangeState(new DustPan_Find(this));

            if (trashBags == trashBagsMax)
                behaviorStateMachine.ChangeState(new DustPan_Wait(this));
        }

        public TrashBag GetRandomTrashBag()
        {
            TrashBag[] tb = FindObjectsOfType<TrashBag>();
            return tb[Random.Range(0, tb.Length - 1)];
        }

        public Vector3 home;
        public Cooldown cooldown;
        public AudioManager audMan;
        public SoundObject startVoiceline, endVoiceline, placeVoiceline, findVoiceline;
        public TrashBag trbag;
        public int trashBags = 0, trashBagsMax = 5;
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
            dp.trbag.GetComponent<BoxCollider>().isTrigger = true;
            npc.Navigator.SetSpeed(7, 22);
            ChangeNavigationState(new NavigationState_TargetPosition(npc, 0, dp.trbag.transform.position));
        }

        public override void Update()
        {
            base.Update();
            if (Vector2.Distance(new Vector2(dp.transform.position.x, dp.transform.position.z), new Vector2(dp.trbag.transform.position.x, dp.trbag.transform.position.z)) <= 3)
                dp.behaviorStateMachine.ChangeState(new DustPan_TrowBag(dp, dp.trbag));
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
                tb.transform.position = new Vector3(dp.transform.position.x, 0, dp.transform.position.z);
                dp.trashBags++;
                dp.behaviorStateMachine.ChangeState(new DustPan_Find(dp));
            }
        }
    }
}
