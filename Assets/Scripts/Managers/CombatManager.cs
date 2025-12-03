using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }
    EntityStats player;
    EntityStats enemy;

    public int currentPlayerHp { get; private set; }
    public int currentEnemyHp { get; private set; }

    void Awake()
    {
        player = Resources.Load<EntityStats>("PlayerStats");
        enemy = Resources.Load<EntityStats>("BossStats");
        Instance = this;
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
