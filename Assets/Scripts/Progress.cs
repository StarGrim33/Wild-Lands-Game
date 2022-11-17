using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int Money;
    public List<WeaponItem> WeaponItems;
}
public class Progress : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] TextMeshProUGUI _playerInfoText;
    public PlayerInfo PlayerInfo;
    public List <WeaponItem> WeaponItems;

    [DllImport("_Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("_Internal")]
    private static extern void LoadExtern();
    public static Progress Instance;

    private void Awake()
    {

        if(Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
#if UNITY_WEBGL && !UNITY_EDITOR
            LoadExtern();
#endif
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadWeapons(List<WeaponItem> weaponItems)
    {
        player._weapons.Clear();

        foreach (var weaponItem in weaponItems)
        {
            player.AddWeaponToList(weaponItem);
        }

        
    }
#if UNITY_WEBGL && !UNITY_EDITOR
    public void Save() // Сохраняем объект на сервер по пути: Unity - SDK - JS
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        string weaponsString = string.Join(",", WeaponItems);
        //SaveExtern(jsonString);
        //SaveExtern(weaponsString);
    }
#endif
    public void SetPlayerInfo(string value)  // Получаем объект из javaScript (по пути: Unity - SDK - JS)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        _playerInfoText.text = PlayerInfo.Money.ToString();
    }
}
