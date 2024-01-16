using UnityEngine;
using UnityEditor;

namespace A
{
    [CustomEditor(typeof(Maze))]
    public class MapRandomGenEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Maze myGenerator = (Maze)target;
            if (GUILayout.Button("¸Ê »ý¼º. GenMap"))
            {
                myGenerator.BuildGenerator();
            }
            if (GUILayout.Button("¸Ê »èÁ¦. DelMap"))
            {
                myGenerator.DeleteMap();
            }
        }
    }
}