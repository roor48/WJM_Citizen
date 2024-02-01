using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A
{
    public class EnemyAttack : MonoBehaviour
    {
        public int damage = 1;
        public bool canAttack = true;
        private void OnTriggerEnter(Collider other)
        {
            if (canAttack && other.TryGetComponent(out Player player))
            {
                canAttack = false;
                player.SetHp(damage);
            }
        }
    }
}
