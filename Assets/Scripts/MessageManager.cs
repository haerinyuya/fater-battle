using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public MessageUI messageUi;
    public RectTransform canvasRect;

    private List<MessageUI> messageList = new();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 新規メッセージを生成
    /// </summary>
    public void MessageGenerate(string message)
    {
        var newMessage = Instantiate(messageUi, canvasRect);

        newMessage.transform.position = new Vector3(-633, -1370, 0);
        
        newMessage.Generate(message);

        if (messageList.Count > 0)
        {
            MessageMoveUp();
        }
        messageList.Add(newMessage);
    }

    /// <summary>
    /// メッセージを上に移動
    /// </summary>
    private void MessageMoveUp()
    {
        StartCoroutine(MoveUp());

    }

    private IEnumerator MoveUp()
    {
        foreach (var message in messageList)
        {
            message.MoveUp();
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }
}
