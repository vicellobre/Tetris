using UnityEngine;

public class PieceComplexB : Piece
{
    protected override void Start()
    {
        base.Start();
        //Debug.Break();
        // Re-ubicando la pieza
        //rigidbody2D.position += new Vector2(-tolerance, tolerance);
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
            RotateChildren();
        }
    }
}