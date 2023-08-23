using System.Collections;
using System.Collections.Generic;
using BlitzyUI;
using UnityEngine;
using Screen = BlitzyUI.Screen;

public class PopupBase : Screen
{
    public Transform content;
    public GameObject background;

    public virtual void Hide()
    {
        UIManager.Instance.QueuePop(id);
    }

    public override void OnSetup()
    {
    }

    public override void OnPush(DataPopup dataPopup)
    {
        PushFinished();
    }

    public override void OnPush(params object[] customParams)
    {
        PushFinished();
    }

    public override void OnPop()
    {
        PopFinished();
    }

    public override void OnFocus()
    {
    }

    public override void OnFocusLost()
    {
    }
}