using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nasser.io
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] int startEnemyAmount;
        private const string enemyTag = "Enemy";
        void Start()
        {
            CallEnmeyAmount(startEnemyAmount);
            
        }


        private void CallEnmeyAmount(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                ObjectPooler.SharedInstance.GetPooledObject(enemyTag);
            }
        }
    }
}
