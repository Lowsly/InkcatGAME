using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDetector : MonoBehaviour
{
    public bool _rightSide;
    void OnTriggerEnter2D(Collider2D collision)
    {
            _rightSide = true;
			Player player = collision.GetComponent<Player>();
            player.IsStunned(null, _rightSide);
    }
}
