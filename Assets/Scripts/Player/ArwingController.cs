using UnityEngine;
using UnityEngine.InputSystem;

public class ArwingController : MonoBehaviour
{
    CharacterController charController;

    [SerializeField] float moveSpeed = 5f;
    Vector2 moveDirection = Vector2.zero;

    [SerializeField] InputActionAsset inputActions;
    InputAction move;

    void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    void Awake()
    {
        charController = GetComponent<CharacterController>();

        move = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        moveDirection = move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveDirection.x, moveDirection.y, 0);

        charController.Move(movement * moveSpeed * Time.deltaTime);
    }
}
