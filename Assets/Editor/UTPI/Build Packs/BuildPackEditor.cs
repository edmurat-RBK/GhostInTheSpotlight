using UnityEngine;
using UnityEditor;

namespace UTPI.BuildPacks
{
    [CustomEditor(typeof(BuildPack))]
    public class BuildPackEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BuildPack buildPack = (BuildPack)target;

            GUILayout.Label("");

            GUILayout.BeginHorizontal();

            if(GUILayout.Button("Enable all scenes"))
            {
                buildPack.SetEnabling(true);
            }

            if (GUILayout.Button("Disable all scenes"))
            {
                buildPack.SetEnabling(false);
            }

            GUILayout.EndHorizontal();

            GUILayout.Label("");

            if (GUILayout.Button("Place this pack in build settings"))
            {
                buildPack.SetToBuildSettings();
            }
        }
    }
}