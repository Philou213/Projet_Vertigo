using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FloatingComponent : MonoBehaviour
{
    [Header("Float Settings")]
    public float amplitude = 0.25f;   // how high it moves up/down
    public float frequency = 1f;      // speed of the float

    [Header("Rotation Settings")]
    public Vector3 rotationSpeed = new Vector3(0f, 60f, 0f); // degrees/sec

    private Vector3 startPos;

    private XRGrabInteractable grab;
    private bool isGrabbed;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(_ => isGrabbed = true);
        grab.selectExited.AddListener(OnRelease);
    }

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (isGrabbed) return;

        // Floating (sin wave)
        float offsetY = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = startPos + new Vector3(0f, offsetY, 0f);

        Vector3 euler = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, euler.y, 0f);

        // Rotation
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
    }
}
