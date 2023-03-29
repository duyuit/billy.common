using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneUtils
{
    [MenuItem("SceneUtils/Scene 1")]
    public static void SplashScene()
    {
        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
    }

    [MenuItem("SceneUtils/Scene 2")]
    public static void MenuScene()
    {
        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[1].path);
    }

    [MenuItem("SceneUtils/Scene 3")]

    public static void GameScene()
    {
        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[2].path);

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
 
    async static void RunGameAsync ()
    {
        await Task.Delay(1000);
        RunGame();
    }

    private static void RunGame()
    {
        openSceneWithSaveConfirm(EditorBuildSettings.scenes[0].path);
        EditorApplication.isPlaying = true;
    }
    
    public static void openSceneWithSaveConfirm(string scenePath)
    {
        // Refresh first to cause compilation and include new assets
        AssetDatabase.Refresh();
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene(scenePath);
    }
}