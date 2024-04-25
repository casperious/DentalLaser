using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this speed as needed
    public Camera cam;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezePositionY; // Fix one end of the cylinder
    }

    void Update()
    {
        // Get the position of the mouse cursor in screen coordinates
        Vector3 mousePos = Input.mousePosition;
        // Convert the screen coordinates to world coordinates
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0)); // 10f is the distance from the camera
        // Move the non-fixed end of the cylinder towards the mouse position
        //rb.MovePosition(Vector3.Lerp(transform.position, mousePos, moveSpeed * Time.deltaTime));
        transform.LookAt(mousePos);
    }
}
