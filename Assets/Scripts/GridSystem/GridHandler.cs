using System;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    public static GridHandler Instance;
    GridSystem<GridObject> gridSystem;
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
        
    }
    

    

    public static Vector3 GetWorlPositionFromGridPosition(GridPosition gridPosition) => Instance.gridSystem.GetWorlPositionFromGridPosition(gridPosition);
    public static GridPosition GetGridPositionFromWorlposition(Vector3 position) => Instance.gridSystem.GetGridPositionFromWorlposition(position);
    public static GridObject GetGridObjectFromGridPosition(GridPosition gridPosition) => Instance.gridSystem.GetGridObjectMainFromGridPosition(gridPosition);
    public static GridObject[,] GetGridObjectArray() => Instance.gridSystem.GetDateMainArray();
   
} 
