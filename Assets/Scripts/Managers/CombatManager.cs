using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }
    EntityStats player;
    EntityStats enemy;

    //Player
    public int playerCurrentHp { get; private set; }
    public int playerMaxHp { get; private set; }

    //Enemy
    public int enemyCurrentHp { get; private set; }
    public int enemyMaxHp { get; private set; }

    void Awake()
    {
        player = Resources.Load<EntityStats>("PlayerStats");
        enemy = Resources.Load<EntityStats>("BossStats");
        Instance = this;
    }

    void Start()
    {
        //Initialize entity values
        (playerCurrentHp, playerMaxHp) = (player.maxHp, player.maxHp);
        (enemyCurrentHp, enemyMaxHp) = (enemy.maxHp, enemy.maxHp);
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
                enemyCurrentHp = -player.attack;
                break;
            case "Player":
                playerCurrentHp = -enemy.attack;
                break;
            default:
                Debug.Log("No target found for attack");
                break;
        }
    }
}
