using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionPoolController : MonoBehaviour
{
    [SerializeField]
    private CharacterManager characterManager;

    [SerializeField]
    public List<ActionMetadata> Actions;

    private CharacterActionController currentAction;

    public void StartAction(ActionMetadata actionMetadata) {
        currentAction = actionMetadata.GetController(characterManager);
        currentAction.Load();
        currentAction.Begin();
    }

    public void CancelAction() {
        if(currentAction != null) {
            currentAction.Cancel();
        }
    }
}
