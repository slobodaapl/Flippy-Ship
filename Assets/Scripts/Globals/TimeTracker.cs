using UnityEngine;

public static class TimeTracker
{
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

    private static float GetUpdateDelta()
    {
        var dur = Time.time - LastTimeStamp;
        LastTimeStamp = Time.time;
        TickAll(dur);
        
        return dur;
    }

    public static void CooldownShip(float cooldown)
    {
        EnemyShipTicker = cooldown;
    }
    
    public static bool TickPillar(float min)
    {
        var dur = GetUpdateDelta();

        if (!(PillarTicker <= 0)) return false;
        PillarTicker = Mathf.Clamp(2 + 2 * (-Time.time / 300), min, 2);
        return true;
    }

    public static bool TickDebris(float min)
    {
        var dur = GetUpdateDelta();

        if (!(DebrisTicker <= 0)) return false;
        DebrisTicker = Mathf.Clamp(6 + 6 * (-Time.time / 600), min, 6);
        return true;
    }

    public static bool TickEnemyShip(float min)
    {
        var dur = GetUpdateDelta();

        if (!(EnemyShipTicker <= 0)) return false;
        EnemyShipTicker = Mathf.Clamp(10 + 10 * (-Time.time / 600), min, 10);
        return true;
    }

    public static bool TickEnemyMine(float min)
    {
        var dur = GetUpdateDelta();

        if (!(EnemyMineTicker <= 0)) return false;
        EnemyMineTicker = Mathf.Clamp(1 + 1 * (-Time.time / 150), min, 1);
        return true;
    }

    public static float GetMoveMultiplier()
    {
        return 1 + Mathf.Log10(Mathf.Sqrt(Time.time + 1)) / 4;
    }
}