using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Scriptable Objects/EntityStats")]
public class EntityStats : ScriptableObject
{
    [field: SerializeField] public double ParryWindow { get; private set; }
    [field: SerializeField] public int MaxHp { get; private set; }
    [field: SerializeField] public int Attack { get; private set; }
    [field: SerializeField] public double TimeUntilContact { get; private set; }
    [field: SerializeField] public double ParryCooldown { get; private set; }
}


