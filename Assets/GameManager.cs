using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;

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
            Destroy(player.gameObject);
            gm.StartCoroutine(gm.RespawnPlayer());
        }
    }
}
