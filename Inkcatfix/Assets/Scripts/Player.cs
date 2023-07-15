using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//Bullets
	public GameObject bulletPrefab;
	public GameObject splashPrefab;
    public GameObject shooter;
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
		
       
	   localScale = transform.localScale;
	
    }

    void Update()
    {	
		
		dirX = Input.GetAxis ("Horizontal");		
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		float verticalInput = Input.GetAxisRaw("Vertical");
		/*mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x)* Mathf.Rad2Deg - 90f;
		_firePoint.localRotation = Quaternion.Euler(0,0, angle);*/
			// Movement
		_movement = new Vector2(horizontalInput, 0f);

			// Flip character
		/*if (horizontalInput < 0f && _facingRight == true) {
			Flip();
		} else if (horizontalInput > 0f && _facingRight == false) {
			Flip();
		}*/

		// Is Grounded?
		_isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

		// Is Jumping?
		if (Input.GetButtonDown("Jump") && _isGrounded == true && _isAttacking == false) {
			_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
			
	}
	public void ShootHor(){
		float verticalInput = Input.GetAxisRaw("Vertical");
		if(localScale.x >0 ){
				var firedBullet = Instantiate (bulletPrefab, _firePoint.position, Quaternion.Euler(0,0, 0));
				var splash = Instantiate (splashPrefab, new Vector3 (0f,0.015f,0f) + _firePoint.position, Quaternion.Euler(0,0, 0));
		}
		if(localScale.x < 0 ){
				var firedBullet = Instantiate (bulletPrefab, _firePoint.position, Quaternion.Euler(0,0,180));
				var splash = Instantiate (splashPrefab,new Vector3 (0f,0.015f,0f) + _firePoint.position, Quaternion.Euler(0,0, 180));
		}
		
	}
	public void ShootUp(){
		
		var firedBullet = Instantiate (bulletPrefab, new Vector3 (localScale.x * -0.345f,0.412f,0f) + _firePoint.position, Quaternion.Euler(0,0, 90));
		var splash = Instantiate (splashPrefab, new Vector3 (localScale.x * -0.345f,0.412f,0f) + _firePoint.position, Quaternion.Euler(0,0, 90));
	}
	public void ShootWalk(){
		if(localScale.x >0){
		var firedBullet = Instantiate (bulletPrefab, _firePoint.position, Quaternion.Euler(0,0, 0));
		var splash = Instantiate (splashPrefab,new Vector3 (0f,0.015f,0f) + _firePoint.position, Quaternion.Euler(0,0, 0));
		}
		if(localScale.x < 0){
		var firedBullet = Instantiate (bulletPrefab, _firePoint.position, Quaternion.Euler(0,0, 180));
		var splash = Instantiate (splashPrefab,new Vector3 (0f,0.015f,0f) + _firePoint.position, Quaternion.Euler(0,0, 180));
		}
	}
	public void ShootDiagon(){
		if(localScale.x >0){
		var firedBullet = Instantiate (bulletPrefab, new Vector3 (0.05f,0.264f,0f) + _firePoint.position, Quaternion.Euler(0,0,45));
		var splash = Instantiate (splashPrefab, new Vector3 (0.05f,0.264f,0f) + _firePoint.position, Quaternion.Euler(0,0, 45));
		}
		if(localScale.x <0){
		var firedBullet = Instantiate (bulletPrefab, new Vector3 (-0.05f,0.264f,0f) + _firePoint.position, Quaternion.Euler(0,0,135));
		var splash = Instantiate (splashPrefab, new Vector3 (-0.05f,0.264f,0f) + _firePoint.position, Quaternion.Euler(0,0, 135));
		}
	}
	void FixedUpdate()
	{		
			float horizontalInput = Input.GetAxisRaw("Horizontal");
			float verticalInput = Input.GetAxisRaw("Vertical");
			float horizontalVelocity = _movement.normalized.x * speed;
			_rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
			if (Input.GetButton("Fire1") && _isGrounded == true && Time.time > _cdShoot) {
			_cdShoot = _shootDelay + Time.time;
			if (_isGrounded == true && horizontalInput == 0 && verticalInput == 0){
				_attackRight =! _attackRight;
				//Invoke("ShootHor",0f);
				if (_attackRight == true ){
					_animator.SetTrigger("ShootRight");
				}
				if (_attackRight == false ){
					_animator.SetTrigger("ShootLeft");
				}
				//Invoke("Shoot",0.05f);
			}
			if (_isGrounded == true && (horizontalInput == 1 || horizontalInput == -1) &&  verticalInput == 0 ){
				//Invoke("ShootWalk",0f);
				//Invoke("Shoot",0.05f);
				_animator.SetTrigger("ShootWalk");
			}
			if (_isGrounded == true && horizontalInput == 0 && verticalInput == 1){
				_animator.SetTrigger("ShootUp");

				//Invoke("ShootUp",0f);
				//Invoke("Shoot",0.05f);
			}
			if (_isGrounded == true &&  (horizontalInput == 1 || horizontalInput == -1) && verticalInput == 1){
				//("ShootDiagon",0f);
				//Invoke("Shoot",0.05f);
				_animator.SetTrigger("ShootDiagon");
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
		if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
			_isAttacking = true;
		} else {
			_isAttacking = false;
		}

		// Long Idle
		if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Isidle")) {
			_longIdleTimer += Time.deltaTime;

			if (_longIdleTimer >= longIdleTime) {
				_animator.SetTrigger("LongIdle");
			}
		} else {
			_longIdleTimer = 0f;
		}
	}

	private void Flip()
	{
		/*_facingRight = !_facingRight;
		float localScaleX = transform.localScale.x;
		localScaleX = localScaleX * -1f;
		transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);*/
		
		if (dirX > 0)
			_facingRight = true;
		else
			if (dirX < 0)
				_facingRight = false;
		if (((_facingRight) && (localScale.x < 0)) || ((!_facingRight) && (localScale.x > 0)))
			localScale.x *= -1;
		transform.localScale = localScale;
	}

	public void Shoot()
	{
		
			//if(_firePoint.localRotation.z >= -180 && _firePoint.localRotation.z <= 0 && _facingRight == true){
			
			//GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Vector2.left) as GameObject;
			//
			/*
			float horizontalInput = Input.GetAxisRaw("Horizontal");
			float verticalInput = Input.GetAxisRaw("Vertical");
			GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject;
			GameObject mySplash = Instantiate(splashPrefab, _firePoint.position, Quaternion.identity) as GameObject;
			Bullet bulletComponent = myBullet.GetComponent<Bullet>();
			Splash splashComponent = mySplash.GetComponent<Splash>();

			if (horizontalInput == -1f && verticalInput == 0f) {
				// Left
				bulletComponent.transform.localRotation = Quaternion.Euler(0,0, 180);
				splashComponent.transform.localRotation = Quaternion.Euler(0,0, 180);
				
			}  
			if (horizontalInput == -1f && verticalInput == 1f) {	

					bulletComponent.transform.localRotation = Quaternion.Euler(0,0, 135);
					splashComponent.transform.localRotation = Quaternion.Euler(0,0, 135);

			}
			if (horizontalInput == 1f && verticalInput == 1f){
				
					bulletComponent.transform.localRotation = Quaternion.Euler(0,0, 45);
					splashComponent.transform.localRotation = Quaternion.Euler(0,0, 45);
			}
			if (horizontalInput == 1f && verticalInput == 0f){
				// Right
				bulletComponent.transform.localRotation = Quaternion.Euler(0,0, 0);
				splashComponent.transform.localRotation = Quaternion.Euler(0,0, 0);
			}
			if (horizontalInput == 0f && verticalInput == 0f){
				if(_facingRight == true){
					bulletComponent.transform.localRotation = Quaternion.Euler(0,0, 0);
					splashComponent.transform.localRotation = Quaternion.Euler(0,0, 0);
				}
				else {
					bulletComponent.transform.localRotation = Quaternion.Euler(0,0, 180);
					splashComponent.transform.localRotation = Quaternion.Euler(0,0, 180);
				}
			}
			if (verticalInput == 1f && horizontalInput == 0f ){
				// Right
				bulletComponent.transform.localRotation = Quaternion.Euler(0,0, 90);
				splashComponent.transform.localRotation = Quaternion.Euler(0,0, 90);
			}
			*/

	}

}
