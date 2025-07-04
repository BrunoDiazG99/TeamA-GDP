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

    public event Action onHeartPickup;
    public void HeartPickup()
    {
        onHeartPickup?.Invoke();
    }


    // Player Events

    public event Action onEnemyDamage;
    public void EnemyDamage()
    {
        onEnemyDamage?.Invoke();
    }

    // Game Events

    public event Action onGameOver;
    public void GameOver()
    {
        onGameOver?.Invoke();
    }

    public event Action onGameFinish;
    public void GameFinish()
    {
        onGameFinish?.Invoke();
    }

}
