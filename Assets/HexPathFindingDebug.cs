using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPathFindingDebug : MonoBehaviour
{
    [SerializeField]
    private HexSpaceManager hexSpaceManager;

    private List<HexSpaceManager> currentPath;

    private void Start() {
        hexSpaceManager.EventManager.HoverStart += ShowPath;

        hexSpaceManager.EventManager.HoverEnd += HidePath; 
    }

    private void ShowPath() {
        currentPath = PathFinder.GetPath(hexSpaceManager.ParentBoardManager, hexSpaceManager.Coordinate, new Vector2Int(0, 2));
    }

    private void HidePath() {
        currentPath = null;
    }

    private void OnDrawGizmos() {

        if(currentPath == null || hexSpaceManager.IsOccupied()) {
            return;
        }

        for(int i = 0; i <= currentPath.Count - 2; i++) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(currentPath[i].transform.position, currentPath[i+1].transform.position);
        }
    }
}
