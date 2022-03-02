
using UnityEngine;

namespace Nasser.io
{
    [CreateAssetMenu(menuName = "Game State/New Gume State", fileName = "New Gume State")]
    public class GameState : ScriptableObject
    {
        public enum State
        {
            Start,
            Play,
            Puase,
            End
        }
        public State currentState;
        
    }
}
