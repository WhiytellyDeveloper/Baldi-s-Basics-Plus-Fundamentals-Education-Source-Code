using UnityEngine;
using System.Collections.Generic;
using bbpfer.CustomLoaders;
using PixelInternalAPI.Extensions;
using PixelInternalAPI.Classes;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_GenericSoda : Item, IEntityTrigger, CustomDataItem
    {
        public void Setup()
        {
            SpriteRenderer sprite = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.CreateSprite("GenerixSoda_Spray", "Items", 25)).AddSpriteHolder(0, LayerStorage.ignoreRaycast);
            sprite.gameObject.layer = LayerStorage.billboardLayer;
            sprite.transform.SetParent(transform);
            entity = gameObject.CreateEntity(1, 1, sprite.transform).SetEntityCollisionLayerMask(0);

            spraySound = FundamentalCodingHelper.GetVariable<SoundObject>(MTM101BaldAPI.Registers.ItemMetaStorage.Instance.FindByEnum(Items.Bsoda).value.item.GetComponent<ITM_BSODA>(), "sound");
        }

        public override bool Use(PlayerManager pm)
        {
            this.pm = pm;
            Vector3 spawnPosition = pm.transform.position - Singleton<CoreGameManager>.Instance.GetCamera(pm.playerNumber).transform.forward;
            transform.position = spawnPosition;
            transform.forward = Singleton<CoreGameManager>.Instance.GetCamera(pm.playerNumber).transform.forward;
            entity.Initialize(pm.ec, transform.position);
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(spraySound);
            pm.RuleBreak("Drinking", 0.8f, 0.1f);
            return true;
        }

        private void Update()
        {
            moveMod.movementAddend = entity.ExternalActivity.Addend + transform.forward * 42 * pm.ec.EnvironmentTimeScale;
            entity.UpdateInternalMovement(transform.forward * 42 * pm.ec.EnvironmentTimeScale);
        }

        public void EntityTriggerEnter(Collider other)
        {
            Entity component = other.GetComponent<Entity>();
            if ((other.CompareTag("Player")) && component != null)
            {
                component.ExternalActivity.moveMods.Add(this.moveMod);
                activityMods.Add(component.ExternalActivity);
            }
        }

        public void EntityTriggerStay(Collider other)
        {
        }

        public void EntityTriggerExit(Collider other)
        {
            Entity component = other.GetComponent<Entity>();
            if (component != null)
            {
                component.ExternalActivity.moveMods.Remove(moveMod);
                activityMods.Remove(component.ExternalActivity);
            }
        }

        private MovementModifier moveMod = new MovementModifier(Vector3.zero, 1);
        public Entity entity;
        private List<ActivityModifier> activityMods = new List<ActivityModifier>();
        public SoundObject spraySound;
    }
}
