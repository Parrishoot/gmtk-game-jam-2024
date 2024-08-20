using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    public Color ResetColor { get; private set; }

    void Start() {
        SetResetColor(meshRenderer.material.color);
    }

    public Color GetColor() {
        return meshRenderer.material.color;
    }

    public void Hide() {
        Color color = meshRenderer.material.color;
        color.a = 0f;

        meshRenderer.material.color = color;
    }

    public void SetColor(Color color) {
        meshRenderer.material.color = color;
    }

    public void SetTargeted(Color color) {

    }
    
    public void ResetHexColor() {
        meshRenderer.material.color = ResetColor;
    }

    public void FadeIn(float fadeTime, Ease easeType=Ease.InOutCubic) {
        Fade(fadeTime, 1f, easeType);
    }

    public void FadeOut(float fadeTime, Ease easeType=Ease.InOutCubic) {
        Fade(fadeTime, 0f, easeType);
    }

    private void Fade(float fadeTime, float targetValue, Ease easeType=Ease.InOutCubic) {
        meshRenderer.material.DOFade(targetValue, fadeTime).SetEase(easeType);   
    }

    public void SetResetColor(Color newResetColor) {

        newResetColor.a = 1f;

        ResetColor = newResetColor;
    }
}


