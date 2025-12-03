using UnityEngine;

public class HPBarController : MonoBehaviour
{
    private RectTransform rt;
    private int hp;
    private int previousHp;
    private int hpDisplayed;
    //Harder hits decrease the HP bar faster
    private float hpBarAnimDuration = 1f;
    private float timeElapsed = 0;


    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame IF the component is enabled!
    void Update()
    {
        // If HP Bar has not yet become the current hp
        if (hpDisplayed != hp)
        {
            timeElapsed += Time.deltaTime;
            float interpolationRatio = timeElapsed / hpBarAnimDuration;
            hpDisplayed = (int)Mathf.SmoothStep(previousHp, hp, interpolationRatio);

            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hpDisplayed);
            Debug.Log("Ratio: " + interpolationRatio + " Elapsed frames: " + timeElapsed);
        }
        else
        {
            // HP Bar transition completed, store current hp into previous hp, and disable to stop Update calls
            previousHp = hp;
            timeElapsed = 0;
            enabled = false;
        }
    }

    public void UpdateHpToDisplay(int hp)
    {
        this.hp = hp;
        enabled = true;
    }

    // Call this from UIManager within Start() function.
    public void SetInitialHp(int hp)
    {
        //Get player's HP TODO: Access Player's health state here
        (this.hp, previousHp, hpDisplayed) = (hp, hp, hp);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hp);
        enabled = false;
    }
}
