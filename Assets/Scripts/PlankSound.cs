using UnityEngine;

public class PlankSound : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float stepDistance = 0.5f;
    [SerializeField] private float minMoveThreshold = 0.02f;
    [SerializeField] private float minTimeBetweenSteps = 0.3f;

    private AudioSource audioSource;
    private bool playerOnPlank = false;

    private Vector3 lastPosition;
    private float distanceAccumulated = 0f;
    private float stepCooldown = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!playerOnPlank || playerTransform == null || audioSource == null)
            return;

        stepCooldown += Time.deltaTime;

        Vector3 currentPosition = playerTransform.position;
        float moved = Vector3.Distance(currentPosition, lastPosition);

        // Ignore micro mouvements (respiration VR)
        if (moved > minMoveThreshold)
        {
            distanceAccumulated += moved;
        }

        if (distanceAccumulated >= stepDistance && stepCooldown >= minTimeBetweenSteps)
        {
            audioSource.PlayOneShot(audioSource.clip);

            distanceAccumulated = 0f;
            stepCooldown = 0f;
        }

        lastPosition = currentPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlank = true;
            lastPosition = playerTransform.position;
            distanceAccumulated = 0f;
            stepCooldown = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlank = false;
            distanceAccumulated = 0f;
        }
    }
}