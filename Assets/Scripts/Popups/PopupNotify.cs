using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupNotify : PopupBase
{
    public Text txtNotify;
    
    public override void OnSetup()
    {
        base.OnSetup();
    }

    public override void OnPush(DataPopup dataPopup)
    {
        base.OnPush(dataPopup);
        string contentText = dataPopup.Get<string>("content");
        txtNotify.text = contentText;
    }

    public override void OnPush(params object[] customParams)
    {
        base.OnPush(customParams);
        string contentText = (string)customParams[0];
        txtNotify.text = contentText;
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
