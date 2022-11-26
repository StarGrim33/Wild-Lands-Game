using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

public class NextWaveButton : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Button _nextWaveButton;
    public WavesCounter WavesCounter;

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    public event UnityAction<int> WavesChange;
    public int WaveNumber;
    public int BestWaveNumber;

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
        ShowAdv();
        WaveCounter();
        BestScoreCounter();
        WavesCounter.MySave();
        _spawner.NextWave();
        _nextWaveButton.gameObject.SetActive(false);
        WavesChange?.Invoke(WaveNumber);
    }
    public void WaveCounter()
    {
        WaveNumber++;
        BestScoreCounter();
    }
    public void BestScoreCounter()
    {
        if (WaveNumber > BestWaveNumber)
        {
            BestWaveNumber = WaveNumber;
        }
    }

}
