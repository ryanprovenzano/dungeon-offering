using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    HPBarController playerHpBarController;
    HPBarController enemyHpBarController;

    void Awake()
    {
        Instance = this;
        playerHpBarController = GameObject.FindWithTag("PlayerHPBar").GetComponent<HPBarController>();
        enemyHpBarController = GameObject.FindWithTag("EnemyHPBar").GetComponent<HPBarController>();
    }

    void Start()
    {
        (PlayerController playerController, EnemyController enemyController) = CombatManager.Instance.GetCombatantControllers();

        playerHpBarController.InitializeHpBar(playerController.CurrentHp, playerController.stats.maxHp);
        enemyHpBarController.InitializeHpBar(enemyController.CurrentHp, enemyController.stats.maxHp);

    }

    public void UpdateHp(int enemyCurrentHp, int playerCurrentHp)
    {
        enemyHpBarController.NotifyHpBar(enemyCurrentHp);
        playerHpBarController.NotifyHpBar(playerCurrentHp);
    }
}
