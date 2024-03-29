using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct FlippedDirs
{
    public bool flipX;
    public bool flipY;
}

public class TransformablePrefab : MonoBehaviour // Makes it possible to generate mine groups upside down or mirrored on y axis, randomly
{
    public FlippedDirs flipDirs;
    public float rotateAngle;
    public float floatChance = 0.5f;

    private void Start()
    {
        if (Random.Range(0, 1) <= floatChance)
        {
            var scale = transform.localScale;
            scale.x = scale.x * (flipDirs.flipX ? -1 : 1);
            scale.y = scale.x * (flipDirs.flipX ? -1 : 1);
            transform.localScale = scale;

            var rotation = transform.localEulerAngles;
            rotation.z = rotateAngle;
            transform.localEulerAngles = rotation;
        }
    }
}