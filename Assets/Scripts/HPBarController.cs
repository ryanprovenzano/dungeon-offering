using UnityEngine;

public class HPBarController : MonoBehaviour
{
    private RectTransform rt;
    public int currentHp;
    private int maxHp;
    private int interpolatedHp;
    private int previousHp;
    //Harder hits decrease the HP bar faster
    private float hpBarAnimDuration = 1f;
    private float timeElapsed = 0;
    private readonly int fullWidth = 354;

    void Awake()
    {
        rt = GetComponent<RectTransform>();

    }

    void Start()
    {

    }

    // Update is called once per frame IF the component is enabled!
    void Update()
    {
        // If HP Bar has not yet become the current hp
        if (currentHp != interpolatedHp)
        {
            HpUpdateHandler();
        }
        else
        {
            // HP Bar transition completed, store current hp into previous hp, and disable to stop Update calls
            previousHp = currentHp;
            timeElapsed = 0;
            enabled = false;
        }
    }

    public void NotifyHpBar(int currentHp)
    {
        enabled = true;
        this.currentHp = currentHp;
    }

    private void HpUpdateHandler()
    {
        timeElapsed += Time.deltaTime;
        float interpolationRatio = timeElapsed / hpBarAnimDuration;
        interpolatedHp = (int)Mathf.SmoothStep(previousHp, currentHp, interpolationRatio);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, CalcBarWidth());
    }

    private float CalcBarWidth()
    {
        return (float)interpolatedHp / maxHp * fullWidth;
    }

    // Call this from UIManager within Start() function.
    public void InitializeHpBar(int currentHp, int maxHp)
    {
        this.currentHp = currentHp;
        this.maxHp = maxHp;

        interpolatedHp = previousHp = currentHp;

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, CalcBarWidth());
        enabled = false;
    }
}
