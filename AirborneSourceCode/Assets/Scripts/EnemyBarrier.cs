using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarrier : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
     
        if(other.gameObject.tag == "enemy")
        {
            enemy.StopChase();
        }
    }
}
