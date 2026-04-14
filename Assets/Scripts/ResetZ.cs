using UnityEngine;

public class ResetY : MonoBehaviour
{
    public float limiteY = 7.5f;
    private Vector3 positionInitiale;

    void Start()
    {
        positionInitiale = transform.position;
    }

    void Update()
    {
        if (transform.position.y < limiteY)
        {
            transform.position = positionInitiale;
        }
    }
}