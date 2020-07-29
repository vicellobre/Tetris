using UnityEngine;

public class PieceNext : StringEventInvoker
{

    float tolerance;

    void Start()
    {
        EventManager.AddListener(EventName.CreatePieceEvent, HandlePlayPiece);
        tolerance = ConfigurationUtils.Tolerance;
    }

    void HandlePlayPiece(string nothing)
    {
        transform.position = new Vector2(0, ConfigurationUtils.YPositionPlaying);
        if (TryGetComponent(out PieceComplexA pieceA))
        {
            pieceA.enabled = true;
            transform.position += new Vector3(-tolerance, 0, 0);
        }
        else if (TryGetComponent(out PieceComplexB pieceB))
        {
            pieceB.enabled = true;
            transform.position += new Vector3(-tolerance, tolerance, 0);
        }
        else
            GetComponent<Piece>().enabled = true;

        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Block>().enabled = true;
        }
        this.enabled = false;
    }
}