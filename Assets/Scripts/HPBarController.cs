using UnityEngine;

public class HPBarController : MonoBehaviour
{
    private RectTransform rt;
    private int maxHp;
    private int interpolatedHp;
    private int previousHp;
    //Harder hits decrease the HP bar faster
    private float hpBarAnimDuration = 1f;
    private float timeElapsed = 0;
    private readonly int fullWidth = 354;

    //Reference to player's controller for HP
    public EntityController entityController;

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
        if (entityController.CurrentHp != interpolatedHp)
        {
            HandleHpUpdate();
        }
        else
        {
            // HP Bar transition completed, store current hp into previous hp, and disable to stop Update calls
            previousHp = entityController.CurrentHp;
            timeElapsed = 0;
            enabled = false;
        }
    }

    public void WakeHpBar()
    {
        enabled = true;
    }

    private void HandleHpUpdate()
    {
        timeElapsed += Time.deltaTime;
        float interpolationRatio = timeElapsed / hpBarAnimDuration;
        interpolatedHp = (int)Mathf.SmoothStep(previousHp, entityController.CurrentHp, interpolationRatio);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, CalcBarWidth());
    }

    private float CalcBarWidth()
    {
        return (float)interpolatedHp / maxHp * fullWidth;
    }

    // Call this from UIManager within Start() function.
    public void InitializeHpBar(EntityController controller)
    {
        this.entityController = controller;
        maxHp = entityController.stats.maxHp;

        interpolatedHp = previousHp = entityController.CurrentHp;

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, CalcBarWidth());
        enabled = false;
    }
}
