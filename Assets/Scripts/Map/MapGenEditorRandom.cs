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
            if(GUILayout.Button("���� �����մϴ�. GenMap"))
            {
                myGenerator.BuildGenerator();
            }

            if (GUILayout.Button("�� ����"))
            {
                myGenerator.DeleteMap();
            }
        }
    }
}