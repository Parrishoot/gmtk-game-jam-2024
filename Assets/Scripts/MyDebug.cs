using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDebug : MonoBehaviour
{
    [SerializeField]
    private MoveableOccupantManager hexOccupantManager;

    [SerializeField]
    private BoardManager boardManager;

    private void OnEnable() {
        
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            boardManager.GetClosestOpenHexTo(new Vector2Int(2, 2)).Occupy(hexOccupantManager);
            hexOccupantManager.MovementController.Move(boardManager.LayoutController.Board[4, 2]);
        }
    }
}
