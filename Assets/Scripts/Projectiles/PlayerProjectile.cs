using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	public float durationVisible;

	private Vector3 targetPos;
	private GameObject ship;

	void Start()
	{
		ship = GameObject.FindWithTag("Player");
	}

	public void Init(Vector3 target)
	{
		targetPos = target;
	}

	void Update()
	{
		if (durationVisible <= 0)
			Destroy(gameObject);

		durationVisible -= Time.deltaTime;

		targetPos -= new Vector3(Time.deltaTime * 5.7f, 0, 0);
		var shipPos = ship.transform.position;
		var targetVec = targetPos - shipPos;
		var forwardVec = ship.transform.right;
		var angle = Vector3.SignedAngle(targetVec, forwardVec, Vector3.up);
		var scale = Vector3.Distance(gameObject.transform.position, targetPos);
		gameObject.transform.localScale = new Vector3(scale, 1, 1);
		gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
		gameObject.transform.position = shipPos;
	}
	
}
