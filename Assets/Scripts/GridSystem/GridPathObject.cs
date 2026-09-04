using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
/* a formula do A * Pathfinding é: "F = G + H". G = é o valor fixo de movimentação para cada direção possivel aumentando a cada movimento feito
                                                H = é o caminho em linha "reta", ignorando obstáculos, ate o destino final quanto mais longe do destino, maior seu valor,
                                                F = Os menores valores de F serão a rota mais curta possivel*/
public class GridPathObject
{
private const int MOVE_FORWARDS = 10;
 private GridSystem<GridPathObject> gridSystem;
 private GridPosition gridPosition;
private GridPathObject cameFrom;
 private int H;
 private int G;
 private  int F;


    public GridPathObject( GridSystem<GridPathObject> gridSystem,GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
      
    }
    
    public void SetH(int amount)
    {
       H = amount + MOVE_FORWARDS;
    }
    public void SetG(int amount)
    {
       G = MOVE_FORWARDS * amount;
    }
    public void CalculeF() => F = G + H;
  
    public void SetNodePai(GridPathObject nodePai) => cameFrom = nodePai;
    public  int GetFValue() => F; 
    public GridPosition GetGridPosition() => gridPosition;
}
