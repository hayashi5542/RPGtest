using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<EnemyController> enemylist = new List<EnemyController>();
    public void Add(EnemyController enemy)
    {
        enemylist.Add(enemy);
    }
}
