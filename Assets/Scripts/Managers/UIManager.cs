using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    HPBarController playerHpBarController;
    HPBarController enemyHpBarController;

    void Awake()
    {
        instance = this;
        playerHpBarController = GameObject.FindWithTag("PlayerHPBar").GetComponent<HPBarController>();
        enemyHpBarController = GameObject.FindWithTag("EnemyHPBar").GetComponent<HPBarController>();
    }

    void Start()
    {
        (PlayerController playerController, EnemyController enemyController) = CombatManager.instance.GetCombatantControllers();

        playerHpBarController.InitializeHpBar(playerController.CurrentHp, playerController.stats.MaxHp);
        enemyHpBarController.InitializeHpBar(enemyController.CurrentHp, enemyController.stats.MaxHp);

    }

    public void UpdateHp(int enemyCurrentHp, int playerCurrentHp)
    {
        enemyHpBarController.NotifyHpBar(enemyCurrentHp);
        playerHpBarController.NotifyHpBar(playerCurrentHp);
    }
}
