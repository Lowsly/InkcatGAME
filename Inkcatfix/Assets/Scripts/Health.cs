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
    public Sprite fullHeart;
    public Sprite emptyHeart;
    void Update()
    {
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
    }
    public void Hit()
    {
        health = health - 1;
        Debug.Log("Shoot");
        if (health == 0){
            Destroy(player);
            hearts[0].sprite = emptyHeart;
            Debug.Log("Sht");
        } 
    }
    public void Heal()
    {
        health = health + 1;
        Debug.Log("Heal");
    }
    
}
