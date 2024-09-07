using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;
using PixelInternalAPI.Extensions;
using MTM101BaldAPI;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_IceCreamMask : Item, CustomDataItem
    {
        public void Setup()
        {
            canvasPrefab = ObjectCreationExtensions.CreateCanvas();
            canvasPrefab.gameObject.ConvertToPrefab(true);
            image = PixelInternalAPI.Extensions.ObjectCreationExtensions.CreateImage(canvasPrefab, false);
            image.transform.localPosition = new Vector3(85, 140);
            image.transform.localScale = new Vector3(0.7f, 0.7f);

            far = AssetsCreator.CreateSprite("IceCreamMask_Large", "Items", 1);
            close = AssetsCreator.CreateSprite("IceCreamMask_Close", "Items", 1);
            veryClose = AssetsCreator.CreateSprite("IceCreamMask_VeryClose", "Items", 1);
            notHere = AssetsCreator.CreateSprite("IceCreamMask_Sleeping", "Items", 1);

            changeSound = FundamentalCodingHelper.FindResourceObjectContainingName<SoundObject>("ItemPickup");
        }

        public Canvas canvasPrefab;
        public Image image;
        public Sprite far, close, veryClose, notHere;
        private float riseSpeed = 7f;
        private float animHeight;
        private float targetHeightMin = 140f;
        private float targetHeightMax = 145f;
        private float riseTargetHeight = 250f;
        public SoundObject changeSound;
        private Sprite oldPSite;
        public Cooldown cooldown;

        public override bool Use(PlayerManager pm)
        {
            this.pm = pm;
            cooldown = new Cooldown(65, 0, true, RiseUp);
            if (pm.ec.GetBaldi() == null && Singleton<CoreGameManager>.Instance.currentMode != Mode.Free)
            {
                Destroy(gameObject);
                return false;
            }
            Canvas canvas = Instantiate(canvasPrefab);
            canvas.name = "IceCreamMaskCanvas";
            canvas.transform.SetParent(transform);
            canvas.worldCamera = Singleton<CoreGameManager>.Instance.GetCamera(pm.playerNumber).canvasCam;
            image = canvas.GetComponentInChildren<Image>();
            StartCoroutine(Anim());
            cooldown.Initialize();
            return true;
        }

        private void Update()
        {
            if (Singleton<CoreGameManager>.Instance.currentMode == Mode.Free)
            {
                StopAllCoroutines();
                StartCoroutine(Rise());
                image.sprite = notHere;
            }
            else
            {
                Transform baldi = pm.ec.GetBaldi().transform;
                float distanceToBaldi = Vector3.Distance(pm.transform.position, baldi.position);

                if (distanceToBaldi <= 100)
                    Reload(veryClose);
                else if (distanceToBaldi <= 230)
                    Reload(close);
                else if (distanceToBaldi <= 300)
                    Reload(far);
            }
        }

        private IEnumerator Anim()
        {
            while (true)
            {
                while (image.transform.localPosition.y < targetHeightMax)
                {
                    image.transform.localPosition += Vector3.up * riseSpeed * Time.deltaTime;
                    animHeight = image.transform.localPosition.y;
                    yield return null;
                }

                while (image.transform.localPosition.y > targetHeightMin)
                {
                    image.transform.localPosition -= Vector3.up * riseSpeed * Time.deltaTime;
                    animHeight = image.transform.localPosition.y;
                    yield return null;
                }

                image.transform.localPosition = new Vector3(image.transform.localPosition.x, animHeight, image.transform.localPosition.z);
            }
        }

        public IEnumerator Rise()
        {
            while (image.transform.localPosition.y < riseTargetHeight)
            {
                image.transform.localPosition += Vector3.up * riseSpeed * Time.deltaTime;
                yield return null;
            }
            image.transform.localPosition = new Vector3(image.transform.localPosition.x, riseTargetHeight, image.transform.localPosition.z);
            Destroy(gameObject);
        }

        public void RiseUp()
        {
            StartCoroutine(Rise());
        }

        public void Reload(Sprite newSprite)
        {
            if (newSprite != image.sprite)
            {
                image.sprite = newSprite;

                if (newSprite != oldPSite)               
                    Singleton<CoreGameManager>.Instance.audMan.PlaySingle(changeSound);               

                oldPSite = newSprite;
            }
        }
    }
}
