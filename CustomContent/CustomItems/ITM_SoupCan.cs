using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_SoupCan : Item, CustomDataItem
    {
        public void Setup() {
            drinkingSound = FundamentalCodingHelper.FindResourceObjectContainingName<SoundObject>("WaterSlurp");
        }

        public override bool Use(PlayerManager pm)
        {
            pm.plm.AddStamina(50, true);
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(drinkingSound);
            return true;
        }

        public SoundObject drinkingSound;
    }
}
