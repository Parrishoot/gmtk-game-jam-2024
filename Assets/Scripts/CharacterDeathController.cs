using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeathController : MonoBehaviour
{
    [SerializeField]
    private CharacterManager characterManager;

    void Start() {
        characterManager.HealthController.OnDeath += ProcessDeath;
    }

    private void ProcessDeath() {

        characterManager.Hex.ClearOccupant();
        characterManager.Hex.ParentBoardManager.boardControlManager.UpdateControlType();

        TeamMasterManager.Instance.Managers[characterManager.CharacterType].Roster.Remove(characterManager);
        Destroy(gameObject);
    }
}
