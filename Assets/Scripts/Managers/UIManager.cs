using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    HPBarController playerHpBarControl;
    HPBarController enemyHpBarControl;

    void Awake()
    {
        playerHpBarControl = GameObject.FindWithTag("PlayerHPBar").GetComponent<HPBarController>();
        enemyHpBarControl = GameObject.FindWithTag("EnemyHPBar").GetComponent<HPBarController>();

        Instance = this;
    }

    void Start()
    {
        InitializeHpBars();
    }

    // Update is called once per frame
    void Update()
    {
        //hpBarControl.UpdateHpToDisplay(CombatManager.Instance.currentPlayerHp);

    }

    private void InitializeHpBars()
    {
        (int playerCurrentHp, int playerMaxHp, int enemyCurrentHp, int enemyMaxHp) = CombatManager.Instance.GetCombatantsHpValues();
        playerHpBarControl.SetInitialHp(playerCurrentHp, playerMaxHp);
        enemyHpBarControl.SetInitialHp(enemyCurrentHp, enemyMaxHp);
    }

    public void UpdateHp(string targetType, int currentHp)
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
