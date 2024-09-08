using bbpfer.FundamentalManagers;
using bbpfer.CustomLoaders;
using MTM101BaldAPI.Registers;
using UnityEngine;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_ZestyWhiteFlavored : Item, CustomDataItem
    {
        public void Setup() =>
            eatingSound = FundamentalCodingHelper.GetVariable<SoundObject>(ItemMetaStorage.Instance.FindByEnum(Items.ZestyBar).value.item.GetComponent<ITM_ZestyBar>(), "audEat");

        public override bool Use(PlayerManager pm)
        {
            pm.plm.stamina = Random.Range(0, 200);
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(eatingSound);
            return true;
        }

        public SoundObject eatingSound;
    }
}
