using bbpfer.FundamentalManagers;
using HarmonyLib;
using MTM101BaldAPI.Registers;
using bbpfer.CustomLoaders;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_AdvertenceBook : NpcItem, CustomDataItem
    {
        public void Setup() =>
            writeSound = AssetsCreator.CreateSound("AdvertenceBookWrite", "Items", "Sfx_Write", SoundType.Effect, "#FFFFFF", 1);

        public bool connected;
        public SoundObject writeSound;

        public override void OnUse(PlayerManager pm, NPC npc)
        {
            base.OnUse(pm, npc);
            AccessTools.Method(typeof(NPC), "SetGuilt").Invoke(npc, new object[] { 15, "Bullying" });
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(writeSound);

            if (connected)
            UnityEngine.Object.Instantiate<Item>(ItemMetaStorage.Instance.FindByEnum(Items.PrincipalWhistle).value.item).Use(pm);
        }

        public override void PostUseItem()
        {
            base.PostUseItem();
            destroyOnUse = true;
            notAllowedCharacters.Add(Character.Principal);
        }
    }
}
