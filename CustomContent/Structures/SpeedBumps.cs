using UnityEngine;

namespace bbpfer.CustomContent.Structures
{
    public class SpeedBumps : MonoBehaviour, IEntityTrigger
    {
        public void EntityTriggerEnter(Collider other) {
        }

        public void EntityTriggerStay(Collider other) {
            Debug.Log(other.name);
            if (other.GetComponent<PlayerManager>() != null)
            {
                PlayerManager player = other.GetComponent<PlayerManager>();
                if (player.ruleBreak == "Running")
                {
                    collider.isTrigger = false;
                    player.plm.Entity.AddForce(new Force(-player.transform.forward, 100f, 75f));
                }
            }
        }

        public void EntityTriggerExit(Collider other) {
        }

        public BoxCollider collider;
    }
}
