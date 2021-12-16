using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
	public List<GenericUpgrade> upgradeList;
	public double upgradeThreshold = 2000;

	public GameObject upgradePanelObject;

	private PlayerShip ship;
	private PlayerShooter shooter;
	private PointController pointController;
	private PauseController pauseController;
	private UpgradePanel upgradePanel;

	private int upgradeExponent = 1;

	void Start()
	{
		ship = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
		shooter = GameObject.FindWithTag("Shooter").GetComponent<PlayerShooter>();
		pointController = GetComponent<PointController>();
		pauseController = GetComponent<PauseController>();
		upgradePanel = upgradePanelObject.GetComponent<UpgradePanel>();
	}
	
	void Update()
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

	private (GenericUpgrade, GenericUpgrade, GenericUpgrade) GetThreeRandom()
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

		var u1 = filteredUpgrades.RandomElementByWeight(x => x.rarity);
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
