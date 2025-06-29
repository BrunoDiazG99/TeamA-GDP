using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInfoManager : MonoBehaviour
{
    // Bomb Information and Variables
    [SerializeField]
    GameObject bomba;
    [SerializeField]
    int numBombas = 0;
    [SerializeField]
    TextMeshProUGUI bombText;
    InputAction placeBombAction;


    // Health Information and Variables
    [SerializeField]
    int health;
    [SerializeField]
    int maxHealth;
    [SerializeField]
    Image[] healthIcons;
    [SerializeField]
    Sprite fullHeart;
    [SerializeField]
    Sprite emptyHeart;

    private void Awake()
    {

    }

    void Start()
    {
        placeBombAction = InputSystem.actions.FindAction("Interact");

        GameEvents.current.onBombPickup += BombPickedUp;
        GameEvents.current.onEnemyDamage += TakeDamage;
    }

    private void Update()
    {
        if (placeBombAction.WasPressedThisFrame())
        {
            Debug.Log("E pressed");
            PlaceBomb();
        }
    }

    void UpdateHealth()
    {
        int i = 1;
        foreach (Image healthSlot in healthIcons)
        {
            if (healthSlot.enabled)
            {
                if (i <= health)
                {
                    healthSlot.sprite = fullHeart;
                }
                else
                {
                    healthSlot.sprite = emptyHeart;
                }
            }
            i++;
        }
    }

    void TakeDamage()
    {
        health -= 1;
        UpdateHealth();
        if (health == 0)
        {
            // game over
            Debug.Log("Game should end");
        }

    }

    void UpdateBombText()
    {
        bombText.text = "x " + numBombas.ToString();

    }

    void BombPickedUp()
    {
        numBombas++;
        UpdateBombText();
    }

    void PlaceBomb()
    {
        if(numBombas >= 1)
        {

            Transform currentPosition = gameObject.transform;

            Instantiate(bomba, currentPosition.position, Quaternion.identity);

            numBombas--;
            UpdateBombText();

        }

    }
}
