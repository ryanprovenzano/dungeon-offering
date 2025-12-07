using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityController : MonoBehaviour
{
    public EntityStats stats;


    //State to be kept track of
    [HideInInspector]
    public int CurrentHp { get; private set; }

    //Turn into a non-state function later
    public bool isInAttackAnimation;

    //Parrying
    public InputAction parryAction;

    public double lastAttackOverlapTime;
    //Parrying
    public double lastParryTime;
    public bool canParry;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        stats = Resources.Load<EntityStats>(gameObject.tag);
        CurrentHp = stats.maxHp;

        lastParryTime = Time.realtimeSinceStartup;
        parryAction = InputSystem.actions.FindAction("Parry");
        parryAction.started += Parry;
    }

    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void ReduceHp(int damage)
    {
        CurrentHp -= Math.Abs(damage);
    }

    public void BeginAttackAnimation()
    {
        isInAttackAnimation = true;
        // call or set animation bool here, or make the above an animation bool
    }

    public void Parry(InputAction.CallbackContext context)
    {
        if (!canParry) return;
        //callback context: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.17/api/UnityEngine.InputSystem.InputAction.CallbackContext.html
        double keyPressedAt = context.time;
        lastParryTime = keyPressedAt;
        canParry = false;
    }
}
