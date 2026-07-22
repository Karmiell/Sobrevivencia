using System.Collections.Generic;


public class GridObject
{
    private GridSystem<GridObject> gridSystem;
    private GridPosition gridPosition;
    private List<Base_Unit> units;

    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        units = new List<Base_Unit>();
    }
    public override string ToString()
    {
        return gridPosition.ToString();
    }
    public GridPosition GetGridPosition() => gridPosition;

  
   
}
