using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnUIController : Singleton<TurnUIController>
{
    public Action AnimationFinished;

    [SerializeField]
    private float transitionTime = 1f;

    [SerializeField]
    private TMP_Text playerText;

    [SerializeField]
    private TMP_Text turnText;

    [SerializeField]
    private Image background;

    [SerializeField]
    private float offsetAmount = 10f;

    private Vector3 startingPos;

    void Start() {
        startingPos = transform.position;

        GameManager.Instance.GameOver += (characterType) => {
            gameObject.SetActive(false);
        };
    }

    void Update() {
        playerText.text = GameManager.Instance.CurrentControllingPlayer == CharacterType.PLAYER ? "YOUR" : "THEIR";
    }

    public void Animate(CharacterType characterType) {

        
        transform.position = startingPos + Vector3.up * offsetAmount;
        transform.localScale = Vector3.one * .9f;

        
    }
}
