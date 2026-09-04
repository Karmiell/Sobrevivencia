using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public class GridSystem<TObject>
{
    private int altura;
    private int largura;
    private int tamnhoDaCelula;
    private TObject[,] dateMainArray;
    public GridSystem(int altura, int largura, int tamnhoDaCelula, Func<GridSystem<TObject>, GridPosition, TObject> createObject)
    {
        this.altura = altura;
        this.largura = largura;
        this.tamnhoDaCelula = tamnhoDaCelula;
        dateMainArray = new TObject[altura, largura];

        for (int i = 0; i < altura; i++)
        {
            for (int j = 0; j < largura; j++)
            {
                var gridPosition = new GridPosition(i, j);
                dateMainArray[i, j] = createObject(this, gridPosition);
            }
        }
    }


    public List<GridPosition> PickNighborArroundGridPosition(GridPosition origin)
    {
     List<GridPosition> nighbors = new List<GridPosition>{
        origin+new GridPosition(1,0),
        origin+new GridPosition(0,1),
        origin+new GridPosition(-1,0),
        origin+new GridPosition(0,-1),
     }; 
     return nighbors;  
    }
 
      public Vector3 GetWorlPositionFromGridPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.GetX(),0,gridPosition.GetZ()) * tamnhoDaCelula;
    }
     public int DistanceBetwenGridPositions(GridPosition a, GridPosition b)
    {  
     var distanRough = a-b;
      return math.abs(distanRough.GetX()) + math.abs(distanRough.GetZ());
    }
    public GridPosition GetGridPositionFromWorlposition(Vector3 position)
    {
        return new GridPosition(Mathf.RoundToInt(position.x) / tamnhoDaCelula,
                                Mathf.RoundToInt(position.z) / tamnhoDaCelula);
    }
    public GridPosition GetGridPositionFromGridObject(TObject position)
    {
       if( position is GridPathObject)
        {
            return (position as GridPathObject).GetGridPosition();
        }
        else
        {
            return (position as GridObject).GetGridPosition();
        }
    }
    public TObject GetGridObjectMainFromGridPosition(GridPosition gridPosition)
    {
        return dateMainArray[gridPosition.GetX(),gridPosition.GetZ()];
    }


    public int GetAltura() => altura;
    public int GetLargura() => largura;
    public TObject[,] GetDateMainArray() => dateMainArray;
    public bool IsInside(GridPosition atual)
    {
        if(atual.GetX() < 0 || atual.GetX() > altura ||
           atual.GetZ() < 0 || atual.GetZ() > largura)return false;
        else return true;
    
    }

}
