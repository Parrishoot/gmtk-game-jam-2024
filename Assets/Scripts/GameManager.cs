using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Action<CharacterType> TurnStarted { get; set; }

    public Action<CharacterType> TurnEnded { get; set; }

    public Action<CharacterType> GameOver { get; set; }

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

    private bool gameOver = false;

    private Vector2Int[] ENEMY_STARTING_SPACES = new Vector2Int[]{ 
        new Vector2Int(1, 0),
        new Vector2Int(2, 0),
        new Vector2Int(3, 0),
        new Vector2Int(0, 1),
        new Vector2Int(3, 1),
    };

    private Vector2Int[] PLAYER_STARTING_SPACES = new Vector2Int[]{ 
        new Vector2Int(1, 4),
        new Vector2Int(2, 4),
        new Vector2Int(3, 4),
        new Vector2Int(0, 3),
        new Vector2Int(3, 3),
    };

    private Dictionary<CharacterType, TurnController> turnControllers = new Dictionary<CharacterType, TurnController>();

    public CharacterType CurrentControllingPlayer { get; private set; }

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

        parentBoardManager.boardControlManager.UpdateControlType();
    }

    public void Start() {
        CurrentControllingPlayer = CharacterType.PLAYER;
        StartTurn();
    }

    public void EndCurrentTurn() {
        
        turnControllers[CurrentControllingPlayer].EndTurn();
        TurnEnded?.Invoke(CurrentControllingPlayer);
        
        CurrentControllingPlayer = CurrentControllingPlayer == CharacterType.PLAYER ? CharacterType.ENEMY : CharacterType.PLAYER;

        if(HexMasterManager.Instance.ActiveHex != null) {
            HexMasterManager.Instance.ZoomFinished += StartTurn;
            HexMasterManager.Instance.ZoomOut();
            return;
        }

        StartTurn();
    }

    private void CheckGameOver()
    {
        if(gameOver && Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        

        if(!gameOver) {
            if(TeamMasterManager.Instance.Managers[CharacterType.PLAYER].Roster.Count == 0) {
                GameOver?.Invoke(CharacterType.ENEMY);
                MouseLockManager.Instance.MouseLocked = true;
                gameOver = true;
            }

            if(TeamMasterManager.Instance.Managers[CharacterType.ENEMY].Roster.Count == 0) {
                GameOver?.Invoke(CharacterType.PLAYER);
                MouseLockManager.Instance.MouseLocked = true;
                gameOver = true;
            }
        }

    }

    private void StartTurn() {

        HexMasterManager.Instance.ZoomFinished -= StartTurn;

        turnControllers[CurrentControllingPlayer].StartTurn();
        TurnStarted?.Invoke(CurrentControllingPlayer);
    }

    void Update() {
        CheckGameOver();
    }
}
