using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListUtils
{
    public static T GetRandomSelection<T>(this List<T> l) {
        
        if(l.Count == 0) {
            return default;
        }
        
        return l[Random.Range(0, l.Count)];
    } 
}
