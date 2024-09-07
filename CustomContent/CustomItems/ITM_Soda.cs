using UnityEngine;
using System.Collections;
using bbpfer.FundamentalManagers;
using bbpfer.CustomLoaders;
using bbpfer.CustomContent.CustomItems;
using MTM101BaldAPI.Registers;
using MTM101BaldAPI;
using PixelInternalAPI.Extensions;
using PixelInternalAPI.Classes;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Soda : Item, CustomDataItem
    {
        public void Setup()
        {
            sprite = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.CreateSprite("SodaSprite", "Items", 12)).AddSpriteHolder(0, LayerStorage.ignoreRaycast);
            sprite.gameObject.layer = LayerStorage.billboardLayer;
            sprite.transform.SetParent(transform);
            entity = gameObject.CreateEntity(1, 1, sprite.transform);

            splashSound = AssetsCreator.CreateSound("Soda_open", "Items", "", SoundType.Effect, "", 1);
            trashSound = AssetsCreator.CreateSound("Soda_end", "Items", "", SoundType.Effect, "", 1);
        }

        public override bool Use(PlayerManager pm)
        {
            cooldown = new Cooldown(30, 0, true, End);
            modfire = new MovementModifier(Vector3.zero, 1.5f);
            if (Singleton<InGameManager>.Instance.sodaInUse == 3)
            {
                Destroy(base.gameObject);
                return false;
            }

            this.pm = pm;
            transform.position = pm.transform.position;
            entity.Initialize(pm.ec, transform.position);
            cooldown.Initialize();
            pm.Am.moveMods.Add(modfire);
            Singleton<InGameManager>.Instance.sodaInUse++;
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(splashSound);
            StartCoroutine(Fade(1.5f, 1));
            return true;
        }

        private void Update()
        {
            entity.UpdateInternalMovement(Singleton<CoreGameManager>.Instance.GetCamera(0).transform.forward * 40 * pm.ec.EnvironmentTimeScale);
        }

        public void End()
        {
            pm.Am.moveMods.Remove(modfire);
            Singleton<InGameManager>.Instance.sodaInUse--;
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(trashSound);
            Destroy(base.gameObject);
        }

        private IEnumerator Fade(float speed, float duration)
        {
            float elapsedTime = 0f;
            Color color = sprite.color;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Lerp(Color.white.a, 0, elapsedTime * speed / duration);
                sprite.color = color;
                yield return null;
            }
        }

        public MovementModifier modfire;
        public Cooldown cooldown;
        public SoundObject splashSound;
        public SoundObject trashSound;
        public SpriteRenderer sprite;
        public Entity entity;
    }
}
