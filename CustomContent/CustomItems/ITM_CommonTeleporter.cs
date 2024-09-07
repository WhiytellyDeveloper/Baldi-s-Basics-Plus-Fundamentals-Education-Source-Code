using bbpfer.CustomLoaders;
using MTM101BaldAPI.Registers;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_CommonTeleporter : Item, CustomDataItem
    {
        public void Setup() =>      
            teleportSound = FundamentalCodingHelper.GetVariable<SoundObject>(ItemMetaStorage.Instance.FindByEnum(Items.Teleporter).value.item.GetComponent<ITM_Teleporter>(), "audTeleport");

        public override bool Use(PlayerManager pm)
        {
            pm.Teleport(pm.ec.RandomCell(true, false, true).FloorWorldPosition);
            pm.ec.MakeNoise(pm.transform.position, 80);
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(teleportSound);
            Destroy(base.gameObject);
            return true;
        }

        public SoundObject teleportSound;
    }
}
