using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nasser.io
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] int minEnemyAmount;
        [SerializeField] int maxEnemyAmount;
        [SerializeField] int minhealAmount;
        [SerializeField] int maxhealAmount;


        private int enemyRand;
        private int HealsRand;
        int count = 0;

        GameObject enemy;
        GameObject heal;

        private const string enemyTag = "Enemy";
        private const string healTag = "Heal";
        void Start()
        {
            enemyRand = Random.Range(minEnemyAmount, maxEnemyAmount);
            CallEnmeyAmount(enemyRand);
            StartCoroutine(AttackTime(500));
        }


        private void CallEnmeyAmount(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                enemy = ObjectPooler.SharedInstance.GetPooledObject(enemyTag);
                enemy.SetActive(true);
                enemy.transform.position = SpwanManager.instance.GetSpwanEnemyPoint().position;
                
            }
        }
        
        private void CallHealAmount(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                heal = ObjectPooler.SharedInstance.GetPooledObject(healTag);
                heal.SetActive(true);
                heal.transform.position = SpwanManager.instance.GetSpwanHealPoint().position;
            }
        }

        IEnumerator AttackTime(int seconds)
        {

            count = 0;
            while (count < seconds)
            {
                yield return new WaitForSeconds(1);
                count++;
                seconds--;

                if (count % 30 == 0)
                {
                    enemyRand = Random.Range(minEnemyAmount, maxEnemyAmount);

                    CallEnmeyAmount(enemyRand);
                }
                if (count % 60 == 0)
                {
                    HealsRand = Random.Range(minhealAmount, maxhealAmount);
                    CallHealAmount(HealsRand);
                }
            }
            
            
        }
    }
}
