using UnityEngine;

public static class CalcUtil
{
    public static float ClampAngle(float unadjusted)
    {
        return unadjusted > 180 ? unadjusted - 360 : unadjusted;
    }

    public static float GetCoord(float posOne, float posTwo, bool horizontal)
    {
        if (horizontal)
        {
            switch (Random.Range(0, 1))
            {
                case 0:
                    return posOne;
                case 1:
                    return posTwo;
            }
        }
        else
        {
            return Random.Range(posOne, posTwo);
        }

        return 0;
    }
}