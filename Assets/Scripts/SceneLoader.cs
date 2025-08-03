using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Canvas settingsPanel;
    private static List<Tuple<int, int>> resolutionList = new List<Tuple<int, int>>();
    private static int resolutionIndex;
    private static bool fullScreen;

    void Start()
    {
        TMP_Dropdown resolutionDropDown = settingsPanel.transform.Find("Background").Find("Resolution").GetComponent<TMP_Dropdown>();
        foreach (var setting in resolutionDropDown.options)
        {
            string[] resolution = setting.text.Split('x');
            resolutionList.Add(new Tuple<int, int>(Int32.Parse(resolution[0]), Int32.Parse(resolution[1])));
            Debug.Log("Found resolution " + resolution[0] + "x" + resolution[1]);
        }
        LevelManager.Init();
    }

    public void LoadLevel(int level)
    {
        LevelManager.LoadLevel(level);
    }

    public void SetResolution(int option)
    {
        resolutionIndex = option;
        RefreshScreenSettings();
    }

    public void SetFullScreen(bool option)
    {
        fullScreen = option;
        RefreshScreenSettings();
    }

    private void RefreshScreenSettings()
    {
        Screen.SetResolution(resolutionList[resolutionIndex].Item1, resolutionList[resolutionIndex].Item2, fullScreen);
        Debug.Log("Resolution changed to " + Screen.width + "x" + Screen.height);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void EnableSettingsPanel(bool enable)
    {
        settingsPanel.gameObject.SetActive(enable);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}