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
        (EntityController playerController, EntityController enemyController) = CombatManager.Instance.GetCombatantControllers();

        playerHpBarController.InitializeHpBar(playerController);
        enemyHpBarController.InitializeHpBar(enemyController);

    }

    public void UpdateHp()
    {
        enemyHpBarController.WakeHpBar();
        playerHpBarController.WakeHpBar();
    }
}
