using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }

    //Player
    public PlayerController playerController;

    //Enemy
    public EnemyController enemyController;

    //State
    private string turnStatus = "Player";

    //Parry window
    double parryWindow;


    void Awake()
    {
        enemyController = GameObject.FindWithTag("Boss").GetComponent<EnemyController>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        Instance = this;
    }

    void Start()
    {
        parryWindow = enemyController.stats.parryWindow;
    }

    void Update()
    {

    }





    public double GetParryMultiplier(double lastParriedAt, double attackOverlapsAt)
    {
        double difference = Math.Abs(lastParriedAt - attackOverlapsAt);
        double ratio = Math.Clamp(difference / parryWindow, 0, 1);
        // If player would get a 10% damage multiplier, we go all the way to 0
        return ratio < 0.1 ? 0 : ratio;
    }

    public void StartAttackStepCoroutine()
    {
        StartCoroutine(ResolveAttackStep());
    }
    // We can't separate the Coroutine out of the rest of the attack step! Because the coroutine would just exit and the rest of the attack step will resolve
    private IEnumerator ResolveAttackStep()
    {
        if (turnStatus == "Player")
        {
            enemyController.ReduceHp(playerController.stats.attack);
            turnStatus = "Enemy";
        }
        else
        {
            playerController.canParry = true;
            enemyController.animController.BeginAttackAnimation();
            //get the contact frame of the attack
            enemyController.lastAttackOverlapTime = Time.timeAsDouble + 1.03;

            // Need animation state of enemy to update
            yield return new WaitForSeconds(0.1f);

            // Wait until player has parried or the enemy's parry window has ended
            // Old: waiting until enemyController.animController.IsIdle()
            yield return new WaitUntil(() => Time.timeAsDouble > enemyController.lastAttackOverlapTime + parryWindow);


            // Apply damage
            double damageMultiplier = GetParryMultiplier(playerController.lastParryTime, enemyController.lastAttackOverlapTime);
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

