using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubedCoordinate
{
    public int q { get; private set; }

    public int r { get; private set; }

    public int s { get; private set; }

    public CubedCoordinate(int q, int r, int s)
    {
        this.q = q;
        this.r = r;
        this.s = s;
    }   
}
