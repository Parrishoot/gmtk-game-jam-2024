using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResultUIPanelController : MonoBehaviour
{
    [SerializeField]
    private float transitionTime = .5f;

    [SerializeField]
    private TMP_Text youText;

    [SerializeField]
    private TMP_Text winOrLoseText;

    [SerializeField]
    private TMP_Text clickAnywhereText;

    [SerializeField]
    private Image background;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.GameOver += Animate;
    }

    private void Animate(CharacterType type)
    {
        string winnerText = type == CharacterType.PLAYER ? "WIN" : "LOSE";

        winOrLoseText.text = winnerText;

        DOTween.Sequence()
            .Join(youText.DOFade(1f, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Join(winOrLoseText.DOFade(1f, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Join(clickAnywhereText.DOFade(1f, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Join(background.DOFade(1f, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Join(transform.DOScale(Vector3.one * 1.1f, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Append(transform.DOScale(Vector3.one, transitionTime / 2).SetEase(Ease.InOutCubic))
            .Play();
    }
}
