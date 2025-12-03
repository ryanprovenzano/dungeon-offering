using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    HPBarController hpBarControl;

    void Awake()
    {
        GameObject hpBar = GameObject.FindWithTag("HPBar");
        hpBarControl = hpBar.GetComponent<HPBarController>();
    }

    void Start()
    {
        //Todo: Get state from PlayerCombat
        hpBarControl.SetInitialHp(900);

    }

    // Update is called once per frame
    void Update()
    {
        //hpBarControl.UpdateHpToDisplay()

    }
}
