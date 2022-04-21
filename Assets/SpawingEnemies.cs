using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawingEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab;
    public int number;
    public int radius;
    public Text gameOverText;
    public GameObject buttons;
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
    public void GameOver(string text)
    {
        buttons.SetActive(true);
        gameOverText.text = text;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quits()
    {
        SceneManager.LoadScene(0);
    }
}
