using UnityEngine;
using bbpfer.CustomContent.CustomNPCs;

namespace bbpfer.CustomRooms.CustomObjects
{
    public class TrashBag : MonoBehaviour
    {  
        private void Update()
        {
            if (dust != null)
            {
                if (Vector2.Distance(new Vector2(dust.transform.position.x, dust.transform.position.z), new Vector2(transform.position.x, transform.position.z)) >= 15 && !exploded)
                    Explode();
            }
            
        }

        public void Explode()
        {
            Vector3[] directions = { Vector3.forward, -Vector3.forward, Vector3.right, -Vector3.right };
            for (int i = 0; i < 4; i++)
                Instantiate<ITM_NanaPeel>(nanaPeelPref).Spawn(dust.ec, transform.position, directions[i], 30f);
            transform.position = new Vector3(home.x, 0, home.z);
            GetComponent<BoxCollider>().isTrigger = false;
            exploded = true;
        }
        

        public Vector3 home;
        public DustPan dust;
        public ITM_NanaPeel nanaPeelPref;
        public bool exploded;
    }
}
