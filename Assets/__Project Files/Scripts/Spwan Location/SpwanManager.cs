using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nasser.io
{
    public class SpwanManager : MonoBehaviour
    {
        public static SpwanManager instance;

        [SerializeField] SpwanPoint[] spwanEnemyPoints;
        [SerializeField] SpwanPoint[] spwanHealPoints;
        int spwaneEnemyPointsSize ; 
        int spwanHealPointsSize ; 
        private void Awake()
        {
            instance = this;
            spwaneEnemyPointsSize = spwanEnemyPoints.Length;
            spwanHealPointsSize = spwanHealPoints.Length;   
        }
        

        public Transform GetSpwanEnemyPoint()
        {
            return spwanEnemyPoints[Random.Range(0, spwaneEnemyPointsSize)].transform;
        }
        public Transform GetSpwanHealPoint()
        {
            return spwanHealPoints[Random.Range(0, spwanHealPointsSize)].transform;
        }
    }
}
