#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class TextureCruncher : OdinEditorWindow
{
    [ReadOnly] [PropertyOrder(99)] public string madeBy = "Made by Billy";

    [ReadOnly] [LabelText("Message")] [ShowIfGroup("isRunning")] [PropertyOrder(100)]
    public string message;

    public bool overrideAndroid;
    [ShowIfGroup("overrideAndroid")] public TextureImporterFormat androidTextureFormat;

    public int imagePerFrame = 20;

    [Range(0, 100)] public int compressQuality = 50;

    public bool resizeToPowerOf = true;

    [ShowIfGroup("resizeToPowerOf")] [Header("Power of image (2, 4)")]
    public int powerOf = 4;

    private IEnumerator jobRoutine;
    private bool isRunning = false;

    [ShowIfGroup("isRunning")]
    [Button(ButtonSizes.Large)]
    public void Cancel()
    {
        jobRoutine = null;
        isRunning = false;
    }

    [Button(ButtonSizes.Large)]
    public void CrunchFolder(Object folder)
    {
        string assetPath = AssetDatabase.GetAssetPath(folder);
        var allFileName = Directory.GetFiles(assetPath);
        foreach (var fileName in allFileName)
        {
            try
            {
                if (!fileName.Contains(".meta"))
                {
                    var texture = AssetDatabase.LoadAssetAtPath<Texture>(fileName);
                    CrunchSingle(texture);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error when crunch at path {fileName}, error: {e}");
            }
        }
        
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
    }

    [Button(ButtonSizes.Large)]
    public void CrunchMulti(List<Texture> textures)
    {
        if (textures == null || textures.Count == 0)
            return;

        foreach (var texture in textures)
        {
            CrunchSingle(texture, false);
        }

        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
    }

    [Button(ButtonSizes.Large)]
    public void CrunchSingle(Texture texture, bool reloadAfterCrunch = true)
    {
        if (texture == null)
            return;

        string assetPath = AssetDatabase.GetAssetPath(texture);
        var assetImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (assetImporter != null)
        {
            if (resizeToPowerOf)
            {
                ResizeTexture(assetPath);
            }

            if (!assetImporter.crunchedCompression || assetImporter.compressionQuality != compressQuality)
            {
                assetImporter.compressionQuality = compressQuality;
                assetImporter.crunchedCompression = true;
            }

            if (overrideAndroid)
            {
                var androidSetting =
                    assetImporter.GetPlatformTextureSettings("Android", out var maxTextureSize,
                        out var textureFormat);

                assetImporter.SetPlatformTextureSettings("Android", maxTextureSize, androidTextureFormat);
                Debug.LogError("Update android at path " + assetPath);
            }

            if (reloadAfterCrunch)
            {
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            }
        }
    }

    [Button(ButtonSizes.Large)]
    public void CrunchAllTextureInProject()
    {
        if (EditorUtility.DisplayDialog("Crunch all textures?",
            "Are you sure you want to crunch all textures?", "Yes", "Cancel"))
        {
            if (jobRoutine != null)
            {
                jobRoutine = null;
            }
            else
            {
                jobRoutine = CrunchAllTextures();
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EditorApplication.update += HandleCallbackFunction;
    }

    protected void OnDisable()
    {
        EditorApplication.update -= HandleCallbackFunction;
    }


    void HandleCallbackFunction()
    {
        if (jobRoutine != null && !jobRoutine.MoveNext())
            jobRoutine = null;
    }

    [Button(ButtonSizes.Large)]
    public void RemoveAllTextureMeta()
    {
        if (EditorUtility.DisplayDialog("Remove all texture meta?",
            "Are you sure you want to remove all texture meta?", "Yes", "Cancel"))
        {
            var allTextureUUID =
                AssetDatabase.FindAssets("t:texture", null);
            foreach (var uuid in allTextureUUID)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(uuid);
                string metaAssetPath = assetPath + ".meta";
                if (File.Exists(metaAssetPath))
                {
                    File.Delete(metaAssetPath);
                }
            }
        }
    }

    IEnumerator CrunchAllTextures()
    {
        isRunning = true;
        var allTextureUUID =
            AssetDatabase.FindAssets("t:texture", null);

        int totalCount = allTextureUUID.Length;
        int counter = 0;
        int counter2 = 0;

        foreach (var uuid in allTextureUUID)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(uuid);
            var tex = AssetDatabase.LoadAssetAtPath<Texture>(assetPath);
            CrunchSingle(tex);

            counter++;
            if (counter == imagePerFrame)
            {
                counter = 0;
                yield return null;
            }

            counter2++;
            message = $"Crunching {counter2}/{totalCount}";
        }

        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        jobRoutine = null;
        isRunning = false;
    }


    public void ResizeTexture(string filePath)
    {
        try
        {
            Texture2D tex = null;
            if (File.Exists(filePath))
            {
                Texture2D tmpTexture = new Texture2D(1, 1); //, TextureFormat.ARGB32,false);
                byte[] tmpBytes = File.ReadAllBytes(filePath);
                tmpTexture.LoadImage(tmpBytes);

                int oldWidth = tmpTexture.width;
                int oldHeight = tmpTexture.height;

                if (tmpTexture.width % powerOf == 0 && tmpTexture.height % powerOf == 0)
                {
                    Debug.LogError($"Skip resize at path {filePath}, size {oldWidth}x{oldHeight}");
                    return;
                }

                int newWidth = ConvertToSquare(tmpTexture.width);
                int newHeight = ConvertToSquare(tmpTexture.height);
                // tmpTexture.Resize(newWidth,newHeight)

                TextureScale.Bilinear(tmpTexture, newWidth, newHeight);
                byte[] itemBGBytes = tmpTexture.EncodeToPNG();
                File.WriteAllBytes(filePath, itemBGBytes);
                Debug.LogError(
                    $"Resize success at path {filePath}, from size {oldWidth}x{oldHeight} to {tmpTexture.width}x{tmpTexture.height}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Resize failed at path {filePath}, error {e}");
        }
    }

    private int ConvertToSquare(int size)
    {
        while (size % powerOf != 0)
        {
            size++;
        }

        return size;
    }

    [MenuItem("Window/TextureCruncher")]
    private static void OpenWindow()
    {
        GetWindow<TextureCruncher>().Show();
    }
}
#endif