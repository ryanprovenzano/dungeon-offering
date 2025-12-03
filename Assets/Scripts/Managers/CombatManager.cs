using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    EntityStats player;
    EntityStats enemy;

    int currentPlayerHp;
    int currentEnemyHp;

    void Awake()
    {
        player = Resources.Load<EntityStats>("PlayerStats");
        enemy = Resources.Load<EntityStats>("BossStats");
    }

    void Start()
    {
        //Initialize entity values
        currentPlayerHp = player.maxHp;
        currentEnemyHp = enemy.maxHp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AttackTarget(string targetType)
    {
        switch (targetType)
        {
            case "Player":
                currentEnemyHp = -player.attack;
                break;
            case "Enemy":
                currentPlayerHp = -enemy.attack;
                break;
            default:
                Debug.Log("No target found for attack");
                break;
        }
    }
}
