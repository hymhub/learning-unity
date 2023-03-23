using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialog : MonoBehaviour
{
    public GameObject dialogBox;
    public float displayTime = 4.0f;

    private float _timerDisplay;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        _timerDisplay = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerDisplay > 0)
        {
            _timerDisplay -= Time.deltaTime;
            if (_timerDisplay <= 0)
            {
                DialogShow(false);
            }
        }
    }

    public void DialogShow(bool show)
    {
        if (show)
        {
            UIHealthBar.Instance.canUseLaunch = true;
            _timerDisplay = displayTime;
        }

        dialogBox.SetActive(show);
    }
}
