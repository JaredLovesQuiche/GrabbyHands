using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Collections.LowLevel.Unsafe;

public class PlayerController : MonoBehaviour, PlayerControls.IPlayerActions
{
    private PlayerControls controls;
    private Camera playerCam;

    public Transform body;

    private void Start()
    {
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();

        playerCam = Camera.main;
    }

    private void Update()
    {
        Debug.DrawRay(body.position, body.forward, Color.blue);
    }

    private void FixedUpdate()
    {
        Debug.Log(playerCam.transform.forward);
    }

    private void MoveInDirection()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();
        print(delta);
    }

    public void OnMouse(InputAction.CallbackContext context)
    {
    }
}
