
using UnityEngine;

namespace Nasser.io
{
    public class PlayerGroundCheck : MonoBehaviour
    {
        PlayerController controller;
        string ground = "Ground";
        private void Awake()
        {
            controller = GetComponentInParent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(ground))
            controller.SetGroundedState(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ground))
            controller.SetGroundedState(false);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag(ground))
                return;
            controller.SetGroundedState(true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(ground))
            controller.SetGroundedState(true);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag(ground))
            controller.SetGroundedState(false);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!collision.gameObject.CompareTag(ground))
                return;
            controller.SetGroundedState(true);
        }
    }
}
