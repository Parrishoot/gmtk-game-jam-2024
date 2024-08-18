using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Action<CharacterType> TurnStarted { get; set; }

    public Action<CharacterType> TurnEnded { get; set; }

    [SerializeField]
    private PlayerTurnManager playerTurnController;

    [SerializeField]
    private TurnController enemyTurnController;

    [SerializeField]
    private BoardManager parentBoardManager;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject enemyPrefab;

    private Vector2Int[] ENEMY_STARTING_SPACES = new Vector2Int[]{ 
        new Vector2Int(1, 0),
        new Vector2Int(2, 0),
        new Vector2Int(3, 0),
    };

    private Vector2Int[] PLAYER_STARTING_SPACES = new Vector2Int[]{ 
        new Vector2Int(1, 4),
        new Vector2Int(2, 4),
        new Vector2Int(3, 4),
    };

    private Dictionary<CharacterType, TurnController> turnControllers = new Dictionary<CharacterType, TurnController>();

    private CharacterType currentControllingPlayer;

    // TODO: Update this
    public bool IsPlayersTurn() {
        return true;
    }

    protected override void Awake()
    {
        base.Awake();

        turnControllers[CharacterType.PLAYER] = playerTurnController;
        turnControllers[CharacterType.ENEMY] = enemyTurnController;

        InitGame();
    }

    private void InitGame()
    {
        parentBoardManager.CreateBoard();

        foreach(Vector2Int coordinate in ENEMY_STARTING_SPACES) {
            HexSpaceManager hex = parentBoardManager.LayoutController.Board[coordinate.x, coordinate.y];
            CharacterManager characterManager = Instantiate(enemyPrefab).GetComponent<CharacterManager>();

            characterManager.transform.position = hex.OccupantPivot.position;
            hex.Occupy(characterManager);
        }

        foreach(Vector2Int coordinate in PLAYER_STARTING_SPACES) {
            HexSpaceManager hex = parentBoardManager.LayoutController.Board[coordinate.x, coordinate.y];
            CharacterManager characterManager = Instantiate(playerPrefab).GetComponent<CharacterManager>();

            characterManager.transform.position = hex.OccupantPivot.position;
            hex.Occupy(characterManager);
        }
    }

    public void Start() {
        StartTurn(CharacterType.PLAYER);
    }

    public void EndCurrentTurn() {
        
        turnControllers[currentControllingPlayer].EndTurn();
        TurnEnded?.Invoke(currentControllingPlayer);

        currentControllingPlayer = currentControllingPlayer == CharacterType.PLAYER ? CharacterType.ENEMY : CharacterType.PLAYER;

        StartTurn(currentControllingPlayer);
    }

    private void StartTurn(CharacterType characterType) {
        currentControllingPlayer = characterType;
        
        turnControllers[currentControllingPlayer].StartTurn();
        TurnStarted?.Invoke(currentControllingPlayer);
    }
}
