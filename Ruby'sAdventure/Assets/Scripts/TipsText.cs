using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "嗨，按WSAD或↑↓←→移动，找到青蛙先生面对他按T接任务哦";
        Invoke(nameof(CloseTips), 5);
    }

    private void CloseTips()
    {
        var text = GetComponent<Text>();
        text.CrossFadeAlpha(0, 1f, true);
        Invoke(nameof(ActiveOff), 1f);
    }

    private void ActiveOff()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
