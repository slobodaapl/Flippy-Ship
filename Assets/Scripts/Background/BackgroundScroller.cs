using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public static float scrollSpeed = 0.15f;

    private float singleScrollSpeed;
    private Renderer texRenderer;

    private void Start()
    {
        texRenderer = GetComponent<Renderer>();

        // Not an efficient way I know. Previously I had it as each bg had its own component
        // and each component could have a reference to a gameobject containing the same component, and would offset
        // its speed based on it by a given amount. Profiling showed it's slow for some reason so I removed it.
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

    private void Update() // Simply scroll background elements based on time spent ingame (speeds up over time) and base scrollspeed
    {
        var x = Mathf.Repeat(Time.timeSinceLevelLoad * singleScrollSpeed * TimeTracker.GetMoveMultiplier(), 1);
        var offset = new Vector2(x, 0);
        texRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset); // Just shifting texture, no actual movement
    }
}