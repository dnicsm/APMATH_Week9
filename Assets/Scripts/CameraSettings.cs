using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField]
    public static float focalLength = 5f;

    public static Vector3 vanishingPoint = Vector3.zero + new Vector3(0, 4, 0);
}
