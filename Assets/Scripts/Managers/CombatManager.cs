using System;
using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }

    //Player
    public PlayerController playerController;

    //Enemy
    public EnemyController enemyController;

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
        enemyController = GameObject.FindWithTag("Boss").GetComponent<EnemyController>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        Instance = this;
    }

    void Update()
    {

    }



    private ParryGrade GetParryGrade(double lastParriedAt, double attackOverlapsAt)
    {
        Debug.Log("Last Parried at: " + lastParriedAt + " Attack overlaps at: " + attackOverlapsAt);
        double error = Math.Abs(lastParriedAt - attackOverlapsAt);
        return error switch
        {
            < 0.2f => ParryGrade.Perfect,
            < 0.4f => ParryGrade.Good,
            < 0.6f => ParryGrade.Poor,
            _ => ParryGrade.None
        };
    }

    /// <summary>
    /// Expects "Enemy" or "Player" as arguments. The recipient of the attack.
    /// </summary>
    /// <param name="targetType"></param>

    private IEnumerator ResolveAttackStep()
    {
        playerController.canParry = true;
        enemyController.animController.BeginAttackAnimation();
        //get the contact frame of the attack
        enemyController.lastAttackOverlapTime = Time.timeAsDouble + 1.03;

        // Wait until player has parried or the enemy's attack animation has ended
        yield return new WaitUntil(() => (playerController.canParry == false) || enemyController.animController.IsIdle());
        Debug.Log("Parry window over, can character parry? " + playerController.canParry + " Enemy is idle? " + enemyController.animController.IsIdle());
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


            // Apply damage
            float damageMultiplier = (float)parryGrade / 100;
            playerController.ReduceHp((int)Math.Ceiling(enemyController.stats.attack * damageMultiplier));

            // Change turn status
            turnStatus = "Player";
        }

        UIManager.Instance.UpdateHp(enemyController.CurrentHp, playerController.CurrentHp);
    }

    /// <summary>
    /// Returns the player's EntityController and enemy's EnemyController respectively
    /// </summary>
    /// <returns></returns>
    public (PlayerController, EnemyController) GetCombatantControllers()
    {
        return (playerController, enemyController);
    }



}

