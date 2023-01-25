using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header ("-----[Enemy]-----")]
    public GameObject       enemyS_Prefab;
    public GameObject       enemyM_Prefab;
    public GameObject       enemyL_Prefab;
    GameObject[]            enemyS;
    GameObject[]            enemyM;
    GameObject[]            enemyL;

    [Header ("-----[Item]-----")]
    public GameObject       itemCoin_Prefab;
    public GameObject       itemPower_Prefab;
    public GameObject       itemBoom_Prefab;
    GameObject[]            itemCoin;
    GameObject[]            itemPower;
    GameObject[]            itemBoom;

    [Header ("-----[Bullet]-----")]
    public GameObject       bulletPlayerA_Prefab;
    public GameObject       bulletPlayerB_Prefab;
    GameObject[]            bulletPlayerA;
    GameObject[]            bulletPlayerB;
    public GameObject       bulletEnemyA_Prefab;
    public GameObject       bulletEnemyB_Prefab;
    GameObject[]            bulletEnemyA;
    GameObject[]            bulletEnemyB;

    GameObject[]            targetPool;

    void Awake()
    {
        enemyS =        new GameObject[20];
        enemyM =        new GameObject[15];
        enemyL =        new GameObject[10];

        itemCoin =      new GameObject[20];
        itemPower =     new GameObject[10];
        itemBoom =      new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA =  new GameObject[100];
        bulletEnemyB =  new GameObject[100];

        Generate();
    }

    void Generate(){
        // -----[Enemy]-----
        for(int index = 0; index < enemyS.Length; index++){
            enemyS[index] = Instantiate(enemyS_Prefab);
            enemyS[index].SetActive(false);
        }
        for(int index = 0; index < enemyM.Length; index++){
            enemyM[index] = Instantiate(enemyM_Prefab);
            enemyM[index].SetActive(false);
        }
        for(int index = 0; index < enemyL.Length; index++){
            enemyL[index] = Instantiate(enemyL_Prefab);
            enemyL[index].SetActive(false);
        }

        // -----[Item]-----
        for(int index = 0; index < itemCoin.Length; index++){
            itemCoin[index] = Instantiate(itemCoin_Prefab);
            itemCoin[index].SetActive(false);
        }
        for(int index = 0; index < itemPower.Length; index++){
            itemPower[index] = Instantiate(itemPower_Prefab);
            itemPower[index].SetActive(false);
        }
        for(int index = 0; index < itemBoom.Length; index++){
            itemBoom[index] = Instantiate(itemBoom_Prefab);
            itemBoom[index].SetActive(false);
        }

        // -----[Bullet]-----
        for(int index = 0; index < bulletPlayerA.Length; index++){
            bulletPlayerA[index] = Instantiate(bulletPlayerA_Prefab);
            bulletPlayerA[index].SetActive(false);
        }
        for(int index = 0; index < bulletPlayerB.Length; index++){
            bulletPlayerB[index] = Instantiate(bulletPlayerB_Prefab);
            bulletPlayerB[index].SetActive(false);
        }
        for(int index = 0; index < bulletEnemyA.Length; index++){
            bulletEnemyA[index] = Instantiate(bulletEnemyA_Prefab);
            bulletEnemyA[index].SetActive(false);
        }
        for(int index = 0; index < bulletEnemyB.Length; index++){
            bulletEnemyB[index] = Instantiate(bulletEnemyB_Prefab);
            bulletEnemyB[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type){
        switch(type){
            // -----[Enemy]-----
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyL": 
                targetPool = enemyL;
                break;

            // -----[Item]-----
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;

            // -----[Bullet]-----
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;    
        }

        for (int index = 0; index < targetPool.Length; index++){
            if ( !targetPool[index].activeSelf ){
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }


}
