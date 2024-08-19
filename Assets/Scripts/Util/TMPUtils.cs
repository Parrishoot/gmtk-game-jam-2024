using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static class TMPUtil
{
    public static void SetAlpha(this TMP_Text text, float a) {
        Color color = text.color;
        color.a = a;

        text.color = color;
    }
}
