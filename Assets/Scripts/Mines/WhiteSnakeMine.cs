using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WhiteSnakeMine : WhiteMine
{
    public GameObject minePrefab;
    
    protected override void Start()
    {
        base.Start();
        
        var pos = transform.position;

        for (int i = 1; i < 5; i++)
            Instantiate(minePrefab, new Vector3(pos.x + 2.12f * i, pos.y, pos.z), new Quaternion())
                .SendMessage("setFixedOffset", -(Mathf.PI * 2*i) / 10);

        var allChildren = new List<GameObject>();

        foreach (Transform child in transform)
        {
            allChildren.Add(child.gameObject);
        }

        allChildren.ForEach(child => DestroyImmediate(child.gameObject));
    }
}