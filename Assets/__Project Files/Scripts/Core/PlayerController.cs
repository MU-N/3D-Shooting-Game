using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;



namespace Nasser.io
{
    public class PlayerController : MonoBehaviour, IDamagable
    {
        [Header("State")]
        [SerializeField] GameState state;
        [Header("Health Bar UI")]
        [SerializeField] Image healthBarImage;
        [SerializeField] TextMeshProUGUI healthAmountText;

        [Header("Camera")]
        [SerializeField] GameObject cameraHolder;

        [Header("Controlles")]
        [SerializeField] float mouseSensitivity;

        [Space]
        [SerializeField] float sprintSpeed;
        [SerializeField] float walkSpeed;

        [Space]
        [SerializeField] float jumpForce;

        [Space]
        [SerializeField] float smoothTime;

        [Space]
        [SerializeField] Item[] items;

        [Header("Joystick")]
        [SerializeField] Joystick androidJoystick;

        private int itemIndex;
        private int prevoiusItemIndex = -1;
        private int itemsArraySize;

        private float verticalLookRotation;

        private bool isGrounded;


        private Vector3 smoothMoveVelocity;
        private Vector3 moveAmount;
        private Vector3 moveDirection;

        private Rigidbody rb;



        private const float maxHealth = 100;
        private float currentHealth = maxHealth;

        private const string mouseX = "Mouse X";
        private const string mouseY = "Mouse Y";
        private const string horizontal = "Horizontal";
        private const string vertical = "Vertical";
        private const string mouseScrollWheel = "Mouse ScrollWheel";


        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        void Start()
        {

            EquipItem(0);


            itemsArraySize = items.Length;
            healthBarImage.fillAmount = currentHealth / maxHealth;
            healthAmountText.text = $"{currentHealth}";
        }

        void Update()
        {
            if (state.currentState == GameState.State.Play)
            {
                Look();
                Move();
                Jump();
                SwitchWapons();
                Shoot();
                CheckMaxYPostion();
            }

        }



        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.deltaTime);
        }


        private void Look()
        {

            transform.Rotate(Vector3.up * Input.GetAxisRaw(mouseX) * mouseSensitivity);


            verticalLookRotation += Input.GetAxisRaw(mouseY) * mouseSensitivity;

            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60f, 60f);

            cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
        }

        private void Move()
        {
            moveDirection = new Vector3(Input.GetAxisRaw(horizontal), 0f, Input.GetAxisRaw(vertical)).normalized;
#if UNITY_ANDROID
            moveDirection = new Vector3(androidJoystick.Horizontal, 0f, androidJoystick.Vertical).normalized;
#endif

            moveAmount = Vector3.SmoothDamp(moveAmount, moveDirection *
                (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
        }

        private void Jump()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                JumpCall();
            }
        }

        private void JumpCall()
        {
            if (isGrounded)
                rb.AddForce(Vector3.up * jumpForce);
        }
        public void SetGroundedState(bool grounded)
        {
            isGrounded = grounded;
        }


        private void SwitchWapons()
        {
            for (int i = 0; i < itemsArraySize; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    EquipItem(i);
                    break;
                }
            }

            if (Input.GetAxisRaw(mouseScrollWheel) > 0f)
            {
                if (itemIndex >= itemsArraySize - 1)
                    EquipItem(0);
                else
                    EquipItem(itemIndex + 1);
            }

            if (Input.GetAxisRaw(mouseScrollWheel) < 0f)
            {
                if (itemIndex <= 0)
                    EquipItem(itemsArraySize - 1);
                else
                    EquipItem(itemIndex - 1);
            }
        }




        private void EquipItem(int index)
        {
            if (index == prevoiusItemIndex)
                return;
            itemIndex = index;
            items[itemIndex].itemGameObject.SetActive(true);
            if (prevoiusItemIndex != -1)
            {
                items[prevoiusItemIndex].itemGameObject.SetActive(false);
            }
            prevoiusItemIndex = itemIndex;


        }

        public void EquipRifle()
        {
            EquipItem(0);
        }

        public void EquipPistol()
        {
            EquipItem(1);
        }





        private void Shoot()
        {
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            {
                ShootCall();
            }
        }

        private void ShootCall()
        {
            items[itemIndex].Use();
        }
        //#if  UNITY_ANDROID


        public void AndroidShoot()
        {
            ShootCall();
        }

        public void AndroidJump()
        {
            JumpCall();
        }
        //#endif


        // IDamagable intrface
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
            transform.position = Vector3.zero;
        }

        private void CheckMaxYPostion()
        {
            if (transform.position.y < -10f)
                Die();
        }
        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}
