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

}
