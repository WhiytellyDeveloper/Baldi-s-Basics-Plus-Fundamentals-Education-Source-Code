using bbpfer.FundamentalManagers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using PixelInternalAPI.Extensions;
using MTM101BaldAPI;
using bbpfer.CustomLoaders;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Potion : Item, CustomDataItem
    {
        public void Setup()
        {
            canvasPrefab = ObjectCreationExtensions.CreateCanvas();
            canvasPrefab.gameObject.ConvertToPrefab(true);
            image = PixelInternalAPI.Extensions.ObjectCreationExtensions.CreateImage(canvasPrefab, false);
            image.sprite = AssetsCreator.CreateSprite("MysteryPotion_Overlay", "Items", 1);
            image.transform.localPosition = new Vector3(-381, 0);
            image.transform.localScale = new Vector3(2f, 2f);
        }

        public override bool Use(PlayerManager pm)
        {
            this.pm = pm;
            movMod = new MovementModifier(UnityEngine.Vector3.zero, 1.1f);
            cooldown = new Cooldown(10, 0, true, CooldownEnd);
            cooldown.Initialize();
            pm.SetInvisible(true);
            pm.invincible = true;
            pm.plm.am.moveMods.Add(movMod);
            Singleton<InGameManager>.Instance.potionInUse = true;
            Canvas canvas = Instantiate(canvasPrefab);
            canvas.name = "PotionCanvas";
            canvas.transform.SetParent(transform);
            canvas.worldCamera = Singleton<CoreGameManager>.Instance.GetCamera(pm.playerNumber).canvasCam;
            image = canvas.GetComponentInChildren<Image>();
            StartCoroutine(Rise());
            StartCoroutine(MoveUpDown());
            return true;
        }

        public IEnumerator Rise()
        {
            while (image.transform.localPosition.x < 400)
            {
                image.transform.localPosition += Vector3.right * 300 * Time.deltaTime;
                yield return null;
            }
            image.transform.localPosition = new Vector3(400, image.transform.localPosition.y, image.transform.localPosition.z);
        }

        public IEnumerator ReverseRise()
        {
            while (image.transform.localPosition.x > -400)
            {
                image.transform.localPosition -= Vector3.right * 300 * Time.deltaTime;
                yield return null;
            }
            image.transform.localPosition = new Vector3(-400, image.transform.localPosition.y, image.transform.localPosition.z);
            Destroy(gameObject);
        }




        public IEnumerator MoveUpDown()
        {
            while (true)
            {
                while (image.transform.localPosition.y < 20)
                {
                    image.transform.localPosition += Vector3.up * 50 * Time.deltaTime;
                    yield return null;
                }
                while (image.transform.localPosition.y > -10)
                {
                    image.transform.localPosition += Vector3.down * 50 * Time.deltaTime;
                    yield return null;
                }
            }
        }


        public void CooldownEnd()
        {
            pm.SetInvisible(false);
            pm.invincible = false;
            pm.plm.am.moveMods.Remove(movMod);
            Singleton<InGameManager>.Instance.potionInUse = false;
            StartCoroutine(ReverseRise());
        }

        public MovementModifier movMod;
        public Cooldown cooldown;
        public Canvas canvasPrefab;
        public Image image;
    }
}
