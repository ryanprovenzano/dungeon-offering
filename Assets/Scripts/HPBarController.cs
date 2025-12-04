using UnityEngine;

public class HPBarController : MonoBehaviour
{
    private RectTransform rt;
    private int maxHp;
    private int currentlyDisplayedHp;
    private int previousHp;
    private int targetHp;
    //Harder hits decrease the HP bar faster
    private float hpBarAnimDuration = 1f;
    private float timeElapsed = 0;
    private int fullWidth = 354;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame IF the component is enabled!
    void Update()
    {
        // If HP Bar has not yet become the current hp
        if (targetHp != currentlyDisplayedHp)
        {
            HandleHpUpdate();
        }
        else
        {
            // HP Bar transition completed, store current hp into previous hp, and disable to stop Update calls
            previousHp = currentlyDisplayedHp;
            timeElapsed = 0;
            enabled = false;
        }
    }

    public void UpdateHpToDisplay(int hp)
    {
        targetHp = hp;
        enabled = true;
    }

    private void HandleHpUpdate()
    {
        timeElapsed += Time.deltaTime;
        float interpolationRatio = timeElapsed / hpBarAnimDuration;
        currentlyDisplayedHp = (int)Mathf.SmoothStep(previousHp, targetHp, interpolationRatio);
        float hpRatio = (float)currentlyDisplayedHp / maxHp;

        Debug.Log("uhhhhhhh" + " " + interpolationRatio.ToString() + " " + targetHp.ToString() + " " + hpRatio.ToString());
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hpRatio * fullWidth);
    }


    // Call this from UIManager within Start() function.
    public void SetInitialHp(int currentHp, int maxHp)
    {
        //Get player's HP TODO: Access Player's health state here
        (this.currentlyDisplayedHp, previousHp, targetHp, this.maxHp) = (currentHp, currentHp, currentHp, maxHp);
        float hpRatio = (float)currentHp / maxHp;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hpRatio * fullWidth);
        enabled = false;
    }
}
