using bbpfer.Extessions;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class Bonjur : NPC
    {
        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new Bonjur_Wandering(this));
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();
            spriteRenderer[0].SquishingAnim(0.8f, 1, 0.8f, 0);
        }
    }

    public class Bonjur_BaseState : NpcState { protected Bonjur bonjur; public Bonjur_BaseState(Bonjur npc) : base(npc) { this.npc = npc; bonjur = npc; } }

    public class Bonjur_Wandering : Bonjur_BaseState
    {
        public Bonjur_Wandering(Bonjur npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(12, 12);
            ChangeNavigationState(new NavigationState_WanderRandom(bonjur, 0));
        }
    }
}
