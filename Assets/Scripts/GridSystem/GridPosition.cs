using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public struct GridPosition
{
    int X;
    int Z;

    public GridPosition(int X, int Z)
    {
        this.X = X;
        this.Z = Z;
    }
    public int GetX() => X;
    public int GetZ() => Z;
     
    public override string ToString()
    {
        return $"X:{X}|{Z}";
    }

    public override bool Equals(object obj)
    {
        if(!(obj is GridPosition))return false;
        return (GridPosition)obj == this;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Z,X);
    }


    public static GridPosition operator + (GridPosition a, GridPosition b) => new GridPosition(a.X+b.X, a.Z+b.Z);
    public static GridPosition operator - (GridPosition a, GridPosition b) => new GridPosition(a.X-b.X, a.Z-b.Z);
    public static bool operator == (GridPosition a, GridPosition b) => a.X == b.X && a.Z == b.Z;
    public static bool operator != (GridPosition a, GridPosition b) => !(a == b);


}
