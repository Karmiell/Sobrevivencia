using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathingHandler : MonoBehaviour
{
 

public static List<GridPosition> FindPath(GridPosition origin, GridPosition destiny)
    {
        //Debug.Log("iniciando a função FindPath()!");
        int securityVault = 0;
        var GridSystem = GridHandler.GetSystemPath();
        var nodeClose = new List<GridPathObject>();
        var nodeOpen = new List<GridPathObject>();
        GridPathObject originObjectPosition = GridSystem.GetGridObjectMainFromGridPosition(origin);
        GridPathObject EndObjectPosition = GridSystem.GetGridObjectMainFromGridPosition(destiny);
        GridPathObject currentPosition;

        for (int i = 0; i < GridSystem.GetAltura(); i++)
        {
            for (int j = 0; j < GridSystem.GetLargura(); j++)
            {
                var gridPosition = new GridPosition(i,j);
                var gridPathObject = GridSystem.GetGridObjectMainFromGridPosition(gridPosition);
                gridPathObject.SetH(0);
                gridPathObject.SetG(int.MaxValue);
                gridPathObject.CalculeF();
                gridPathObject.ResetCameFrom();
            }
        }
        currentPosition = originObjectPosition;
        currentPosition.SetG(0);
        currentPosition.SetH(GridSystem.DistanceBetwenGridPositions(GridSystem.GetGridPositionFromGridObject(currentPosition), destiny));
        currentPosition.CalculeF();
        
        nodeOpen.Add(originObjectPosition);
        
    while(nodeOpen.Count > 0)
        {
        //Debug.Log("iniciando o While! ja foram" + securityVault + "Loops conpletos");
        currentPosition = GridHandler.Instance.GetLowestF(nodeOpen);
       

          if(currentPosition == EndObjectPosition)
            {
                //Debug.Log("Achou o caminho!");
                return GridHandler.CalculatePath(EndObjectPosition);
            } 

           foreach(var atual in GridSystem.PickNighborArroundGridPosition(GridSystem.GetGridPositionFromGridObject(currentPosition)))
            {
                if(GridSystem.IsOutside(atual))continue;    
                var gridPathObject = GridSystem.GetGridObjectMainFromGridPosition(atual);
                if(nodeClose.Contains(gridPathObject))continue;
                var tentativeGCost = currentPosition.GetGValue() + GridSystem.DistanceBetwenGridPositions(atual, GridSystem.GetGridPositionFromGridObject(currentPosition));
                if(tentativeGCost < gridPathObject.GetGValue())
                {
                    //Debug.Log($"Cheguei no lugar para atribuir os valores para o objecto da posição: {atual}");
                    gridPathObject.SetG(tentativeGCost);
                    gridPathObject.SetH(GridSystem.DistanceBetwenGridPositions(atual, GridSystem.GetGridPositionFromGridObject(currentPosition)));
                    gridPathObject.CalculeF();
                    gridPathObject.SetCameFrom(currentPosition);
                    if(!nodeOpen.Contains(gridPathObject))
                    {
                    nodeOpen.Add(gridPathObject);
                    //Debug.Log($"Foi adicionado o {gridPathObject} a lisda nodeOpen!");
                    }                 
                }
            }
            nodeOpen.Remove(currentPosition);
            nodeClose.Add(currentPosition);
            securityVault++;
            if(securityVault >= 50)
            {
                Debug.Log("nao achou um caminho depois de 50 loops completos!");
                break;
            }

        }
     
        return null;
    }


}
