using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Cheese : Item, CustomDataItem
    {
        public void Setup() {
            eatingSound = AssetsCreator.CreateSound("eat_cheese", "Items", "", SoundType.Effect, "", 1);
        }

        public override bool Use(PlayerManager pm)
        {
            pm.plm.AddStamina(pm.plm.stamina, false);
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(eatingSound);
            Destroy(base.gameObject);
            return true;
        }

        public SoundObject eatingSound;
    }
}
