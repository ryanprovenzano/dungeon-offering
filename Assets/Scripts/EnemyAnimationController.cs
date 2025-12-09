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
        animator = GetComponentInChildren<Animator>();
    }


    void Start()
    {
        Debug.Log("Running the Start method");
        enemyStats = GetComponent<EnemyController>().stats;
        combatManager = CombatManager.instance;
        combatManager.OnEnemyAttackBegins += BeginAttackAnimation;

    }

    void OnEnable()
    {

    }

    void OnDisable()
    {
        combatManager.OnEnemyAttackBegins -= BeginAttackAnimation;
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

    public void BeginAttackAnimation(object sender, EventArgs e)
    {
        animator.SetTrigger("AttackAnimTrig");
        AudioClip audioClip = AudioManager.Instance.GetMeleeSoundClip();
        StartCoroutine(AudioManager.Instance.PlayDelayedSound(audioClip, CalcAttackSFXDelay(audioClip)));
        // call or set animation bool here, or make the above an animation bool
    }

    private float CalcAttackSFXDelay(AudioClip audioClip)
    {
        return (float)enemyStats.TimeUntilContact - audioClip.length / 1.7f;
    }
}
