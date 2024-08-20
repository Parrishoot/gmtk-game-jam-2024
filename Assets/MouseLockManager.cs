using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The kind of solutions you come up with at midnight before the game jam ends
public class MouseLockManager : Singleton<MouseLockManager>
{
    public bool MouseLocked { get; set; } = false; 
}
