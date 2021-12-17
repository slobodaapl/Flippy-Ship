using UnityEngine;
using UnityEngine.UI;

public class UpgradeDetail : MonoBehaviour // Panel with upgrade's description
{
    public GameObject upgradeSlotObject;
    public GameObject detailTextObject;

    private GenericUpgrade currentUpgrade;

    private Text detailText;

    private PlayerShip ship;
    private PlayerShooter shooter;

    private void Start() // Invisible on start
    {
        detailText = detailTextObject.GetComponent<Text>();
        gameObject.SetActive(false);
        ship = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
        shooter = GameObject.FindWithTag("Shooter").GetComponent<PlayerShooter>();
    }

    public void Choice(GenericUpgrade chosen) // Once we choose upgrade, show this panel and show description of upgrade
    {
        gameObject.SetActive(true);

        currentUpgrade = Instantiate(chosen, Vector3.zero, Quaternion.identity);
        currentUpgrade.gameObject.transform.SetParent(upgradeSlotObject.transform, false);

        currentUpgrade.gameObject.GetComponent<Button>().enabled = false; // We don't want the upgrade button prefab usable here

        detailText.text = currentUpgrade.upgradeDesc;
    }

    public void CancelChoice() // Return to the choice of 3 upgrades
    {
        Destroy(currentUpgrade.gameObject);
        currentUpgrade = null;
        gameObject.SetActive(false);
    }

    public void ConfirmChoice() // Apply the upgrade, destroy the instantiated prefab and make the panel poof
    {
        currentUpgrade.ApplyUpgrade(ship, shooter);
        Destroy(currentUpgrade.gameObject);
        currentUpgrade = null;
        gameObject.SetActive(false);
    }
}