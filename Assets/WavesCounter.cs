using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WavesCounter : MonoBehaviour
{
    [SerializeField] private NextWaveButton nextWaveButton;
    [SerializeField] private TMP_Text _waves;
    
    private void OnEnable()
    {
        _waves.text = nextWaveButton.WaveNumber.ToString();
        nextWaveButton.WavesChange += WavesChange;
    }
    public void WavesChange(int WaveNumber)
    {
        _waves.text = WaveNumber.ToString();
    }

}
