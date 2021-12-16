using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float globalSpawnCooldown = 1;
        
    private List<Spawnable> spawners;
    private float globalCurrentCooldown;

    void Start()
    {
        TimeTracker.ResetAll();
        spawners = GetComponents<Spawnable>().ToList();
        
        // I quickly decided to scrap this, cause Reflection is horribly buggy and hard to maintain
        // I refactored the code to work better with OOP patterns.. but I'm leaving this here to scare those who read it

        // var fields =  
        //     GetType()
        //     .GetFields(BindingFlags.NonPublic)
        //     .Select(field => new { type = field.FieldType, name = field.Name, val = field.GetValue(this) }).ToList();
        //
        // foreach (var init in listInit)
        // {
        //     var firstOrDefault = fields.FirstOrDefault(field => field.name == init.fieldName);
        //     if (firstOrDefault != null)
        //     {
        //         var range = getCoords(init.TopLeftMarker, init.BottomRightMarker);
        //         var xrange = (range.xleft, range.xright);
        //         var yrange = (range.ytop, range.ybottom);
        //         
        //         var methodInfo = firstOrDefault.type.GetMethod("InitValues");
        //         var method = methodInfo?.MakeGenericMethod(firstOrDefault.type);
        //         method?.Invoke(firstOrDefault.val, new object[] { xrange, yrange, init.Prefabs });
        //     }
        // }
    }
    
    void FixedUpdate()
    {
        if (globalCurrentCooldown > 0)
        {
            globalCurrentCooldown -= Time.fixedDeltaTime;
            return;
        }

        var spawnable = spawners.Where(x => x.isActiveAndEnabled).Shuffle().ToList();
        foreach (var spawnableinst in spawnable)
        {
            if (spawnableinst.Spawn())
            {
                globalCurrentCooldown = globalSpawnCooldown;
                return;
            }
        }
    }
}
