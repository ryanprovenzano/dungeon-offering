using UnityEngine;

public class HPBarController : MonoBehaviour
{
    private RectTransform rt;
    private int maxHp;
    private int currentHp;
    private int previousHp;
    private int hpToDisplay;
    //Harder hits decrease the HP bar faster
    private float hpBarAnimDuration = 1f;
    private float timeElapsed = 0;
    private int fullWidth = 354;
    private float currentWidth = 354;


    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame IF the component is enabled!
    void Update()
    {
        // If HP Bar has not yet become the current hp
        if (hpToDisplay != currentHp)
        {
            HandleHpUpdate();
        }
        else
        {
            // HP Bar transition completed, store current hp into previous hp, and disable to stop Update calls
            previousHp = currentHp;
            timeElapsed = 0;
            enabled = false;
        }
    }

    public void UpdateHpToDisplay(int hp)
    {
        hpToDisplay = hp;
        enabled = true;
    }

    private void HandleHpUpdate()
    {
        timeElapsed += Time.deltaTime;
        float interpolationRatio = timeElapsed / hpBarAnimDuration;
        hpToDisplay = (int)Mathf.SmoothStep(previousHp, currentHp, interpolationRatio);
        float hpRatio = (float)hpToDisplay / maxHp;

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hpRatio * fullWidth);
    }


    // Call this from UIManager within Start() function.
    public void SetInitialHp(int currentHp, int maxHp)
    {
        //Get player's HP TODO: Access Player's health state here
        (this.currentHp, previousHp, hpToDisplay, this.maxHp) = (currentHp, currentHp, currentHp, maxHp);
        float hpRatio = (float)currentHp / maxHp;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hpRatio * fullWidth);
        enabled = false;
    }
}
