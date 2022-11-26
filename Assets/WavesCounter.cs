using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class WavesCounter : MonoBehaviour
{
    [SerializeField] private NextWaveButton nextWaveButton;
    [SerializeField] private TMP_Text _waves;
    [SerializeField] private TMP_Text _bestWaves;

    public int BestWaveNumber { get; private set; }

    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
        _waves.text = nextWaveButton.WaveNumber.ToString();
        _bestWaves.text = nextWaveButton.WaveNumber.ToString();
        nextWaveButton.WavesChange += WavesChange;
    }

    public void WavesChange(int WaveNumber)
    {
        _waves.text = WaveNumber.ToString();
        _bestWaves.text = BestWaveNumber.ToString();
    }
    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }
    public void GetLoad()
    {
        _bestWaves.text = YandexGame.savesData.BestWaveNumber.ToString();
    }

    public void MySave()
    {
        YandexGame.savesData.BestWaveNumber = BestWaveNumber;
        YandexGame.SaveProgress();
    }

}
