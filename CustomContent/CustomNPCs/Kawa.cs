using MTM101BaldAPI.Components;
using UnityEngine;
using bbpfer.FundamentalManagers;
using bbpfer.CustomLoaders;
using bbpfer.Extessions;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class Kawa : NPC, CustomDataNPC
    {
        public void Setup()
        {
            animator = spriteRenderer[0].AddAnimatorSprite();

            audMan = this.getAudMan("#C9D3EF");
            pushSound = AssetsCreator.CreateSound("Kawa_Nope", "Characters", "Sfx_Kawa_Nope", SoundType.Effect, "#C9D3EF", 1);
        }

        public void InGameSetup()
        {
            animator.animations.Add("Idle", new CustomAnimation<Sprite>(4, new Sprite[] { AssetsCreator.Get<Sprite>("KawaV2_Idle"), AssetsCreator.Get<Sprite>("KawaV2_Idle1"), AssetsCreator.Get<Sprite>("KawaV2_Idle"), AssetsCreator.Get<Sprite>("KawaV2_Idle2") }));
            animator.animations.Add("Protecting", new CustomAnimation<Sprite>(4, new Sprite[] { AssetsCreator.Get<Sprite>("KawaV2_Protecting"), AssetsCreator.Get<Sprite>("KawaV2_Protecting1"), AssetsCreator.Get<Sprite>("KawaV2_Protecting"), AssetsCreator.Get<Sprite>("KawaV2_Protecting2") }));
            animator.SetDefaultAnimation("Idle", 1f);
            cooldown = new Cooldown(22, 0, false, Swap);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new Kawa_Wandering(this));
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();
            cooldown.timeScale = ec.NpcTimeScale;
            cooldown.UpdateCooldown();
        }

        public void Swap()
        {
            isProtecting = !isProtecting;
            if (isProtecting)
                animator.SetDefaultAnimation("Protecting", 1);
            if (!isProtecting)
                animator.SetDefaultAnimation("Idle", 1);
            cooldown.Restart();
        }

        public void Push(Entity entity)
        {
            entity.AddForce(new Force((entity.transform.position - base.transform.position).normalized, 42, -42));
            audMan.PlaySingle(pushSound);
        }

        public CustomSpriteAnimator animator;
        public bool isProtecting;
        public Cooldown cooldown;
        public AudioManager audMan;
        public SoundObject pushSound;
    }

    public class Kawa_StateBase : NpcState { protected Kawa kawa; public Kawa_StateBase(Kawa kawa) : base(kawa) { npc = kawa; this.kawa = kawa; } }

    public class Kawa_Wandering : Kawa_StateBase
    {
        public Kawa_Wandering(Kawa kawa) : base(kawa) { }

        public override void Initialize()
        {
            base.Initialize();
            kawa.Navigator.SetSpeed(7);
            ChangeNavigationState(new NavigationState_WanderRandom(npc, 0));
        }

        public override void OnStateTriggerEnter(Collider other)
        {
            base.OnStateTriggerEnter(other);

            if (other.GetComponent<PlayerManager>() && kawa.isProtecting)
            {
                if (!other.GetComponent<PlayerManager>().Tagged)
                    kawa.Push(other.GetComponent<PlayerManager>().plm.Entity);
            }
        }
    }
}
