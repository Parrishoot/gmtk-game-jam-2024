using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : Singleton<TeamManager>
{
    public Dictionary<CharacterType, List<CharacterManager>> Roster {get; private set;} = new Dictionary<CharacterType, List<CharacterManager>>();

    protected override void Awake() {

        base.Awake();

        Roster[CharacterType.PLAYER] = new List<CharacterManager>();
        Roster[CharacterType.ENEMY] = new List<CharacterManager>();
    }
}
