using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BoardManager : MonoBehaviour
{
    [field:SerializeField]
    public BoardEventManager EventManager { get; private set; }

    [field:SerializeReference]
    public HexBoardLayoutController LayoutController { get; private set; }

    [field:SerializeField]
    public Transform HexesTransform { get; private set; }

    [field:SerializeField]
    public Transform ChildBoardsTransform { get; private set; }

    public bool BoardEnabled { get; private set; } = true;

    // Update is called once per frame
    public void CreateBoard()
    {
        LayoutController.InitBoard();
        EventManager.Created?.Invoke();
    }

    public void ForEachHex(Action<HexSpaceManager> action) {
        for(int i = 0; i < LayoutController.Board.GetLength(0); i++) {
            for(int j = 0; j < LayoutController.Board.GetLength(1); j++) {
                Vector2Int coordinate = new Vector2Int(j, i);
                if(LayoutController.Valid(coordinate)) {
                    action?.Invoke(LayoutController.Board[j, i]);
                }
            }    
        }
    }

    public void ZoomIn()
    {
        EventManager.ZoomIn?.Invoke();
    }

    public void ZoomOut() {
        EventManager.ZoomOut?.Invoke();
    }

    public void EnableBoard() {
        BoardEnabled = true;
        EventManager.EnableBoard?.Invoke();
    }

    public void DisableBoard() {
        BoardEnabled = false;
        EventManager.DisableBoard?.Invoke();
    }
}
