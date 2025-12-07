using System;
using Unity.VisualScripting;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public EntityStats stats;

    //State to be kept track of
    [HideInInspector]
    public int CurrentHp { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        stats = Resources.Load<EntityStats>(gameObject.tag);
        CurrentHp = stats.maxHp;

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
}
