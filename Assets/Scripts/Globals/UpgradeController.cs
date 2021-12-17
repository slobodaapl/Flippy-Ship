using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public List<GenericUpgrade> upgradeList; // List of created upgrades
    public double upgradeThreshold = 2000; // When the first upgrade triggers

    public GameObject upgradePanelObject;
    
    private PauseController pauseController;
    private PointController pointController;

    private PlayerShip ship;
    private PlayerShooter shooter;

    private int upgradeExponent = 1; // Used as a scaler, not really an exponent anymore
    private UpgradePanel upgradePanel;

    private void Start()
    {
        ship = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
        shooter = GameObject.FindWithTag("Shooter").GetComponent<PlayerShooter>();
        pointController = GetComponent<PointController>();
        pauseController = GetComponent<PauseController>();
        upgradePanel = upgradePanelObject.GetComponent<UpgradePanel>();
    }

    private void Update()
    {
        if (pointController.GetScore() >= upgradeThreshold)
        {
            TriggerUpgrade();
            ship.TriggerInvincible(4f);
            //upgradeThreshold += upgradeThreshold * Mathf.Pow(2, upgradeExponent / 2f); <- Didn't scale well
            upgradeThreshold += upgradeThreshold + 500 * upgradeExponent; // Linear scaling instead
            upgradeExponent += 1;
        }
    }

    private (GenericUpgrade, GenericUpgrade, GenericUpgrade) GetThreeRandom() // Pick 3 random upgraades from available ones
    {
        var filteredUpgrades = upgradeList.Where(x => x.CheckValid(ship, shooter)).ToList();

        switch (filteredUpgrades.Count)
        {
            case 0:
                return (null, null, null);
            case 1:
                return (filteredUpgrades[0], filteredUpgrades[0], filteredUpgrades[0]);
            case 2:
                return (filteredUpgrades[0], filteredUpgrades[0], filteredUpgrades[1]);
            case 3:
                return (filteredUpgrades[0], filteredUpgrades[1], filteredUpgrades[2]);
        }

        var u1 = filteredUpgrades.RandomElementByWeight(x => x.rarity); // Each upgrade has a weight .. "rarity"
        filteredUpgrades.Remove(u1);
        var u2 = filteredUpgrades.RandomElementByWeight(x => x.rarity);
        filteredUpgrades.Remove(u2);
        var u3 = filteredUpgrades.RandomElementByWeight(x => x.rarity);

        return (u1, u2, u3);
    }

    private void TriggerUpgrade()
    {
        var (u1, u2, u3) = GetThreeRandom();
        pauseController.Pause();
        upgradePanel.Setup(u1, u2, u3);
    }
}