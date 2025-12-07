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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        stats = Resources.Load<EntityStats>(gameObject.tag);

        CurrentHp = stats.maxHp;

        lastParryTime = Time.realtimeSinceStartup;
        parryAction = InputSystem.actions.FindAction("Parry");
        parryAction.started += Parry;
    }

    public void ReduceHp(int damage)
    {
        CurrentHp -= Math.Abs(damage);
    }

    public void Parry(InputAction.CallbackContext context)
    {

        if (!canParry) return;
        Debug.Log("Parry went through");
        //callback context: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.17/api/UnityEngine.InputSystem.InputAction.CallbackContext.html
        lastParryTime = Time.timeAsDouble;
        canParry = false;
    }
}
