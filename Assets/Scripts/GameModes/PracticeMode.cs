using UnityEngine;

namespace GameMode {
    [CreateAssetMenu(fileName = "PracticeMode", menuName = "Gun Brawl/Game Modes/Practice Mode", order = 0)]
    public class PracticeMode : GameMode
    {
        public override void StartMatch()
        {
            
        }

        public override bool CheckMatch()
        {
            return false;
        }
    }
}