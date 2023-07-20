using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health, numHearts;

    public GameObject player;

    public Image[] hearts;

    public Image[] Ink;

    public Sprite fullInk, emptyInk, fullInkJar,  dosInkJar, emptyInkJar, brokenInkJar, fullHeart, emptyHeart;

    private Animator _animator;

    public Color dmgColor, originalColor;
    
    private bool _noJar = false;

    private bool _isImmune, _lowHealth;

    private int _maxInk = 5,_inkUses = 5;

    private SpriteRenderer _renderer;

    private float colorTime, colorTime2, colorTime3; 

    
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
    public void Hit()
    {   
        if (_isImmune == false){
            StartCoroutine(Immune());
            StartCoroutine(HeartColor());
            StartCoroutine(Damaged());
            health = health + 1;
            Debug.Log("Shoot");
            
            if (health == 0){
                _lowHealth = true;
                StartCoroutine(LowHealth());
            }
            if (health <= -1){
                Destroy(player);
                hearts[0].sprite = emptyHeart;
                Debug.Log("Sht");
            } 
        }
    }
    public void Heal()
    {
        if (_inkUses > 0){
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput == 0){
                _animator.SetTrigger("Heal");
            }
            if (horizontalInput != 0){
                _animator.SetTrigger("HealWalk");
            }
            _lowHealth = false;
            health = health + 1;
            Debug.Log("Heal");
            _inkUses = _inkUses -1;
            
            
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

    IEnumerator HeartColor()
    {
        for (int i = 0; i<health; i++)
        {
            hearts[health-1].color = Color.red;
        }
       yield return new WaitForSecondsRealtime(1f);
       foreach (Image img in hearts)
            {
                img.color = Color.white;
            }
    }

    IEnumerator Damaged()
    {
        for (int i = 0; i < 4; i++)
            {
             _renderer.color = new Color (0, 0, 0, 0f);
             yield return new WaitForSecondsRealtime(.1f);
             _renderer.color = Color.white;
             yield return new WaitForSecondsRealtime(.1f);
             Debug.Log("ay");
            }
        
    }

    IEnumerator Immune() 
    {
     _isImmune = true;
     yield return new WaitForSecondsRealtime(0.7f);
     _isImmune = false;
    }
    IEnumerator LowHealth() 
    {
        while (_lowHealth == true){
            foreach (Image img in hearts)
            {
                img.color = Color.red;
            }
            yield return new WaitForSecondsRealtime(0.45f);
            foreach (Image img in hearts)
            {
                img.color = Color.white;
            }
            yield return new WaitForSecondsRealtime(0.45f);
            
        }
        yield return null;
    }
}
