using System.Collections.Generic;
using UnityEngine;

namespace InputController
{
    
    [CreateAssetMenu(menuName = "Gun Brawl/InputController List")]
    public class InputControllerList : ScriptableObject
    {
        public List<IInputController> controllers;
    }
}