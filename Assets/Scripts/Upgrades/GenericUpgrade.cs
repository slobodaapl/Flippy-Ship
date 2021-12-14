using UnityEngine;

public abstract class GenericUpgrade : MonoBehaviour
{
    [TextArea] 
    public string upgradeDesc;
    public string upgradeName;
    
    [Header("Weight of the upgrade")]
    [Range(0,1)]
    public float rarity; // 1 is not rare at all, 0 is impossible to get.. Represents weight not probability

    public abstract bool CheckValid(PlayerShip ship, PlayerShooter shooter);
    public abstract void ApplyUpgrade(PlayerShip ship, PlayerShooter shooter);
}
