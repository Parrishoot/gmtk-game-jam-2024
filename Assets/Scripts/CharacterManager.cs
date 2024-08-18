using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : HexOccupantManager
{
    [field:SerializeField]
    public CharacterActionPoolController ActionPool { get; private set; }

    [field:SerializeReference]
    public TargetSelector TargetSelector{ get; private set; }

    public MovementController MovementController { get; set; }

    public void Start() {
        TeamManager.Instance.Roster[CharacterType].Add(this);
    }
}
