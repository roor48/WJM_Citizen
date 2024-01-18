using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A
{
    public class EnemyManager : MonoBehaviour
    {
        public Transform Terrain;
        public GameObject enemy;
        public List<Transform> response;

        private void Start()
        {
            response = new List<Transform>();
            for (int i = 0; i < Terrain.childCount; i++)
            {
                if (Terrain.GetChild(i).name.Contains("Floor_response"))
                {
                    response.Add(Terrain.GetChild(i));
                    Instantiate(enemy, response[^1].position, enemy.transform.rotation);
                }
            }
        }
    }
}
