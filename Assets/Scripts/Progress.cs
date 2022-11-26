using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int BestScore;
}
public class Progress : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] TextMeshProUGUI _playerInfoText;
    public PlayerInfo PlayerInfo;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    public static Progress Instance;

    private void Awake()
    {

        if(Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
