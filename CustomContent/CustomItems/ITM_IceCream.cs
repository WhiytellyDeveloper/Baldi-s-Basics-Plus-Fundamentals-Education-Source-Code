using MTM101BaldAPI.PlusExtensions;
using MTM101BaldAPI.Components;
using bbpfer.FundamentalManagers;
using UnityEngine;
using UnityEngine.UI;
using bbpfer.CustomLoaders;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_IceCream : Item, CustomDataItem
    {
        public void Setup()
        {
            freezeStaminatorSprite = AssetsCreator.CreateSprite("StaminometerSheet_OverlayFreeze", "", 50);
            slurp = AssetsCreator.CreateSound("IceCreamSlurp", "Items", "*Slurp*", SoundType.Effect, "", 1);
        }

        public override bool Use(PlayerManager pm)
        {
            cooldown = new Cooldown(10, 0, true, End);
            modifire = new ValueModifier(0, 0);
            if (Singleton<InGameManager>.Instance.iceCreamInUse)
            {
                Destroy(base.gameObject);
                return false;
            }

                this.pm = pm;
                pm.GetMovementStatModifier().AddModifier("staminaDrop", modifire);
                pm.GetMovementStatModifier().AddModifier("staminaRise", modifire);
                cooldown.Initialize();
                pm.plm.stamina = 50;
                Singleton<InGameManager>.Instance.iceCreamInUse = true;

            HudManager hudMan = Singleton<CoreGameManager>.Instance.GetHud(0);
            staminatorOverlay = hudMan.transform.Find("Staminometer").Find("Overlay").GetComponent<Image>();
            staminatorSprite = staminatorOverlay.sprite;
            staminatorOverlay.sprite = freezeStaminatorSprite;

            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(slurp);

            if (nextItem != null)
            {
                pm.itm.SetItem(nextItem, pm.itm.selectedItem);
                return false;
            }

            return true;

        }

        public void End()
        {
            pm.GetMovementStatModifier().RemoveModifier(modifire);
            Singleton<InGameManager>.Instance.iceCreamInUse = false;
            staminatorOverlay.sprite = staminatorSprite;
            Destroy(base.gameObject);
        }
        
        public void OnDestroy()
        {
            Singleton<InGameManager>.Instance.iceCreamInUse = false;
            staminatorOverlay.sprite = staminatorSprite;
        }

        public Cooldown cooldown;
        public ValueModifier modifire;
        public ItemObject nextItem;
        private Sprite staminatorSprite;
        public Sprite freezeStaminatorSprite;
        private Image staminatorOverlay;
        public SoundObject slurp;
    }
}
