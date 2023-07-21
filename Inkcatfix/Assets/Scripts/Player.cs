using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//Bullets
	public GameObject bulletPrefab, splashPrefab, shooter;
	private Transform _firePoint;

	//Basic
	public float longIdleTime = 5f;
	public float speed = 2.5f;
	public float jumpForce = 2.5f;

	//Ground

	public Transform groundCheck;
	public LayerMask groundLayer;
	public float groundCheckRadius;

	// References
	private Rigidbody2D _rigidbody;

	private Health health;
	private Animator _animator;

	// Long Idle
	private float _longIdleTimer;

	// Movement
	private Vector2 _movement;
	private bool _facingRight = true;
	private bool _isGrounded;

	// Attack

	private bool _isAttacking;
	private float _cdShoot = 0f;
	private float _shootDelay = 0.5f;
	private bool _attackRight = true;
	private bool _attackUp;
	private bool _attackHorizontal;

	private float horizontalInput, verticalInput;
	//heal 

	private bool _stunned = false;
	private float _cdHeal = 0f;
	private float _HealDelay = 0.5f;

	//facing
	float dirX;

	Vector3 localScale;



	private Vector2 mousePos;
	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		_firePoint = transform.Find("FirePoint");
		
		
	}

	void Start()
    {
		health = GetComponent<Health>();
       
	   localScale = transform.localScale;
	
    }

    void Update()
    {	
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");
		if (_stunned == false)
		{
			dirX = Input.GetAxis ("Horizontal");
			_movement = new Vector2(horizontalInput, 0f);
			_isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

			// Is Jumping?
			if (Input.GetButtonDown("Jump") && _isGrounded == true && _isAttacking == false) {
				_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			}
		}
	}
	
	void FixedUpdate()
	{		
		if(_stunned == false ){
			float horizontalVelocity = _movement.normalized.x * speed;
			_rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
			if (Input.GetButton("Fire1") && _isGrounded == false && Time.time > _cdShoot && Input.GetButton("Fire2") == false ) 
			{
				if(_rigidbody.velocity.y>0.1){
					_animator.SetTrigger("ShootJumpUp");
				}
				if(_rigidbody.velocity.y<=0){
					_animator.SetTrigger("ShootJumpDown");
				}
			}
			if (Input.GetButton("Fire1") && _isGrounded == true && Time.time > _cdShoot && Input.GetButton("Fire2") == false ) 
			{
				_cdShoot = _shootDelay + Time.time;
				if (_isGrounded == true && horizontalInput == 0 && verticalInput == 0)
				{
					_attackRight =! _attackRight;
					if (_attackRight == true )
					{
						_animator.SetTrigger("ShootRight");
					}
					if (_attackRight == false )
					{
						_animator.SetTrigger("ShootLeft");
					}
				}
				if (_isGrounded == true && (horizontalInput == 1 || horizontalInput == -1) &&  verticalInput == 0 )
				{
					_animator.SetTrigger("ShootWalk");
				}
				if (_isGrounded == true && horizontalInput == 0 && verticalInput == 1)
				{
					_animator.SetTrigger("ShootUp");
				}
				if (_isGrounded == true &&  (horizontalInput == 1 || horizontalInput == -1) && verticalInput == 1)
				{
					_animator.SetTrigger("ShootDiagon");
				
				}
			
			}
			if (_isGrounded == true && Input.GetButton("Fire1") == false && Input.GetButton("Fire2") == true && _stunned == false) 
			{
				
				if (Time.time > _cdHeal)
				{
					_cdHeal = _HealDelay + Time.time;			
					health.Heal();
				}

			}
			if (Input.GetButtonDown("Fire3") == true || Input.GetButtonUp("Fire3") == true)
			{
				health.InkUsed();
			}
		}
	}

	void LateUpdate()
	{
		Flip();
		_animator.SetBool("Isidle", _movement == Vector2.zero);
		_animator.SetBool("IsGrounded", _isGrounded);
		_animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);
		_animator.SetFloat("SpeedShoot", _cdShoot);

		// Animator
		if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) 
		{
			_isAttacking = true;
		} 
		else 
		{
			_isAttacking = false;
		}
		// Long Idle
		if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Isidle")) 
		{
			_longIdleTimer += Time.deltaTime;

			if (_longIdleTimer >= longIdleTime) 
			{
				_animator.SetTrigger("LongIdle");
			}
		} 
		else 
		{
			_longIdleTimer = 0f;
		}
	}

	public void IsStunned(){
		_animator.SetTrigger("Stunned");
		StartCoroutine(Stunned());
	}

	public IEnumerator Stunned() 
    {
		
		_rigidbody.velocity = new Vector2((-1*dirX)/1.45f, _rigidbody.velocity.y);
		_stunned = true;
		yield return new WaitForSecondsRealtime(0.6f);
		_stunned = false;
    }

	private void Flip()
	{	
		if (dirX > 0)
			_facingRight = true;
		else
			if (dirX < 0)
				_facingRight = false;
		if (((_facingRight) && (localScale.x < 0)) || ((!_facingRight) && (localScale.x > 0)))
			localScale.x *= -1;
		transform.localScale = localScale;
	}
	public void ShootHor(){
		if(localScale.x >0 )
		{
				var firedBullet = Instantiate (bulletPrefab, _firePoint.position, Quaternion.Euler(0,0, 0));
				var splash = Instantiate (splashPrefab, new Vector3 (0f,0.015f,0f) + _firePoint.position, Quaternion.Euler(0,0, 0));
		}
		if(localScale.x < 0 )
		{
				var firedBullet = Instantiate (bulletPrefab, _firePoint.position, Quaternion.Euler(0,0,180));
				var splash = Instantiate (splashPrefab,new Vector3 (0f,0.015f,0f) + _firePoint.position, Quaternion.Euler(0,0, 180));
		}
		
	}
	public void ShootUp()
	{
		var firedBullet = Instantiate (bulletPrefab, new Vector3 (localScale.x * -0.345f,0.412f,0f) + _firePoint.position, Quaternion.Euler(0,0, 90));
		var splash = Instantiate (splashPrefab, new Vector3 (localScale.x * -0.345f,0.412f,0f) + _firePoint.position, Quaternion.Euler(0,0, 90));
	}
	public void ShootWalk()
	{
		if(localScale.x >0)
		{
			var firedBullet = Instantiate (bulletPrefab, _firePoint.position, Quaternion.Euler(0,0, 0));
			var splash = Instantiate (splashPrefab,new Vector3 (0.1f,0.015f,0f) + _firePoint.position, Quaternion.Euler(0,0, 0));
		}
		if(localScale.x < 0)
		{
			var firedBullet = Instantiate (bulletPrefab, _firePoint.position, Quaternion.Euler(0,0, 180));
			var splash = Instantiate (splashPrefab,new Vector3 (-0.1f,0.015f,0f) + _firePoint.position, Quaternion.Euler(0,0, 180));
		}
	}
	public void ShootDiagon()
	{
		if(localScale.x >0)
		{
			var firedBullet = Instantiate (bulletPrefab, new Vector3 (0.05f,0.264f,0f) + _firePoint.position, Quaternion.Euler(0,0,40));
			var splash = Instantiate (splashPrefab, new Vector3 (0.05f,0.264f,0f) + _firePoint.position, Quaternion.Euler(0,0, 40));
		}
		if(localScale.x <0)
		{
			var firedBullet = Instantiate (bulletPrefab, new Vector3 (-0.05f,0.264f,0f) + _firePoint.position, Quaternion.Euler(0,0,140));
			var splash = Instantiate (splashPrefab, new Vector3 (-0.05f,0.264f,0f) + _firePoint.position, Quaternion.Euler(0,0, 140));
		}
	}
	
}
