using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A
{
    public class MapGenerator : MonoBehaviour
    {
        public readonly Color ColorFloor = Color.white;
        public readonly Color ColorWall = Color.red;
        public readonly Color ColorCurveWall = Color.green;
        public readonly Color ColorEdgeWall = Color.blue;
        // �� ���� ��ġ
        public Color ColorResponse = new Color(64, 128, 128, 255);

        public Transform Terrain;
        public Texture2D MapInfo;
        public float tileSize = 4.0f;
        private int mapWidth;
        private int mapHeight;
        public GameObject Floor;
        public GameObject Wall;
        public GameObject CurveWall;
        public GameObject EdgeWall;
        public GameObject Floor_Response;
        public void BuildGenerator()
        {
            GenerateMap();
        }
        private void GenerateMap()
        {
            mapWidth = MapInfo.width;
            mapHeight = MapInfo.height;
            Color[] pixels = MapInfo.GetPixels();

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    Color pixelColor = pixels[i * mapWidth + j];
                    // �ٴ�
                    if (pixelColor == ColorFloor)
                    {
                        GameObject floor = Instantiate(Floor, Terrain);
                        floor.transform.position = new Vector3(j * tileSize, 0, i * tileSize);
                    }
                    // ��
                    else if (pixelColor == ColorWall)
                    {
                        GameObject wall = Instantiate(Wall, Terrain);
                        wall.transform.position = new Vector3(j * tileSize, 0, i * tileSize);
                        wall.transform.Rotate(new Vector3(0, GetWallRot(pixels, i, j), 0), Space.Self);
                    }
                    // Ŀ�� ��
                    else if (pixelColor == ColorCurveWall)
                    {
                        GameObject curveWall = Instantiate(CurveWall, Terrain);
                        curveWall.transform.position = new Vector3(j * tileSize, 0, i * tileSize);
                        curveWall.transform.Rotate(new Vector3(0, GetCurveWallRot(pixels, i, j), 0), Space.Self);
                    }
                    // �𼭸� ��
                    else if (pixelColor == ColorEdgeWall)
                    {
                        GameObject edgeWall = Instantiate(EdgeWall, Terrain);
                        edgeWall.transform.position = new Vector3(j * tileSize, 0, i * tileSize);
                        edgeWall.transform.Rotate(new Vector3(0, GetEdgeRot(pixels, i, j), 0), Space.Self);
                    }
                    // ���� ���� ��ġ
                    
                    else if (pixelColor == ColorResponse)
                    {
                        Debug.Log("Enemy");
                        GameObject floor = Instantiate(Floor_Response, Terrain);
                        floor.transform.position = new Vector3(j * tileSize, 0, i * tileSize);
                    }
                    
                }
            }
        }
        private float GetWallRot(Color[] pixels, int i, int j)
        {
            // ������
            float rot = 0f;
            // �Ʒ�
            if (i - 1 >= 0
                && (pixels[(i - 1) * mapHeight + j] == Color.black
                || pixels[(i - 1) * mapHeight + j] == Color.cyan))
            {
                rot = 90f;
            }
            // ����
            else if (j - 1 >= 0
                && (pixels[i * mapHeight + (j - 1)] == Color.black
                || pixels[i * mapHeight + (j - 1)] == Color.cyan))
            {
                rot = 180f;
            }
            // ��
            else if (i + 1 < mapHeight
                && (pixels[(i + 1) * mapHeight + j] == Color.black
                || pixels[(i + 1) * mapHeight + j] == Color.cyan))
            {
                rot = 270f;
            }

            return rot;
        }
        
        private float GetCurveWallRot(Color[] pixels, int i, int j)
        {
            // ������ ��
            float rot = 0f;
            // ������ �Ʒ�
            if (((pixels[i * mapHeight + j - 1] == Color.black
                || pixels[i * mapHeight + j - 1] == Color.cyan))
                && ((pixels[(i - 1) * mapHeight + j] == Color.black)
                || (pixels[(i - 1) * mapHeight + j] == Color.cyan)))
            {
                rot = 180f;
            }
            // ���� ��
            else if (((pixels[i * mapHeight + j - 1] == Color.black)
                || (pixels[i * mapHeight + j - 1] == Color.cyan))
                && ((pixels[(i + 1) * mapHeight + j] == Color.black)
                || (pixels[(i + 1) * mapHeight + j] == Color.cyan)))
            {
                rot = 270f;
            }
            // ���� �Ʒ�
            else if (((pixels[i * mapHeight + j + 1] == Color.black)
                || (pixels[i * mapHeight + j + 1] == Color.cyan))
                && ((pixels[(i - 1) * mapHeight + j] == Color.black)
                || (pixels[(i - 1) * mapHeight + j] == Color.cyan)))
            {
                rot = 90f;
            }

            return rot;
        }

        private float GetEdgeRot(Color[] pixels, int i, int j)
        {
            //������ ��
            float rot = 0f;
            // ������ �Ʒ�
            if (i - 1 >= 0 && j + 1 < mapWidth
                && (pixels[(i - 1) * mapHeight + (j + 1)] == Color.black
                || pixels[(i - 1) * mapHeight + (j + 1)] == Color.cyan))
            {
                rot = 90f;
            }
            // ���� ��
            else if (i - 1 >= 0 && j - 1 >= 0
                && (pixels[(i - 1) * mapHeight + (j - 1)] == Color.black
                || pixels[(i - 1) * mapHeight + (j - 1)] == Color.cyan))
            {
                rot = 180f;
            }
            // ���� �Ʒ�
            else if (i + 1 < mapHeight && j - 1 >= 0
                && (pixels[(i + 1) * mapHeight + (j - 1)] == Color.black
                || pixels[(i + 1) * mapHeight + (j - 1)] == Color.cyan))
            {
                rot = 270f;
            }

            return rot;
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
