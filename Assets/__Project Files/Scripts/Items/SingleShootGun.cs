
using UnityEngine;

namespace Nasser.io
{
    public class SingleShootGun : Gun
    {
        [SerializeField] Camera cam;
        Ray ray;
        RaycastHit hit;

        public override void Use()
        {
            Shoot();
        }

        public void Shoot()
        {
            ray = cam.ViewportPointToRay(new Vector3(0.5f, 0f, 0.5f));
            ray.origin = cam.transform.position;
            ray.direction = cam.transform.forward;

            if (Physics.Raycast(ray, out hit))
            {
                hit.collider.gameObject.GetComponent<IDamagable>()?.TakeDamage(((GunInfo)itemInfo).damage);
                BullectEffect(hit.point, hit.normal, hit.transform);
            }
        }

        void BullectEffect(Vector3 hitPosition, Vector3 hitNormal , Transform hitTransform)
        {

            var bulletObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
            Destroy(bulletObj, 10f);
            bulletObj.transform.parent = hitTransform;
        }
    }
}
