using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  public GameObject enemyPrefab;
  public Transform player;
  public int waveMin = 5;
  public int waveMax = 10;
  public float intervalMin = 3f;
  public float intervalMax = 5f;
  public float spawnRadius = 7f;
  public int maxAlive = 20;

  private float timer;
  private List<GameObject> alive = new List<GameObject>();    //enemy1 0, enemy3 1

  private void Start()
  {
    GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
    player = playerObj.transform;
  }

  private void Update()
  {
    for (int i = alive.Count - 1; i >= 0; i--)    //3 - 1 > 2 > 1 > 0 > -1
    {
      if (alive[i] == null)
      {
        alive.RemoveAt(i);
      }
    }

    timer = timer - Time.deltaTime;

    if (timer <= 0f || alive.Count == 0)
    {
      SpawnWave();
      timer = Random.Range(intervalMin, intervalMax + 1); //4
    }
  }

  private void SpawnWave()
  {
    int desired = Random.Range(waveMin, waveMax + 1);   //5
    int slotsLeft = maxAlive - alive.Count; //20 - 10 > 10
    int count = Mathf.Min(desired, slotsLeft); //5

    for (int i = 0; i < count; i++) //0 - 4 > 5
    {
      SpawnOne();
    }
  }

  private void SpawnOne()
  {
    Vector2 offset = Random.insideUnitCircle.normalized * spawnRadius;
    Vector3 spawnPos = player.position + (Vector3)offset;
    spawnPos = ArenaBounds.Clamp(spawnPos, 0.6f);

    GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    alive.Add(enemy);
  }
}
