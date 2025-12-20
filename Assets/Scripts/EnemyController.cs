using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EntityStats stats;
    public Animator swordAnimator;

    public double lastAttackOverlapTime;
    public EnemyAnimationController animController;
    public event EventHandler EnemyDeath;
    private Health _health;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        stats = Resources.Load<EntityStats>(gameObject.tag);
        animController = GetComponentInChildren<EnemyAnimationController>();
        _health = GetComponent<Health>();
    }

    void Start()
    {
        CombatManager.instance.TurnEnded += TurnEndedHandler;
    }

    public void TurnEndedHandler(object sender, EventArgs e)
    {
        if (_health.IsDepleted())
        {
            AudioManager.Instance.PlayDeathSound();
            CombatManager.instance.TurnEnded -= TurnEndedHandler;

            EnemyDeath?.Invoke(this, e);

            Destroy(gameObject);
        }
    }

    public void ReceiveDamage(int damage)
    {
        _health.Reduce(damage);
    }


}
