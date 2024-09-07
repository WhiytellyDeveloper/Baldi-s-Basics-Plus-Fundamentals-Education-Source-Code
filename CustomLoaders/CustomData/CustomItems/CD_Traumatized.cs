/*
using System;
using System.Collections.Generic;
using UnityEngine;
using PixelInternalAPI.Extensions;
using bbpfer.FundamentalManagers;
using PixelInternalAPI.Classes;
using bbpfer.CustomContent.CustomItems;
using bbpfer.Extessions;

namespace bbpfer.CustomLoaders.CustomData.CustomItems
{
    public class CD_Traumatized : CustomDataItem
    {
        public override void LoadAllPrefabs()
        {
            base.LoadAllPrefabs();

            ITM_Traumatized traumatized = (ITM_Traumatized)item;
            SpriteRenderer sprite = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.CreateSprite("NewTraumatized", "Items", 14));
            sprite.gameObject.layer = LayerStorage.billboardLayer;
            sprite.transform.SetParent(traumatized.transform);
            traumatized.entity = traumatized.gameObject.CreateEntity(2, 2);
            sprite.transform.localPosition = new Vector3(0, -1.6f, 0);
        }
    }
}
        */
