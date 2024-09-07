using bbpfer.Extessions;
using UnityEngine;
using bbpfer.FundamentalManagers;
using bbpfer.CustomLoaders;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class StellaLog : NPC, CustomDataNPC
    {
        public void Setup()
        {
            idle = AssetsCreator.Get<Sprite>("StellaLog");
            needCatch = AssetsCreator.Get<Sprite>("StellaLog_Catch");

            audMan = this.getAudMan("#9E5050");
            catchSound = FundamentalCodingHelper.FindResourceObjectWithName<SoundObject>("Slap");
        }

        public void InGameSetup() =>
            cooldown = new Cooldown(15, 0, false);
        

        //---------------------------------------------------------------------------------------------------------

        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new StellaLog_Wandering(this));
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();
            cooldown.UpdateCooldown();
            cooldown.timeScale = ec.NpcTimeScale;

            if (withAm)
                entity.Teleport(transform.position);

            spriteRenderer[0].sprite = (!withAm && cooldown.cooldownIsEnd) ? needCatch : idle;
        }

        public Entity entity;
        public Cooldown cooldown;
        public bool withAm;
        public Sprite idle, needCatch;
        public AudioManager audMan;
        public SoundObject catchSound;
    }

    public class StellaLog_BaseState : NpcState
    {
        protected StellaLog stella;
        public StellaLog_BaseState(StellaLog npc) : base(npc)
        {
            this.npc = npc;
            stella = npc;
        }
    }

    public class StellaLog_Wandering : StellaLog_BaseState
    {
        public StellaLog_Wandering(StellaLog npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(16, 16);
            ChangeNavigationState(new NavigationState_WanderRandom(npc, 0));
        }

        public override void OnStateTriggerEnter(Collider other)
        {
            base.OnStateTriggerEnter(other);

            Entity activityModifier = other.GetComponent<Entity>();
            if (activityModifier != null && !stella.withAm && activityModifier != stella.Navigator.Am && stella.cooldown.cooldownIsEnd)
            {
                if (other.CompareTag("NPC") || other.CompareTag("Player"))
                {
                    stella.audMan.PlaySingle(stella.catchSound);
                    stella.entity = activityModifier;
                    stella.entity.SetFrozen(true);
                    stella.withAm = true;

                    if (stella.entity.GetComponent<ItemManager>() != null)
                        stella.entity.GetComponent<ItemManager>().Disable(true);

                    stella.behaviorStateMachine.ChangeState(new StellaLog_WanderingWithAm(stella));
                }
            }
        }
    }

    public class StellaLog_WanderingWithAm : StellaLog_BaseState
    {
        private int roomIndex;
        public StellaLog_WanderingWithAm(StellaLog npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(22, 45);
            roomIndex = Random.Range(0, stella.ec.rooms.Count - 1);
            Vector3 targetPosition = stella.ec.rooms[roomIndex].RandomEntitySafeCellNoGarbage().FloorWorldPosition;
            ChangeNavigationState(new NavigationState_TargetPosition(npc, int.MaxValue, targetPosition));
        }

        public override void Update()
        {
            base.Update();
            Vector3 targetPosition = stella.ec.rooms[roomIndex].RandomEntitySafeCellNoGarbage().FloorWorldPosition;
            if (new Vector2(stella.transform.localPosition.x, stella.transform.localPosition.z) == new Vector2(targetPosition.x, targetPosition.z))        
                Out();
        }

        private void Out()
        {
            if (stella.withAm && stella.entity != null)
            {
                stella.entity.SetFrozen(false);
                if (stella.entity.GetComponent<ItemManager>() != null)
                    stella.entity.GetComponent<ItemManager>().Disable(false);
                stella.entity = null;
                stella.withAm = false;
                stella.cooldown.Restart();
                stella.behaviorStateMachine.ChangeState(new StellaLog_Wandering(stella));
            }
        }
    }
}
