using UnityEngine;

public class MovementDetection : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip crackSFX;
    public Camera xrCamera;
    public float StepSensitivity = 0.01f;
    public float feetDistance = 2f;
    public LayerMask plankLayer;

    private Vector3 lastPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPos = xrCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        IsPlayerWalkingOnPlank();
    }

    void IsPlayerWalkingOnPlank()
    {
        if (IsPlayerStepping() && IsOnPlank())
        {
            Debug.Log("Crack");
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(crackSFX);
        }
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

    bool IsOnPlank()
    {
        Vector3 origin = xrCamera.transform.position;

        // cast straight down from player
        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, feetDistance, plankLayer))
        {
            return hit.collider.CompareTag("Plank");
        }

        return false;
    }
}
