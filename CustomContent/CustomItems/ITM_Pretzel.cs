using UnityEngine;
using bbpfer.CustomLoaders;
using System.Linq;
using bbpfer.FundamentalManagers;
using PixelInternalAPI.Extensions;
using PixelInternalAPI.Classes;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Pretzel : Item, IEntityTrigger, CustomDataItem
    {
        public void Setup()
        {
            sprite = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.Get<Sprite>("Pretzel_Large")).AddSpriteHolder(0, LayerStorage.ignoreRaycast);
            sprite.gameObject.layer = LayerStorage.billboardLayer;
            sprite.transform.SetParent(transform);
            gameObject.layer = LayerStorage.standardEntities;
            entity = gameObject.CreateEntity(1, 1, sprite.transform);
            audMan = gameObject.CreatePropagatedAudioManager(125, 250);

            trowingSound = AssetsCreator.CreateSound("TrowPretzel", "Items", "", SoundType.Effect, "", 1);
            stickSound = Resources.FindObjectsOfTypeAll<SoundObject>().First((SoundObject x) => x.name.Contains("Bang"));
        }

        public override bool Use(PlayerManager pm)
        {
            this.pm = pm;
            cooldown = new Cooldown(20, 0, true);
            dir = Singleton<CoreGameManager>.Instance.GetCamera(0).transform.forward;
            transform.position = pm.transform.position;
            entity.Initialize(pm.ec, transform.position);
            audMan.PlaySingle(trowingSound);
            entity.OnEntityMoveInitialCollision += (hit) =>
                Destroy(base.gameObject);


            return true;
        }

        private void Update()
        {
            entity.UpdateInternalMovement(dir * 20 * pm.ec.EnvironmentTimeScale);
        }

        public void EntityTriggerEnter(Collider other)
        {
            if (other.CompareTag("NPC") && !active)
            {
                active = true;
                npc = other.GetComponent<NPC>();

                if (!npc.looker.enabled && npc.Character != Character.Bully)
                    return;
                else if (!npc.looker.enabled && npc.Character == Character.Bully)
                {
                    npc.GetComponent<Bully>().behaviorStateMachine.ChangeState(new Bully_TmeporaryHiding(npc.GetComponent<Bully>(), npc.GetComponent<Bully>().behaviorStateMachine.CurrentState, pm));
                    return;
                }

                if (npc.Character != Character.Principal)
                {
                    audMan.PlaySingle(stickSound);
                    npc.PlayerLost(pm);
                    npc.looker.enabled = false;
                    sprite.enabled = false;
                    entity.SetFrozen(true);

                    if (npc.Character == Character.Playtime)
                    {
                        if (npc.GetComponent<Playtime>().Navigator.maxSpeed == 0)
                            npc.GetComponent<Playtime>().EndJumprope(true);
                    }
                }
                else
                {
                    npc.behaviorStateMachine.ChangeState(new Principal_Pretzel(npc.GetComponent<Principal>(), npc.behaviorStateMachine.currentState, pm));
                    Destroy(base.gameObject);
                }

            }
        }
        public void EntityTriggerStay(Collider other)
        {
        }
        public void EntityTriggerExit(Collider other)
        {
        }

        public void RemoveEffects()
        {
            npc.looker.enabled = true;
            Destroy(base.gameObject);
        }

        public Entity entity;
        public SpriteRenderer sprite;
        private Vector3 dir;
        private NPC npc;
        public Cooldown cooldown;
        public AudioManager audMan;
        public SoundObject trowingSound;
        public SoundObject stickSound;
        public bool active;
    }

    public class Principal_Pretzel : Principal_SubState
    {
        protected PlayerManager pm;
        protected Cooldown cooldown = new Cooldown(1, 0, true);

        public Principal_Pretzel(Principal pri, NpcState state, PlayerManager player) : base(pri, state)
        {
            principal = pri;
            npc = pri;
            previousState = state;
            pm = player;
        }

        public override void Initialize()
        {
            base.Initialize();
            pm.ClearGuilt();
            FundamentalManagers.FundamentalCodingHelper.GetVariable<AudioManager>(principal, "audMan").PlaySingle(FundamentalManagers.FundamentalCodingHelper.GetVariable<SoundObject>(MTM101BaldAPI.Registers.ItemMetaStorage.Instance.FindByEnum(Items.ZestyBar).value.item.GetComponent<ITM_ZestyBar>(), "audEat"));
            principal.Navigator.Entity.SetFrozen(true);
            cooldown.endAction = EndCooldown;
            cooldown.Initialize();
        }

        public void EndCooldown()
        {
            principal.Navigator.Entity.SetFrozen(false);
            principal.behaviorStateMachine.ChangeState(new Principal_Wandering(principal));
        }
    }

    public class Bully_TmeporaryHiding : Bully_StateBase
    {
        protected PlayerManager pm;
        protected Cooldown cooldown = new Cooldown(2, 0, true);
        protected NpcState bullyState;

        public Bully_TmeporaryHiding(Bully bully, NpcState state, PlayerManager player) : base(bully, bully)
        {
            this.bully = bully;
            pm = player;
            bullyState = state;
        }

        public override void Initialize()
        {
            base.Initialize();
            pm.ClearGuilt();
            bully.SetComponents(false);
            cooldown.endAction = EndCooldown;
            cooldown.Initialize();
        }

        public void EndCooldown()
        {
            bully.SetComponents(true);
            bully.behaviorStateMachine.ChangeState(bullyState);
        }
    }
}
