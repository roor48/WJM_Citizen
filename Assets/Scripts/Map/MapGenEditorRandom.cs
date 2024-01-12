using UnityEngine;
using UnityEditor;

namespace A
{
    [CustomEditor(typeof(MapGenerator))]
    public class MapGenEditorRandom : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MapGenerator myGenerator = (MapGenerator)target;
            if(GUILayout.Button("맵 생성. GenMap"))
            {
                myGenerator.BuildGenerator();
            }

            if (GUILayout.Button("맵 삭제. DelMap"))
            {
                myGenerator.DeleteMap();
            }
        }
    }
}