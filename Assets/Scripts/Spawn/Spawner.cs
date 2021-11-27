using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Serializable]
    public struct InitSpawner
    {
        public string fieldName;
        public List<GameObject> Prefabs;
        public GameObject TopLeftMarker;
        public GameObject BottomRightMarker;
    }

    private MineSpawnable mineSpawner = MineSpawnable.Instance;
    public List<InitSpawner> listInit;

    (float xleft, float xright, float ytop, float ybottom) getCoords(GameObject topleft, GameObject bottomright)
    {
        var postopleft = topleft.transform.position;
        var posbottomright = bottomright.transform.position;

        return (postopleft.x, posbottomright.x, postopleft.y, posbottomright.y);
    }
    
    void Start()
    {
        var fields = 
            GetType()
            .GetFields(BindingFlags.NonPublic)
            .Select(field => new { type = field.FieldType, name = field.Name, val = field.GetValue(this) }).ToList();
        
        foreach (var init in listInit)
        {
            var firstOrDefault = fields.FirstOrDefault(field => field.name == init.fieldName);
            if (firstOrDefault != null)
            {
                var range = getCoords(init.TopLeftMarker, init.BottomRightMarker);
                var xrange = (range.xleft, range.xright);
                var yrange = (range.ytop, range.ybottom);
                
                var methodInfo = firstOrDefault.type.GetMethod("InitValues");
                var method = methodInfo?.MakeGenericMethod(firstOrDefault.type);
                method?.Invoke(firstOrDefault.val, new object[] { xrange, yrange, init.Prefabs });
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
