using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Collections.LowLevel.Unsafe;

public class PlayerController : MonoBehaviour, PlayerControls.IPlayerActions
{
    private PlayerControls controls;
    private Camera playerCam;
    private Rigidbody rb;

    public Transform body;

    private Vector2 mouseDelta = Vector2.zero;
    private float rotX = 0f;
    private float rotY = 0f;

    private Vector2 move = Vector2.zero;
    public float acceleration = 8f;
    public float maxSpeed = 3f;

    private void Start()
    {
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();

        playerCam = Camera.main;

        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Debug.DrawRay(body.position, body.forward, Color.blue);
    }

    private void LateUpdate()
    {
        MouseLook();
    }

    private void FixedUpdate()
    {
        MovePlayerCharacter();
    }

    private void MovePlayerCharacter()
    {
        Vector3 moveDirection = (move.y * transform.forward + move.x * transform.right).normalized;
        rb.AddForce(moveDirection * acceleration);
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    private void MouseLook()
    {
        float sensitivity = 0.1f;

        float minX = -80f;
        float maxX = 80f;

        rotY += mouseDelta.x * sensitivity;
        rotX += -mouseDelta.y * sensitivity;
        rotX = Mathf.Clamp(rotX, minX, maxX);

        transform.localEulerAngles = new Vector3(0f, rotY, 0f);
        playerCam.transform.localEulerAngles = new Vector3(rotX, 0f, 0f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMouse(InputAction.CallbackContext context)
    {
    }
}
