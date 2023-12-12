using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ADSN
{

    [RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float initialPlayerSpeed = 4f;
        [SerializeField]
        private float maximumPlayerSpeed = 30f;
        [SerializeField]
        private float playerSpeedIncreaseRate = .1f;
        [SerializeField]
        private float jumpHeight = 1f;
        [SerializeField]
        private float initialGravity = -9.81f;
        [SerializeField]
        private float scoreMultiplier = 10f;

        [SerializeField]
        private LayerMask groundLayer;
        [SerializeField]
        private LayerMask turnLayer;
        [SerializeField]
        private LayerMask obstacleLayer;
        
        [SerializeField]
        private UnityEvent<Vector3> turnEvent;
        [SerializeField]
        private UnityEvent<int> gameOverEvent;
        [SerializeField]
        private UnityEvent<int> scoreUpdateEvent;

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private AnimationClip slidingAnimationClip;
        
        [SerializeField]
        private float playerSpeed;
        private float gravity;
        private bool sliding;
        private Vector3 movementDirection = Vector3.forward;
        private Vector3 playerVelocity = Vector3.zero;

        private PlayerInput playerInput;
        private InputAction turnAction;
        private InputAction jumpAction;
        private InputAction slideAction;

        private CharacterController controller;

        private int slidingAnimationId;
        private float score = 0;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            controller = GetComponent<CharacterController>();
            /// referencing the slide animation
            //slidingAnimationId = Animator.StringToHash("Slide");
            
            /// Assigning input actions to variables
            turnAction = playerInput.actions["Turn"];
            //jumpAction = playerInput.actions["Jump"];
            //slideAction = playerInput.actions["Slide"];

        }

        private void OnEnable()
        {
            turnAction.performed += PlayerTurn;
            //slideAction.performed += PlayerSlide;
            //jumpAction.performed += PlayerJump;
        }

        private void OnDisable()
        {
            turnAction.performed -= PlayerTurn;
            //slideAction.performed -= PlayerSlide;
            //jumpAction.performed -= PlayerJump;
        }

        /*private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            /// checking if player hit an obstacle, using binary AND operation
            if (((1 << hit.collider.gameObject.layer) & obstacleLayer) != 0) GameOver();
        }*/

        /*private void PlayerSlide(InputAction.CallbackContext context)
        {
            if (!sliding && IsGrounded())
            {
                StartCoroutine(Slide());
            }
        }*/

        /*private void PlayerJump(InputAction.CallbackContext context)
        {
            /// multiplying required jump height with gravity and a negative number to get a positive value, since gravity is negative
            if (IsGrounded())
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * gravity * -3f);
                controller.Move(playerVelocity * Time.deltaTime);
            }
        }*/

        private void PlayerTurn(InputAction.CallbackContext context)
        {
            Vector3? turnPosition = CheckTurn(context.ReadValue<float>());

            /*if (!turnPosition.HasValue)
            {
                GameOver();
                return;
            }*/

            /// Calculating expected angle / direction to turn
            /// Also used to generate tile in the correct direction after turn
            Vector3 targetDirection = Quaternion.AngleAxis(90 * context.ReadValue<float>(), Vector3.up) * movementDirection;

            /// Calling AddDirection Method form TileSpawner without hard refernce
            turnEvent.Invoke(targetDirection);

            /// Actually turning the player
            Turn(context.ReadValue<float>(), turnPosition.Value);
        }

        private void Start()
        {
            playerSpeed = initialPlayerSpeed;
            gravity = initialGravity;
        }

        private void Update()
        {
            /// Calculate score
            /*score += scoreMultiplier * Time.deltaTime;
            scoreUpdateEvent.Invoke((int)score);*/

            /// Check if player has fallen down / out
            /*if (!IsGrounded(20f))
            {
                GameOver();
                return;
            }*/

            controller.Move(transform.forward * playerSpeed * Time.deltaTime);

            /// bringing player to ground every frame
            if (IsGrounded() && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            playerVelocity.y += gravity * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            /// Increase player speed with time
            if (playerSpeed < maximumPlayerSpeed)
            {
                playerSpeed += playerSpeedIncreaseRate * Time.deltaTime;
                /// Also adjust gravity in proportion to player speed for jump
                gravity = initialGravity - playerSpeed;
                /// Increase animation speed for slide
                /*if (animator.speed < 1f)
                {
                    animator.speed += (1/playerSpeed) * Time.deltaTime;
                }*/
            }
        }

        private bool IsGrounded(float length = .2f)
        {
            /// generating 2 raycasts towards the ground to check if player is grounded
            Vector3 raycastOriginFirst = transform.position;
            raycastOriginFirst.y -= controller.height / 2f;
            raycastOriginFirst.y += .1f;

            Vector3 raycastOriginSecond = raycastOriginFirst;
            raycastOriginFirst -= transform.forward * .2f;
            raycastOriginSecond += transform.forward * .2f;

            //Debug.DrawLine(raycastOriginFirst, Vector3.down, Color.green, 2f);
            //Debug.DrawLine(raycastOriginSecond, Vector3.down, Color.red, 2f);

            if (Physics.Raycast(raycastOriginFirst, Vector3.down, out RaycastHit hit, length, groundLayer) ||
                Physics.Raycast(raycastOriginSecond, Vector3.down, out RaycastHit hit2, length, groundLayer))
            {
                //Debug.Log("true");
                return true;
            }
            //Debug.Log("false");
            return false;
        }

        private Vector3? CheckTurn(float turnValue)
        {
            /// Checking if player hit any colliders
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, .1f, turnLayer);
            if (hitColliders.Length != 0)
            {
                Tile tile = hitColliders[0].transform.parent.GetComponent<Tile>();
                TileType type = tile.type;

                /// Checking if player turned in the correct direction
                if (type == TileType.LEFT && turnValue == -1
                || type == TileType.RIGHT && turnValue == 1
                || type == TileType.SIDEWAYS)
                {
                    return tile.pivot.position;
                }

            }
            return null;
        }

        private void Turn(float turnValue, Vector3 turnPosition)
        {
            Vector3 tempPlayerPosition = new Vector3(turnPosition.x, transform.position.y, turnPosition.z);
            controller.enabled = false;
            transform.position = tempPlayerPosition;
            controller.enabled = true;

            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 90 * turnValue, 0);
            transform.rotation = targetRotation;
            movementDirection = transform.forward.normalized;
        }

        /*private IEnumerator Slide()
        {
            sliding = true;

            /// Shrink the collider / character controller to avoid obstacle 
            Vector3 originalControllerCenter = controller.center;
            Vector3 newControllerCenter = originalControllerCenter;
            controller.height /= 2;
            newControllerCenter.y -= controller.height / 2;
            controller.center = newControllerCenter;
            
            /// Play sliding animation
            animator.Play(slidingAnimationId);
            /// Adjusting length of animation according to speed of player
            yield return new WaitForSeconds(slidingAnimationClip.length / animator.speed);

            /// Back to original size
            controller.height *= 2;
            controller.center = originalControllerCenter;

            sliding = false;
        }*/

        private void GameOver()
        {
            Debug.Log("Game Over");
            gameOverEvent.Invoke((int)score);
            gameObject.SetActive(false);
        }
    }
}