using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    public Image powerBar;

    public Sprite[] powerBarSprite;

    private int _maxPower=11, _currentPower;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        for(int i=0; i<_currentPower+1;i++){
            powerBar.sprite = powerBarSprite[i];
        }
        if (Input.GetButtonDown("Fire3"))
			{
				_currentPower++; 
                if(_currentPower+1>_maxPower){
                    _currentPower = _maxPower;
                }
			}
        if (Input.GetButtonDown("Fire2"))
			{
                if(_currentPower>0){
                    _currentPower--; 
                }
				
                
			}
            if(_currentPower<0){
                    _currentPower = 0;
                }
        if (Input.GetKeyDown(KeyCode.Alpha1) && _currentPower>2){
            _currentPower= _currentPower-3; 
            Player player = GetComponent<Player>();
            player.ActivateSpecialShoot();
            
        }
    }
    void SpecialShootTrimode(){

    }
}
