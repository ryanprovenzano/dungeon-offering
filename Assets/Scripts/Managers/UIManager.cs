using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    HPBarController playerHpBarControl;
    HPBarController enemyHpBarControl;

    void Awake()
    {
        GameObject playerHpBar = GameObject.FindWithTag("PlayerHPBar");
        playerHpBarControl = playerHpBar.GetComponent<HPBarController>();

        GameObject enemyHpBar = GameObject.FindWithTag("EnemyHPBar");
        enemyHpBarControl = enemyHpBar.GetComponent<HPBarController>();

        Instance = this;
    }

    void Start()
    {
        playerHpBarControl.SetInitialHp(CombatManager.Instance.playerCurrentHp, CombatManager.Instance.playerMaxHp);
        enemyHpBarControl.SetInitialHp(CombatManager.Instance.enemyCurrentHp, CombatManager.Instance.enemyMaxHp);

    }

    // Update is called once per frame
    void Update()
    {
        //hpBarControl.UpdateHpToDisplay(CombatManager.Instance.currentPlayerHp);

    }

    void UpdateHp(string targetType, int currentHp)
    {
        switch (targetType)
        {
            case "Enemy":
                enemyHpBarControl.UpdateHpToDisplay(currentHp);
                break;
            case "Player":
                playerHpBarControl.UpdateHpToDisplay(currentHp);
                break;
            default:
                Debug.Log("No hp bar found");
                break;
        }
    }

}
