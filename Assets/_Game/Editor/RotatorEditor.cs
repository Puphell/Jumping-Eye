using UnityEditor;
using UnityEngine;

namespace Sans.Core
{
    [CustomEditor(typeof(Rotator))]
    public class RotatorEditor : Editor
    {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            Rotator rotator = (Rotator)target;

            if (GUILayout.Button("Generate Map")) {
                rotator.GenerateLevel();
            }

            if (GUILayout.Button("Reset")) {
                rotator.Reset();
            }
        }
    }

}