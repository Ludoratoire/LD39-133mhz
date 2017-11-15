using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class LevelLoader : MonoBehaviour {

    public Object selectedLevelField;

    private Scene _currentlyLoadedLevel;

    public void CreateLevel() {
        // First remove current level if any
        UnloadCurrentLevel();

        _currentlyLoadedLevel = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
    }

    public void LoadLevel() {
        // First remove current level if any
        UnloadCurrentLevel();

        // Then add the content of the selected level to the master scene
        _currentlyLoadedLevel = EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(selectedLevelField), OpenSceneMode.Additive);
    }

    public void SaveLevel() {
        if (_currentlyLoadedLevel.path != null && _currentlyLoadedLevel.isDirty)
            EditorSceneManager.SaveScene(_currentlyLoadedLevel);
    }

    public void UnloadCurrentLevel() {
        if (_currentlyLoadedLevel.path != null) {
            // Ask if we should save the modif on the level to unload
            if (_currentlyLoadedLevel.isDirty && EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new Scene[] { _currentlyLoadedLevel })) {
                EditorSceneManager.SaveScene(_currentlyLoadedLevel);
            }

            // Unload the level
            EditorSceneManager.CloseScene(_currentlyLoadedLevel, true);
        }
    }


}

