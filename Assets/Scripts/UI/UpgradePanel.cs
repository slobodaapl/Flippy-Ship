using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public GameObject upgradeSlotOneObject;
    public GameObject upgradeSlotOneTextObject;

    public GameObject upgradeSlotTwoObject;
    public GameObject upgradeSlotTwoTextObject;

    public GameObject upgradeSlotThreeObject;
    public GameObject upgradeSlotThreeTextObject;

    public GameObject upgradeDetailObject;

    private List<GenericUpgrade> instantiatedUpgrades;
    private UpgradeDetail upgradeDetail;

    private Text upgradeSlotOneText;
    private Text upgradeSlotThreeText;
    private Text upgradeSlotTwoText;

    private void Awake()
    {
        instantiatedUpgrades = new List<GenericUpgrade>();
        upgradeDetail = upgradeDetailObject.GetComponent<UpgradeDetail>();
    }

    private void Start() // A bit clumsy to separately update them all.. but it's only 3 so spaghetti code all the way
    {
        upgradeSlotOneText = upgradeSlotOneTextObject.GetComponent<Text>();
        upgradeSlotTwoText = upgradeSlotTwoTextObject.GetComponent<Text>();
        upgradeSlotThreeText = upgradeSlotThreeTextObject.GetComponent<Text>();
        gameObject.SetActive(false);
    }

    public void Setup(GenericUpgrade u1, GenericUpgrade u2, GenericUpgrade u3)
        // I'm not proud of this method, trust me, but it's just 3, so spaghetti-code-mode enabled
    {
        gameObject.SetActive(true);

        if (instantiatedUpgrades.Count != 0)
        {
            foreach (var u in instantiatedUpgrades)
                Destroy(u.gameObject);

            instantiatedUpgrades.Clear();
        }

        upgradeSlotOneText.text = u1.upgradeName; // Set upgrade names
        upgradeSlotTwoText.text = u2.upgradeName;
        upgradeSlotThreeText.text = u3.upgradeName;

        var u1Obj = Instantiate(u1, Vector3.zero, Quaternion.identity); // Create the upgrades button prefabs
        var u2Obj = Instantiate(u2, Vector3.zero, Quaternion.identity);
        var u3Obj = Instantiate(u3, Vector3.zero, Quaternion.identity);

        u1Obj.transform.SetParent(upgradeSlotOneObject.transform, false); // Put them in the correct spots
        u2Obj.transform.SetParent(upgradeSlotTwoObject.transform, false);
        u3Obj.transform.SetParent(upgradeSlotThreeObject.transform, false);

        // Make it so that we can move the chosen upgrade to the next panel after clicking the upgrade
        u1Obj.GetComponent<Button>().onClick.AddListener(delegate { upgradeDetail.Choice(u1Obj); }); 
        u2Obj.GetComponent<Button>().onClick.AddListener(delegate { upgradeDetail.Choice(u2Obj); });
        u3Obj.GetComponent<Button>().onClick.AddListener(delegate { upgradeDetail.Choice(u3Obj); });
    }

    public void Finish() // Clear the shown upgrades and hide panel when the player chooses a upgrade on the Detail panel
    {
        if (instantiatedUpgrades.Count != 0)
        {
            foreach (var u in instantiatedUpgrades)
                Destroy(u.gameObject);

            instantiatedUpgrades.Clear();
        }

        gameObject.SetActive(false);
    }
}