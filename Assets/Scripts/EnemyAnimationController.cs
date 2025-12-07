using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //Debug.Log(IsIdle());
    }

    public bool IsIdle()
    {
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animStateInfo.IsTag("Idle");

    }

    public void BeginAttackAnimation()
    {
        animator.SetTrigger("AttackAnimTrig");

        // call or set animation bool here, or make the above an animation bool
    }
}
