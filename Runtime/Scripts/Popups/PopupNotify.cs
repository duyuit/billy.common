using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupNotify : PopupBase
{
    public Text txtNotify;
    public Text txtTitle;
    public Button btnLeft;
    public Button btnRight;
    public Text txtLeft;
    public Text txtRight;

    private Action leftCallback;
    private Action rightCallback;

    public override void OnSetup()
    {
        base.OnSetup();
        btnLeft.onClick.AddListener(OnLeftBtnClick);
        btnRight.onClick.AddListener(OnRightBtnClick);
    }

    private void OnLeftBtnClick()
    {
        leftCallback?.Invoke();
        Hide();
    }

    private void OnRightBtnClick()
    {
        rightCallback?.Invoke();
        Hide();
    }

    public override void OnPush(DataPopup dataPopup)
    {
        base.OnPush(dataPopup);

        string contentText = dataPopup.Get<string>("content");
        bool getTitleText = dataPopup.TryGet("title", out string titleTextString);
        bool getLeftText = dataPopup.TryGet("leftText", out string leftTextString);
        bool getRightText = dataPopup.TryGet("rightText", out string rightTextString);
        bool hasLeftBtn = dataPopup.TryGet("leftCallback", out leftCallback);
        bool hasRightBtn = dataPopup.TryGet("rightCallback", out rightCallback);

        txtNotify.text = contentText;
        txtTitle.text = getTitleText ? titleTextString : "Notify";
        txtLeft.text = getLeftText ? leftTextString : "OK";
        txtRight.text = getRightText ? rightTextString : "Cancel";
        btnLeft.gameObject.SetActive(hasLeftBtn);
        btnRight.gameObject.SetActive(hasRightBtn);
    }

    public override void OnPush(params object[] customParams)
    {
        base.OnPush(customParams);
        
        string contentText = (string) customParams[0];
        txtNotify.text = contentText;
        txtTitle.text = "Notify";
        txtRight.text = "OK";
        
        btnLeft.gameObject.SetActive(false);
        btnRight.gameObject.SetActive(true);
    }
}