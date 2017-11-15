using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelLoader))]
public class LevelLoaderEditor : Editor {

    public override void OnInspectorGUI() {

        LevelLoader component = (LevelLoader)target;
        component.selectedLevelField = EditorGUILayout.ObjectField("Selected Scene", component.selectedLevelField, typeof(SceneAsset), false);


        if (GUILayout.Button("New level")) {
            component.CreateLevel();
        }

        if (GUILayout.Button("Open level")) {
            component.LoadLevel();
        }

        if (GUILayout.Button("Save level")) {
            component.SaveLevel();
        }

        if (GUILayout.Button("Close level")) {
            component.UnloadCurrentLevel();
        }

    }

}
