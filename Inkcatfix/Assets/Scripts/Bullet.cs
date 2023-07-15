using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public GameObject splashEndPrefab;
    private Rigidbody2D _rigidbody;
  	private bool _isTouching;
    public Transform groundCheck;
    public LayerMask groundLayer;
	public float groundCheckRadius;
	public float speed = 2f;
	public Vector2 direction;

	public float livingTime = 3f;
	public Color initialColor = Color.white;
	public Color finalColor;

	private SpriteRenderer _renderer;
	private float _startingTime;


	void Awake()
	{
		_renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
    {
		//  Save initial time
		_startingTime = Time.time;

		// Destroy the bullet after some time
		Invoke ("DestroyBullet",livingTime);
    }

    // Update is called once per frame
    void Update()
    {
        _isTouching = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		//Physics2D.CapsuleCast
		//  Move object
		Vector2 movement = direction.normalized * speed * Time.deltaTime;
		transform.Translate(movement);

		// Change bullet's color over time
		/*float _timeSinceStarted = Time.time - _startingTime;
		float _percentageCompleted = _timeSinceStarted / livingTime;

		_renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);*/
		if(_isTouching == true){
			DestroyBullet();
		}
    }
	void DestroyBullet(){
		Instantiate (splashEndPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
