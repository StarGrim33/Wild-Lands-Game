using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Button _nextWaveButton;

    [DllImport("_Internal")]
    private static extern void ShowAdv();

    public event UnityAction<int> WavesChange;
    public int WaveNumber;
    private void Awake()
    {
        _spawner.AllEnemySpawned += OnAllEnemySpawned;
    }

    private void OnDestroy()
    {
        _spawner.AllEnemySpawned -= OnAllEnemySpawned;
    }

    public void OnAllEnemySpawned()
    {
        _nextWaveButton.gameObject.SetActive(true);
    }

    public void OnNextWaveButtonClick()
    {
        WaveNumber++;
        _spawner.NextWave();
        _nextWaveButton.gameObject.SetActive(false);
        WavesChange?.Invoke(WaveNumber);
    }

}
