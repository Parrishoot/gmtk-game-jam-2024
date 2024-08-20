using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    public void SetColor(Color color) {
        meshRenderer.material.SetColor("_Color", color);
    }
}
