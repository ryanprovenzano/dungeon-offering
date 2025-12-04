using Unity.VisualScripting;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    private EntityStats stats;

    //State to be kept track of
    public int currentHp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        stats = Resources.Load<EntityStats>(gameObject.tag);
    }

    void Start()
    {
        currentHp = stats.maxHp;

    }


    // Update is called once per frame
    void Update()
    {

    }
}
