using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    [SerializeField]
    private HealthController healthController;

    [field:SerializeReference]
    public TMP_Text Text { get; private set; }

    [field:SerializeReference]
    public Image Background { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = healthController.CurrentHealth.ToString();
    }
}
