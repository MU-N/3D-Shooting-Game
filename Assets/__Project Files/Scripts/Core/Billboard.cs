using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nasser.io
{
    public class Billboard : MonoBehaviour
    {
        Camera cam;
        void Update()
        {
            if (cam == null)
                cam = Camera.main;

            transform.LookAt(cam.transform);
            transform.Rotate(Vector3.up * 180);
        }
    }
}
