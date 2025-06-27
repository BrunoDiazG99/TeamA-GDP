using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDestructionScript : MonoBehaviour
{
    Tilemap tilemp;
    public Transform player;
    void Start()
    {

        tilemp = GameObject.Find("Destructible").GetComponent<Tilemap>();

    }

    void Update()
    {

        // Using mouse for now

        /* Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int selectedTile = tilemp.WorldToCell(point);
            tilemp.SetTile(selectedTile, null);
        } */

        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int selectedTile = tilemp.WorldToCell(point);
            Vector3 positionTile = tilemp.CellToWorld(selectedTile);

            float distancia = Vector3.Distance(positionTile, player.position);
            if (distancia <= 1.5f)
            {
                tilemp.SetTile(selectedTile, null);
            }
        }



    }

}
