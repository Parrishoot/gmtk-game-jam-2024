using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexControlManager : MonoBehaviour
{
    [SerializeField]
    private HexSpaceManager hexSpaceManager;

    // Start is called before the first frame update
    void Start()
    {
        if (hexSpaceManager.ChildBoardManager == null) {
            return;
        }

        hexSpaceManager.ChildBoardManager.boardControlManager.ControlTypeChanged += ProcessControlTypeChange;
    }

    private void ProcessControlTypeChange(ControlType type)
    {
        Color color = type switch {
            ControlType.ENEMY => Color.red,
            ControlType.PLAYER => Color.cyan,
            ControlType.CONTESTED => Color.gray,
            _ => hexSpaceManager.MaterialController.ResetColor
        };

        hexSpaceManager.MaterialController.SetResetColor(color);
        hexSpaceManager.MaterialController.SetColor(color);
    }
}
