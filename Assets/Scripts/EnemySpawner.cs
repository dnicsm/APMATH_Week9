using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPos;
    public EnemyBehavior enemySpawn;
    public float speed = 2f; 
    public Transform parent;

    [SerializeField]
    public Transform[] spawnPoints = new Transform[3];
    private Vector2[] RowPositions = new Vector2[3]; 

    public float spawnZ = 500f;

    private List<EnemyBehavior> enemies = new List<EnemyBehavior>();
    private HealthManager healthManager;

    private void Start()
    {
        healthManager = FindAnyObjectByType<HealthManager>();

        RowPositions = new Vector2[] { 
            new Vector2(spawnPoints[0].position.x, spawnPoints[0].position.y), 
            new Vector2(spawnPoints[1].position.x, spawnPoints[1].position.y), 
            new Vector2(spawnPoints[2].position.x, spawnPoints[2].position.y) 
        };
        
        InvokeRepeating(nameof(BeginSpawn), 1f, 1.5f);
    }

    private void BeginSpawn()
    {
        Vector2 randomRow = RowPositions[Random.Range(0, RowPositions.Length)];

        var spawnedEnemy = Instantiate(enemySpawn, parent);
        
        spawnedEnemy.enemyPosition = new Vector3(randomRow.x, randomRow.y, spawnZ);
        
        enemies.Add(spawnedEnemy);
    }

    private void Update()
    {
        playerPos = player.transform.position;
        
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            var enemy = enemies[i];
            if (enemy == null) 
            { 
                enemies.RemoveAt(i); continue; 
            }

            EnemyMover(enemy);

            if (enemy.enemyPosition.z <= playerPos.z)
            {
                float xDistance = Mathf.Abs(enemy.transform.position.x - player.transform.position.x);

                float playerJumpHeight = 0f;

                if (playerMovement != null)
                {
                    float playerFloorY = playerMovement.spawnPoints[playerMovement.currentLane].position.y;
                    
                    playerJumpHeight = player.transform.position.y - playerFloorY;
                }

                if (xDistance < 0.5f && playerJumpHeight < 0.5f)
                {

                    if (healthManager != null)
                    {
                        healthManager.TakeDamage();
                    }

                    enemies.RemoveAt(i);
                    Destroy(enemy.gameObject);
                    continue;
                }

                if (enemy.enemyPosition.z <= -2f)
                {
                    enemies.RemoveAt(i);
                    Destroy(enemy.gameObject);
                }
            }
        }

    }

    private void EnemyMover(EnemyBehavior enemy)
    {
        enemy.enemyPosition.z -= speed * Time.deltaTime;
    }
}