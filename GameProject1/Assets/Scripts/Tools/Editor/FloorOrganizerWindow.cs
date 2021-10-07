using UnityEditor;
using UnityEngine;

namespace Tools.Editor
{
    [CanEditMultipleObjects, CustomEditor(typeof(FloorOrganizer))]
    public class FloorOrganizerWindow : UnityEditor.Editor
    {
        public SerializedObject so;
        public FloorOrganizer reference;

        private void OnEnable()
        {
            reference = target as FloorOrganizer;
            so = serializedObject;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            if (GUILayout.Button("Organize Tiles"))
            {
                reference.Reorganize();
            }
        }
    }
}