using UnityEngine;

namespace A
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10.0f;
        public float endTime = 2.0f;
        public int damage = 1;

        private void Start()
        {
            Destroy(gameObject, endTime);
        }
        private void Update()
        {
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Enemy");

                Enemy enemy = other.GetComponent<Enemy>();
                enemy.SetHp(damage);
                Destroy(this.gameObject);
            }
        }
    }
}
