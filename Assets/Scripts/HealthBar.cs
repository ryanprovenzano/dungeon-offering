using System;
using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private RectTransform rt;
    private int interpolatedHp;
    //Harder hits decrease the HP bar faster
    private float hpBarAnimDuration = 1f;
    private readonly int fullWidth = 354;
    private Health _healthComponent;
    private CombatManager _combatManager;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        _healthComponent = GetComponent<Health>();

        if (gameObject.tag == "PlayerHPBar")
        {
            _healthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        }
        else if (gameObject.tag == "EnemyHPBar")
        {
            _healthComponent = GameObject.FindGameObjectWithTag("Boss").GetComponent<Health>();
        }


    }

    void Start()
    {
        _combatManager = CombatManager.instance;
        interpolatedHp = _healthComponent.Current;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, CalcBarWidth());
        _combatManager.HpCheck += HpCheckHandler;
        _combatManager.GameFinished += GameFinishedHandler;
    }



    private void OnDisable()
    {
        _combatManager.HpCheck -= HpCheckHandler;
        _combatManager.GameFinished -= GameFinishedHandler;
    }

    private void GameFinishedHandler(object sender, EventArgs e)
    {
        var healthPanel = gameObject.GetComponentInParent<HealthPanelUI>();
        Debug.Assert(healthPanel != null);

        if (_healthComponent.IsDepleted())
        {
            Destroy(healthPanel.gameObject);
        }
    }

    private void HpCheckHandler(object sender, EventArgs e)
    {
        StartCoroutine(TransitionHp());
    }

    private IEnumerator TransitionHp()
    {
        float previousHp = interpolatedHp;
        float timeElapsed = 0;
        float interpolationRatio;

        while (interpolatedHp != _healthComponent.Current)
        {
            timeElapsed += Time.deltaTime;
            interpolationRatio = EaseOutQuad(timeElapsed / hpBarAnimDuration);
            interpolatedHp = (int)Mathf.SmoothStep(previousHp, _healthComponent.Current, interpolationRatio);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, CalcBarWidth());

            yield return null;
        }
    }

    // Custom ease-out (fast start, slow end) for snappier HP bar animation
    float EaseOutQuad(float t)
    {
        return t * (2 - t);
    }

    private float CalcBarWidth()
    {
        return (float)interpolatedHp / _healthComponent.Max * fullWidth;
    }
}
