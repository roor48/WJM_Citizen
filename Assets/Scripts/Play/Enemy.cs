using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

namespace A
{
    public class Enemy : MonoBehaviour
    {
        public int damage = 1;
        private int hp = 2;
        private bool isStop = false;
        public bool isAttackCheck = false;
        private Renderer[] renderers;
        private Color originColor;

        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private Transform playerTrans;

        private void Start()
        {
            animator = this.GetComponent<Animator>();
            navMeshAgent = this.GetComponent<NavMeshAgent>();
            playerTrans = FindObjectOfType<Player>().transform;
            navMeshAgent.destination = playerTrans.position;

            renderers = this.GetComponentsInChildren<Renderer>();
            originColor = renderers[0].material.color;
        }

        private readonly int isWalk_Hash = Animator.StringToHash("isWalk");
        private void Update()
        {
            if (isStop) return;

            if (!navMeshAgent.isStopped)
            {
                if (Vector3.Distance(this.transform.position, playerTrans.position)
                    < navMeshAgent.stoppingDistance)
                {
                    navMeshAgent.isStopped = true;
                    StartCoroutine(Attack());
                }
                else
                {
                    navMeshAgent.isStopped = false;
                    navMeshAgent.destination = playerTrans.position;
                }
            }

            animator.SetBool(isWalk_Hash, !navMeshAgent.isStopped);
            this.transform.LookAt(playerTrans.position);
        }

        private readonly int isAttack_Hash = Animator.StringToHash("isAttack");
        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(.5f);
            animator.SetTrigger(isAttack_Hash);
            yield return new WaitForSeconds(.5f);
            //if (Vector3.Distance(this.transform.position, playerTrans.position)
            //    < navMeshAgent.stoppingDistance)
            //{
            //    StartCoroutine(Attack());
            //}
            //else
            //{
            //    navMeshAgent.isStopped = false;
            //}
            navMeshAgent.isStopped = false;
        }

        public void SetHp(int dmg)
        {
            if (isStop) return;
            StartCoroutine(HitColor());

            hp -= dmg;
            if (hp <= 0)
            {
                hp = 0;
                animator.SetTrigger("Death");
                isAttackCheck = false;
                isStop = true;
                navMeshAgent.isStopped = true;
                this.gameObject.GetComponent<Collider>().enabled = false;
            }
        }
        private IEnumerator HitColor()
        {
            foreach(Renderer render in renderers)
            {
                render.material.color = Color.red;
            }
            yield return new WaitForSeconds(0.5f);
            foreach(Renderer render in renderers)
            {
                render.material.color = originColor;
            }
        }
    }
}
