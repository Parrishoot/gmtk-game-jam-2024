using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : Singleton<ColorManager>
{
    [field:SerializeReference]
    public Color PlayerHexColor { get; private set; }

    [field:SerializeReference]
    public Color EnemyHexColor { get; private set; }

    [field:SerializeReference]
    public Color ContestedColor { get; private set; }

    [field:SerializeReference]
    public Color TargetColor { get; private set; }
}
