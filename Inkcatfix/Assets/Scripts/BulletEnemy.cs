using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
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

	void Start()
    {
		Health health = GetComponent<Health>();
		_startingTime = Time.time;
		Invoke ("DestroyBullet",livingTime);
    }

    void Update()
    {
		Vector2 movement = direction.normalized * speed * Time.deltaTime;
		transform.Translate(movement);
		
		_isTouching = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		if (_isTouching == true){
			DestroyBullet();
		}
		// Change bullet's color over time
		/*float _timeSinceStarted = Time.time - _startingTime;
		float _percentageCompleted = _timeSinceStarted / livingTime;

		_renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);*/
		
    }
	void OnTriggerEnter2D(Collider2D collision)
    {
            Health health = collision.GetComponent<Health>();
            health.Hit();
            DestroyBullet();
    }
	void DestroyBullet(){
		Instantiate (splashEndPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}