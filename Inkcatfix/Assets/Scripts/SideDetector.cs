using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDetector : MonoBehaviour
{
    public void left()
    {
			Player player = GetComponent<Player>();
            player.leftSided();
    }
}
