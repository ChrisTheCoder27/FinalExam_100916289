using Chapter.Command;
using Chapter.State;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Chapter.Command
{
    public class ArwingController : MonoBehaviour
    {
        CharacterController charController;

        public float maxSpeed = 12f;
        Vector2 moveDirection = Vector2.zero;

        [SerializeField] float maxHealth = 100f;
        float currentHealth;
        float previousHealth;

        [SerializeField] InputActionAsset inputActions;
        InputAction move;

        [SerializeField] Invoker commandInvoker;
        Command damagePlayer;

        IArwingState stableState, damagedState, wreckedState;
        ArwingStateContext arwingStateContext;

        public float CurrentSpeed
        {
            get; set;
        }

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

        void Start()
        {
            arwingStateContext = new ArwingStateContext(this);

            stableState = gameObject.AddComponent<ArwingStableState>();
            damagedState = gameObject.AddComponent<ArwingDamagedState>();
            wreckedState = gameObject.AddComponent<ArwingWreckedState>();
        }

        public void Stabilized()
        {
            arwingStateContext.Transition(stableState);
        }

        public void Damaged()
        {
            arwingStateContext.Transition(damagedState);
        }

        public void Wrecked()
        {
            arwingStateContext.Transition(wreckedState);
        }

        void Update()
        {
            if (currentHealth >= 75f)
            {
                Stabilized();
            }
            else if (currentHealth >= 25f)
            {
                Damaged();
            }
            else
            {
                Wrecked();
            }
        }

        void FixedUpdate()
        {
            moveDirection = move.ReadValue<Vector2>();
            Vector3 movement = new Vector3(moveDirection.x, moveDirection.y, 0);

            charController.Move(movement * CurrentSpeed * Time.deltaTime);
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