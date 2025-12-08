using Chapter.Command;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Chapter.Command
{
    public class ArwingController : MonoBehaviour
    {
        CharacterController charController;

        [SerializeField] float moveSpeed = 5f;
        Vector2 moveDirection = Vector2.zero;

        [SerializeField] float maxHealth = 100f;
        float currentHealth;
        float previousHealth;

        [SerializeField] InputActionAsset inputActions;
        InputAction move;

        [SerializeField] Invoker commandInvoker;
        Command damagePlayer;

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
            currentHealth = maxHealth;
            previousHealth = currentHealth;
            Debug.Log(currentHealth);

            move = InputSystem.actions.FindAction("Move");

            damagePlayer = new DamagePlayer(gameObject.GetComponent<ArwingController>());
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

        public void DamagePlayer()
        {
            previousHealth = currentHealth;
            if (currentHealth > 0)
            {
                currentHealth -= 10f;
            }
            else
            {
                currentHealth = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Debug.Log(currentHealth);
        }

        public void ReverseDamage()
        {
            currentHealth = previousHealth;
            Debug.Log(currentHealth);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                commandInvoker.ExecuteCommand(damagePlayer);
            }

            if (other.gameObject.CompareTag("Powerup"))
            {
                commandInvoker.UndoCommand(damagePlayer);
                Destroy(other.gameObject);
            }
        }
    }
}