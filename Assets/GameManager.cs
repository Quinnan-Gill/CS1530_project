using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static void KillPlayer (Player player)
    {
        if (player != null)
        {
            Destroy(player.gameObject);
        }
    }
}
