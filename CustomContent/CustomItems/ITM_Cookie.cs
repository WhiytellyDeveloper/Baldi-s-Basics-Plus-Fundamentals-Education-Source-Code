using bbpfer.FundamentalManagers;
using bbpfer.CustomLoaders;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Cookie : Item, CustomDataItem
    {
        public void Setup() =>
            eatingSound = FundamentalCodingHelper.GetVariable<SoundObject>(MTM101BaldAPI.Registers.ItemMetaStorage.Instance.FindByEnum(Items.ZestyBar).value.item.GetComponent<ITM_ZestyBar>(), "audEat");

        public override bool Use(PlayerManager pm)
        {
            pm.plm.AddStamina(100, true);
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(eatingSound);

            if (nextItem != null)
            {
                pm.itm.SetItem(nextItem, pm.itm.selectedItem);
                Destroy(base.gameObject);
                return false;
            }

            Destroy(base.gameObject);
            return true;
        }

        public ItemObject nextItem;
        public SoundObject eatingSound;
    }
}
