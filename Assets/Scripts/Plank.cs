using Unity.XR.CoreUtils;
using UnityEngine;

public class Plank : MonoBehaviour
{
    public Vector3 targetSize = new Vector3(1.1f, 0.016f, 0.09f);

    void Start()
    {
        ScalePlank();
    }

    void ScalePlank()
    {
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        var original_size = mesh.bounds.size;

        Debug.Log("Original_plank_size : " + original_size);

        Vector3 new_scale = new Vector3(
            targetSize.x / original_size.x,
            targetSize.y / original_size.y,
            targetSize.z / original_size.z
        );

        transform.localScale = new_scale;

        Debug.Log("Final_size : " + new_scale);

        MovePlank(original_size, new_scale);
    }

    void MovePlank(Vector3 originalSize, Vector3 NewSize)
    {
        float newLength = originalSize.z * NewSize.z; // actual final size in meters
        float offset = (newLength - originalSize.z) * 0.5f;

        transform.position += transform.forward * offset;
    }


}