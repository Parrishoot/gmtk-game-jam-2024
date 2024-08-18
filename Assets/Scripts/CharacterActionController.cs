using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterActionController
{
    public Action ActionEnded { get; set; }

    public abstract void Begin();

    protected MoveableOccupantManager occupantManager { get; private set; }

    public CharacterActionController(MoveableOccupantManager occupantManager) {
        this.occupantManager = occupantManager;
    }
}
