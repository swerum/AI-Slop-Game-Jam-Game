using System.Collections.Generic;
using Enemy.States;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LevelManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _prefabEnemies;
    [SerializeField] List<int> _enemiesPerLevel;
    [SerializeField] GameObject _levelExitFence;
    List<GameObject> _enemies;

    public void InitializeLevel(int levelIndex)
    {
        _enemies = new List<GameObject>();
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Bounds bounds = boxCollider.bounds;
        int numEnemies = _enemiesPerLevel[levelIndex];
        // generate the enemies inside the area described by the BoxCollider
        for (int i = 0; i < numEnemies; i++)
        {
            int randomEnemyIndex = Mathf.FloorToInt(Random.Range(0, _prefabEnemies.Count));
            GameObject prefab = _prefabEnemies[randomEnemyIndex];
            GameObject enemy = Instantiate(prefab);
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);
            Vector3 randomPosition = new Vector3(randomX, 0, randomZ);
            enemy.transform.position = randomPosition;
            EnemyStateMachine enemyStateMachine = enemy.GetComponent<EnemyStateMachine>();
            enemyStateMachine.LevelManager = this;
            _enemies.Add(enemy);
        }
    }
    public void OnEnemyKilled(GameObject enemy)
    {
        _enemies.Remove(enemy);
        if (_enemies.Count == 0)
        {
            _levelExitFence.SetActive(false);
        }
    }

}
