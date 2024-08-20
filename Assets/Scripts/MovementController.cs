using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private CharacterManager hexOccupantManager;

    [SerializeField]
    private float movementSpeed = .25f;

    [SerializeField]
    private AudioSource movementAudioSource;

    void Start() {
        hexOccupantManager.MovementController = this;
    }

    public void Move(HexSpaceManager target, Action OnMovementFinished = null) {
        Move(PathFinder.GetPath(hexOccupantManager.Hex.ParentBoardManager, hexOccupantManager.Hex.Coordinate, target.Coordinate), OnMovementFinished);
    }

    public void Move(List<HexSpaceManager> path, Action OnMovementFinished = null) {
        
        Sequence moveSequence = DOTween.Sequence();

        if(path == null) {
            Debug.LogWarning("Trying to move on a null path");
            return;
        }

        foreach(HexSpaceManager pathNode in path) {
            moveSequence.Append(transform.DOMove(pathNode.OccupantPivot.position, movementSpeed).SetEase(Ease.InOutQuad));
        }

        movementAudioSource.Play();

        moveSequence.OnComplete(() => { 
            path.Last().Occupy(hexOccupantManager);
            OnMovementFinished?.Invoke();

            movementAudioSource.Stop();
        });

        moveSequence.Play();
    }    
}
