using System.Collections.Generic;
using UnityEngine;

public class WhiteSnakeMine : WhiteMine
{
    public GameObject minePrefab;

    protected override void Start() // This mine creates a long snake that wiggles across the screen
    {
        base.Start();

        var pos = transform.position;

        for (var i = 1; i < 5; i++)
            Instantiate(minePrefab, new Vector3(pos.x + 2.12f * i, pos.y, pos.z), new Quaternion())
                .SendMessage("setFixedOffset", -(Mathf.PI * 2 * i) / 10); // To avoid using GetComponent here pointlessly, since it happens rarely

        var allChildren = new List<GameObject>();

        foreach (Transform child in transform) allChildren.Add(child.gameObject);

        allChildren.ForEach(child => DestroyImmediate(child.gameObject));
    }
}