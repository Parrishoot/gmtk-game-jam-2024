using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUIController : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    [SerializeField]
    private HealthController healthController;

    [SerializeField]
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        canvas.worldCamera = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {
        text.text = healthController.CurrentHealth.ToString();
    }
}
