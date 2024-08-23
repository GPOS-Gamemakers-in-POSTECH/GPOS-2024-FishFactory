using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 3;
    public Vector2 offset;

    private float limitMinX, limitMaxX, limitMinY, limitMaxY;
    private float cameraHalfWidth, cameraHalfHeight;

    private void Start()
    {
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;

        SetCameraBounds();  // initialize boundary
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),   // X
            Mathf.Clamp(target.position.y + offset.y, limitMinY + cameraHalfHeight, limitMaxY - cameraHalfHeight), // Y
            -10);                                                                                                  // Z

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetCameraBounds();
    }

    private void SetCameraBounds()
    {
        Tilemap[] tilemaps = FindObjectsOfType<Tilemap>();  // make a list of all tilemaps in the scene
        if (tilemaps.Length > 0)
        {
            Tilemap largestTilemap = tilemaps[0];
            Bounds largestBounds = largestTilemap.localBounds;

            // find the largest tilemap and make it as a boundary of camera
            foreach (Tilemap tilemap in tilemaps)
            {
                tilemap.CompressBounds();
                Bounds bounds = tilemap.localBounds;
                if (bounds.size.x * bounds.size.y > largestBounds.size.x * largestBounds.size.y)
                {
                    largestTilemap = tilemap;
                    largestBounds = bounds;
                }
            }

            limitMinX = largestBounds.min.x;
            limitMaxX = largestBounds.max.x;
            limitMinY = largestBounds.min.y;
            limitMaxY = largestBounds.max.y;
        }
    }
}