using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaterHpUI : MonoBehaviour
{
    public Transform target { get; set; }

    public float UiOffset { get; set; }

    void Update()
    {
        transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position + Vector3.up * (0.6f + UiOffset * 0.2f));
    }

    public void HpReset()
    {
        GetComponent<Slider>().value = 1f;
    }

    public void HpUpdate(float Hp)
    {
        GetComponent<Slider>().value = Hp;
    }
}