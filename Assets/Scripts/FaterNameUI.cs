using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaterNameUI : MonoBehaviour
{
    public Transform target { get; set; }

    public float UiOffset { get; set; }

    void Update()
    {
        transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position + Vector3.up * (0.7f + UiOffset * 0.2f));
    }

    public void ShowName(string name)
    {
        GetComponent<TextMeshProUGUI>().text = name;
    }
}
