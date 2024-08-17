using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HexBoardLayoutController : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardManager;

    [SerializeField]
    private GameObject hexObject;

    [SerializeField]
    private Transform hexesTransform; 

    [SerializeField]
    private GameObject childBoardControllerObject;

    [SerializeField]
    private Transform boardsTransform;

    [SerializeField]
    protected float hexSize = 1f;

    [SerializeField]
    protected float hexBuffer = .1f;

    public abstract float HexWidth { get; }

    public abstract float HexHeight { get; }

    public abstract float HexHorizontalOffset  { get; }

    public abstract float HexVerticalOffset { get; }

    public HexSpaceManager[,] Board { get; private set; }

    protected abstract int[,] Configuration { get; }


    public abstract Vector3 GetPositionForCoordinate(Vector2Int coordinate);

    public abstract CubedCoordinate GetCubedCoordinates(Vector2Int a);

    public List<HexSpaceManager> GetAdjacentObjects(Vector2Int coordinate) {
        
        int hexOffset = coordinate.y  % 2;

        List<Vector2Int> adjacentOffsets = new List<Vector2Int>{
            new Vector2Int(hexOffset - 1, -1),
            new Vector2Int(hexOffset, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(hexOffset - 1, 1),
            new Vector2Int(hexOffset, 1)
        };

        List<HexSpaceManager> adjacentObjects = new List<HexSpaceManager>();

        foreach(Vector2Int offset in adjacentOffsets) {

            Vector2Int coordinateToCheck = coordinate + offset;

            if(Valid(coordinateToCheck)) {
                adjacentObjects.Add(Board[coordinateToCheck.x, coordinateToCheck.y]);
            }
        }

        return adjacentObjects;
    }

    public bool Valid(Vector2Int coordinate) {

        // If it's out of bounds, it's not valid
        if (!(coordinate.x >= 0 && coordinate.x < Board.GetLength(0) &&
              coordinate.y >= 0 && coordinate.y < Board.GetLength(1))) {
            return false;
        }
        
        // Otherwise, it's only valid if there's a space there
        return Board[coordinate.x, coordinate.y] != null;
    }

    public void InitBoard() {
        
        Board = new HexSpaceManager[Configuration.GetLength(0), Configuration.GetLength(1)];

        for(int i = 0; i < Board.GetLength(0); i++) {
            for(int j = 0; j < Board.GetLength(1); j++) {
                
                // Don't Create the Object if we don't want one
                // THIS IS NOT A BUG DO NOT MESS THIS UP AGAIN
                // THIS IS [i,j] AND NOT [j,i] BECAUSE OF HOW THE
                // INDEXES IN 2D ARRAYS WORK
                if(Configuration[i, j] == 0) {
                    continue;
                }

                Vector2Int coordinate = new Vector2Int(j, i);

                BoardManager childBoardManager = InitAsChild(coordinate);

                HexSpaceManager hexGameObject = Instantiate(hexObject, hexesTransform).GetComponent<HexSpaceManager>();
                hexGameObject.Init(boardManager, childBoardManager, coordinate);

                // TODO: REMOVE THIS WHEN DONE DEBUGGING
                if(i != 0 && j == 2) {
                    hexGameObject.Occupy(new HexOccupant());
                    MeshRenderer meshRenderer = hexGameObject.gameObject.GetComponentInChildren<MeshRenderer>();
                    meshRenderer.material.color = Color.red;
                }

                Board[j, i] = hexGameObject;
            }
        }
    }

    private BoardManager InitAsChild(Vector2Int coordinate) {

        if(childBoardControllerObject == null) {
            return null;
        }

        GameObject childBoardObject = Instantiate(childBoardControllerObject, boardsTransform);
        BoardManager childBoardManager = childBoardObject.GetComponent<BoardManager>();

        
        childBoardManager.CreateBoard();
        childBoardManager.ForEachHex((hex) => {
            hex.Hide();
        });

        childBoardManager.DisableBoard();

        childBoardManager.transform.localPosition = GetPositionForCoordinate(coordinate);
        
        return childBoardManager;
    }

    public int CalculateDistance(Vector2Int a, Vector2Int b) {

        CubedCoordinate aCubed = GetCubedCoordinates(a);
        CubedCoordinate bCubed = GetCubedCoordinates(b);

        Vector3 cube = new Vector3(
            aCubed.q - bCubed.q,
            aCubed.r - bCubed.r,
            aCubed.s - bCubed.s
        );

        return (int) ((Mathf.Abs(cube.x) + Mathf.Abs(cube.y) + Mathf.Abs(cube.z)) / 2);
    }
}
