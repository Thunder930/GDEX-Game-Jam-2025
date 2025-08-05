using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public static class LevelManager
{
    private static List<int> levels = new List<int>(); // Level's build index = levels[level number - 1]
    private static int currentLevelIndex;
    private static Tilemap _tilemap;
    public static void Init()
    {
        levels.Clear();
        currentLevelIndex = 0;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            if (scenePath.StartsWith("Assets/Scenes/Levels/"))
            {
                levels.Add(i);
            }
        }
    }

    public static void LoadLevel(int level)
    {
        currentLevelIndex = level - 1; // Account for 0 indexing
        SceneManager.LoadScene(levels[currentLevelIndex]);
    }

    public static void LoadNextLevel()
    {
        currentLevelIndex++;
        SceneManager.LoadScene(levels[currentLevelIndex]);
    }

    public static void SetTileMap(Tilemap tilemap)
    {
        _tilemap = tilemap;
    }

    public static void ReplaceTile(Vector3Int location, TileBase tile)
    {
        int purificationPower = 0;
        if (_tilemap.GetInstantiatedObject(location).TryGetComponent<IPurifyingPasser>(out IPurifyingPasser purifyingPasser))
        {
            purificationPower = purifyingPasser.purificationPower;
        }
        _tilemap.SetTile(location, tile);
        if (_tilemap.GetInstantiatedObject(location).TryGetComponent<IPurifyingPasser>(out IPurifyingPasser purifyingPasserOut))
        {
            purifyingPasserOut.purificationPower = purificationPower;
        }
    }
}