using UnityEngine;
using PixelInternalAPI.Extensions;
using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;
using bbpfer.CustomContent.CustomItems;
using PixelInternalAPI.Classes;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Gum : Item, IEntityTrigger, CustomDataItem
    {
        public void Setup()
        {
            gumFlying = ObjectCreationExtensions.CreateSpriteBillboard(
                diet ? AssetsCreator.CreateSprite("beans_dietgumwad", "Items", (int)FundamentalCodingHelper.FindResourceObjectContainingName<Sprite>("beans_gumwad").pixelsPerUnit)
                     : FundamentalCodingHelper.FindResourceObjectContainingName<Sprite>("beans_gumwad"));
            gumFlying.gameObject.layer = LayerStorage.billboardLayer;
            gumFlying.transform.SetParent(transform);
            gameObject.layer = LayerStorage.standardEntities;

            entity = gameObject.CreateEntity(Mathf.Max(gumFlying.sprite.bounds.size.x, gumFlying.sprite.bounds.size.y) / 2f,
                Mathf.Max(gumFlying.sprite.bounds.size.x, gumFlying.sprite.bounds.size.y) / 2f, gumFlying.transform)
                .SetEntityCollisionLayerMask(LayerStorage.gumCollisionMask);

            gumGrounded = ObjectCreationExtensions.CreateSpriteBillboard(
                diet ? AssetsCreator.CreateSprite("beans_dietenemywad", "Items", 20)
                     : FundamentalCodingHelper.FindResourceObjectContainingName<Sprite>("beans_enemywad"));
            gumGrounded.gameObject.layer = LayerStorage.billboardLayer;
            gumGrounded.transform.SetParent(transform);
            gumGrounded.transform.localPosition = new Vector3(0, -5, 0);
            gumGrounded.gameObject.SetActive(false);

            audMan = gameObject.CreatePropagatedAudioManager(0, 200);
            Color color = diet ? ColorUtility.TryParseHtmlString("#C07255", out color) ? color : Color.red : Color.red;

            FundamentalCodingHelper.SetValue<bool>(audMan, "overrideSubtitleColor", !diet);
            FundamentalCodingHelper.SetValue<Color>(audMan, "subtitleColor", color);

            Spit = AssetsCreator.CreateSound("SplitGum", "Items", "", SoundType.Effect, "", 1);
            Woosh = FundamentalCodingHelper.FindResourceObjectContainingName<SoundObject>("Ben_Gum_Whoosh");
            Splat = FundamentalCodingHelper.FindResourceObjectContainingName<SoundObject>("Ben_Splat");

            if (diet) { time = 5; speed = 40; }
        }

        public override bool Use(PlayerManager pm)
        {
            this.pm = pm;
            destroyCooldown = new Cooldown(3, 0, true, Destroy);
            cooldown = new Cooldown(time, 0, true, Destroy);
            audMan.PlaySingle(Spit);
            transform.position = pm.transform.position;
            entity.Initialize(pm.ec, transform.position);
            transform.forward = Singleton<CoreGameManager>.Instance.GetCamera(0).transform.forward;
            audMan.AddStartingAudiosToAudioManager(true, Woosh);
            entity.OnEntityMoveInitialCollision += (hit) =>
            {
                flying = false;
                entity.SetFrozen(true);
                destroyCooldown.Initialize();
                audMan.FlushQueue(true);
                audMan.PlaySingle(Splat);
            };

            return true;
        }

        private void Update()
        {
            if (flying)
                entity.UpdateInternalMovement(transform.forward * speed * pm.ec.EnvironmentTimeScale);
            else if (!flying && modifire != null)
            {
                entity.UpdateInternalMovement(Vector3.zero);
                transform.position = modifire.transform.position;
            }
        }

        public void Destroy()
        {
            if (modifire != null)
                modifire.moveMods.Remove(moveMod);

            Destroy(base.gameObject);
        }

        public void EntityTriggerEnter(Collider other)
        {
            if (other.GetComponent<ActivityModifier>() && (other.CompareTag("NPC")) && flying)
            {
                cooldown.Initialize();
                modifire = other.GetComponent<ActivityModifier>();
                modifire.moveMods.Add(moveMod);
                flying = false;
                gumFlying.gameObject.SetActive(false);
                gumGrounded.gameObject.SetActive(true);
                audMan.FlushQueue(true);
                audMan.PlaySingle(Splat);
            }
        }
        public void EntityTriggerStay(Collider other)
        {
        }
        public void EntityTriggerExit(Collider other)
        {
        }

        public Entity entity;
        public float speed = 30;
        public float time = 10;
        public Cooldown destroyCooldown;
        private MovementModifier moveMod = new MovementModifier(Vector3.zero, 0.1f);
        public SpriteRenderer gumFlying;
        public SpriteRenderer gumGrounded;
        public ActivityModifier modifire;
        public Cooldown cooldown;
        public AudioManager audMan;
        private bool flying = true;
        public SoundObject Spit;
        public SoundObject Woosh; 
        public SoundObject Splat;
        public bool diet;
    }
}
