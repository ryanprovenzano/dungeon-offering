using System;
using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }

    //Player
    EntityController playerController;

    //Enemy
    EntityController enemyController;

    //State
    private string turnStatus = "Player";

    private enum ParryGrade
    {
        None = 0,
        Poor = 20,
        Good = 70,
        Perfect = 100
    }

    void Awake()
    {
        enemyController = GameObject.FindWithTag("Boss").GetComponent<EntityController>();
        playerController = GameObject.FindWithTag("Player").GetComponent<EntityController>();
        Instance = this;
    }

    void Update()
    {

    }



    private ParryGrade GetParryGrade(double lastParriedAt, double attackOverlapsAt)
    {
        double error = Math.Abs(lastParriedAt - attackOverlapsAt);
        ParryGrade parryGrade;
        switch (error)
        {
            case < 0.2f:
                parryGrade = ParryGrade.Perfect;
                break;
            case < 0.4f:
                parryGrade = ParryGrade.Good;
                break;
            case < 0.6f:
                parryGrade = ParryGrade.Poor;
                break;
            default:
                parryGrade = ParryGrade.None;
                break;
        }
        return parryGrade;
    }

    /// <summary>
    /// Expects "Enemy" or "Player" as arguments. The recipient of the attack.
    /// </summary>
    /// <param name="targetType"></param>

    private IEnumerator ResolveAttackStep()
    {
        playerController.canParry = true;
        enemyController.BeginAttackAnimation();
        //get the contact frame of the attack
        enemyController.lastAttackOverlapTime = Time.timeAsDouble;

        // Wait until player has parried or the enemy's attack animation has ended
        yield return new WaitUntil(() => (playerController.canParry == false) || !enemyController.isInAttackAnimation);
    }

    public void BeginCombatStep()
    {
        if (turnStatus == "Player")
        {
            enemyController.ReduceHp(playerController.stats.attack);
            turnStatus = "Enemy";
        }
        else
        {
            StartCoroutine(ResolveAttackStep());

            // Parry grade
            ParryGrade parryGrade = GetParryGrade(playerController.lastParryTime, enemyController.lastAttackOverlapTime);
            float damageMultiplier = (float)parryGrade / 100;
            // Apply damage
            playerController.ReduceHp((int)Math.Ceiling(enemyController.stats.attack * damageMultiplier));

            // Change turn status
            turnStatus = "Player";
        }

        UIManager.Instance.UpdateHp();
    }

    /// <summary>
    /// Returns the player's EntityController and enemy's EntityController respectively
    /// </summary>
    /// <returns></returns>
    public (EntityController, EntityController) GetCombatantControllers()
    {
        return (playerController, enemyController);
    }



}

