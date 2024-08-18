using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexOccupantManager: MonoBehaviour
{
    [field:SerializeField]
    public HealthController HealthController { get; private set; }

    [field:SerializeField]
    public MaterialController MaterialController { get; private set; }

    public HexSpaceManager Hex { get; set; }

    public bool IsDamageable() {
        return HealthController != null;
    }
}
