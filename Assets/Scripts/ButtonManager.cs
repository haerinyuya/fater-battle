using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static UnityEngine.Rendering.DebugUI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public GameObject origin;

    public GameObject roomCube;

    public ARCameraManager arCameraManager;

    public Canvas canvas;

    public FaterManager faterManager;

    public Material[] materials;

    public GameObject inputField;
    private string resultText;

    private bool creatable;

    void Update()
    {
        creatable = origin.GetComponent<ARFaceManager>().trackables.count > 0;
    }

    /// <summary>
    /// 部屋のキューブの大きさを確定
    /// </summary>
    public void RoomDecision()
    {
        origin.GetComponent<RoomManager>().roomCube.GetComponent<MeshRenderer>().material = materials[1];

        origin.GetComponent<ARPlaneManager>().enabled = false;
        origin.GetComponent<RoomManager>().enabled = false;

        canvas.transform.GetChild(0).gameObject.SetActive(false);

        faterManager.GameStart();
    }

    /// <summary>
    /// 内カメラに切り替え
    /// </summary>
    //public void CameraSwitchingUser()
    //{
    //    canvas.transform.GetChild(1).gameObject.SetActive(false);
    //    canvas.transform.GetChild(2).gameObject.SetActive(true);
    //    canvas.transform.GetChild(3).gameObject.SetActive(true);

    //    origin.GetComponent<ARFaceManager>().enabled = true;

    //    arCameraManager.requestedFacingDirection = CameraFacingDirection.User;
    //}

    /// <summary>
    /// Fater作り終了
    /// </summary>
    public void CameraSwitchingWorld()
    {
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        canvas.transform.GetChild(3).gameObject.SetActive(false);
        inputField.SetActive(false);
        canvas.transform.GetChild(0).gameObject.SetActive(true);

        origin.GetComponent<ARFaceManager>().enabled = false;

        foreach (var face in origin.GetComponent<ARFaceManager>().trackables)
        {
            Destroy(face.gameObject);
        }

        roomCube.SetActive(true);
        origin.GetComponent<ARPlaneManager>().enabled = true;
        origin.GetComponent<RoomManager>().enabled = true;

        arCameraManager.requestedFacingDirection = CameraFacingDirection.World;
    }

    /// <summary>
    /// Faterを作る
    /// </summary>
    public void CreateFater()
    {
        if (creatable)
        {
            canvas.gameObject.SetActive(false);

            origin.GetComponent<ARFaceManager>().enabled = false;

            faterManager.TakeFaceShot(resultText);

            InitInputField();
        }
    }

    /// <summary>
    /// フィールドの初期化
    /// </summary>
    private void InitInputField()
    {
        inputField.GetComponent<TMP_InputField>().text = "";
        resultText = "";
    }

    /// <summary>
    /// 入力
    /// </summary>
    public void ChangeText()
    {
        resultText = inputField.GetComponent<TMP_InputField>().text;
    }

    /// <summary>
    /// 入力終わり
    /// </summary>
    public void FinishEditText()
    {
        
    }
}
