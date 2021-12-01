using System;
using UnityEditor;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(0.01f, 2)]
    public float scrollSpeed = 1;

    public bool offsetFromHigherLayer = false;
    
    public float offsetAmount = 0;
    public BackgroundScroller higherLayer;
    
    private Renderer texRenderer;
    private Vector2 savedOffset;

    void Start () {
        texRenderer = GetComponent<Renderer>();

        if (offsetFromHigherLayer)
            scrollSpeed = higherLayer.scrollSpeed + offsetAmount;

        if (scrollSpeed <= 0)
        {
            scrollSpeed = 1;
            Debug.LogError("Scroll speed offset or base speed was 0");
        }
    }

    void Update () {
        float x = Mathf.Repeat (Time.time * scrollSpeed * TimeTracker.GetMoveMultiplier(), 1);
        Vector2 offset = new Vector2 (x, 0);
        texRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}

[CustomEditor(typeof(BackgroundScroller))]
public class BackgroundScrollerEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as BackgroundScroller;
 
        myScript.offsetFromHigherLayer = GUILayout.Toggle(myScript.offsetFromHigherLayer, "Offset based on another");

        if (myScript.offsetFromHigherLayer)
        {
            myScript.offsetAmount = EditorGUILayout.FloatField("Secondary offset:", myScript.offsetAmount);
            myScript.higherLayer = (BackgroundScroller) EditorGUILayout.ObjectField("Higher layer object:", myScript.higherLayer,
                typeof(BackgroundScroller), true);
        }
        else
        {
            myScript.scrollSpeed = EditorGUILayout.FloatField("Scroll speed:", myScript.scrollSpeed);
        }
    }
}
