using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject spiderPrefab;
    private EnemyController enemy;
    private float dieTimer = 0;

    // Update is called once per frame
    void Update()
    {
        
        if (enemy.DieMotion() == true)
        {
            dieTimer += Time.deltaTime;
            if (dieTimer > 7)
            {
                Instantiate(spiderPrefab);
            }

        }
    }
}
