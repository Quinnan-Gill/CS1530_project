using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameController gm;
    public float hitboxX = 0.25f;
    public float hitboxY = 0.25f;
    public float hitboxWidth = 0.50f;
    private bool player_collide;
    private Transform groundCheck;
    private Collider2D col;
    
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameController>();
        // groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
        // col = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameController.KillEnemy(this);
        }
    }

}
