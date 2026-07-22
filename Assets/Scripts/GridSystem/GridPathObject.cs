using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
/* a formula do A * Pathfinding é: "F = G + H". G = é o valor fixo de movimentação para cada direção possivel aumentando a cada movimento feito
                                                H = é o caminho em linha "reta", ignorando obstáculos, ate o destino final quanto mais longe do destino, maior seu valor,
                                                F = Os menores valores de F serão a rota mais curta possivel*/
public class GridPathObject
{
 private GridSystem<GridPathObject> gridSystem;
 private GridPosition gridPosition;

 private int F;
 //private int H = 10;
 private int G = 10;
 
    public GridPathObject( GridSystem<GridPathObject> gridSystem,GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        
    }

    public void SetPathingFromGridPosition(GridPosition origin, GridPosition destiny)
    {
    
    }
    public int GetValueF() => F;
    public void SetValueG(int mod, bool isDiagonal = false)
    {
        if(!isDiagonal)G *= mod;
        else G = (G+4)*mod; 
    }
    private void SetValueH(int mod, int H){}
    private int DistanceBetwenGridPositions(GridPosition a, GridPosition b)
    {
      int aX = a.GetX();int aZ = a.GetZ(); int bX = b.GetX(); int bZ = b.GetZ();
      
      return math.abs(math.abs(aX) - math.abs(aZ)) + math.abs(math.abs(bX) - math.abs(bZ));
    }
    private bool FirstIsDiagonal(GridPosition gridPosition, GridPosition origin)
    {
        if(gridPosition == origin)return false;
        else
        {
            if(gridPosition.GetX() != origin.GetX()&& 
               gridPosition.GetZ() != origin.GetZ())return true;
        }
       return false;
    }
}
