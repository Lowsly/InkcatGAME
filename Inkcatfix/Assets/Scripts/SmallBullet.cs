using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBullet : MonoBehaviour
{
public GameObject splashEndPrefab, smallBulletPrefab;
    private Rigidbody2D _rigidbody;
  	private bool _isTouching;
    public LayerMask groundLayer;
	public float groundCheckRadius;
	public float speed = 2f;
	public Vector2 direction;
	public float livingTime = 3f;

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
		
		_isTouching = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
		if (_isTouching == true){
			DestroyBullet();
		}
		
    }
	void OnTriggerEnter2D(Collider2D collision)
    {
            Health health = collision.GetComponent<Health>();
            if (health != null)
    {
        health.Hit();
    }
            DestroyBullet();
    }
	void DestroyBullet(){
		GameObject splash = Instantiate (splashEndPrefab, transform.position, Quaternion.identity);
		splash.transform.localScale *= 0.4f;
		Destroy(this.gameObject);
	}
	void SummonExtra(){
		
		Instantiate (smallBulletPrefab, new Vector3(0,0.01f,0) + transform.position, Quaternion.identity);
		Instantiate (smallBulletPrefab, new Vector3(0,-0.01f,0) + transform.position, Quaternion.identity);
		
	}
}