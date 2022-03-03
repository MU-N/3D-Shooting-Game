using DG.Tweening;
using UnityEngine;

namespace Nasser.io
{
    public class Heal : MonoBehaviour
    {
        string player = "Player";
        Vector3 normalScale;
        private void Start()
        {
            normalScale = transform.localScale;
        }
        private void OnTriggerEnter(Collider other)
        {
            
            if(other.CompareTag(player))
            {
                other.GetComponent<PlayerController>().Heal();
                transform.DOScale(new Vector3(0, 0, 0), 1.5f).SetEase(Ease.InBounce).OnComplete(()=>
                {
                    gameObject.SetActive(false);
                    transform.localScale = normalScale;
                });
            }
        }
    }
}
