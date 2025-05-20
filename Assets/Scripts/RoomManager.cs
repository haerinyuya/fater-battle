using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;

public class RoomManager : MonoBehaviour
{
    public GameObject roomCube;

    public ARPlaneManager arPlaneManager;

    private List<Vector3>planePositionList = new();

    private Bounds boundingBox;


    void Start()
    {
        if (arPlaneManager == null)
        {
            arPlaneManager = FindObjectOfType<ARPlaneManager>();
        }
    }

    void Update()
    {
        if (arPlaneManager != null)
        {
            foreach (var plane in arPlaneManager.trackables) // ARPlaneManagerが管理するすべてのARPlaneを反復処理
            {
                Transform planeTransform = plane.transform;

                Vector3 planePosition = planeTransform.position;

                planePositionList.Add(planePosition);
            }
        }

        CreateBoundingBox(planePositionList);
    }

    void CreateBoundingBox(List<Vector3> points)
    {
        boundingBox = new Bounds(points[0], Vector3.zero);

        foreach (Vector3 point in points)
        {
            boundingBox.Encapsulate(point);
        }

        roomCube.transform.position = boundingBox.center;
        roomCube.transform.localScale = boundingBox.size;
    }
}
