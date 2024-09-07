using UnityEngine;
using MTM101BaldAPI.Registers;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Compass : NpcItem
    {
        public override void OnUse(PlayerManager pm, NPC npc)
        {
            base.OnUse(pm, npc);
            cooldown.Initialize();
            if (npc == null)
            {
                returnTrue = false;
                destroyOnUse = true;
            }
            else
                target = npc.transform;      
        }

        private void Update()
        {
            if (target != null)
            {
                audMan.transform.position = target.position;

                foreach (SubtitleController sub in FindObjectsOfType<SubtitleController>())
                {
                    if (sub.text.text == "*THE COMPASS IS POINTING HERE*" && !compassEnd)
                    {
                        sub.text.text = $"{Singleton<LocalizationManager>.Instance.GetLocalizedText(NPCMetaStorage.Instance.Get(target.GetComponent<NPC>()).nameLocalizationKey)} IS HERE";
                        sub.text.color = FundamentalCodingHelper.GetVariable<Color>(NPCMetaStorage.Instance.Get(target.GetComponent<NPC>()).value.GetComponent<AudioManager>(), "subtitleColor");
                        compassEnd = true;
                    }
                }
            }
        }

        public void EndCooldown() => 
            Destroy(base.gameObject);

        public AudioManager audMan;
        public Transform target;
        public bool compassEnd;
        public Cooldown cooldown;
    }
}
