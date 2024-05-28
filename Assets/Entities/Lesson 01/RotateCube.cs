using UnityEngine;

public class RotateCube : MonoBehaviour
{
    [Range(0, 360f)]
    public float speed = 180;
    void Update()
    {
        transform.Rotate(Vector3.up * speed);
    }
}
