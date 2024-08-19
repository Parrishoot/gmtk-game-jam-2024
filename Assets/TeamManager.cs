using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{

    [SerializeField]
    private int totalActionPoints = 10;

    public List<CharacterManager> Roster { get; private set; } = new List<CharacterManager>();

    public int ActionPoints { get; private set; }

    void Awake() {
        ActionPoints = totalActionPoints;
    }

    public void SpendActionPoints(int cost) {
        ActionPoints = Math.Max(0, ActionPoints - cost);
    }

    public void ResetActionPoints() {
        ActionPoints = totalActionPoints;
    }
}
