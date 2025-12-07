using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EntityStats stats;
    public Animator swordAnimator;


    //State to be kept track of
    [HideInInspector]
    public int CurrentHp { get; private set; }

    public double lastAttackOverlapTime;
    public EnemyAnimationController animController;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        stats = Resources.Load<EntityStats>(gameObject.tag);
        animController = GetComponentInChildren<EnemyAnimationController>();

        CurrentHp = stats.maxHp;
    }

    public void ReduceHp(int damage)
    {
        CurrentHp -= Math.Abs(damage);
    }
}
