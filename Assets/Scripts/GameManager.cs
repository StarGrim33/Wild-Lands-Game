using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu;

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    public void StartGame()
    {
        ShowAdv();
        SceneManager.LoadScene(1);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
