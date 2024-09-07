using bbpfer.FundamentalManagers;
using MTM101BaldAPI.Registers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_CleanChalkEaser : Item
    {
        public override bool Use(PlayerManager pm)
        {
            foreach (NPC chalkFace in pm.ec.Npcs)
            {
                if (chalkFace.Character == Character.Chalkles)
                {
                    chalkFace.GetComponent<ChalkFace>().Cancel();
                    Destroy(base.gameObject);
                    return false;
                }

            }
            return false;
        }
    }
}
