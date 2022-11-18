using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Hello();

    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] RawImage _photo;
    

    [DllImport("__Internal")]
    private static extern void GiveMePlayerData();

    [DllImport("__Internal")]
    private static extern void RateGame();

    public void SetName(string name)
    {
        _nameText.text = name;
    }
    public void HelloButton()
    {
        GiveMePlayerData();
    }

}
