using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public class Character
    {
        public int Health = 100;
    }

    public Character playerStatus = new Character();

    public void DamagePlayer(int damage)
    {
        playerStatus.Health -= damage;
        if (playerStatus.Health <= 0)
        {
            GameManager.KillPlayer(this);
        }
    }
}
