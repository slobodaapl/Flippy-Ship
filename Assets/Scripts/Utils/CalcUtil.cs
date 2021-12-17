using UnityEngine;

public static class CalcUtil
{
    public static float ClampAngle(float unadjusted) // This was before I found out unity has a dedicated method for it.. but I kept it
    {
        return unadjusted > 180 ? unadjusted - 360 : unadjusted; // Just isolates angle to [-180;180] range
    }

    public static float GetCoord(float posOne, float posTwo, bool horizontal) // Used by spawners to determine whether to spawn ship on top or bottom
    {
        if (horizontal)
            switch (Random.Range(0, 1))
            {
                case 0:
                    return posOne;
                case 1:
                    return posTwo;
            }
        else
            return Random.Range(posOne, posTwo); // If it's not horizontal spawn (see Spawnable), random position vertically on screen to spawn

        return 0;
    }
}