using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class APUIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    void Start() {
        GameManager.Instance.TurnStarted += (characterType) => {
            if (characterType == CharacterType.PLAYER) {
                gameObject.SetActive(true);
            }
        };

        GameManager.Instance.TurnEnded += (characterType) => {
            if (characterType == CharacterType.PLAYER) {
                gameObject.SetActive(false);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        text.text = TeamMasterManager.Instance.Managers[CharacterType.PLAYER].ActionPoints.ToString();
    }
}
