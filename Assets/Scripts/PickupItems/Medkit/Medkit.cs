using UnityEngine;

public class Medkit : PickupItem<Medkit>
{
    [SerializeField] private int _healAmount = 25;
    
    public int HealAmount => _healAmount;
}
