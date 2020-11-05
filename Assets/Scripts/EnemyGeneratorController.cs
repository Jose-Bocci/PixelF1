using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float generatorTimer = 1.75f;
    Vector3 tempPos;
    int posicion;
    float arriba = -3.3f;
    float medio = 0f;
    float abajo = 3.3f;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy()
    {
        posicion = UnityEngine.Random.Range(0, 2);
        if (posicion == 0)
        {
            tempPos = transform.position;
            tempPos.x = medio;
            transform.position = tempPos;
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
        if (posicion == 1)
        {
            tempPos = transform.position;
            tempPos.x = arriba;
            transform.position = tempPos;
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
        if (posicion == 2)
        {
            tempPos = transform.position;
            tempPos.x = abajo;
            transform.position = tempPos;
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }

    public void StartGenerator()
    {

        InvokeRepeating("CreateEnemy", 0f, generatorTimer);

    }

    public void CancelGenerator(bool clean = false)
    {
        CancelInvoke("CreateEnemy");
        if (clean)
        {
            object[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in allEnemies)
            {
                Destroy(enemy);
            }
        }
    }

}
