using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEventManager : MonoBehaviour
{
    public Action Created {get; set; }

    public Action ZoomIn { get; set; }

    public Action ZoomOut { get; set; }

    public Action EnableBoard { get; set; }

    public Action DisableBoard { get; set; }
}
