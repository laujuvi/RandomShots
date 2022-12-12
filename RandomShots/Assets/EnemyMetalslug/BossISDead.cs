using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossISDead : MonoBehaviour
{
    public EnemyBossSpider bossSPider;

    private void Update()
    {
        if (!bossSPider.isActiveAndEnabled)
        {
            SceneManager.LoadScene(4);
        }
    }
}
