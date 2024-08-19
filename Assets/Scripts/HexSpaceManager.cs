using System.Collections.Generic;
using UnityEngine;

public class HexSpaceManager : MonoBehaviour
{
    [field:SerializeReference]
    public HexEventManager EventManager { get; private set; }

    [field:SerializeField]
    public Transform OccupantPivot { get; private set; }

    [field:SerializeReference]
    public MaterialController MaterialController {get; private set; }

    public Vector2Int Coordinate { get; private set; }

    public BoardManager ParentBoardManager { get; private set; }

    public BoardManager ChildBoardManager { get; private set; }

    public HexOccupantManager Occupant { get; private set; }

    public void Init(BoardManager parentBoardController, BoardManager childBoardController, Vector2Int coordinate) {
        
        this.ParentBoardManager = parentBoardController;
        ChildBoardManager = childBoardController;

        if(childBoardController != null) {
            childBoardController.ContainingHex = this;
        }

        transform.localPosition = ParentBoardManager.LayoutController.GetPositionForCoordinate(coordinate);
        Coordinate = coordinate;

        gameObject.name = coordinate.x.ToString() + "_" + coordinate.y.ToString();
    }

    public List<HexSpaceManager> GetAdjacentHexes() {
        return ParentBoardManager.LayoutController.GetAdjacentObjects(Coordinate);
    }

    public Vector3 GetOffset() {
        return ParentBoardManager.transform.position + ParentBoardManager.LayoutController.GetPositionForCoordinate(Coordinate);
    }

    public List<HexSpaceManager> GetHexesInRange(int range) {
        return ParentBoardManager.FindAllHexes((targetHex) => DistanceToHex(targetHex) <= range && targetHex != this);
    }

    public int DistanceToHex(HexSpaceManager targetHex) {
        return ParentBoardManager.LayoutController.CalculateDistance(Coordinate, targetHex.Coordinate);
    }

    public bool ContainsBoard() {
        return ChildBoardManager != null;
    }

    public void ZoomIn() {
        EventManager.ZoomIn?.Invoke();
    }

    public void ZoomOut() {
        EventManager.ZoomOut?.Invoke();
    }

    public void FadeIn() {
        EventManager.FadeIn?.Invoke();
    }

    public void FadeOut() {
        EventManager.FadeOut?.Invoke();
    }

    public void Hide() {
        EventManager.Hide?.Invoke();
    }

    public bool IsOccupied() {
        return Occupant != null;
    }

    public void Occupy(HexOccupantManager occupant) {

        if(occupant.Hex != null) {
            occupant.Hex.ClearOccupant();
        }

        Occupant = occupant;
        occupant.Hex = this;
        occupant.transform.SetParent(OccupantPivot);
    }

    public void ClearOccupant() {   
        Occupant.transform.parent = null;
        Occupant = null;
    }
}
