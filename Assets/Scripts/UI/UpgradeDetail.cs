using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDetail : MonoBehaviour
{
	public GameObject upgradeSlotObject;
	public GameObject detailTextObject;

	private Text detailText;

	void Start()
	{
		detailText = detailTextObject.GetComponent<Text>();
		gameObject.SetActive(false);
	}
}
