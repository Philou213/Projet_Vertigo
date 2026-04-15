using UnityEngine;

public class MovementDetection : MonoBehaviour
{
    public Camera xrCamera;
    public GameObject plank;
    public float StepSensitivity = 0.01f;

    private Vector3 lastPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPos = xrCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckIfIsOnPlank();
        IsPlayerStepping();
    }

    bool IsPlayerStepping()
    {
        Vector3 currentPos = xrCamera.transform.position;

        float distance = Vector3.Distance(currentPos, lastPos);

        lastPos = currentPos;

        if (distance > StepSensitivity) // small threshold to ignore noise
        {
            Debug.Log("Player is moving IRL");
            return true;
        }
        return false;
    }

    void CheckIfIsOnPlank()
    {
        Vector3 localPos = transform.InverseTransformPoint(xrCamera.transform.position);
        Vector3 halfSize = GetComponent<MeshFilter>().sharedMesh.bounds.extents;

        bool onPlank =
            Mathf.Abs(localPos.x) <= halfSize.x &&
            Mathf.Abs(localPos.z) <= halfSize.z &&
            localPos.y >= -0.1f && localPos.y <= halfSize.y + 0.2f;

        Debug.Log("On plank: " + onPlank);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Plank"))
        {
            Debug.Log("On plank");
        }
    }
}
