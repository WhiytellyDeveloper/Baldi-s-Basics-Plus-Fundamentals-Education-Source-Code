using UnityEngine;
using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_SweepWhistle : Item, CustomDataItem
    {
		public void Setup() =>  
			whistleSound = FundamentalCodingHelper.GetVariable<SoundObject>(MTM101BaldAPI.Registers.ItemMetaStorage.Instance.FindByEnum(Items.PrincipalWhistle).value.item.GetComponent<ITM_PrincipalWhistle>(), "audWhistle");

        public override bool Use(PlayerManager pm)
        {
			this.pm = pm;
			WhistleReact(pm.transform.position);
			Singleton<CoreGameManager>.Instance.audMan.PlaySingle(whistleSound);
			Destroy(base.gameObject);
            return true;
        }

		public void WhistleReact(Vector3 target)
		{
			foreach (NPC npc in pm.ec.Npcs)
			{
				if (npc.Character == Character.Sweep)				
					npc.behaviorStateMachine.ChangeState(new GottaSweep_WhistleApproach(npc.GetComponent<GottaSweep>(), target));			
			}
		}

		public SoundObject whistleSound;
	}

	public class GottaSweep_WhistleApproach : GottaSweep_StateBase
	{
		protected NpcState previousState;
		public GottaSweep_WhistleApproach(GottaSweep sweep, Vector3 destination) : base(sweep, sweep)
		{
			this.destination = destination;
			npc = sweep;
			gottaSweep = sweep;
		}

		public override void Initialize()
		{
			base.Initialize();
			gottaSweep.StartSweeping();
		}

        public override void Update()
        {
            base.Update();
			base.ChangeNavigationState(new NavigationState_TargetPosition(this.npc, 63, this.destination));
		}

        public override void Resume()
		{
			base.Resume();
			this.npc.behaviorStateMachine.ChangeState(new GottaSweep_GoNow(npc, gottaSweep));
		}

		public override void DestinationEmpty()
		{
			base.DestinationEmpty();
			this.npc.behaviorStateMachine.ChangeState(new GottaSweep_GoNow(npc, gottaSweep));
		}

		public override void Exit()
		{
			base.Exit();

		}

		protected Vector3 destination;
	}

	public class GottaSweep_GoNow : GottaSweep_StateBase
	{
		public GottaSweep_GoNow(NPC npc, GottaSweep gottaSweep) : base(npc, gottaSweep)
		{
		}

		public override void Enter()
		{
			base.Enter();
			base.ChangeNavigationState(new NavigationState_DoNothing(this.npc, 0));
			this.waitTime = 0;
			this.gottaSweep.StopSweeping();
		}

		public override void Update()
		{
			base.Update();
			this.waitTime -= Time.deltaTime * this.npc.TimeScale;
			if (this.waitTime <= 0f)			
				this.npc.behaviorStateMachine.ChangeState(new GottaSweep_SweepingTime(this.npc, this.gottaSweep));		
		}

		private float waitTime;
	}
}
