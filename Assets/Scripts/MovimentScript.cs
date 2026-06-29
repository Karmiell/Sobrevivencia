using System;
using UnityEngine;

public class MovimentScript : MonoBehaviour
{
    [SerializeField]private float moveVelocity = 5f;

    public static event Action<Vector2> OnAnyMovimentValue;

    private Vector2 moveValue;

    private void Update()
    {
        moveValue = InputPlayerHandler.Instance.GetMovimentInputNormalized();
        if(moveValue == Vector2.zero)return;
        MoveFrom(moveValue);
        OnAnyMovimentValue?.Invoke(moveValue);
       
    }

    private void MoveFrom(Vector2 moveValue)
    {
        var moveDir = new Vector3(moveValue.x,transform.position.y,moveValue.y);

        transform.position += moveDir * Time.deltaTime * moveVelocity; 
    }
}
