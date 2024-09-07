using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;
using bbpfer.CustomContent.CustomItems;
using UnityEngine;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Shovel : NpcItem, CustomDataItem
    {
        public void Setup() =>
            shovelSound = AssetsCreator.CreateSound("ShovelSound", "Items", "*BANG*", SoundType.Effect, "#FFFFFF", 1);    

        public override void OnUse(PlayerManager pm, NPC npc)
        {
            base.OnUse(pm, npc);
            movMod = new MovementModifier(Vector3.zero, 0);
            cooldown = new Cooldown(3, 0, true, cooldownEnd);
            this.pm = pm;
            if (npc != null)
            {
                this.npc = npc;
                cooldown.Initialize();
                force = new Force(-pm.transform.forward, 200f, 75f);
                npc.Navigator.Entity.AddForce(force);
                npc.Navigator.Entity.IgnoreEntity(pm.plm.Entity, true);
                npc.Navigator.Am.moveMods.Add(movMod);
                Singleton<CoreGameManager>.Instance.audMan.PlaySingle(shovelSound);
            }
            else
            {
                returnTrue = false;
                destroyOnUse = true;
            }
        }

        public void cooldownEnd()
        {
            npc.Navigator.Entity.RemoveForce(force);
            npc.Navigator.Entity.IgnoreEntity(pm.plm.Entity, false);
            npc.Navigator.Am.moveMods.Remove(movMod);
            Destroy(base.gameObject);
        }

        public NPC npc;
        public Cooldown cooldown;
        public Force force;
        public SoundObject shovelSound;
        public MovementModifier movMod;
    }
}
