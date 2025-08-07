using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] counters;
    private int[] counts;
    public TileBase currentTile { private get; set; }
    Tilemap tilemap;

    private void Start()
    {
        tilemap = FindAnyObjectByType<Tilemap>();
        counts = new int[counters.Length];
    }

    public void PlaceBlock()
    {
        Vector3Int location = tilemap.WorldToCell(transform.position);
        GetComponent<Rigidbody2D>().linearVelocityY += 10.0f;
        tilemap.SetTile(location, currentTile);
    }

    public void IncrementCounter(int counterIndex)
    {
        counts[counterIndex]++;
        counters[counterIndex].text = "x " + counts[counterIndex];
    }
}