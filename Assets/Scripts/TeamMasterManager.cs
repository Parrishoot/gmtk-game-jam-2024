using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMasterManager : Singleton<TeamMasterManager>
{
    [SerializeField]
    private TeamManager playerTeamManager;

    [SerializeField]
    private TeamManager enemyTeamManager;

    public Dictionary<CharacterType, TeamManager> Managers {get; private set;} = new Dictionary<CharacterType, TeamManager>();

    protected override void Awake() {

        base.Awake();

        Managers[CharacterType.PLAYER] = playerTeamManager;
        Managers[CharacterType.ENEMY] = enemyTeamManager;

        GameManager.Instance.TurnEnded += ResetActionPoints;
    }

    private void ResetActionPoints(CharacterType type)
    {
        Managers[type].ResetActionPoints();
    }
}
