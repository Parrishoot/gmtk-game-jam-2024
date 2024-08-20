using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CreditsButtonController : MonoBehaviour
{
    public void OnClick() {
        gameObject.SetActive(true);
    }

    public void Update() {
        if(gameObject.activeSelf && Input.GetMouseButtonDown(1)) {
            gameObject.SetActive(false);
        }
    }
}
