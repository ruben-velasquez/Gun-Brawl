using UnityEngine;

namespace Items {
    public abstract class Item : MonoBehaviour
    {
        public void GrabItem(GameObject player) {
            AttachItem(player);
            OnGrab();
        }

        public abstract void AttachItem(GameObject player);
        public abstract bool ShouldUseItem(Fighter.Fighter fighter);

        protected virtual void OnGrab()
        {
            ItemFactory.Instance.ReturnItem(this);
        }
    }
}
