using System.Collections;
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

	private Text upgradeSlotOneText;
	private Text upgradeSlotTwoText;
	private Text upgradeSlotThreeText;

	private List<GenericUpgrade> instantiatedUpgrades;
	private UpgradeDetail upgradeDetail;

	void Awake()
	{
		instantiatedUpgrades = new List<GenericUpgrade>();
		upgradeDetail = upgradeDetailObject.GetComponent<UpgradeDetail>();
	}

	void Start()
	{
		upgradeSlotOneText = upgradeSlotOneTextObject.GetComponent<Text>();
		upgradeSlotTwoText = upgradeSlotTwoTextObject.GetComponent<Text>();
		upgradeSlotThreeText = upgradeSlotThreeTextObject.GetComponent<Text>();
		gameObject.SetActive(false);
	}

	public void Setup(GenericUpgrade u1, GenericUpgrade u2, GenericUpgrade u3) // I'm not proud of it trust me, but I had no idea how else to do it
	{
		gameObject.SetActive(true);
		
		if (instantiatedUpgrades.Count != 0)
		{
			foreach (var u in instantiatedUpgrades)
				Destroy(u.gameObject);
			
			instantiatedUpgrades.Clear();
		}
		
		upgradeSlotOneText.text = u1.upgradeName;
		upgradeSlotTwoText.text = u2.upgradeName;
		upgradeSlotThreeText.text = u3.upgradeName;

		var u1Obj = Instantiate(u1, Vector3.zero, Quaternion.identity);
		var u2Obj = Instantiate(u2, Vector3.zero, Quaternion.identity);
		var u3Obj = Instantiate(u3, Vector3.zero, Quaternion.identity);
		
		u1Obj.gameObject.transform.parent = upgradeSlotOneObject.transform;
		u2Obj.gameObject.transform.parent = upgradeSlotTwoObject.transform;
		u3Obj.gameObject.transform.parent = upgradeSlotThreeObject.transform;
		
		u1Obj.gameObject.transform.localScale = Vector3.one;
		u2Obj.gameObject.transform.localScale = Vector3.one;
		u3Obj.gameObject.transform.localScale = Vector3.one;

		u1Obj.gameObject.transform.localPosition = Vector3.zero;
		u2Obj.gameObject.transform.localPosition = Vector3.zero;
		u3Obj.gameObject.transform.localPosition = Vector3.zero;

		u1Obj.GetComponent<Button>().onClick.AddListener(delegate { upgradeDetail.Choice(u1Obj); } );
		u2Obj.GetComponent<Button>().onClick.AddListener(delegate { upgradeDetail.Choice(u2Obj); } );
		u3Obj.GetComponent<Button>().onClick.AddListener(delegate { upgradeDetail.Choice(u3Obj); } );
	}

	public void Finish()
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