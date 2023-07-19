using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numHearts;

    public GameObject player;

    public Image[] hearts;

    public Image[] Ink;

    public Sprite fullInk;
    public Sprite emptyInk;

    public Sprite fullInkJar;

     public Sprite dosInkJar;

    public Sprite emptyInkJar;

    public Sprite brokenInkJar;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Animator _animator;

    public Color dmgColor;

    public Color originalColor;
    
    private bool _noJar = false;

    private int _maxInk = 5;

    private int _inkUses = 5;

    private SpriteRenderer _renderer;

    private float colorTime, colorTime2, colorTime3; 
    private float _cdShoot = 0f;
	private float _shootDelay = 0.5f;

    
     void Start(){
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        
        if(_inkUses < 0){
            _inkUses = 0;
        }
        if(health > numHearts){
            numHearts = health;
        }
        for (int i=0; i<hearts.Length;i++){
            if(i<health){
                hearts[i].sprite = fullHeart;
            }
            else {
                 hearts[i].sprite = emptyHeart;
            }
            if(i < numHearts){
                hearts[i].enabled = true;
            }
            else {
                hearts[i].enabled = false;
            }
        }
        if (_noJar == false){
            if(_inkUses < 1){
                Ink[2].sprite = emptyInk;
                Ink[1].sprite = emptyInk;
                Ink[0].sprite = brokenInkJar;
                _noJar = true;
            }
            if(_inkUses == 1){
                Ink[2].sprite = emptyInk;
                Ink[1].sprite = emptyInk;
                Ink[0].sprite = emptyInkJar;
            }
            if(_inkUses == 2){
                Ink[2].sprite = emptyInk;
                Ink[1].sprite = emptyInk;
                Ink[0].sprite = dosInkJar;
            }
            if(_inkUses == 3){
                Ink[2].sprite = emptyInk;
                Ink[1].sprite = emptyInk;
                Ink[0].sprite = fullInkJar;
            }
            if(_inkUses == 4){
                Ink[2].sprite = emptyInk;
                Ink[1].sprite = fullInk;
                Ink[0].sprite = fullInkJar;
            }
            if(_inkUses == 5){
                Ink[2].sprite = fullInk;
                Ink[1].sprite = fullInk;
                Ink[0].sprite = fullInkJar;
            }
        }
        if (_noJar == true) {
            _maxInk = 2;
            if (_inkUses > 2){
                _inkUses = 2;
            }
            if(_inkUses == 2){
                Ink[2].sprite = fullInk;
                Ink[1].sprite = fullInk;
                Ink[0].sprite = brokenInkJar;
            }
             if(_inkUses == 1){
                Ink[2].sprite = emptyInk;
                Ink[1].sprite = fullInk;
                Ink[0].sprite = brokenInkJar;
            }
            if(_inkUses == 0){
                Ink[2].sprite = emptyInk;
                Ink[1].sprite = emptyInk;
                Ink[0].sprite = brokenInkJar;
            }
        }
    }
    public int Hit()
    {
        StartCoroutine(HeartColor());
        StartCoroutine(Damaged());
        health = health + 1;
        Debug.Log("Shoot");

        if (health <= -1){
            Destroy(player);
            //hearts[0].sprite = emptyHeart;
            Debug.Log("Sht");
        } 
        return health;
    }
    public void Heal()
    {
        if (_inkUses > 0){
            health = health + 1;
            Debug.Log("Heal");
            _inkUses = _inkUses -1;
            _animator.SetTrigger("Heal");
            if (health > 3){
                health = 3;
            }
        }
    }
    public void InkUsed(){
        if (_inkUses < _maxInk){
            _inkUses = _inkUses + 1;
        }
        if (_inkUses > _maxInk) {
            _inkUses = _maxInk;
        }
    }

    IEnumerator HeartColor(){
        for (int i = 0; i<health; i++){
        hearts[health-1].color = Color.red;
        //hearts[health-1].color = new Color (0, 0, 0, 0);
        }
      
        colorTime = 0;
        while (colorTime < 0.8)
        {
            colorTime+= Time.deltaTime;    
            yield return null;   
        }
        hearts[0].color = Color.white;
        hearts[1].color = Color.white;
        hearts[2].color = Color.white;
    }

    IEnumerator Damaged(){
        colorTime2 = 45;
        for(int j=0; j<colorTime2;j++){
            _renderer.color = new Color (0, 0, 0, 0f);
            colorTime3 = 0;
            var timesince = Time.time;
            while (colorTime3 < 2.65)
            {
                colorTime3+= Time.deltaTime+timesince/Time.time; 
                yield return null;    
                
            }
            _renderer.color = Color.white;
            yield return null;  
            
            
        }
        
    }
   
    
}
