using UnityEngine;
using UnityEngine.Tilemaps;
public class HitoBoxPicoScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Tilemap tilempDestructible;



    void Awake()
    {
        tilempDestructible = GameObject.FindGameObjectWithTag("DestructibleTilemap").gameObject.GetComponent<Tilemap>();
        //extraemos el objeto qye tiene el tilemap 
    }
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyAI>().TakeDamage();
        }

        if (other.gameObject == tilempDestructible.gameObject)
        {
            AudioManager.instance.PlaySound("sf_pickaxe");
            // Obtenemos los bounds del collider de este GameObject (la hitbox)
            Bounds bounds = GetComponent<Collider2D>().bounds;

            Debug.Log(bounds);

            // Convertimos los bounds del mundo a celdas del tilemap
            Vector3Int min = tilempDestructible.WorldToCell(bounds.min);
            Vector3Int max = tilempDestructible.WorldToCell(bounds.max);

            Debug.Log("min: " + min);
            Debug.Log("max: " + max);

            // Iteramos sobre todas las celdas en el área
            for (int x = min.x; x <= max.x; x++)
            {
                for (int y = min.y; y <= max.y; y++)
                {
                    Vector3Int cellPos = new Vector3Int(x, y, 0);
                    TileBase tile = tilempDestructible.GetTile(cellPos);

                    if (tile != null)
                    {
                        tilempDestructible.SetTile(cellPos, null);
                        Debug.Log("Tile destruido en: " + cellPos);
                    }
                }
            }
        }
    }
}
