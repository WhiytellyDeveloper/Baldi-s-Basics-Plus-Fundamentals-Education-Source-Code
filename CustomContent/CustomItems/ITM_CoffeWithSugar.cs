using bbpfer.FundamentalManagers;
using bbpfer.CustomLoaders;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_CoffeWithSugar : Item, CustomDataItem
    {
        public void Setup() =>   
            drinking = AssetsCreator.CreateSound("drinking-coffe", "Items", "", SoundType.Effect, "", 2);

        public override bool Use(PlayerManager pm)
        {
            pm.plm.stamina = 350;
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(drinking);
            Destroy(base.gameObject);
            return true;
        }

        public SoundObject drinking;
    }
}
