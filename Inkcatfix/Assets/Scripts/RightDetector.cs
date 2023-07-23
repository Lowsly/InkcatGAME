using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDetector : MonoBehaviour
{
       public void right()
    {
			Player player = GetComponent<Player>();
            player.rightSided();
    }
}
