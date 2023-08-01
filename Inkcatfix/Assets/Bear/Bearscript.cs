using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bearscript : MonoBehaviour
{
    public GameObject player;
    public int Health = 3;
    private void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }
}
