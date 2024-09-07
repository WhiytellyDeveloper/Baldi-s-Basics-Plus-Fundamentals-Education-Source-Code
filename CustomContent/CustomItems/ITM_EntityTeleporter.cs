using UnityEngine;
using System.Linq;
using bbpfer.CustomLoaders;
using MTM101BaldAPI.Registers;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_EntityTeleporter : NpcItem, CustomDataItem
    {
        public void Setup() =>
            teleportSound = FundamentalCodingHelper.GetVariable<SoundObject>(ItemMetaStorage.Instance.FindByEnum(Items.Teleporter).value.item.GetComponent<ITM_Teleporter>(), "audTeleport");

        public override void OnUse(PlayerManager pm, NPC npc)
        {
            base.OnUse(pm, npc);
            npc.Navigator.Entity.Teleport(pm.ec.RandomCell(true, false, true).FloorWorldPosition);

            if (teleportSound != null)
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(teleportSound);
        }

        public override void PostUseItem()
        {
            base.PostUseItem();
            destroyOnUse = true;
        }

        public SoundObject teleportSound;
    }
}
