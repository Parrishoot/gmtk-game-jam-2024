using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Action OnMovementFinished {get; set; }

    [SerializeField]
    private MoveableOccupantManager hexOccupantManager;

    [SerializeField]
    private float movementSpeed = .25f;

    void Start() {
        hexOccupantManager.MovementController = this;
    }

    public void Move(HexSpaceManager target) {
        Move(PathFinder.GetPath(hexOccupantManager.Hex.ParentBoardManager, hexOccupantManager.Hex.Coordinate, target.Coordinate));
    }

    public void Move(List<HexSpaceManager> path) {
        
        Sequence moveSequence = DOTween.Sequence();

        foreach(HexSpaceManager pathNode in path) {
            moveSequence.Append(transform.DOMove(pathNode.OccupantPivot.position, movementSpeed).SetEase(Ease.InOutQuad));
        }

        moveSequence.OnComplete(() => { 
            path.Last().Occupy(hexOccupantManager);
            OnMovementFinished?.Invoke();

            OnMovementFinished = null;
        });

        moveSequence.Play();
    }    
}
