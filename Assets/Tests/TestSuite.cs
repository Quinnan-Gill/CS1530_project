using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{

    [OneTimeSetUp]
    public void LoadScene() {
        SceneManager.LoadScene("LivingRoom");
    }

    [UnityTest]
    public IEnumerator TestPlayerHurt() {
        GameObject playerobj = GameObject.FindWithTag("Player");
        Player player = playerobj.GetComponent<Player>();
        int hp = player.Health;
        Assert.Greater(hp, 0);
        player.DamagePlayer(1, false);
        yield return null;
        Assert.AreEqual(player.Health, hp-1);
        yield break;
    }

    [UnityTest]
    public IEnumerator TestCameraFollow() {
        GameObject playerobj = GameObject.FindWithTag("Player");
        Transform playertr = playerobj.transform;

        GameObject camobj = GameObject.FindWithTag("MainCamera");
        Transform camtr = camobj.transform;
        float x = camtr.position.x;

        // Move the player, wait a bit, then check that the camera moved.
        playertr.position += Vector3.right * 0.5f;
        yield return new WaitForSeconds(0.25f);
        Assert.AreNotEqual(playertr.position.x, x);
    }

}
