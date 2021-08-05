using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gm;

    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameController>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 1f;

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);

        Vector3 spawnPlace = new Vector3(
            spawnPoint.position.x, spawnPoint.position.y, 0
        );
        Instantiate(playerPrefab, spawnPlace, spawnPoint.rotation);
    }

    public static void KillPlayer (Player player)
    {
        if (player != null)
        {
            Debug.Log(player);
            Destroy(player.gameObject);
            gm.StartCoroutine(gm.RespawnPlayer());
        }
    }
}
