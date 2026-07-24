using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
public class GridSystem<TObject>
{
    private int altura;
    private int largura;
    private int tamnhoDaCelula;
    private TObject[,] dateMainArray;
    private List<GridPathObject> nodeClose = new List<GridPathObject>();
    private List<GridPathObject> nodeOpen = new List<GridPathObject>();
    private List<GridPathObject> nodesChange = new List<GridPathObject>();
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
    private TObject[,] GetTObjectsArroundGridPosition(GridPosition gridPosition)
    {
        var arroundTObjects = new TObject[3,3];
        int ii = 0;
        int jj = 0;
       for(int i = -gridPosition.GetX(); i < (gridPosition.GetX() + 1); i++)
        {
            for(int j = -gridPosition.GetZ(); j < (gridPosition.GetZ() + 1); j++)
            {
                if (dateMainArray[i, j].GetHashCode() == dateMainArray[gridPosition.GetX(), gridPosition.GetZ()].GetHashCode()) continue;
                arroundTObjects[ii, jj] = dateMainArray[i, j];
                jj++;
            }
            ii++;
            jj = 0;
        }
        return arroundTObjects;
    }

    public void AlgoritimoA(GridPosition origin, GridPosition destiny)
    {
        var gridObject = GetGridObjectFromGridPosition(origin) as GridPathObject;
        var pathing = new List<GridPathObject>();
        int securityVault = 0;
        while (true) 
        {
            nodeOpen = new List<GridPathObject>(){GetTObjectsArroundGridPosition(origin) as GridPathObject};
        
            foreach(var atual in nodeOpen)
            {
                atual.SetValuesForGridPositions(origin, destiny);
            }
            var atualNode = PickHigthFValue(nodeOpen);
            gridObject.SetNodePai(atualNode);
            gridObject = atualNode;
            pathing.Add(gridObject);
            if(gridObject.GetGridPosition() == destiny)break;
            if(securityVault >= 50)
            {
                Debug.LogError("Não Funcionou :/");
                break;
            }
            securityVault++;
        }


    }
    private GridPathObject PickHigthFValue(List<GridPathObject> nodeOpen)
    {
        List<int> fValueList = new List<int>();
        foreach(var atual in nodeOpen)
        {
            fValueList.Add(atual.GetFValue());
        }
        var fMaxValue = fValueList.Max();

        return nodeOpen[fValueList.IndexOf(fMaxValue)];
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

    public TObject GetGridObjectFromGridPosition(GridPosition gridPosition)
    {
        return dateMainArray[gridPosition.GetX(),gridPosition.GetZ()];
    }
}
