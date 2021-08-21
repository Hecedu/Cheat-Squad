using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class CharacterController2D : MonoBehaviour
{
	public ParticleSystem dust;
	private Rigidbody2D rigidBody2D;
	//Feel variables
	[SerializeField] private float jumpForce = 400f;	
	[SerializeField] private float moveSpeed = 10f;
	private Vector2 knockbackForce = new Vector2(0,0);
	[Range(0, 1)] [SerializeField] private float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool airControlEnabled = true;
	//ground/ceiling check system
	[SerializeField] private Transform ceilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Transform groundCheck;
	const float groundedRadius = 0.1f; 
	const float ceilingRadius = 0.2f; 
	public LayerMask solidsLayerMask;
	[SerializeField] private Collider2D crouchDisableCollider;				// A collider that will be disabled when crouching
	[SerializeField] private bool isGrounded;            // Whether or not the player is grounded.
	//Reference variables
	private bool isFacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;

	//knockback system
	[SerializeField] private bool isKnockbackPlaying = false;
	private bool isKnockbackDirectionRight = true;
	private List<string> otherPlayerLayers;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;



	private void Awake()
	{
		rigidBody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}
	private void Start() {
		otherPlayerLayers = new List<string>();
		foreach (int playerNumber in GameController.instance.GetOtherPlayerNumbers(this.GetComponent<PlayerStats>().playerNumber)){
			otherPlayerLayers.Add($"Player{playerNumber}");
		}
	}

	private void FixedUpdate()
	{
		bool wasGrounded = isGrounded;
		isGrounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, solidsLayerMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}
	public void Move(float inputDirectionX, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(ceilingCheck.position, ceilingRadius, solidsLayerMask);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject)
				{
					crouch = true;
				}
			}
		}
		if (isKnockbackPlaying) {
			if (isKnockbackDirectionRight) ApplyForceHorizontal(1f,knockbackForce.x);
			else	ApplyForceHorizontal(-1f,knockbackForce.x);
		}


		//only control the player if grounded or airControl is turned on
		if (isGrounded || airControlEnabled)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				inputDirectionX *= crouchSpeed;

				// Disable one of the colliders when crouching
				if (crouchDisableCollider != null)
					crouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (crouchDisableCollider != null)
					crouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}
			if (!isKnockbackPlaying) {
				ApplyForceHorizontal(inputDirectionX,moveSpeed);
			}
			if (inputDirectionX > 0 && !isFacingRight)
			{
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (inputDirectionX < 0 && isFacingRight)
			{
				Flip();
			}
		}
	// If the player should jump...
		if (isGrounded && jump)
		{
			ApplyForceUp(jumpForce);
		}
	}
	
	public void StartKnockback (Vector2 newKnockbackForce, bool applyRight) {
		knockbackForce = newKnockbackForce;
		isKnockbackDirectionRight = applyRight;
		if (isGrounded) {
			ApplyForceUp(knockbackForce.y);
			isKnockbackPlaying = true;
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		isFacingRight = !isFacingRight;

		transform.Rotate(0f,180f,0f);
	}

	private void ApplyForceUp(float jumpForce){
		isGrounded = false;
		rigidBody2D.AddForce(new Vector2(0f, jumpForce));
		CreateDust();
	}
	private void ApplyForceHorizontal(float moveDirection, float moveSpeed) {
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(moveDirection * moveSpeed, rigidBody2D.velocity.y);
		// And then smoothing it out and applying it to the character
		rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);	
	}

	private void CreateDust(){
		dust.Play();
	}
	private void OnCollisionEnter2D(Collision2D other) {
		isKnockbackPlaying = false;
	}
}