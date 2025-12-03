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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get player's HP
        (hp, previousHp) = (900, 900);

        rt = GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hpDisplayed);
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


            // HP Bar transition completed, store current hp into previous hp
            if (hpDisplayed == hp)
            {
                previousHp = hp;
                timeElapsed = 0;
            }
        }
        else
        {
            enabled = false;
        }

    }

    public void SetHpToDisplay(int hp)
    {
        this.hp = hp;
        enabled = true;
    }
}
