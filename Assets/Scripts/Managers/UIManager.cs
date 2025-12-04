using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    HPBarController hpBarControl;

    void Awake()
    {
        GameObject hpBar = GameObject.FindWithTag("PlayerHPBar");
        hpBarControl = hpBar.GetComponent<HPBarController>();
    }

    void Start()
    {
        hpBarControl.SetInitialHp(CombatManager.Instance.playerCurrentHp, CombatManager.Instance.playerMaxHp);

    }

    // Update is called once per frame
    void Update()
    {
        //hpBarControl.UpdateHpToDisplay(CombatManager.Instance.currentPlayerHp);

    }
}
