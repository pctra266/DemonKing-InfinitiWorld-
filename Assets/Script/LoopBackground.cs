using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;
    private Camera mainCamera;

    private float backgroundWidth;
    private float cameraWidth;

    void Start()
    {
        mainCamera = Camera.main;

        if (backgrounds.Length > 0)
        {
            SpriteRenderer sr = backgrounds[0].GetComponent<SpriteRenderer>();
            backgroundWidth = sr.bounds.size.x;
        }

        float height = 2f * mainCamera.orthographicSize;
        cameraWidth = height * mainCamera.aspect;
        //Debug.Log($"Camera width: {cameraWidth}, Background width: {backgroundWidth}");
    }

    void Update()
    {
        Vector3 camPos = mainCamera.transform.position;

        foreach (Transform bg in backgrounds)
        {
            if (camPos.x - cameraWidth / 2 > bg.position.x + backgroundWidth / 2)
            {
                MoveToRight(bg);
            }
            else if (camPos.x + cameraWidth / 2 < bg.position.x - backgroundWidth / 2)
            {
                MoveToLeft(bg);
            }
        }
    }

    private void MoveToRight(Transform bg)
    {
        Transform rightmost = backgrounds[0];
        foreach (Transform other in backgrounds)
        {
            if (other.position.x > rightmost.position.x)
                rightmost = other;
        }

        bg.position = new Vector3(
            rightmost.position.x + backgroundWidth,
            bg.position.y,
            bg.position.z
        );
    }

    private void MoveToLeft(Transform bg)
    {
        Transform leftmost = backgrounds[0];
        foreach (Transform other in backgrounds)
        {
            if (other.position.x < leftmost.position.x)
                leftmost = other;
        }

        bg.position = new Vector3(
            leftmost.position.x - backgroundWidth,
            bg.position.y,
            bg.position.z
        );
    }
}
