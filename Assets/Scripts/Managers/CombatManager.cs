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

    /// <summary>
    /// Expects "Enemy" or "Player" as arguments
    /// </summary>
    /// <param name="targetType"></param>
    public void AttackTarget(string targetType)
    {

        switch (targetType)
        {
            case "Enemy":
                currentEnemyHp = -player.attack;
                break;
            case "Player":
                currentPlayerHp = -enemy.attack;
                break;
            default:
                Debug.Log("No target found for attack");
                break;
        }
    }
}
