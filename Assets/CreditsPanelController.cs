using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CreditsPanelController : MonoBehaviour
{    public void Update() {
        if(gameObject.activeSelf && Input.GetMouseButtonDown(1)) {
            gameObject.SetActive(false);
        }
    }
}
