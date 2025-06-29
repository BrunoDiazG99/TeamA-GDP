using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }


    // Object Events

    public event Action onBombPickup;
    public void BombPickup()
    {
        onBombPickup?.Invoke();
    }


    // Player Events

    public event Action onEnemyDamage;
    public void EnemyDamage()
    {
        onEnemyDamage?.Invoke();
    }

}
