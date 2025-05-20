using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using static UnityEngine.GraphicsBuffer;

public class FaterManager : MonoBehaviour
{
    public GameObject faterBase;

    public Renderer faterFace;

    public GameObject origin;

    public Canvas canvas;

    public MessageManager messageManager;

    public List<FaterData> Faters { get; private set; } = new();
    public bool IsGame { get; private set; } = false;
    private string newFaterName;

    public AudioSource audioSource;
    public AudioSource audioSource2;

    public void TakeFaceShot(string name)
    {
        newFaterName = name;

        StartCoroutine(FaceShot());
    }

    /// <summary>
    /// 顔をテクスチャ化
    /// </summary>
    /// <returns></returns>
    private IEnumerator FaceShot()
    {
        yield return new WaitForEndOfFrame();

        int captureWidth = 800;
        int captureHeight = 800;
        int captureX = Screen.width / 2 - captureWidth / 2;
        int captureY = Screen.height / 2 - captureHeight / 2 + 150;

        Texture2D faceShot = new(captureWidth, captureHeight, TextureFormat.RGB24, false);
        faceShot.ReadPixels(new Rect(captureX, captureY, captureWidth, captureHeight), 0, 0);
        faceShot.Apply();

        FaterSetUp(faceShot);

        canvas.gameObject.SetActive(true);

        origin.GetComponent<ARFaceManager>().enabled = true;
    }

    /// <summary>
    /// Faterを作成して顔をつける
    /// </summary>
    /// <param name="face"></param>
    private void FaterSetUp(Texture2D face)
    {
        var newFater = Instantiate(faterBase);
        var shader = faterFace.GetComponent<MeshRenderer>().material.shader;
        var newMaterial = new Material(shader);
        var newFaterFace = newFater.transform.Find("root/pelvis/spine_01/spine_02/spine_03/neck_01/head/FaterFace").gameObject.GetComponent<MeshRenderer>();

        newFaterFace.material = newMaterial;

        if (newFaterFace != null)
        {
            newFaterFace.material.mainTexture = face;
        }

        var newFaterData = newFater.GetComponent<FaterData>();

        newFaterData.faterName = newFaterName;

        newFaterData.nameSet();

        Faters.Add(newFaterData);
    }

    public void GameStart()
    {
        foreach(var fater in Faters)
        {
            fater.GameStart();
        }
        IsGame = true;
    }

    /// <summary>
    /// ダメージを与える
    /// </summary>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    public void DamageManage(int target, float damage)
    {
        Faters[target].Receive(damage);
    }

    /// <summary>
    /// 死んだことを全員に反映する
    /// </summary>
    /// <param name="fater"></param>
    public void DieManage(FaterData diedFater)
    {
        foreach (var fater in Faters)
        {
            fater.EnemyFaterData.Remove(diedFater.transform);
        }
    }

    /// <summary>
    /// メッセージの情報を送る
    /// </summary>
    public void Message(string name1, string name2, int attack)
    {
        string message = "";

        switch (attack)
        {
            case 0:
                message = name1 + "は" + name2 + "に変顔をした!!";
                break;
            case 1:
                message = name1 + "が" + name2 + "に剣で斬りかかった!!";
                break;
            case 2:
                message = name1 + "の回転斬りが" + name2 + "に炸裂!!";
                break;
            case 3:
                message = name1 + "が" + name2 + "を思いっきり踏み潰す!!";
                break;

            default:
                break;
        }

        messageManager.MessageGenerate(message);
    }

    //private float timeElapsed = 0f;
    //private float timeOut = 5f;

    void Update()
    {
        //if (IsGame)
        //{
        //    if (Faters.Count <= 1)
        //    {
        //        timeElapsed += Time.deltaTime;

        //        if (timeElapsed >= timeOut)
        //        {
        //            audioSource.gameObject.SetActive(false);
        //            audioSource2.gameObject.SetActive(true);
        //        }
        //    }
        //}
    }
}