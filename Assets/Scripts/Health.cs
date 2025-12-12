using System;
using UnityEngine;

public class Health : MonoBehaviour
{

    //State to be kept track of
    [HideInInspector]
    public int Current { get; private set; }
    public int Max { get; private set; }
    public EntityStats stats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = Resources.Load<EntityStats>(gameObject.tag);
        Current = Max = stats.MaxHp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Reduce(int damage)
    {
        Current -= Math.Abs(damage);
    }

    public bool IsDepleted()
    {
        return Current <= 0;
    }
}
