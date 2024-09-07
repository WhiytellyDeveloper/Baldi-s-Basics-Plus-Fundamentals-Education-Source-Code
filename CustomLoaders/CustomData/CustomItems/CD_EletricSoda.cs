/*
using bbpfer.FundamentalManagers;
using bbpfer.CustomContent.CustomItems;
using PixelInternalAPI.Classes;
using PixelInternalAPI.Extensions;
using UnityEngine;
using MTM101BaldAPI;
using bbpfer.Extessions;

namespace bbpfer.CustomLoaders.CustomData.CustomItems
{
    public class CD_EletricSoda : CustomDataItem
    {
        public override void LoadAllPrefabs()
        {
            base.LoadAllPrefabs();
            ITM_EletricSoda soda = (ITM_EletricSoda)item;
            soda.sprite = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.CreateSprite("EletricBsodaSpray", "Items", 12)).AddSpriteHolder(0, LayerStorage.ignoreRaycast);
            soda.sprite.gameObject.layer = LayerStorage.billboardLayer;
            soda.sprite.transform.SetParent(soda.transform);
            soda.entity = soda.gameObject.CreateEntity(1, 1, soda.sprite.transform);

            soda.eletricity = new GameObject("Eletricity").AddComponent<GroundEletricity>();
            soda.eletricity.sprite = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.CreateSprite("EletricPlacheolderGround", "Items", 30), false);
            soda.eletricity.gameObject.layer = LayerStorage.billboardLayer;
            soda.eletricity.sprite.transform.SetParent(soda.eletricity.transform);
            soda.eletricity.sprite.transform.localPosition = new Vector3(0, -4.99f, 0);
            soda.eletricity.sprite.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            soda.eletricity.entity = soda.eletricity.gameObject.CreateEntity(1, 1, soda.sprite.transform);
            soda.eletricity.gameObject.ConvertToPrefab(true);
        }

        public override void LoadAllSounds()
        {
            base.LoadAllSounds();
            ITM_EletricSoda soda = (ITM_EletricSoda)item;
        }

        public override void PostLoad()
        {
            base.PostLoad();
            ITM_EletricSoda soda = (ITM_EletricSoda)item;
            soda.cooldown = new Cooldown(3, 0, true, soda.Destroy);
        }
    }

    public class GroundEletricity : MonoBehaviour, IEntityTrigger
    {
        public SpriteRenderer sprite;
        public Entity entity;

        public void EntityTriggerEnter(Collider other)
        {
            if ((other.CompareTag("NPC") || other.CompareTag("Player")))
            {
                if (other.GetComponent<ActivityModifier>())                
                    other.GetComponent<ActivityModifier>().moveMods.Add(moveMod);
                
            }
        }
        public void EntityTriggerStay(Collider other)
        {
        }

        public void EntityTriggerExit(Collider other)
        {
            if ((other.CompareTag("NPC") || other.CompareTag("Player")))
            {
                if (other.GetComponent<ActivityModifier>())
                    other.GetComponent<ActivityModifier>().moveMods.Remove(moveMod);
            }
        }

        public MovementModifier moveMod = new MovementModifier(Vector3.zero, 0.1f);
    }
}
*/