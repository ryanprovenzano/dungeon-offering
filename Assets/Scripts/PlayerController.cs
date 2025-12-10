using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public EntityStats stats;


    //State to be kept track of
    [HideInInspector]
    public int CurrentHp { get; private set; }

    //Parrying
    public InputAction parryAction;

    public double lastAttackOverlapTime;
    //Parrying
    public double lastParryTime;
    public bool canParry;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        stats = Resources.Load<EntityStats>(gameObject.tag);
        animator = GetComponent<Animator>();

        CurrentHp = stats.MaxHp;

        lastParryTime = Time.realtimeSinceStartup;

    }

    void Start()
    {
        parryAction = InputSystem.actions.FindAction("Parry");
        parryAction.Enable();
        parryAction.started += Parry;
        CombatManager.instance.PlayerAttackStarted += PlayerAttackStartedHandler;
    }

    void OnDisable()
    {
        parryAction.started -= Parry;
        CombatManager.instance.PlayerAttackStarted -= PlayerAttackStartedHandler;

        parryAction.Disable();
    }

    public void ReduceHp(int damage)
    {
        CurrentHp -= Math.Abs(damage);
    }

    public void Parry(InputAction.CallbackContext context)
    {
        Debug.Log("Checking parry");
        if (!canParry) return;
        Debug.Log("Parry went through");
        //callback context: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.17/api/UnityEngine.InputSystem.InputAction.CallbackContext.html
        lastParryTime = Time.timeAsDouble;

        canParry = false;
    }

    public bool IsIdle()
    {
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animStateInfo.IsTag("Idle");

    }


    public void PlayerAttackStartedHandler(object sender, EventArgs e)
    {
        animator.SetTrigger("AttackAnimTrig");
        AudioClip audioClip = AudioManager.Instance.GetMeleeSoundClip();
        StartCoroutine(AudioManager.Instance.PlayDelayedSound(audioClip, .95f));
        // call or set animation bool here, or make the above an animation bool
    }

    private float CalcAttackSFXDelay(AudioClip audioClip)
    {
        return (float)stats.TimeUntilContact - audioClip.length / 1.7f;
    }
}
