using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab;

    void SelectEnemy()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>(); //save input enemy prefabs



    }



}
