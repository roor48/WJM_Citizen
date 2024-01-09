using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace A
{
    public class Enemy : MonoBehaviour
    {
        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private Transform playerTrans;

        private void Start()
        {
            animator = this.GetComponent<Animator>();
            navMeshAgent = this.GetComponent<NavMeshAgent>();
            playerTrans = FindObjectOfType<Player>().transform;
            navMeshAgent.destination = playerTrans.position;
        }

        private void Update()
        {
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

            animator.SetBool("isWalk", !navMeshAgent.isStopped);
            this.transform.LookAt(playerTrans.position);
        }

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(.5f);
            animator.SetTrigger("isAttack");
            yield return new WaitForSeconds(.5f);
            if (Vector3.Distance(this.transform.position, playerTrans.position)
                < navMeshAgent.stoppingDistance)
            {
                StartCoroutine(Attack());
            }
            else
            {
                navMeshAgent.isStopped = false;
            }
        }
    }
}
