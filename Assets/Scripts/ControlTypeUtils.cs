using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ControlTypeUtils
{
    public static CharacterType? GetControlCharacterType(this ControlType controlType) {
        return controlType switch {
            ControlType.PLAYER => CharacterType.PLAYER,
            ControlType.ENEMY => CharacterType.ENEMY,
            _ => null
        };
    }
}
