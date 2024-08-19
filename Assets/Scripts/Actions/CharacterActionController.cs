using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterActionController
{
    public Action ActionEnded { get; set; }

    public abstract void Load();

    public abstract bool IsValid();

    public abstract void Begin();

    public abstract void Cancel();

    protected CharacterManager characterManager { get; private set; }

    public CharacterActionController(CharacterManager characterManager) {
        this.characterManager = characterManager;
    }
}
