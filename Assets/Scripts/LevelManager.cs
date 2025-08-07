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

    public static void ReplaceTileAndTransferProperties(Vector3Int location, TileBase tile)
    {
        int purificationPower = 0;
        float timeSincePurificationStart = 0.0f;
        bool purificationStarted = false;
        if (_tilemap.GetInstantiatedObject(location).TryGetComponent<IPurifiable>(out IPurifiable purifiable))
        {
            purificationPower = purifiable.purificationPower;
            timeSincePurificationStart = purifiable.timeSincePurificationStart;
            purificationStarted = purifiable.purificationStarted;
        }
        _tilemap.SetTile(location, tile);
        if (_tilemap.GetInstantiatedObject(location).TryGetComponent<IPurifiable>(out IPurifiable purifiableOut))
        {
            purifiableOut.purificationPower = purificationPower;
            purifiableOut.timeSincePurificationStart = timeSincePurificationStart;
            purifiableOut.purificationStarted = purificationStarted;
        }
    }

    public static List<Vector3Int> GetAdjacentTileLocations(Vector3Int location)
    {
        List<Vector3Int> adjacentTiles = new List<Vector3Int>()
        {
            location + new Vector3Int(-1, 0),
            location + new Vector3Int(1, 0),
            location + new Vector3Int(0, -1),
            location + new Vector3Int(0, 1)
        };
        return adjacentTiles;
    }
}