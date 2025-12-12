using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public EntityStats stats;

    //Parrying
    public InputAction parryAction;

    public double lastAttackOverlapTime;
    //Parrying
    public double lastParryTime;
    public bool canParry;
    private Animator animator;
    public EventHandler PlayerDeath;
    private Health _health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        stats = Resources.Load<EntityStats>(gameObject.tag);
        animator = GetComponent<Animator>();

        lastParryTime = Time.realtimeSinceStartup;
        _health = GetComponent<Health>();

    }

    void Start()
    {
        parryAction = InputSystem.actions.FindAction("Parry");
        parryAction.Enable();
        parryAction.started += Parry;
        CombatManager.instance.PlayerAttackStarted += PlayerAttackStartedHandler;
        CombatManager.instance.TurnEnded += TurnEndedHandler;
    }

    void OnDisable()
    {
        parryAction.started -= Parry;
        CombatManager.instance.PlayerAttackStarted -= PlayerAttackStartedHandler;

        parryAction.Disable();
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

    public void PlayerAttackStartedHandler(object sender, EventArgs e)
    {
        animator.SetTrigger("AttackAnimTrig");
        AudioClip audioClip = AudioManager.Instance.GetMeleeSoundClip();
        StartCoroutine(AudioManager.Instance.PlayDelayedSound(audioClip, .95f));
        // call or set animation bool here, or make the above an animation bool
    }

    public void TurnEndedHandler(object sender, EventArgs e)
    {
        if (_health.IsDepleted())
        {
            AudioManager.Instance.PlayDeathSound();
            CombatManager.instance.TurnEnded -= TurnEndedHandler;

            PlayerDeath?.Invoke(this, e);

            Destroy(gameObject);
        }
    }
    public void ReceiveDamage(int damage)
    {
        _health.Reduce(damage);
    }
}
