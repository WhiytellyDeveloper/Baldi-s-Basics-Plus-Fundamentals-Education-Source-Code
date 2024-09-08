using bbpfer.Patches;
using bbpfer.FundamentalManagers;
using bbpfer.CustomLoaders;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_DuctTape : Item, CustomDataItem
    {
        public void Setup() =>
            slapSound = FundamentalCodingHelper.FindResourceObjectWithName<SoundObject>("Slap");

        public override bool Use(PlayerManager pm)
        {
            if (LastRemovedItemPatch.lastRemovedItem != null)
            {
                pm.itm.SetItem(LastRemovedItemPatch.lastRemovedItem, pm.itm.selectedItem);
                Singleton<CoreGameManager>.Instance.audMan.PlaySingle(slapSound);
            }
            return false;
        }

        public SoundObject slapSound;
    }
}
