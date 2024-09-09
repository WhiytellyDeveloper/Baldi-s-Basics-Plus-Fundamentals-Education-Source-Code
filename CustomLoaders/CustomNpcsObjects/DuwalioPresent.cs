using UnityEngine;
using bbpfer.CustomContent.CustomNPCs;

namespace bbpfer.CustomLoaders.CustomNpcsObjects
{
    public class DuwalioPresent : MonoBehaviour, IClickable<int>
    {
        public void Initilalize(Vector3 direction, EnvironmentController ec, Vector3 pos) {
            dir = direction;
            entity.Initialize(ec, pos);
            entity.SetGrounded(false);

            entity.OnEntityMoveInitialCollision += (hit) => {
                Destroy(base.gameObject);
            };
        }

        public void Clicked(int player) {
            base.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (entity != null) { 
                entity.UpdateInternalMovement(dir * 70 * ec.EnvironmentTimeScale * ec.NpcTimeScale); 
            }
        }


        public void ClickableSighted(int player) { }
        public void ClickableUnsighted(int player) { }

        public bool ClickableHidden() => false;
        public bool ClickableRequiresNormalHeight() => true;

        public Entity entity;
        public Duwalio dwl;
        private Vector3 dir;
        protected EnvironmentController ec;
    }
}
