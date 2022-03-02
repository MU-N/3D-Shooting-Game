
using System.Collections;
using UnityEngine;

namespace Nasser.io
{
    public class SingleShootGun : Gun
    {
        [SerializeField] Camera cam;
        [SerializeField] Transform fireLocation;
        Ray ray;
        RaycastHit hit;



        private string bulletImapctPrefab = "BulletImpact";
        private string bulletEffectPrefab = "BulletEffect";
        WaitForSeconds waitDestroy = new WaitForSeconds(2.5f);



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
                BullectEffect(hit.point, hit.normal);
            }
        }

        void BullectEffect(Vector3 hitPosition, Vector3 hitNormal)
        {

            var bulletObj = ObjectPooler.SharedInstance.GetPooledObject(bulletImapctPrefab);
            bulletObj.SetActive(true);
            bulletObj.transform.position = hitPosition + hitNormal * 0.001f;
            bulletObj.transform.rotation = Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation;
            StartCoroutine(WaiTForDestroy(bulletObj));

            var bulletEffect = ObjectPooler.SharedInstance.GetPooledObject(bulletEffectPrefab);
            bulletEffect.transform.position = fireLocation.position;
            bulletEffect.transform.rotation = fireLocation.rotation;
            bulletEffect.SetActive(true);

            StartCoroutine(WaiTForDestroy(bulletEffect));
        }

        IEnumerator WaiTForDestroy(GameObject obj)
        {

            yield return waitDestroy;
            obj.SetActive(false);

        }

    }
}
