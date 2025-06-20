using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDestructionScript : MonoBehaviour
{
    Tilemap tilemp; 
    
    void Start() {

        tilemp = GameObject.Find("Destructible").GetComponent<Tilemap>();

    }

    void Update() {

        // Using mouse for now

        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

        if (Input.GetMouseButtonDown(0)) {

            Vector3Int selectedTile = tilemp.WorldToCell(point);
            tilemp.SetTile(selectedTile, null);
        } 

    }
}
