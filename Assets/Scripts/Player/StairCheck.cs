using UnityEngine;

namespace Fighter
{
    
    public class StairCheck : FighterMovement
    {
        [Space]
        [Header("Stair Check")]
        public bool hasNearStair = false;
        public float stairPosition = 0;
        private float stairCheckLength = 0.75f;
        [SerializeField]
        private LayerMask stairMask;

        public override void Update() {
            base.Update();
            
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, stairCheckLength, stairMask);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, stairCheckLength, stairMask);

            if(hitLeft.collider != null || hitRight.collider != null) {
                hasNearStair = true;
                stairPosition = hitLeft.collider ? hitLeft.collider.transform.position.x : hitRight.collider.transform.position.x;
            } else {
                hasNearStair = false;
            }
        }
    }
}