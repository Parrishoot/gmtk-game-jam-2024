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
            ControlType.ENEMY => ColorManager.Instance.EnemyHexColor,
            ControlType.PLAYER => ColorManager.Instance.PlayerHexColor,
            ControlType.CONTESTED => ColorManager.Instance.ContestedColor,
            _ => Color.white
        };

        bool hidden = hexSpaceManager.MaterialController.IsHidden();

        hexSpaceManager.MaterialController.SetResetColor(color);
        hexSpaceManager.MaterialController.SetColor(color);

        if(hidden) {
            hexSpaceManager.MaterialController.Hide();
        }
    }
}
