using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }

    //Player
    EntityController playerController;

    //Enemy
    EntityController enemyController;

    //State
    private string turnStatus = "Player";

    void Awake()
    {
        enemyController = GameObject.FindWithTag("Boss").GetComponent<EntityController>();
        playerController = GameObject.FindWithTag("Player").GetComponent<EntityController>();
        Instance = this;
    }

    /// <summary>
    /// Expects "Enemy" or "Player" as arguments. The recipient of the attack.
    /// </summary>
    /// <param name="targetType"></param>
    public void ConductCombat()
    {
        if (turnStatus == "Player")
        {
            enemyController.ReduceHp(playerController.stats.attack);
            turnStatus = "Enemy";
        }
        else
        {
            playerController.ReduceHp(enemyController.stats.attack);
            turnStatus = "Player";
        }

        UIManager.Instance.UpdateHp();
    }

    /// <summary>
    /// Returns the player's EntityController and enemy's EntityController respectively
    /// </summary>
    /// <returns></returns>
    public (EntityController, EntityController) GetCombatantControllers()
    {
        return (playerController, enemyController);
    }

}

