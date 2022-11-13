using UnityEngine;

public class Rotator : MonoBehaviour
{
    float prevX = 0;
    float prevY = 0;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float dX = Input.mousePosition.x - prevX;
            float dY = Input.mousePosition.y - prevY;

            transform.RotateAround(transform.position, Camera.main.transform.up, -dX);
            transform.RotateAround(transform.position, Camera.main.transform.right, dY);
        }

        prevX = Input.mousePosition.x;
        prevY = Input.mousePosition.y;
    }
}
