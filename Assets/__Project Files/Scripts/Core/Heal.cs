using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nasser.io
{
    public class Heal : MonoBehaviour
    {
        string player = "Player";
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(player))
            {
                other.GetComponent<PlayerController>().Heal();
            }
        }
    }
}
