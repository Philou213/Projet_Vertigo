using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRSocketInteractor))]
public class Plate : MonoBehaviour
{
    private XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    void OnEnable()
    {
        socket.selectEntered.AddListener(OnItemAttached);
        socket.selectExited.AddListener(OnItemDetached);
    }

    void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnItemAttached);
        socket.selectExited.RemoveListener(OnItemDetached);
    }

    private void OnItemAttached(SelectEnterEventArgs args)
    {
        var obj = args.interactableObject.transform;

        Debug.Log("Attached: " + obj.name);

        // Example: snap perfectly (optional)
        obj.SetPositionAndRotation(socket.attachTransform.position, socket.attachTransform.rotation);

        // Example: disable physics
        var rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    private void OnItemDetached(SelectExitEventArgs args)
    {
        var obj = args.interactableObject.transform;

        Debug.Log("Detached: " + obj.name);

        // Re-enable physics
        var rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}
