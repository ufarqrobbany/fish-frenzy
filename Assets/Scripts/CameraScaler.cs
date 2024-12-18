using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public float targetWidth = 1920f;  // Resolusi lebar referensi
    public float targetHeight = 1080f; // Resolusi tinggi referensi
    public float pixelsToUnits = 100f; // Satuan pixel ke Unity units

    void Start()
    {
        Camera cam = GetComponent<Camera>();

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = targetWidth / targetHeight;

        if (screenRatio >= targetRatio)
        {
            // Jika layar lebih lebar dari target
            cam.orthographicSize = targetHeight / 2f / pixelsToUnits;
        }
        else
        {
            // Jika layar lebih tinggi dari target
            float differenceInSize = targetRatio / screenRatio;
            cam.orthographicSize = targetHeight / 2f / pixelsToUnits * differenceInSize;
        }
    }
}
