using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CharacterActionControllerWithMetadata<T> : CharacterActionController
where T: ActionMetadata
{
    public T Meta { get; private set; }

    public CharacterActionControllerWithMetadata(MoveableOccupantManager moveableOccupantManager, T meta): base(moveableOccupantManager) {
        this.Meta = meta;
    }
}
