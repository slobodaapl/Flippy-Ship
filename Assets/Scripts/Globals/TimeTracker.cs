using UnityEngine;

public static class TimeTracker
{
    // This file's existence is purely optional, I could include all these methods in the Spawnable classes
    // but I chose to move the implementation here to make the game easier to balance and optimize from one place
    
    private static float PillarTicker;
    private static float DebrisTicker;
    private static float EnemyShipTicker;
    private static float EnemyMineTicker;
    private static float LastTimeStamp;

    private static void TickAll(float dur)
    {
        PillarTicker -= dur;
        EnemyShipTicker -= dur;
        EnemyMineTicker -= dur;
    }

    private static void GetUpdateDelta()
    {
        var dur = Time.time - LastTimeStamp;
        LastTimeStamp = Time.time;
        TickAll(dur);
    }

    public static void CooldownShip(float cooldown)
    {
        EnemyShipTicker = cooldown;
    }
    
    public static bool TickPillar(float min=1)
    {
        GetUpdateDelta();

        if (!(PillarTicker <= 0)) return false;
        PillarTicker = Mathf.Clamp(2 + 1 * (-Time.time / 300), min, 2);
        return true;
    }

    public static bool TickDebris(float min=1.0f)
    {
        GetUpdateDelta();

        if (!(DebrisTicker <= 0)) return false;
        DebrisTicker = Mathf.Clamp(6 + 6 * (-Time.time / 600), min, 6);
        return true;
    }

    public static bool TickEnemyShip(float min=2.5f)
    {
        GetUpdateDelta();

        if (!(EnemyShipTicker <= 0)) return false;
        EnemyShipTicker = Mathf.Clamp(10 + 10 * (-Time.time / 600), min, 10);
        return true;
    }

    public static bool TickEnemyMine(float min=3)
    {
        GetUpdateDelta();

        if (!(EnemyMineTicker <= 0)) return false;
        EnemyMineTicker = Mathf.Clamp(6 + 3 * (-Time.time / 150), min, 6);
        return true;
    }

    public static float GetMoveMultiplier()
    {
        return 1 + Mathf.Log10(Mathf.Sqrt(Time.time + 1)) / 4;
    }
}