using System;
using UnityEditor;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public static float scrollSpeed = 0.15f;

    private float singleScrollSpeed;
    private Renderer texRenderer;
    private Vector2 savedOffset;

    void Start () {
        texRenderer = GetComponent<Renderer>();

        switch (transform.name)
        {
            case "buildings":
                singleScrollSpeed = scrollSpeed * 0.9f;
                break;
            case "bg-buildings":
                singleScrollSpeed = scrollSpeed * 0.7f;
                break;
            case "bg":
                singleScrollSpeed = scrollSpeed * 0.25f;
                break;
            default:
                singleScrollSpeed = scrollSpeed;
                break;
        }
    }

    void Update () {
        float x = Mathf.Repeat (Time.timeSinceLevelLoad * singleScrollSpeed * TimeTracker.GetMoveMultiplier(), 1);
        Vector2 offset = new Vector2 (x, 0);
        texRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
