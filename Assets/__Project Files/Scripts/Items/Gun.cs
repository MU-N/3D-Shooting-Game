using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nasser.io
{
    public abstract class Gun : Item
    {
        public GameObject bulletImpactPrefab;
        public abstract override void Use();
    }
}
