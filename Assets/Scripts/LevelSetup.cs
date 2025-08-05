using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelSetup : MonoBehaviour
{
    void Start()
    {
        LevelManager.SetTileMap(GetComponent<Tilemap>());
    }
}