using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A
{
    public class Player : MonoBehaviour
    {
        CharacterController characterController;
        public float speed = 2f;
        Animator animator;
        bool isWalk = false;

        private void Start()
        {
            characterController = this.GetComponent<CharacterController>();
            animator = this.GetComponent<Animator>();
        }

        private void Update()
        {
            Walk();
            Attack();
        }

        readonly int isWalkHash = Animator.StringToHash("isWalk");
        readonly int isAttackHash = Animator.StringToHash("isAttack");
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

            animator.SetBool(isWalkHash, isWalk);
        }
        private void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger(isAttackHash);
            }
        }
    }
}
