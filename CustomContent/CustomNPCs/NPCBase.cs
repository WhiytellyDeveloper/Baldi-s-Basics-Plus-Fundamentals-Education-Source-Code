using bbpfer.Extessions;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class NPCBase : NPC
    {
        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new NPCBase_Wandering(this));
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();
        }
    }

    public class NPCBase_BaseState : NpcState { protected NPCBase baseNPC; public NPCBase_BaseState(NPCBase npc) : base(npc) { this.npc = npc; baseNPC = npc; } }

    public class NPCBase_Wandering : NPCBase_BaseState
    {
        public NPCBase_Wandering(NPCBase npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(12, 12);
            ChangeNavigationState(new NavigationState_WanderRandom(npc, 0));
        }
    }
}
