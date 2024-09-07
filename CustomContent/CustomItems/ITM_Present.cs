using System;
using bbpfer.FundamentalManagers;
using System.Collections.Generic;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Present : Item
    {
        public override bool Use(PlayerManager pm)
        {
            Setup();
            cRNG = new System.Random(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
            pm.itm.AddItem(itens[cRNG.Next(0, itens.Count)].selection);
            return true;
        }

        public void Setup()
        {
            if (Singleton<BaseGameManager>.Instance.levelObject == null)
            {
                foreach (LevelObject level in FundamentalCodingHelper.FindResourceObjects<LevelObject>())
                {
                    foreach (WeightedItemObject item in level.potentialItems)
                    {
                        if (!itens.Contains(item))
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

        public List<WeightedItemObject> itens = new List<WeightedItemObject>();
        public System.Random cRNG;
    }
}
