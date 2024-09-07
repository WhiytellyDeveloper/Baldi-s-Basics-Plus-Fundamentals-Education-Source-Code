using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using bbpfer.FundamentalManagers;
using bbpfer.CustomLoaders;
using PixelInternalAPI.Extensions;
using MTM101BaldAPI;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Tea : Item, CustomDataItem
    {
        public void Setup()
        {
            drinking = AssetsCreator.Get<SoundObject>("drinking-coffe");

            canvasPrefab = ObjectCreationExtensions.CreateCanvas();
            canvasPrefab.gameObject.ConvertToPrefab(true);
            image = PixelInternalAPI.Extensions.ObjectCreationExtensions.CreateImage(canvasPrefab, false);
            image.sprite = AssetsCreator.CreateSprite("laaargeTea_overlay", "Items", 1);
            image.transform.localPosition = new Vector3(0, -260);
            image.transform.localScale = new Vector3(2f, 2f);
        }

        public override bool Use(PlayerManager pm)
        {
            Canvas canvas = Instantiate(canvasPrefab);
            canvas.name = "TeaCanvas";
            canvas.transform.SetParent(transform);
            canvas.worldCamera = Singleton<CoreGameManager>.Instance.GetCamera(pm.playerNumber).canvasCam;
            image = canvas.GetComponentInChildren<Image>();

            if (diet)
            {
                pm.plm.am.moveMods.Clear();

                if (!AssetsCreator.Get<Sprite>("laaargeDietTea_overlay"))
                    image.sprite = AssetsCreator.CreateSprite("laaargeDietTea_overlay", "Items", 1);
                else
                    image.sprite = AssetsCreator.Get<Sprite>("laaargeDietTea_overlay");
            }
            else
            {
                List<MovementModifier> modsToRemove = new List<MovementModifier>();

                foreach (MovementModifier movMod in pm.plm.am.moveMods)
                {
                    if (movMod.movementMultiplier <= 1 && movMod != null)
                        modsToRemove.Add(movMod);
                }

                foreach (MovementModifier mod in modsToRemove)
                    pm.plm.am.moveMods.Remove(mod);
            }

            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(drinking);
            StartCoroutine(Rise());
            return true;
        }

        public IEnumerator Rise()
        {
            while (image.transform.localPosition.y < 260)
            {
                image.transform.localPosition += Vector3.up * 200 * Time.deltaTime;
                yield return null;
            }
            image.transform.localPosition = new Vector3(image.transform.localPosition.x, 260, image.transform.localPosition.z);
            Destroy(gameObject);
        }

        public bool diet;
        public SoundObject drinking;
        public Canvas canvasPrefab;
        public Image image;
    }
}
