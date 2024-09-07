using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTM101BaldAPI.Registers;
using MTM101BaldAPI;
using PixelInternalAPI.Extensions;
using bbpfer.Enums;

namespace bbpfer.FundamentalManagers
{
    public class InGameManager : Singleton<InGameManager>
    {
        private void Start()
        {
        }

        private void Update()
        {
            for (int i = 0; i < cooldowns.Count; i++)
            {
                if (cooldowns[i] == null)
                {
                    cooldowns.Remove(cooldowns[i]);
                    return;
                }


                cooldowns[i].UpdateCooldown();
                if (Singleton<BaseGameManager>.Instance != null && Singleton<BaseGameManager>.Instance.Ec != null)
                    cooldowns[i].timeScale = Time.timeScale * Singleton<BaseGameManager>.Instance.Ec.EnvironmentTimeScale;
            }
        }

        public void Reset()
        {
            /*
            if (!Singleton<CoreGameManager>.Instance.sceneObject.levelObject.finalLevel && Singleton<CoreGameManager>.Instance.sceneObject.levelObject.name.Contains("main"))
            {
                collectedNotebooks += notebooks;
                notebooks = 0;
            }
            else
            {
                collectedNotebooks = 0;
                notebooks = 0;
            }
            */
            CloseBag(0);
            cooldowns.Clear();
            iceCreamInUse = false;
            gpsInUse = false;
            potionInUse = false;
            sodaInUse = 0;

            bagItems = new ItemObject[]
        {
            ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.OpenBag.ToString())).value,
            ItemMetaStorage.Instance.FindByEnum(Items.None).value,
            ItemMetaStorage.Instance.FindByEnum(Items.None).value,
            ItemMetaStorage.Instance.FindByEnum(Items.None).value,
            ItemMetaStorage.Instance.FindByEnum(Items.None).value,
            ItemMetaStorage.Instance.FindByEnum(Items.None).value,
        };

            playerItems = new ItemObject[9];
        }

        public void UpdateVaules()
        {
            Singleton<BaseGameManager>.Instance.AddNotebookTotal(collectedNotebooks);
            Singleton<BaseGameManager>.Instance.CollectNotebooks(collectedNotebooks);
            Singleton<BaseGameManager>.Instance.AngerBaldi(-collectedNotebooks);
            Singleton<CoreGameManager>.Instance.GetHud(0).UpdateNotebookText(0, FundamentalCodingHelper.GetVariable<int>(Singleton<BaseGameManager>.Instance, "foundNotebooks").ToString() + "/" + Mathf.Max(Singleton<BaseGameManager>.Instance.Ec.notebookTotal, FundamentalCodingHelper.GetVariable<int>(Singleton<BaseGameManager>.Instance, "foundNotebooks")).ToString(), false);
        }

        public void OpenBag(int player)
        {
            if (!bagIsOpen)
            {
                var selectedPlayer = Singleton<CoreGameManager>.Instance.GetPlayer(player);
                playerItems = selectedPlayer.itm.items;
                selectedPlayer.itm.items = bagItems;
                selectedPlayer.itm.UpdateItems();
                selectedPlayer.itm.UpdateSelect();
                bagIsOpen = true;
            }
        }

        public void CloseBag(int player)
        {
            if (bagIsOpen)
            {
                var selectedPlayer = Singleton<CoreGameManager>.Instance.GetPlayer(player);
                bagItems = selectedPlayer.itm.items;
                selectedPlayer.itm.items = playerItems;
                selectedPlayer.itm.UpdateItems();
                selectedPlayer.itm.UpdateSelect();
                playerItems = bagItems;
                bagIsOpen = false;
            }
        }


        public List<Cooldown> cooldowns = new List<Cooldown>();
        public bool iceCreamInUse;
        public bool gpsInUse;
        public bool potionInUse;
        public int sodaInUse;
        public TMP_Text text;
        public int notebooks;
        public int collectedNotebooks;

        public ItemObject[] bagItems = new ItemObject[]
        {
            ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.OpenBag.ToString())).value,
            ItemMetaStorage.Instance.FindByEnum(Items.None).value,
            ItemMetaStorage.Instance.FindByEnum(Items.None).value,
            ItemMetaStorage.Instance.FindByEnum(Items.None).value,
            ItemMetaStorage.Instance.FindByEnum(Items.None).value,
            ItemMetaStorage.Instance.FindByEnum(Items.None).value,
        };

        public ItemObject[] playerItems;

        public bool bagIsOpen;
    }
}

