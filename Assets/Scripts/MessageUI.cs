using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    private float messagePosition;
    private float message;

    private bool setUpMove = false;

    void Start()
    {

    }

    void Update()
    {
        if (setUpMove)
        {
            SetUp();
        }
    }

    /// <summary>
    /// 新規メッセージ
    /// </summary>
    public void Generate(string message)
    {
        GetComponent<TextMeshProUGUI>().text = message;

        setUpMove = true;
    }

    private void SetUp()
    {
        if (transform.position.x <= -633)
        {
            var pos = transform.position;
            pos.x += 1f;
            transform.position = pos;
        }
        else
        {
            setUpMove = false;
        }
    }

    public void MoveUp()
    {

    }
}
