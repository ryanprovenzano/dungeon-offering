using System;
using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    //Player
    public PlayerController playerController;

    //Enemy
    public EnemyController enemyController;

    //State
    public string turnStatus = "Player";

    //Parry window
    double parryWindow;

    //Events
    public event EventHandler EnemyAttackStarted;
    public event EventHandler PlayerAttackStarted;
    public event EventHandler TurnEnded;
    public event EventHandler EnemyTurnBegun;
    public event EventHandler HpCheck;
    public event EventHandler GameFinished;

    //Manager references
    private AudioManager _audioManager;


    void Awake()
    {
        enemyController = GameObject.FindWithTag("Boss").GetComponent<EnemyController>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        instance = this;
    }

    void Start()
    {
        parryWindow = enemyController.stats.ParryWindow;
        _audioManager = AudioManager.Instance;

        //Subscribe to events
        enemyController.EnemyDeath += EnemyDeathHandler;
        playerController.PlayerDeath += PlayerDeathHandler;
        EnemyTurnBegun += EnemyTurnBegunHandler;
    }

    void Update()
    {

    }

    void OnDisable()
    {
        //Unsubscribe to events

        enemyController.EnemyDeath -= EnemyDeathHandler;
        playerController.PlayerDeath -= PlayerDeathHandler;
        EnemyTurnBegun -= EnemyTurnBegunHandler;
    }

    public double GetParryMultiplier(double lastParriedAt, double attackOverlapsAt)
    {
        double difference = Math.Abs(lastParriedAt - attackOverlapsAt);
        double ratio = Math.Clamp(difference / parryWindow, 0, 1);
        // If player would get a 15% damage multiplier, we go all the way to 0
        return ratio < 0.15 ? 0 : ratio;
    }



    private bool IsParryPhaseOver()
    {
        if (Time.timeAsDouble > enemyController.lastAttackOverlapTime + parryWindow)
        {
            return true;
        }
        if (!playerController.canParry) return true;

        return false;
    }


    private void EnemyTurnBegunHandler(object sender, EventArgs e)
    {
        if (!(turnStatus == "Enemy")) return;
        StartCoroutine(ResolveEnemyAttackStep());
    }

    private IEnumerator ResolvePlayerAttackStep()
    {
        turnStatus = "PlayerTurnInProgress";
        PlayerAttackStarted?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(AudioManager.Instance.GetMeleeSoundClip().length + 0.5f);

        enemyController.ReceiveDamage(playerController.stats.Attack);

        ResolveTurnEnd();
        yield return new WaitForSeconds(1f);
        EnemyTurnBegun?.Invoke(this, EventArgs.Empty);
    }

    public void TryPlayerAttack()
    {
        if (!(turnStatus == "Player")) return;
        StartCoroutine(ResolvePlayerAttackStep());
    }

    // We can't separate the Coroutine out of the rest of the attack step! Because the coroutine would just exit and the rest of the attack step will resolve
    private IEnumerator ResolveEnemyAttackStep()
    {
        turnStatus = "EnemyTurnInProgress";
        playerController.canParry = true;

        EnemyAttackStarted?.Invoke(this, EventArgs.Empty);
        //get the contact frame of the attack
        enemyController.lastAttackOverlapTime = Time.timeAsDouble + enemyController.stats.TimeUntilContact;

        // Wait until player has parried or the enemy's parry window has ended
        yield return new WaitUntil(IsParryPhaseOver);

        double damageMultiplier = GetParryMultiplier(playerController.lastParryTime, enemyController.lastAttackOverlapTime);

        //Determine & call parry sound. Only calls a sound if the player activated their parry.
        if (!playerController.canParry)
        {
            if (damageMultiplier == 0)
            {
                _audioManager.PlaySound(_audioManager.GetPerfectBlockClip());
            }
            else _audioManager.PlaySound(_audioManager.GetRegularBlockClip());
        }
        playerController.ReceiveDamage((int)Math.Ceiling(enemyController.stats.Attack * damageMultiplier));

        ResolveTurnEnd();
    }

    private void ResolveTurnEnd()
    {
        // Change turn status
        if (turnStatus == "PlayerTurnInProgress")
        {
            turnStatus = "Enemy";
        }
        else if (turnStatus == "EnemyTurnInProgress")
        {
            turnStatus = "Player";
        }

        HpCheck?.Invoke(this, EventArgs.Empty);
        TurnEnded?.Invoke(this, EventArgs.Empty);
    }

    private void EnemyDeathHandler(object sender, EventArgs e)
    {
        turnStatus = "PlayerWon";
        GameFinished?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerDeathHandler(object sender, EventArgs e)
    {
        turnStatus = "PlayerLost";
        GameFinished?.Invoke(this, EventArgs.Empty);
    }

}

