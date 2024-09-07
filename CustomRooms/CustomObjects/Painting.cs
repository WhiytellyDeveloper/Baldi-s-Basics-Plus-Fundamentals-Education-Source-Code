using UnityEngine;

namespace bbpfer.CustomRooms.CustomObjects
{
    public class Painting : MonoBehaviour
    {
        private void Start()
        {
            bool isUnique;
            do
            {
                sp = sprites[Random.Range(1, sprites.Length)];
                isUnique = true;

                foreach (Painting paint in GameObject.FindObjectsOfType<Painting>())
                {
                    if (paint != this && paint.sp == sp)
                    {
                        isUnique = false;
                        break;
                    }
                }
            }
            while (!isUnique);

            sr.sprite = sp;
        }

        public Sprite sp;
        public SpriteRenderer sr;
        public Sprite[] sprites;
    }
}
