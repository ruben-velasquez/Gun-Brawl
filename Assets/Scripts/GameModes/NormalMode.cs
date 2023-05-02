using UnityEngine;

namespace GameMode {
    [CreateAssetMenu(fileName = "NormalMode", menuName = "Gun Brawl/Game Modes/Normal Mode", order = 0)]
    public class NormalMode : GameMode
    {
        public override void StartMatch()
        {
            
        }

        public override bool CheckMatch()
        {
            if (GameManager.Instance.playersState.alivePlayers.Count == 1) {
                return true;
            }
            return false;
        }
    }
}