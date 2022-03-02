using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nasser.io
{
    public class SpwanPoint : MonoBehaviour
    {
        [SerializeField] GameObject visuals;
        private void Awake()
        {
            visuals.SetActive(false);
        }

    }
}
