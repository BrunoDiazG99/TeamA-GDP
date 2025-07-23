using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
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
    [SerializeField]
    float invulnerableTime;
    BoxCollider2D hitBoxCollider;
    [SerializeField] private SpriteRenderer spRender;
    [SerializeField] private Material whiteShader;
    private Material materialOriginal;
    private void Awake()
    {
        hitBoxCollider = gameObject.GetComponent<BoxCollider2D>();
        GameObject vidaInCanvas = GameObject.FindGameObjectWithTag("PlayerHealth").gameObject;
        healthIcons = vidaInCanvas.GetComponentsInChildren<Image>();

    }

    void Start()
    {
        materialOriginal = spRender.material;//Guardar material original
        placeBombAction = InputSystem.actions.FindAction("Interact");

        GameEvents.current.onBombPickup += BombPickedUp;
        GameEvents.current.onHeartPickup += HeartPickup;
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

    public bool HasFullHealth()
    {
        return health >= maxHealth;
    }

    void HeartPickup()
    {
        health += 1;
        UpdateHealth();
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
    private void ActivateHearts(int maxHealth)
    {
        int i = 1;
        foreach (Image healthSlot in healthIcons)
        {
            if (i <= maxHealth)
            {
                healthSlot.enabled = true;
            }
            i++;
        }
    }

    void TakeDamage()
    {

        AudioManager.instance.PlaySound("sf_damage");
        health -= 1;
        UpdateHealth();
        if (health == 0)
        {
            // game over
            Debug.Log("Game should end");
            AudioManager.instance.PlaySound("sfx_death");
            GameEvents.current.GameOver();
        }
        StartCoroutine(InvulnerableTime());

    }

    IEnumerator InvulnerableTime()
    {
        hitBoxCollider.enabled = false;
        for (int i = 0; i < 2; i++)//Efecto Shader
        {
            spRender.material = whiteShader;
            yield return new WaitForSeconds(0.1f); // tiempo visible del blanco
            spRender.material = materialOriginal;
            yield return new WaitForSeconds(0.1f); // tiempo visible del normal
        }
        yield return new WaitForSeconds(invulnerableTime);
        hitBoxCollider.enabled = true;
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
        if (numBombas >= 1)
        {

            Transform currentPosition = gameObject.transform;

            Instantiate(bomba, currentPosition.position, Quaternion.identity);

            numBombas--;
            UpdateBombText();

        }

    }
}
