using System.Collections.Generic;
using UnityEngine;

public class PathindHandler : MonoBehaviour
{
 

public static List<GridPosition> FindPath(GridPosition origin, GridPosition destiny)
    {
        var GridSystem = GridHandler.GetSystemPath();
        var path = new List<GridPosition>();
        var nodeClose = new List<GridPathObject>();
        var nodeOpen = new List<GridPathObject>();

        var gridPositionAtual = origin;
        int time = 1;
        for (int i = 0; i < GridSystem.GetAltura(); i++)
        {
            for (int j = 0; j < GridSystem.GetLargura(); j++)
            {
                var gridPosition = new GridPosition(i,j);
                var gridPathObject = GridSystem.GetGridObjectMainFromGridPosition(gridPosition);
                gridPathObject.SetH(0);
                gridPathObject.SetG(int.MaxValue);
                gridPathObject.CalculeF();
            }
        }

        while(path.Count < GridSystem.DistanceBetwenGridPositions(origin, destiny))
        {
        var nighbors = GridSystem.PickNighborArroundGridPosition(gridPositionAtual);
        foreach(var atual in nighbors)
        {
            if(!GridSystem.IsInside(atual))continue;

            var gridPathObject = GridSystem.GetGridObjectMainFromGridPosition(atual);
            gridPathObject.SetH(GridSystem.DistanceBetwenGridPositions(atual, destiny));
            gridPathObject.SetG(time);
            gridPathObject.CalculeF();
            nodeClose.Add(gridPathObject);
        }
        


        }
        return path;
    }

private GridPathObject GetLowestF(List<GridPathObject> list)
{
   var supostLowesF = list[0];
   for(int i = 0; i < list.Count; i++)
    {
        if(list[i].GetFValue() < supostLowesF.GetFValue())
        {
        supostLowesF = list[i];
        }
    }
    return supostLowesF;
}
}
