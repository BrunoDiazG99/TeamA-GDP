using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels; // Array de niveles
    private int currentLevelIndex = 0;
    public static LevelManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) Instance = this;
        // Al iniciar, solo el primer nivel está activo
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == currentLevelIndex);
        }
    }
    void Start()
    {
        ActivateLevel(currentLevelIndex); // Activa solo el primer nivel
    }

    public void ActivateLevel(int index)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == index); // Solo activa el nivel actual
        }
        currentLevelIndex = index;
        // Mueve al jugador al StartPoint del nuevo nivel
        Transform startPoint = levels[index].transform.Find("StartPoint");
        if (startPoint)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = startPoint.position;
        }
    }

}
