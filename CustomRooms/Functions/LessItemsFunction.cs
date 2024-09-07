using System;
using System.Collections.Generic;
using UnityEngine;

namespace bbpfer.CustomRooms.Functions
{
    public class LessItemsFunction : RoomFunction
    {
        public override void Build(LevelBuilder builder, System.Random rng)
        {
            base.Build(builder, rng);

            Transform parentObject = room.transform;
            Transform transform = parentObject.Find("RoomObjects").transform;

            foreach (Pickup pickup in transform.GetComponentsInChildren<Pickup>())
            {
                float vaule = rng.Next(0, 2);
                if (vaule < chance)
                {
                    pickup.gameObject.SetActive(false);
                    pickup.icon.spriteRenderer.enabled = false;
                    room.ec.items.Remove(pickup);
                }
            }
        }

        public float chance;
    }
}
