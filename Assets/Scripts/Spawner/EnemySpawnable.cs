using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnable : Spawnable<EnemySpawnable>
{
    private int maxEnemies = 3;
    private int currentEnemies;

    private List<EnemyShipType> spawnedTypes = new List<EnemyShipType>();

    public override bool IsSpawnable()
    {
        return TimeTracker.TickEnemyShip();
    }

    public override void SpawnInstantiate()
    {
        var valid_patterns = patterns.Where(x => !spawnedTypes.Contains(x.GetComponent<BaseShip>().type)).ToList();
        
        if (valid_patterns.Count == 0) return;

        currentEnemies += 1;
        var selected_pattern = SpawnChoice(valid_patterns);
        spawnedTypes.Add(selected_pattern.GetComponent<BaseShip>().type);

        Instantiate(selected_pattern, new Vector3(xSpawnCoord, ySpawnCoord, 0), new Quaternion());
    }

    public override bool CheckConstraints()
    {
        return currentEnemies < maxEnemies;
    }

    public override void UpdateConstraints(GameObject obj)
    {
        if (obj == null)
            return;
        
        currentEnemies -= 1;
        spawnedTypes.Remove(obj.GetComponent<BaseShip>().type);
    }
}
