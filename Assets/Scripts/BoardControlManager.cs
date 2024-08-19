using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardControlManager : MonoBehaviour
{
    public Action<ControlType> ControlTypeChanged;

    [SerializeField]
    private BoardManager boardManager;

    public ControlType ControlType = ControlType.EMPTY;

    public void UpdateControlType() {

        List<CharacterType> characterTypes = boardManager
            .LayoutController
            .Board
            .Flatten()
            .Where(x => x != null && x.Occupant != null)
            .Select(x => x.Occupant.CharacterType)
            .Distinct()
            .ToList();

        if(characterTypes.Count == 0) {
            ControlType = ControlType.EMPTY;
        }
        else if(characterTypes.Count > 1) {
            ControlType = ControlType.CONTESTED;
        }
        else if(characterTypes[0] == CharacterType.PLAYER) {
            ControlType = ControlType.PLAYER;
        }
        else {
            ControlType = ControlType.ENEMY;
        }

        ControlTypeChanged?.Invoke(ControlType);
    }
}
