using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : HexOccupantManager
{
    [field:SerializeField]
    public CharacterActionPoolController ActionPool { get; private set; }

    [field:SerializeReference]
    public TargetSelector TargetSelector{ get; private set; }

    [field:SerializeReference]
    public CharacterAnimationController AnimationController { get; private set; }

    public MovementController MovementController { get; set; }

    public void Start() {
        TeamMasterManager.Instance.Managers[CharacterType].Roster.Add(this);
    }

    
    public int GetAdjacencyBonuses() {
        List<HexSpaceManager> hexSpaceManagers = Hex.GetAdjacentHexes();

        int adjacencyBonuses = 0;

        foreach(HexSpaceManager neighbor in hexSpaceManagers) {

            if(neighbor.ChildBoardManager == null) {
                continue;
            }

            if(neighbor.ChildBoardManager.boardControlManager.ControlType.GetControlCharacterType() == CharacterType) {
                adjacencyBonuses++;
            }
        }

        return adjacencyBonuses;
    }
}
