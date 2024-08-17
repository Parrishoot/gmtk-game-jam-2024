using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentBoardManager : MonoBehaviour
{
    [SerializeField]
    private BoardManager parentBoardManager;

    // Start is called before the first frame update
    void Start()
    {
        parentBoardManager.CreateBoard();
    }
}
