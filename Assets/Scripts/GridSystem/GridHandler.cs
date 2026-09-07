using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GridHandler : MonoBehaviour
{
    public static GridHandler Instance;
    GridSystem<GridObject> gridSystem;
    GridSystem<GridPathObject> gridPathSystem;
    GridSystem<GridPathObject> gridSystemPath;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        gridSystem = new GridSystem<GridObject>(20,20,2, (GridSystem, GridPosition) =>
        {
            return new GridObject(GridSystem, GridPosition);
        });
        gridPathSystem = new GridSystem<GridPathObject>(20, 20, 2, (GridPathSystem, GridPathPosition) =>
        {
            return new GridPathObject(GridPathSystem,GridPathPosition);
        }
        );
    }

    private void Update()
    {
        if (Keyboard.current.tKey.isPressed)
        {
            var originGridPosition = new GridPosition(1,1);
            var endGridPosition =  new GridPosition(2,4);
            var pathfing = PathingHandler.FindPath(originGridPosition, endGridPosition);
         
            for(int i = 0; i < pathfing.Count - 1; i++)
            {
                Debug.DrawLine(GetWorlPositionFromGridPosition(pathfing[i]),GetWorlPositionFromGridPosition(pathfing[i + 1]), Color.white, 10f);
            }
        }
    }

    public GridPathObject GetLowestF(List<GridPathObject> list)
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

    

    public static Vector3 GetWorlPositionFromGridPosition(GridPosition gridPosition) => Instance.gridSystem.GetWorlPositionFromGridPosition(gridPosition);
    public static GridPosition GetGridPositionFromWorlposition(Vector3 position) => Instance.gridSystem.GetGridPositionFromWorlposition(position);
    public static GridObject GetGridObjectFromGridPosition(GridPosition gridPosition) => Instance.gridSystem.GetGridObjectMainFromGridPosition(gridPosition);
    public static GridObject[,] GetGridObjectArray() => Instance.gridSystem.GetDateMainArray();

    public static GridSystem<GridPathObject> GetSystemPath() => GridHandler.Instance.gridPathSystem;
    public static List<GridPosition> CalculatePath(GridPathObject gridPathObject)
    {
        int securityVault = 0;
        var path = new List<GridPosition>();
        var endNote = gridPathObject;
        while(endNote.GetCameFrom() != null)
        {
            var gridPosition = endNote.GetGridPosition();
            if(securityVault >= 50)
            {
                Debug.Log("O caminho foi encontrado, mas não foi possivel calcular ele pelas referencias em cada gridpathObject!");
                break;
            }
            path.Add(gridPosition);
            endNote = endNote.GetCameFrom();
            securityVault++;
        }
        path.Reverse();
        foreach(var atual in path)
        {
            Debug.Log($"Possiçoes do caminho Encontrado:{atual}");
        }
        return path;
    }
} 
