using bbpfer.CustomRooms.CustomObjects;
using bbpfer.Enums;
using bbpfer.Extessions;
using bbpfer.FundamentalManagers;
using MTM101BaldAPI;
using MTM101BaldAPI.Registers;
using System.Collections.Generic;
using UnityEngine;

namespace bbpfer.CustomRooms.Functions
{
    public class ArtRoomFunction : RoomFunction
    {
        public override void OnGenerationFinished()
        {
            base.OnGenerationFinished();

            Transform parentObject = room.transform;
            Transform transform = parentObject.Find("RoomObjects").transform;

            foreach (Pickup pickup in transform.GetComponentsInChildren<Pickup>())
            {
                Debug.Log($"Item Name:{FundamentalCodingHelper.GetVariable<int>(pickup.item.item.GetComponent<ITM_YTPs>(), "value")}");
                positions.Add(new Vector2(pickup.transform.position.x, pickup.transform.position.z));
                room.ec.items.Remove(pickup);
                pickup.icon.spriteRenderer.enabled = false;
                Destroy(pickup.gameObject);
            }

            transform.GetComponentsInChildren<Painting>()[0].sr.sprite = transform.GetComponentsInChildren<Painting>()[0].sr.sprite.MergeWithOverlay(paintRankings[0]);
            transform.GetComponentsInChildren<Painting>()[1].sr.sprite = transform.GetComponentsInChildren<Painting>()[1].sr.sprite.MergeWithOverlay(paintRankings[1]);
            transform.GetComponentsInChildren<Painting>()[2].sr.sprite = transform.GetComponentsInChildren<Painting>()[2].sr.sprite.MergeWithOverlay(paintRankings[2]);

            rng = new System.Random(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
        }

        private void Update()
        {
            if (Singleton<BaseGameManager>.Instance.FoundNotebooks == 1)
                PlaceReward(3);
            if (Singleton<BaseGameManager>.Instance.FoundNotebooks == Singleton<BaseGameManager>.Instance.NotebookTotal / 2)
                PlaceReward(2);
            if (Singleton<BaseGameManager>.Instance.FoundNotebooks == Singleton<BaseGameManager>.Instance.NotebookTotal)
                PlaceReward(1);

        }

        public void PlaceReward(int place)
        {
            if (!rewardPlaced[place - 1])
            {
                rewardPlaced[place - 1] = true;
                ItemObject item = WeightedItemObject.ControlledRandomSelection(list[place - 1].items, rng);
                room.ec.CreateItem(room, item, positions[place - 1]);
            }
        }

        public void PreInitialize(List<Sprite> paints, WeightedList[] list)
        {
            paintRankings = paints;
            this.list = list;
        }

        public List<Sprite> paintRankings = new List<Sprite>();
        public List<Vector2> positions = new List<Vector2>();
        private bool[] rewardPlaced = new bool[] { false, false, false };
        private System.Random rng;

        public ArtRoomFunction.WeightedList[] list = new ArtRoomFunction.WeightedList[]
        {
    new ArtRoomFunction.WeightedList
    {
        items = new WeightedItemObject[]
        {
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.BanHammer.ToString())).value,
                weight = 46
            },
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.Tea.ToString())).value,
                weight = 60
            },
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.Potion.ToString())).value,
                weight = 50
            },
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.CoffeAndSugar.ToString())).value,
                weight = 52
            },
        },
    },
    new ArtRoomFunction.WeightedList
    {
        items = new WeightedItemObject[]
        {
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(Items.DoorLock).value,
                weight = 53
            },
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(Items.Quarter).value,
                weight = 70
            },
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(Items.Nametag).value,
                weight = 62
            },
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.GenericSoda.ToString())).value,
                weight = 45
            },
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.GenericHammer.ToString())).value,
                weight = 50
            },
        },
    },
    new ArtRoomFunction.WeightedList
    {
        items = new WeightedItemObject[]
        {
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(Items.DietBsoda).value,
                weight = 60
            },
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(Items.Wd40).value,
                weight = 42
            },
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.Cookie.ToString())).value,
                weight = 75
            },
            new WeightedItemObject
            {
                selection = ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(CustomItems.DietTea.ToString())).value,
                weight = 30
            },
        },
    }
        };


        public class WeightedList
        {
            public WeightedItemObject[] items = new WeightedItemObject[0];

        }
    }
}
