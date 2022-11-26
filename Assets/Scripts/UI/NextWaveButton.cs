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

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    public event UnityAction<int> WavesChange;
    public int WaveNumber;
    public int BestWaveNumber;

    private void Awake()
    {
        _spawner.AllEnemySpawned += OnAllEnemySpawned;
        BestWaveNumber = Progress.Instance.PlayerInfo.BestScore;
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
