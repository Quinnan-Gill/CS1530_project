using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public class Character
    {
        public int Health = 100;
    }
    private bool disableHurt = true;
    public float disableHurtTime = 0.1f;
    public float hurtJumpHeight = 0.1f;
    public float blinkSpeed = 0.1f;

    public Character playerStatus = new Character();
    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private IEnumerator TempDisableHurt()
    {
        disableHurt = false;
        InvokeRepeating("BlinkPlayer", 0, blinkSpeed);
        yield return new WaitForSeconds(disableHurtTime);
        disableHurt = true;
        CancelInvoke("BlinkPlayer");
        if (!gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void BlinkPlayer()
    {
        if(gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void DamagePlayer(int damage)
    {
        if (disableHurt)
        {
            playerStatus.Health -= damage;
            m_Rigidbody2D.AddForce(new Vector2(0f, hurtJumpHeight));
            StartCoroutine(TempDisableHurt());
        }
        if (playerStatus.Health <= 0)
        {
            GameManager.KillPlayer(this);
        }
    }
}
