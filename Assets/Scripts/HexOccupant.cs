using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HexOccupantManager: MonoBehaviour
{
    [field:SerializeReference]
    public CharacterType CharacterType { get; private set; }

    [field:SerializeField]
    public HealthController HealthController { get; private set; }

    [field:SerializeField]
    public MaterialController MaterialController { get; private set; }

    [field:SerializeField]
    public HealthUIController healthUIController;

    public HexSpaceManager Hex { get; set; }

    public bool IsDamageable() {
        return HealthController != null;
    }

    public void FadeOut(float fadeTime, Ease easeType = Ease.InOutCubic) {
        MaterialController.FadeOut(fadeTime, easeType);
        healthUIController.Text.DOFade(0f, fadeTime).SetEase(easeType);
        healthUIController.Background.DOFade(0f, fadeTime).SetEase(easeType);
    }

    public void Hide() {
        MaterialController.Hide();
        healthUIController.Text.SetAlpha(0f);
        Color color = healthUIController.Background.color;
        color.a = 0f;
        healthUIController.Background.color = color;
    }

    public void FadeIn(float fadeTime, Ease easeType = Ease.InOutCubic) {
        MaterialController.FadeIn(fadeTime, easeType);
        healthUIController.Text.DOFade(1f, fadeTime).SetEase(easeType);
        healthUIController.Background.DOFade(1f, fadeTime).SetEase(easeType);
    }
}
