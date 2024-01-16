using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A
{
    [SerializeField]
    public class MapLocation
    {
        public int x;
        public int z;

        public MapLocation(int _x, int _z)
        {
            x = _x;
            z = _z;
        }

        public Vector2 ToVector()
        {
            return new Vector2(x, z);
        }

        public static MapLocation operator +(MapLocation a, MapLocation b)
        => new MapLocation(a.x + b.x, a.z + b.z);

        public bool Equals(MapLocation other)
        {
            return x == other.x && z == other.z;
        }
    }
    public class Maze : MonoBehaviour
    {
        public List<MapLocation> directions = new List<MapLocation>()
        {
            new MapLocation(0,1),
            new MapLocation(1,0),
            new MapLocation(0,-1),
            new MapLocation(-1,0),
        };

        public Transform Terrain;

        public int width = 30;
        public int depth = 30;
        public bool[,] map;
        public int scale = 6;

        public void BuildGenerator()
        {
            InitialiseMap();
            Generate(5, 5);
            DrawMap();
        }

        private void InitialiseMap()
        {
            map = new bool[width,depth];
            for (int z = 0; z < depth; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x,z] = true;
                }
            }
        }

        private void Generate(int x, int z)
        {
            // 자신이 복도일 때 or 4방위 중 2방위 이상이 복도일 경우 return
            // wktlsdl qhrehdlf Eofmf or 4qkddnl wnd 2qkddnl dltkddl qhrehdlf ruddn return
            if (map[x, z] == false || CountSquareNeighbours(x, z) >= 2)
                return;

            map[x, z] = false;

            directions.Shuffle();

            Generate(x + directions[0].x, z + directions[0].z); // 5,6
            Generate(x + directions[1].x, z + directions[1].z); // 6,5
            Generate(x + directions[2].x, z + directions[2].z); // 5,4
            Generate(x + directions[3].x, z + directions[3].z); // 4,5
        }

        /// <summary>
        /// 4방위 복도를 검색한다. 4qkddnl qhrehfmf rjatorgksek.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public int CountSquareNeighbours(int x, int z)
        {
            int count = 0;
            if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
            if (map[x - 1, z] == false) count++;
            if (map[x + 1, z] == false) count++;
            if (map[x, z - 1] == false) count++;
            if (map[x, z + 1] == false) count++;
            return count;
        }
        private void DrawMap()
        {
            for (int z = 0; z < depth; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (map[x, z] == true)
                    {
                        Vector3 pos = new Vector3(x * scale, 0, z * scale);
                        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        wall.transform.localScale = new Vector3(scale, scale, scale);
                        wall.transform.position = pos;
                        wall.transform.parent = Terrain;
                        wall.name = x + ", " + z;
                    }
                }
            }
        }


        public void DeleteMap()
        {
            DelMap();
        }
        private void DelMap()
        {
            int childCount = Terrain.childCount;
            for (int i = 0; i < childCount; i++)
            {
                DestroyImmediate(Terrain.GetChild(0).gameObject);
            }
        }
    }
}
