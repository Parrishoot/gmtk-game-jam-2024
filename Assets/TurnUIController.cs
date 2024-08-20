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
    }

    void Update() {
        playerText.text = GameManager.Instance.CurrentControllingPlayer == CharacterType.PLAYER ? "YOUR" : "THEIR";
    }

    public void Animate(CharacterType characterType) {

        
        transform.position = startingPos + Vector3.up * offsetAmount;
        transform.localScale = Vector3.one * .9f;

        DOTween.Sequence()
            .Append(transform.DOMove(startingPos, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Join(background.DOFade(1f, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Join(playerText.DOFade(1f, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Join(turnText.DOFade(1f, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Join(transform.DOScale(Vector3.one, transitionTime / 2).SetEase(Ease.InOutCubic))
            .AppendInterval(transitionTime / 2)
            .Append(transform.DOMove(startingPos + Vector3.down * offsetAmount, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Join(background.DOFade(0f, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Join(playerText.DOFade(0f, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Join(turnText.DOFade(0f, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Join(transform.DOScale(Vector3.one * .75f, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Play()
            .OnComplete(() => AnimationFinished?.Invoke());
    }
}
