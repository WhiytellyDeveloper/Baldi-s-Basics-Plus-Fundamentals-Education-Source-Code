using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;
using PixelInternalAPI.Extensions;
using MTM101BaldAPI;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class Klip : NPC, CustomDataNPC
    {
        public void Setup()
        {
            canvasPref = ObjectCreationExtensions.CreateCanvas();
            canvasPref.gameObject.ConvertToPrefab(true);
            paint = PixelInternalAPI.Extensions.ObjectCreationExtensions.CreateImage(canvasPref, false);
            paint.sprite = AssetsCreator.CreateSprite("KlipPaint", "", 1);
            paint.transform.localPosition = new Vector3(0, 0);
            paint.transform.localScale = new Vector3(6f, 4f);

            idleSprite = AssetsCreator.Get<Sprite>("Klip1");
            laughingSprite = AssetsCreator.Get<Sprite>("Klip2");
        }

        public void InGameSetup() 
        {
            cooldown = new Cooldown(30, 0, false);
            paintCooldown = new Cooldown(14, 0, false, Hide, null, 1, true);
        }

        //-------------------------------------------------------------------------------------------------------------------

        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new Klip_Wandering(this));
            canvas = Instantiate<Canvas>(canvasPref);
            canvas.gameObject.SetActive(false);
            canvas.transform.SetParent(transform);
            canvas.worldCamera = Singleton<CoreGameManager>.Instance.GetCamera(0).canvasCam;
            paint = canvas.GetComponentInChildren<Image>();
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();
            cooldown.UpdateCooldown();
            paintCooldown.UpdateCooldown();

            cooldown.timeScale = ec.NpcTimeScale;
            paintCooldown.timeScale = ec.EnvironmentTimeScale;
        }

        public IEnumerator ShowPaint()
        {
            canvas.gameObject.SetActive(true);
            float sizeMult = 0f;
            Vector3 ogSize = paint.transform.localScale;
            while (true)
            {
                sizeMult += ec.EnvironmentTimeScale * Time.deltaTime * 1.9f;
                if (sizeMult >= 1f)
                    break;
                paint.transform.localScale = ogSize * sizeMult;
                yield return null;
            }
            paint.transform.localScale = ogSize;
            yield break;
        }

        public IEnumerator HidePaint()
        {
            Vector3 ogSize = paint.transform.localScale;
            float sizeMult = 1f;

            while (sizeMult > 0f)
            {
                sizeMult -= ec.EnvironmentTimeScale * Time.deltaTime * 1.9f;
                paint.transform.localScale = ogSize * sizeMult;
                yield return null;
            }

            paint.transform.localScale = Vector3.zero;
            canvas.gameObject.SetActive(false);
            paint.transform.localScale = new Vector3(6f, 4f);
        }


        public void Hide()
        {
            StartCoroutine(HidePaint());
            cooldown.Pause(false);
            paintCooldown.Pause(true);
            spriteRenderer[0].sprite = idleSprite;
        }

        public Canvas canvas;
        public Canvas canvasPref;
        public Image paint;
        public Cooldown cooldown;
        public Cooldown paintCooldown;
        public Sprite idleSprite;
        public Sprite laughingSprite;
    }

    public class Klip_StateBase : NpcState { protected Klip klip; public Klip_StateBase(Klip klip) : base(klip) { this.klip = klip; npc = klip; } }

    public class Klip_Wandering : Klip_StateBase 
    { 
        public Klip_Wandering(Klip klip) : base(klip) { }

        public override void Initialize()
        {
            base.Initialize();
            klip.Navigator.SetSpeed(12);
            klip.Navigator.maxSpeed = 12;
            ChangeNavigationState(new NavigationState_WanderRandom(klip, 0));
        }

        public override void PlayerSighted(PlayerManager player)
        {
            base.PlayerSighted(player);

            if (klip.cooldown.cooldownIsEnd)
            klip.behaviorStateMachine.ChangeState(new Klip_Approaching(klip));
        }
    }


    public class Klip_Approaching : Klip_StateBase
    {
        public Klip_Approaching(Klip klip) : base(klip) { }

        public override void Initialize()
        {
            base.Initialize(); 
            klip.Navigator.SetSpeed(17);
            klip.Navigator.maxSpeed = 17;
            klip.cooldown.Pause(true);
        }

        public override void Update()
        {
            base.Update();
            ChangeNavigationState(new NavigationState_TargetPlayer(klip, 0, Singleton<CoreGameManager>.Instance.GetPlayer(0).transform.position));
        }

        public override void PlayerLost(PlayerManager player)
        {
            base.PlayerLost(player);
            klip.behaviorStateMachine.ChangeState(new Klip_Wandering(klip));
        }

        public override void OnStateTriggerEnter(Collider other)
        {
            base.OnStateTriggerEnter(other);

            if (other.GetComponent<PlayerManager>())
            {
                klip.canvas.gameObject.SetActive(true);
                klip.StartCoroutine(klip.ShowPaint());
                klip.cooldown.Restart();
                klip.paintCooldown.Pause(false);
                klip.spriteRenderer[0].sprite = klip.laughingSprite;
                PlayerLost(other.GetComponent<PlayerManager>());
            }
        }

    }
}
