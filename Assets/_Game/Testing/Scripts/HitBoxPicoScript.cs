using UnityEngine;
using UnityEngine.Tilemaps;
public class HitoBoxPicoScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Tilemap tilempDestructible;


    //sas
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // Using botton "Space"
    void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 point = other.ClosestPoint(transform.position);
        Vector3Int selectedTile = tilempDestructible.WorldToCell(point);

        if (tilempDestructible.HasTile(selectedTile))
        {
            tilempDestructible.SetTile(selectedTile, null);
        }
    }
}
