# lifter.common
## EditorSceneUtils
- Ctrl + Atl + R to quick play
- Ctrl + Q -> open first scene in EditorBuildSettings
- Ctrl + W -> open second scene in EditorBuildSettings
- Ctrl + E -> open third scene in EditorBuildSettings

## UIManager:
- Create folder Popups in Resources folder to hold popup prefabs.
- Drag **Assets/Prefabs/Popups/Canvas_Popups** to Hierarchy.
- To create new popup, drag prefab **Assets/Prefabs/Popups/PopupBase** to hierarchy and unpack prefab (*option)
- Has 2 option to show a popup: (first param "id" is popup prefab name in folder **Resources/Popups**)
    + UIManager.Instance.QueuePush (string id, PushedDelegate callback = null, params object[] customParams)
    + UIManager.Instance.QueuePush (string id, BlitzyUI.Screen.Data data, PushedDelegate callback = null)

## EditorFileUtils
- Find FileUtils in menu bar in Editor.
- Has ClearAllPlayerPref and OpenPersistentData folder