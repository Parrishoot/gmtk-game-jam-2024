using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> howToPlayPanels = new List<GameObject>();

    private int index = 0;

    // Start is called before the first frame update
    private void OnEnable() {
        index = 0;
        howToPlayPanels[0].SetActive(true);    
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf && Input.GetMouseButtonDown(0)) {
            howToPlayPanels[index].SetActive(false);
            index = (index+1) % howToPlayPanels.Count;
            howToPlayPanels[index].SetActive(true);
        }
    }
}
