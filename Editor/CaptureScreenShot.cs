using System;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEditor;
using UnityEngine;

public class CaptureScreenshot
{
    [MenuItem("Window/Take screenshot #S")]
    static void Screenshot()
    {
        long unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string saveFile = $"Assets/{unixTime}.png";
        ScreenCapture.CaptureScreenshot(saveFile);
    }
}