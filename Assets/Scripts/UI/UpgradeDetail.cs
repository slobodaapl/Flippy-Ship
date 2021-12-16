using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDetail : MonoBehaviour
{
	public GameObject upgradeSlotObject;
	public GameObject detailTextObject;

	private GenericUpgrade currentUpgrade;

	private PlayerShip ship;
	private PlayerShooter shooter;
	
	private Text detailText;

	void Start()
	{
		detailText = detailTextObject.GetComponent<Text>();
		gameObject.SetActive(false);
		ship = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
		shooter = GameObject.FindWithTag("Shooter").GetComponent<PlayerShooter>();
	}

	public void Choice(GenericUpgrade chosen)
	{
		gameObject.SetActive(true);

		currentUpgrade = Instantiate(chosen, Vector3.zero, Quaternion.identity);
		currentUpgrade.gameObject.transform.SetParent(upgradeSlotObject.transform, false);

		currentUpgrade.gameObject.GetComponent<Button>().enabled = false;

		detailText.text = currentUpgrade.upgradeDesc;
	}

	public void CancelChoice()
	{
		Destroy(currentUpgrade.gameObject);
		currentUpgrade = null;
		gameObject.SetActive(false);
	}

	public void ConfirmChoice()
	{
		currentUpgrade.ApplyUpgrade(ship, shooter);
		Destroy(currentUpgrade.gameObject);
		currentUpgrade = null;
		gameObject.SetActive(false);
	}
}
