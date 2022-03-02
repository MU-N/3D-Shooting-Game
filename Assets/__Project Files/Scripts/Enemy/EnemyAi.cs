
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Nasser.io
{
    public class EnemyAi : MonoBehaviour, IDamagable
    {
        [Header("Game State")]
        [SerializeField] GameState state;
        [SerializeField] Transform childTarget;
        [SerializeField] Transform fireLoction;

        [SerializeField] ItemInfo itemInfo;

        [Space]
        [SerializeField] float attackRange = 15f;
        [SerializeField] int attackDelay = 5;
        [Space]
        private bool isTargetInAttackRange;

        [Header("Health Bar UI")]
        [SerializeField] Image healthBarImage;
        [SerializeField] TextMeshProUGUI healthAmountText;

        private Transform target;
        private NavMeshAgent agent;
        private Animator anim;

        [SerializeField] float maxHealth = 100;
        private float currentHealth;

        const string player = "Player";
        Ray ray;
        RaycastHit hit;
        bool isShoot = false;

        private int animWalk = Animator.StringToHash("isWalk");
        private int animShoot = Animator.StringToHash("isShoot");
        private int animDead = Animator.StringToHash("isDead");
        
            private const string dieSound = "Zoom";
        void Start()
        {
            target = GameObject.FindWithTag(player).transform;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
            currentHealth = maxHealth;


        }

        void Update()
        {
            childTarget.position = target.GetChild(0).position;
            if (state.currentState == GameState.State.Play)
            {
                isTargetInAttackRange = Vector3.Distance(transform.position, target.position) <= attackRange;
                if (!isTargetInAttackRange)
                    ChaseTarget();
                else
                    ShootTarget();
            }

        }

        private void ChaseTarget()
        {
            agent.SetDestination(target.position);
            transform.LookAt(target.GetChild(0));
            anim.SetBool(animWalk, true);
        }

        private void ShootTarget()
        {
            agent.SetDestination(transform.position);
            anim.SetBool(animShoot, true);
            transform.LookAt(target.GetChild(0));

            if (!isShoot)
            {
                isShoot = true;
                ray.direction = fireLoction.transform.forward;
                ray.origin = fireLoction.transform.position;

                if (Physics.Raycast(ray, out hit))
                {
                    hit.collider.gameObject.GetComponent<IDamagable>()?.TakeDamage(((GunInfo)itemInfo).damage);

                }
                StartCoroutine(AttackTime(attackDelay));
            }


        }

        public void TakeDamage(float damageAmount)
        {
            currentHealth -= damageAmount;
            healthBarImage.fillAmount = currentHealth / maxHealth;
            healthAmountText.text = $"{currentHealth}";

            if (currentHealth <= 0)
                Die();
        }

        private void Die()
        {
            anim.SetBool(animDead, true);
            StartCoroutine(Countdown());
            AudioManager.instance.Play(dieSound);

        }

        private void RestartEnemy()
        {
            currentHealth = maxHealth;
            anim.SetBool(animDead, false);
            anim.SetBool(animWalk, false);
            anim.SetBool(animShoot, false);
        }
        IEnumerator Countdown()
        {
            yield return new WaitForSeconds(2.5f);
            gameObject.SetActive(false);
            RestartEnemy();
        }

        IEnumerator AttackTime(int seconds)
        {
            int count = seconds;

            while (count > 0)
            {
                yield return new WaitForSeconds(1);
                count--;
            }
            if (count <= 0)
                isShoot = false;

        }
    }
}
