using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject firepoint;
    public float livingTime;
    private float _startingTime;
    void Start()
    {
        _startingTime = Time.time;

		Destroy(gameObject, livingTime);
        
    }

}
