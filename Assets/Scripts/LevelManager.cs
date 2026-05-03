using System.Collections.Generic;
using Enemy.States;
using Player;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LevelManager : MonoBehaviour
{
    [SerializeField] int _initialNumEnemies = 2;
    [SerializeField] float _minDistanceToPlayer = 1f;
    [SerializeField, Range(0,1f)] float chanceForTwoEnemies = 0.5f;
    [SerializeField] List<GameObject> _prefabEnemies;
    [SerializeField] GameObject _levelExitFence;
    [SerializeField] PlayerStateMachine _player;
    [SerializeField] TextMeshPro text;
    List<GameObject> _enemies;
    int _gameScore=0;

    void Start()
    {
        _enemies = new List<GameObject>();
    }
    public void CreateEnemies(int numEnemies)
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Bounds bounds = boxCollider.bounds;
        // generate the enemies inside the area described by the BoxCollider
        for (int i = 0; i < numEnemies; i++)
        {
            int randomEnemyIndex = Mathf.FloorToInt(Random.Range(0, _prefabEnemies.Count));
            GameObject prefab = _prefabEnemies[randomEnemyIndex];
            GameObject enemy = Instantiate(prefab);
            Vector3 randomPosition = GetRandomPositionInBounds(bounds);
            while (Vector3.Distance(_player.transform.position, randomPosition) < _minDistanceToPlayer)
            {
                randomPosition = GetRandomPositionInBounds(bounds);
            } 
            enemy.transform.position = randomPosition;
            EnemyStateMachine enemyStateMachine = enemy.GetComponent<EnemyStateMachine>();
            enemyStateMachine.LevelManager = this;
            _enemies.Add(enemy);
        }
    }
    public void OnEnemyKilled(GameObject enemy)
    {
        _gameScore += 10;
        text.text = "Score: "+_gameScore+" Points";
        _enemies.Remove(enemy);
        int numEnemies = 1;
        if (Random.Range(0f, 1f) <chanceForTwoEnemies) numEnemies++;
        CreateEnemies(numEnemies);
    }
    private Vector3 GetRandomPositionInBounds(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);
        Vector3 randomPosition = new Vector3(randomX, 0, randomZ);
        return randomPosition;
    }
}
