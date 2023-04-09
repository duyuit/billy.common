using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EditorSceneUtils
{
    [MenuItem("SceneUtils/Scene 1 %q")]
    public static void OpenScene1()
    {
        OpenSceneWithSaveConfirm(EditorBuildSettings.scenes[0].path);
    }

    [MenuItem("SceneUtils/Scene 2 %w")]
    public static void OpenScene2()
    {
        OpenSceneWithSaveConfirm(EditorBuildSettings.scenes[1].path);
    }

    [MenuItem("SceneUtils/Scene 3 %e")]
    public static void OpenScene3()
    {
        OpenSceneWithSaveConfirm(EditorBuildSettings.scenes[2].path);
    }

    [MenuItem("SceneUtils/Run Game %&r")]
    public static void Run()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            RunGameAsync();
            return;
        }

        RunGame();
    }

    static async void RunGameAsync()
    {
        await Task.Delay(1000);
        RunGame();
    }

    private static void RunGame()
    {
        OpenSceneWithSaveConfirm(EditorBuildSettings.scenes[0].path);
        EditorApplication.isPlaying = true;
    }

    private static void OpenSceneWithSaveConfirm(string scenePath)
    {
        // Refresh first to cause compilation and include new assets
        AssetDatabase.Refresh();
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene(scenePath);
    }
}