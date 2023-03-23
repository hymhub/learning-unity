using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Image mask;
    
    public static UIHealthBar Instance { get; private set; }

    public bool canUseLaunch = false;

    private float _originalSize;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _originalSize = mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 设置当前UI血条显示
    public void SetValue(float fillPercent)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _originalSize * fillPercent);
    }
}
