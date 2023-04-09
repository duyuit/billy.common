using UnityEditor;
using UnityEngine;

public static class EditorFileUtils
{
    [MenuItem("FileUtils/Open persistentDataPath folder")]
    private static void OpenPersistentDataPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }

    [MenuItem("FileUtils/Clear all PlayerPrefs")]
    private static void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}