using bbpfer.FundamentalManagers;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace bbpfer.CustomContent.CustomEvents
{
    public class SupriseGiftEvent : RandomEvent
    {
        public Pickup pickup;

        public override void Begin()
        {
            base.Begin();
            Singleton<MusicManager>.Instance.PlayMidi(AssetsCreator.LoadMidi("QuaterEvent"), true);
            Singleton<MusicManager>.Instance.SetSpeed(0.5f);

            int room = Random.Range(0, ec.rooms.Count);
            Cell cell = ec.rooms[room].cells[room];
            pickup = ec.CreateItem(cell.room, FundamentalCodingHelper.FindResourceObject<ItemObject>(), new Vector2((cell.position.x * 10) + 5, (cell.position.z * 10) + 5));
            Debug.Log(cell.room.name);
        }

        public override void End()
        {
            base.End();
            Singleton<MusicManager>.Instance.StopMidi();
            Singleton<MusicManager>.Instance.SetSpeed(1f);
            Destroy(pickup.gameObject);
        }
    }
}
