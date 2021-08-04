using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 100;
    private bool disableHurt = true;
    public float disableHurtTime = 0.1f;
    public float hurtJumpHeight = 0.1f;
    public float blinkSpeed = 0.1f;

    private Rigidbody2D m_Rigidbody2D;
    private SpriteRenderer oscRender;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        Transform renderTransform  = transform.Find("PlayerSprite");
        if (renderTransform == null)
        {
            // Since there are no overlaying sprites use the defaul prefab sprite
            oscRender = gameObject.GetComponent<SpriteRenderer>();
        }
        else
        {
            oscRender = renderTransform.GetComponent<SpriteRenderer>();
        }
    }

    private IEnumerator TempDisableHurt(bool jumpLeft)
    {
        disableHurt = false;
        int dir = jumpLeft ? -1 : 1;
        m_Rigidbody2D.AddForce(
            new Vector2(dir * hurtJumpHeight, hurtJumpHeight)
        );
        InvokeRepeating("BlinkPlayer", 0, blinkSpeed);
        yield return new WaitForSeconds(disableHurtTime);
        disableHurt = true;
        CancelInvoke("BlinkPlayer");
        if (!oscRender.enabled)
        {
            oscRender.enabled = true;
        }
    }

    void BlinkPlayer()
    {
        if(oscRender.enabled)
        {
            oscRender.enabled = false;
        }
        else
        {
            oscRender.enabled = true;
        }
    }

    public void DamagePlayer(int damage, bool jumpLeft)
    {
        if (disableHurt)
        {
            Health -= damage;
            Debug.Log(Health);
            StartCoroutine(TempDisableHurt(jumpLeft));
        }
        if (Health <= 0)
        {
            GameManager.KillPlayer(this);
        }
    }
}
