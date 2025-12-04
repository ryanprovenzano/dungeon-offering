using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }


    EntityStats playerStats;
    EntityStats enemyStats;

    //Player
    EntityController playerController;

    //Enemy
    EntityController enemyController;

    void Awake()
    {
        playerStats = Resources.Load<EntityStats>("Player");
        enemyStats = Resources.Load<EntityStats>("Boss");

        enemyController = GameObject.FindWithTag("Boss").GetComponent<EntityController>();
        playerController = GameObject.FindWithTag("Player").GetComponent<EntityController>();
        Instance = this;
    }

    /// <summary>
    /// Expects "Enemy" or "Player" as arguments. The recipient of the attack.
    /// </summary>
    /// <param name="targetType"></param>
    public void AttackTarget(string targetType)
    {
        switch (targetType)
        {
            case "Enemy":
                enemyController.currentHp -= playerStats.attack;
                UIManager.Instance.UpdateHp(targetType, enemyController.currentHp);
                break;
            case "Player":
                playerController.currentHp -= enemyStats.attack;
                UIManager.Instance.UpdateHp(targetType, playerController.currentHp);
                break;
            default:
                Debug.Log("No target found for attack");
                break;
        }
    }

    /// <summary>
    /// Gets Player's current hp & max hp, and then Enemy's current hp & max hp.
    /// </summary>
    /// <returns></returns>
    public (int, int, int, int) GetCombatantsHpValues()
    {
        return (playerController.currentHp, playerStats.maxHp, enemyController.currentHp, enemyStats.maxHp);
    }

}

