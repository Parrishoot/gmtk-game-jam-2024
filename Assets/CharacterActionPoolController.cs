using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionPoolController : MonoBehaviour
{
    [SerializeField]
    private MoveableOccupantManager moveableOccupantManager;

    [SerializeField]
    private List<ActionMetadata> actions;

    private CharacterActionController currentAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            if(currentAction == null) {

                // TODO: ADD REAL LOGIC HERE
                ActionMetadata action = actions[0];
                currentAction = action.GetController(moveableOccupantManager);

                Debug.Log("Performing: " + action.Name);

                currentAction.ActionEnded += () => currentAction = null;
                currentAction.Begin();
            }
        }    
    }
}
