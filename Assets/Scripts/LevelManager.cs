using System.Collections.Generic;
using Enemy.States;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    [SerializeField] int _initialNumEnemies = 2;
    [SerializeField] float _minDistanceToPlayer = 1f;
    [SerializeField, Range(0,1f)] float chanceForTwoEnemies = 0.5f;
    [SerializeField] List<GameObject> _prefabEnemies;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] BoxCollider _boxCollider;
    int _gameScore=0;
    PlayerStateMachine _player;
    // List<GameObject> _enemies;

    private static LevelManager _instance;
    public static LevelManager Instance {get {return _instance; }}
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        _player = PlayerStateMachine.Instance;
        CreateEnemies(_initialNumEnemies);
        SetGameScore(0);
    }


    public void CreateEnemies(int numEnemies)
    {
        Bounds bounds = _boxCollider.bounds;
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
        }
    }
    public void OnEnemyKilled()
    {
        SetGameScore(_gameScore+10);
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
    private void SetGameScore(int newScore)
    {
        _gameScore = newScore;
        text.text = "Score: "+_gameScore+" Points";
    }
}
