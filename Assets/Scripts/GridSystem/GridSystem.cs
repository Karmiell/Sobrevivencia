using System;
using System.Reflection;
using UnityEngine;
public class GridSystem<TObject>
{
 private int altura;
 private int largura;
 private int tamnhoDaCelula;
 private TObject[,] dateMainArray;

 public GridSystem(int altura, int largura, int tamnhoDaCelula, Func<GridSystem<TObject>,GridPosition,TObject> createObject) 
    {
        this.altura = altura;
        this.largura = largura;
        this.tamnhoDaCelula = tamnhoDaCelula;
        dateMainArray = new TObject[altura,largura];

        for(int i = 0; i < altura; i++)
        {
            for(int j = 0; j < largura; j++)
            {
                var gridPosition = new GridPosition(i,j);
                dateMainArray[i,j] = createObject(this,gridPosition);
            }
        }
    }
      public Vector3 GetWorlPositionFromGridPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.GetX(),0,gridPosition.GetZ()) * tamnhoDaCelula;
    }
    public GridPosition GetGridPositionFromWorlposition(Vector3 position)
    {
        return new GridPosition(Mathf.RoundToInt(position.x) / tamnhoDaCelula,
                                Mathf.RoundToInt(position.z) / tamnhoDaCelula);
    }
    public TObject GetGridObjectMainFromGridPosition(GridPosition gridPosition)
    {
        return dateMainArray[gridPosition.GetX(),gridPosition.GetZ()];
    }


    public int GetAltura() => altura;
    public int GetLargura() => largura;
    public TObject[,] GetDateMainArray() => dateMainArray;

    public TObject GetGridObjectFromGridPosition(GridPosition gridPosition)
    {
        return dateMainArray[gridPosition.GetX(),gridPosition.GetZ()];
    }
}
