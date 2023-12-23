using UnityEngine;
using UnityEngine.InputSystem;

namespace ADSN
{
    public class SelectTarget : MonoBehaviour
    {
        public static PlayerInputActions inputActions;
        
        float offset = 0.025f;
        Vector3 targetDirection;
        RaycastHit hit;

        [SerializeField, Tooltip("The marker will show where the projectile will hit")]
        Transform hitMarker;

        [SerializeField]
        Rigidbody objectToThrow;

        [SerializeField, Range(1f, 150.0f)]
        float force = 22f;

        [SerializeField]
        Transform startPosition;

        [SerializeField]
        LayerMask layerMask;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public static void InputToggle(bool ON)
        {
            if (ON)
            {
                inputActions.Enable();
            }
            else
            {
                inputActions.Disable();
            }
        }

        void Start()
        {
            if (startPosition == null)
                startPosition = transform;

            hitMarker.gameObject.SetActive(false);

            inputActions.Shoot.OnTriggerPull.started += ctx => LockTarget(ctx);
            inputActions.Shoot.OnTriggerPull.canceled += ctx => ShootObject(ctx);
        }

        void Update()
        {
            hit = GetWorldPosition();

            if (hit.point == Vector3.zero || GameManager.gameIsPaused || GameManager.gameIsOver)
            {
                InputToggle(false);
            }
            else
            {
                InputToggle(true);
            }
        }

        private void FixedUpdate()
        {
            if (!GameManager.targetIsLocked && (!GameManager.gameIsPaused || !GameManager.gameIsOver))
                MoveHitMarker(hit);
        }

        RaycastHit GetWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue);
            return hitInfo;
        }

        private void MoveHitMarker(RaycastHit hit)
        {
            hitMarker.gameObject.SetActive(true);
            hitMarker.position = hit.point + hit.normal * offset;
            hitMarker.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
        }

        void LockTarget(InputAction.CallbackContext ctx)
        {
            GameManager.targetIsLocked = true;
            targetDirection = (hit.point - transform.position).normalized;
        }

        void ShootObject(InputAction.CallbackContext ctx)
        {
            Rigidbody thrownObject = Instantiate(objectToThrow, startPosition.position, Quaternion.identity);
            thrownObject.AddForce(targetDirection * force, ForceMode.Impulse);
            GameManager.targetIsLocked = false;
        }

        private void OnDestroy()
        {
            inputActions.Shoot.OnTriggerPull.started -= ctx => LockTarget(ctx);
            inputActions.Shoot.OnTriggerPull.canceled -= ctx => ShootObject(ctx);
        }
    }
}
