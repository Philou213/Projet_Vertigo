using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class MovementDetection : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera xrCamera;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip crackSFX;

    [Header("Step Settings")]
    [Tooltip("Distance (meters) required to register one step")]
    [SerializeField] private float stepDistance = 0.2f;

    [Header("Ground Check")]
    [SerializeField] private float feetDistance = 2f;
    [SerializeField] private LayerMask plankLayer;

    private Vector3 lastPosition;
    private float accumulatedMovement = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPosition = xrCamera.transform.position;

        XRInputSubsystem subsystem =
        XRGeneralSettings.Instance.Manager.activeLoader
        .GetLoadedSubsystem<XRInputSubsystem>();

        if (subsystem != null)
        {
            subsystem.TryRecenter();
        }

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

        Vector3 delta = currentPos - lastPosition;
        delta.y = 0;
        float distance = delta.magnitude;

        accumulatedMovement += distance;
        lastPosition = currentPos;

        if (accumulatedMovement > stepDistance) // small threshold to ignore noise
        {
            accumulatedMovement = 0f;
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
