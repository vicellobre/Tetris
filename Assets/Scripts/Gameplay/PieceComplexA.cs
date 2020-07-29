using UnityEngine;

public class PieceComplexA : Piece
{

    protected override void Start()
    {
        base.Start();
        // Re-ubicando la pieza
        //rigidbody2D.position += new Vector2(-tolerance, 0);
        direction = rigidbody2D.position;
    }

    /// <summary>
    /// Rota la pieza 90 grados
    /// </summary>
    protected override void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.forward, angle);
            fixPosition = true;
        }
    }

    
    private void FixedUpdate()
    {
        if (fixPosition)
        {
            RotateChildren();
            rigidbody2D.MovePosition(rigidbody2D.position + new Vector2(tolerance, tolerance));
            tolerance *= -1;
            fixPosition = false;
        }
    }
}