using UnityEngine;
using System;
using bbpfer.FundamentalManagers;
using System.Collections.Generic;

namespace bbpfer.CustomRooms.Functions
{
    public class StorageFunction : RoomFunction
    {
        public override void Build(LevelBuilder builder, System.Random rng)
        {
            base.Build(builder, rng);
            this.rng = rng;
        }

        public override void OnGenerationFinished()
        {
            base.OnGenerationFinished();
            Transform parentObject = room.transform;
            Transform transform = parentObject.Find("RoomObjects").transform;

            if (transform.GetComponentsInChildren<StorageLocker>() != null)
            {
                CreateItemList();
                foreach (StorageLocker locker in transform.GetComponentsInChildren<StorageLocker>())
                {
                    int vaule = rng.Next(0, 5);

                    if (vaule >= 1)
                        FundamentalCodingHelper.GetVariable<Pickup[]>(locker, "pickup")[0].AssignItem(itens[rng.Next(0, itens.Count)].selection);
                    if (vaule >= 3)
                        FundamentalCodingHelper.GetVariable<Pickup[]>(locker, "pickup")[1].AssignItem(itens[rng.Next(0, itens.Count)].selection);
                    if (vaule > 4)
                        FundamentalCodingHelper.GetVariable<Pickup[]>(locker, "pickup")[2].AssignItem(itens[rng.Next(0, itens.Count)].selection);
                }
            }
        }

        public void CreateItemList()
        {
            if (Singleton<BaseGameManager>.Instance.levelObject == null)
            {
                foreach (LevelObject level in FundamentalCodingHelper.FindResourceObjects<LevelObject>())
                {
                    foreach (WeightedItemObject item in level.potentialItems)
                    {
                        if (!itens.Contains(item) && !level.name.Contains("end"))
                            itens.Add(item);
                    }
                }
            }
            else
            {
                foreach (WeightedItemObject item in Singleton<BaseGameManager>.Instance.levelObject.potentialItems)
                {
                    if (!itens.Contains(item))
                        itens.Add(item);
                }
            }
        }

        public List<WeightedItemObject> itens;
        public System.Random rng = new System.Random();
    }
}
