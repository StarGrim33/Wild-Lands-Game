using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber;
    private float _timeAfterLastSpawn;
    private int _spawned;
    public event UnityAction AllEnemySpawned;
    public event UnityAction<int, int> EnemyCountChanged;
    private int random;
    

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        random  = Random.Range(1, _waves.Count);

        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
            EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
        }

        if (_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _currentWaveNumber + 1)
                AllEnemySpawned?.Invoke();

            _currentWave = null;
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Dying += OnEnemyDying;
        enemy.Init(_player);
    }

    private void SetWave(int index)
    {
        index = Mathf.Min(index, _waves.Count - 1);
        _currentWave = _waves[random];
        EnemyCountChanged?.Invoke(0, 1);
    }

    public void NextWave()
    {
        _currentWaveNumber++;
        SetWave(_currentWaveNumber);
        _spawned = 0;
    }
    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _player.AddMoney(enemy.Reward);
    }


}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}
