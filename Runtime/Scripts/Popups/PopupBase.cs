using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Screen = BlitzyUI.Screen;

public class PopupBase : Screen
{
    public Transform content;
    public GameObject background;
    
    public override void OnSetup()
    {
    }

    public override void OnPush(DataPopup dataPopup)
    {
    }

    public override void OnPush(params object[] customParams)
    {
        
    }

    public override void OnPop()
    {
    }

    public override void OnFocus()
    {
    }

    public override void OnFocusLost()
    {
    }
}
