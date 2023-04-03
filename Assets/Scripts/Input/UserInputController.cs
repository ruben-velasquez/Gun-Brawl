using UnityEngine;

namespace InputController {
    [System.Serializable]
    public class UserInputController : IInputController {
        [SerializeField]
        public InputUser inputUser; // Los controles


        // Modificar Obtener las interacciones

        public override int MoveAxis() {
            int axis = 0;

            if(IsMovingLeft()) {
                axis -= 1;
            }
            if(IsMovingRight()) {
                axis += 1;
            }

            return axis;
        }

        public bool IsMovingLeft() {
            return Input.GetKey(inputUser.left);
        }

        public bool IsMovingRight() {
            return Input.GetKey(inputUser.right);
        }
        
        public override bool IsJumping() {
            return Input.GetKeyDown(inputUser.jump);
        }
        
        public override bool IsShooting() {
            return Input.GetKeyDown(inputUser.shoot);
        }
        
        public override bool IsPunching() {
            return Input.GetKeyDown(inputUser.punch);
        }
        
        public override bool IsInteracting() {
            return Input.GetKeyDown(inputUser.punch);
        }

        // Modificar controles

        public void ChangeLeft(KeyCode key) {
            inputUser.left = key;
        }
        
        public void ChangeRight(KeyCode key) {
            inputUser.right = key;
        }
        
        public void ChangeJump(KeyCode key) {
            inputUser.jump = key;
        }
        
        public void ChangeShoot(KeyCode key) {
            inputUser.shoot = key;
        }
        
        public void ChangePunch(KeyCode key) {
            inputUser.punch = key;
        }
        
        public void ChangeInteract(KeyCode key) {
            inputUser.interact = key;
        }
    }
}