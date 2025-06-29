using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BombaScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float timeForExplosion;
    public float timeForDestructionAnimation;


    private Tilemap destructibles;
    private CircleCollider2D explosionHitBox;

    private void Awake()
    {
        destructibles = GameObject.FindGameObjectWithTag("DestructibleTilemap").gameObject.GetComponent<Tilemap>();

        explosionHitBox = gameObject.GetComponent<CircleCollider2D>();
        explosionHitBox.enabled = false;
    }
    private void Start()
    {
        StartCoroutine(StartBombCountdown());
    }

    private IEnumerator StartBombCountdown()
    {
        yield return new WaitForSeconds(timeForExplosion);
        explosionHitBox.enabled=true;

        // set explosion animation 
        yield return new WaitForSeconds(timeForDestructionAnimation);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos que este objeto (el que colisiona) sea el tilemap o un objeto relevante
        if (collision.gameObject == destructibles.gameObject)
        {
            // Obtenemos los bounds del collider de este GameObject (la hitbox)
            Bounds bounds = GetComponent<Collider2D>().bounds;

            Debug.Log(bounds);

            // Convertimos los bounds del mundo a celdas del tilemap
            Vector3Int min = destructibles.WorldToCell(bounds.min);
            Vector3Int max = destructibles.WorldToCell(bounds.max);

            Debug.Log("min: " + min);
            Debug.Log("max: " + max);

            // Iteramos sobre todas las celdas en el área
            for (int x = min.x; x <= max.x; x++)
            {
                for (int y = min.y; y <= max.y; y++)
                {
                    Vector3Int cellPos = new Vector3Int(x, y, 0);
                    TileBase tile = destructibles.GetTile(cellPos);

                    if (tile != null)
                    {
                        destructibles.SetTile(cellPos, null);
                        Debug.Log("Tile destruido en: " + cellPos);
                    }
                }
            }
        }
    }

}
