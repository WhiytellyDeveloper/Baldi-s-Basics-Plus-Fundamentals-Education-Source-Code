using UnityEngine;

namespace bbpfer.CustomRooms.CustomObjects
{
    public class ItemPilar : MonoBehaviour, IClickable<int>
    {
        private void Start() =>
            AssingItem(Singleton<CoreGameManager>.Instance.NoneItem);


        public void Clicked(int player)
        {
            if (item != Singleton<CoreGameManager>.Instance.NoneItem)
            {
                Singleton<CoreGameManager>.Instance.GetPlayer(player).itm.AddItem(item);
                AssingItem(Singleton<CoreGameManager>.Instance.NoneItem);
            }
        }


        public void ClickableUnsighted(int player)
        {

        }

        public void ClickableSighted(int player)
        {

        }

        public void AssingItem(ItemObject item)
        {
            this.item = item;
            sprite.sprite = item.itemSpriteLarge;
        }
        

        public bool ClickableRequiresNormalHeight() => true;
        public bool ClickableHidden() => false;
        public ItemObject item;
        public SpriteRenderer sprite;
    }
}
