using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A
{
    public class Player : MonoBehaviour
    {
        private CharacterController characterController;
        private Animator animator;

        public int hp = 2;
        public float speed = 2f;
        public bool isAttackCheck = false;
        public bool isWalk = false;

        public GameObject prefabBullet;
        public Transform bulletPoint;

        public float bulletDelay = 1.0f;
        public float bulletTime = 0f;
        private bool isBullet = false;

        private void Start()
        {
            characterController = this.GetComponent<CharacterController>();
            animator = this.GetComponent<Animator>();
        }

        private void Update()
        {
            if (!Manager.Instance.isLive) return;

            Walk();
            Attack();
            Rotation();
        }

        readonly int isWalk_Hash = Animator.StringToHash("isWalk");
        readonly int isAttack_Hash = Animator.StringToHash("isAttack");
        private void Walk()
        {
            isWalk = false;

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            if (horizontal != 0 || vertical != 0)
            {
                characterController.Move(speed * Time.deltaTime * (this.transform.right * horizontal + this.transform.forward * vertical));
                isWalk = true;
            }

            animator.SetBool(isWalk_Hash, isWalk);
        }
        private void Attack()
        {
            if (isBullet)
            {
                bulletTime += Time.deltaTime;
                if (bulletTime >= bulletDelay)
                {
                    isBullet = false;
                    bulletTime = 0;
                }
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isBullet = true;
                animator.SetTrigger(isAttack_Hash);
                Invoke(nameof(SpawnBullet), 0.3f);
            }
        }
        private void SpawnBullet()
        {
            Instantiate(prefabBullet, bulletPoint.position, this.transform.rotation);
        }

        private void Rotation()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out float rayLength))
            {
                Vector3 mousePoint = ray.GetPoint(rayLength);

                this.transform.LookAt(new Vector3(mousePoint.x, this.transform.position.y, mousePoint.z));
            }
        }

        public void SetHp(int damage)
        {
            if (Manager.Instance.isLive)
            {
                hp -= damage;
                if (hp <= 0)
                {
                    hp = 0;
                    animator.SetTrigger("Death");
                    Manager.Instance.isLive = false;
                    Invoke(nameof(GameOver), 1.0f);
                }
            }
        }

        private void GameOver()
        {
            Debug.Log("GameOver");
            Manager.Instance.menu.SetMenu(Menu.MenuStatus.GameOver);
        }
    }
}
