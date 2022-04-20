using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawingEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab;
    public int number;
    public int radius;
    void Start()
    {
        SpawnEnemy(number);
    }

    // Update is called once per frame
    
    public void SpawnEnemy(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Vector3 newEnemyPosition = transform.position + Random.insideUnitSphere*radius;
            newEnemyPosition.y = 17.0956f;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(newEnemyPosition, out hit, 1f, NavMesh.AllAreas))
            {
                
                Instantiate(enemyPrefab, newEnemyPosition, Quaternion.identity);
            }
            else
                i--;
            //newEnemyPosition.y = Terrain.activeTerrain.SampleHeight(newEnemyPosition);
            
        }

    }
}
