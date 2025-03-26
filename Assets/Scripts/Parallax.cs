using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPosX, startPosY;
    private Vector3 lastCamPosition;
    public Transform cam;
    public float parallaxEffectX = 0.5f; // Скорость по X
    public float parallaxEffectY = 0.1f; // Скорость по Y

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        lastCamPosition = cam.position;
    }

    void Update()
    {
        Vector3 deltaMovement = cam.position - lastCamPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectX, deltaMovement.y * parallaxEffectY, 0);
        lastCamPosition = cam.position;
    }
}