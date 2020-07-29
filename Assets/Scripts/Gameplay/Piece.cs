using System.Collections;
using UnityEngine;

public class Piece : StringEventInvoker
{
    #region Fields

    [SerializeField]
    protected bool fixPosition, moveToRight, moveToLeft, rotate;
    protected float tolerance;
    protected float wait;
    protected float movePerUnits;
    protected float angle;
    protected int children;
    [SerializeField]
    protected Rigidbody2D rigidbody2D;
    protected Vector2 direction;
    #endregion


    #region Properties

    public bool FixPosition => fixPosition;
    public bool ItCanMoveToRight => moveToRight;
    public bool ItCanMoveToLeft => moveToLeft;
    public bool ItCanRotate => rotate;
    public float Tolerance => tolerance;
    public int Children => children;
    public Rigidbody2D Rigidbody => rigidbody2D;
    public Vector2 Direction => direction;
    #endregion


    #region Private Methods

    protected virtual void Start()
    {   // Inicializando variables
        children = transform.childCount;
        tolerance = ConfigurationUtils.Tolerance;
        wait = ConfigurationUtils.Wait;
        movePerUnits = ConfigurationUtils.MovePerUnits;
        angle = ConfigurationUtils.Angle;
        fixPosition = false;
        moveToRight = moveToLeft = rotate = true;

        // Iniciando Corrutina
        StartCoroutine(Move());

        InitializeEvents();
    }

    /// <summary>
    /// Inicializa todos los metodos oyentes y eventos invocadores
    /// </summary>
    private void InitializeEvents()
    {
        unityEvents.Add(EventName.CreatePieceEvent, new CreatePieceEvent());
        EventManager.AddInvoker(EventName.CreatePieceEvent, this);

        unityEvents.Add(EventName.CheckRowsEvent, new CheckRowsEvent());
        EventManager.AddInvoker(EventName.CheckRowsEvent, this);

        unityEvents.Add(EventName.AddToMatriceEvent, new AddToMatriceEvent());
        EventManager.AddInvoker(EventName.AddToMatriceEvent, this);

        EventManager.AddListener(EventName.StopLateralEvent, HandleStopLateralEvent);
        EventManager.AddListener(EventName.StopRotateEvent, HandleStopRotateEvent);
    }

    protected void Update()
    {
        //Rota la pieza al presionar flecha arriba si rotate es true
        if (Input.GetKeyDown(KeyCode.UpArrow) && rotate)
        {
            Rotate();
        }

        // MOVER HACIA UNA DIRECCION
        direction = rigidbody2D.position;
        // Mueve la pieza a la izquierda al presionar flecha izquierda si moveToLeft es ture
        if (Input.GetKeyDown(KeyCode.LeftArrow) && moveToLeft)
        {
            direction.x -= movePerUnits;
        } // Mueve la pieza a la derecha al presionar flecha derecha si moveToRight es ture
        else if (Input.GetKeyDown(KeyCode.RightArrow) && moveToRight)
        {
            direction.x += movePerUnits;
        } // Mueve la pieza hacia abajo al presionar flecha abajo
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction.y -= movePerUnits;
        }

        // Comprueba si no tiene hijos
        if (transform.childCount == 0)
        {
            unityEvents[EventName.AddToMatriceEvent].Invoke("");
            Delete();
        }

        rigidbody2D.MovePosition(direction);
    }

    /// <summary>
    /// Rota la pieza
    /// </summary>
    protected virtual void Rotate()
    {
        transform.Rotate(Vector3.forward, angle);
        RotateChildren();
    }

    /// <summary>
    /// Rota la posicion de los hijos con respecto a la rotacion de el
    /// </summary>
    protected void RotateChildren()
    {
        Quaternion rotation = transform.localRotation;
        rotation.z *= -1;
        foreach (Transform child in transform)
        {
            child.localRotation = rotation;
        }
    }

    /// <summary>
    /// Mueve la pieza hacia abajo cada cierto tiempo
    /// dependiendo de la dificultad elegida
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Move()
    {
        WaitForSeconds wait = new WaitForSeconds(this.wait);
        while (true)
        {
            if (true)
            {
                rigidbody2D.MovePosition(rigidbody2D.position - new Vector2(0, movePerUnits));
                yield return wait;
            }
        }
    }

    /// <summary>
    /// Destruye la pieza, crea una nueva pieza
    /// y chequea si hay filas completadas
    /// </summary>
    protected void Delete()
    {
        Destroy(gameObject);
        if (!ConfigurationUtils.GameOver)
        {
            unityEvents[EventName.CreatePieceEvent].Invoke("");
        }
        unityEvents[EventName.CheckRowsEvent].Invoke("");
    }

    /// <summary>
    /// Bloquea/desbloquea el movimiento hacia los lados
    /// </summary>
    /// <param name="side">Lado bloqueado</param>
    protected void HandleStopLateralEvent(string side)
    {   // Bloquea el lado izquierdo
        if (side == "L")
        {
            moveToLeft = false;
        } // Bloquea el lado derecho
        else if (side == "R")
        {
            moveToRight = false;
        }
        else // Levanta los bloqueos
        {
            moveToLeft = true;
            moveToRight = true;
        }
    }

    /// <summary>
    /// Bloquea/desbloquea la rotacion
    /// </summary>
    /// <param name="rotate">valor booleano en string</param>
    protected void HandleStopRotateEvent(string rotate)
    {
        if (rotate == bool.TrueString)
        {
            this.rotate = false;
        }
        else
        {
            this.rotate = true;
        }
    }
    #endregion
}