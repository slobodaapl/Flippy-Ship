﻿using UnityEngine;

public static class MultiplierColor // Just an util to get various colors for the multiplier, especially the cool rainbow changing one
{
    private static readonly Color one = new Color(1, 1, 1);
    private static readonly Color two = new Color(63f / 255, 199f / 255, 26f / 255);
    private static readonly Color three = new Color(41f / 255, 32f / 255, 212f / 255);
    private static readonly Color four = new Color(222f / 255, 47f / 255, 47f / 255);

    public static Color GetColor(int multiplier)
    {
        switch (multiplier)
        {
            case 1:
                return one;
            case 2:
                return two;
            case 3:
                return three;
            case 4:
                return four;
            case 5:
                return Color.HSVToRGB(Mathf.Lerp(0, 1, Mathf.Repeat(Time.time / 5, 1)), 1, 1);
        }

        return new Color();
    }
}