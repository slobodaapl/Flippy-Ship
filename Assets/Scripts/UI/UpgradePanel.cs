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

	private Text upgradeSlotOneText;
	private Text upgradeSlotTwoText;
	private Text upgradeSlotThreeText;

	void Start()
	{
		upgradeSlotOneText = upgradeSlotOneTextObject.GetComponent<Text>();
		upgradeSlotTwoText = upgradeSlotTwoTextObject.GetComponent<Text>();
		upgradeSlotThreeText = upgradeSlotThreeTextObject.GetComponent<Text>();
		gameObject.SetActive(false);
	}
	
}
