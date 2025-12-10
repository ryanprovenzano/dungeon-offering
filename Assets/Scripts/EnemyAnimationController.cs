using System;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator animator;
    private EntityStats enemyStats;
    private CombatManager combatManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        Debug.Log("Running the Start method");
        enemyStats = GetComponent<EnemyController>().stats;
        combatManager = CombatManager.instance;
        combatManager.EnemyAttackStarted += EnemyAttackStartedHandler;

    }

    void OnDisable()
    {
        combatManager.EnemyAttackStarted -= EnemyAttackStartedHandler;
    }

    void Update()
    {
        //Debug.Log(IsIdle());
    }


    public bool IsIdle()
    {
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animStateInfo.IsTag("Idle");

    }

    public void EnemyAttackStartedHandler(object sender, EventArgs e)
    {
        animator.SetTrigger("AttackAnimTrig");
        AudioClip audioClip = AudioManager.Instance.GetMeleeSoundClip();
        StartCoroutine(AudioManager.Instance.PlayDelayedSound(audioClip, CalcAttackSFXDelay(audioClip)));
        // call or set animation bool here, or make the above an animation bool
    }

    private float CalcAttackSFXDelay(AudioClip audioClip)
    {
        return (float)enemyStats.TimeUntilContact - audioClip.length / 1.8f;
    }
}
