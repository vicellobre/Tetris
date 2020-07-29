using UnityEngine;

public class Block : StringEventInvoker
{
    #region  Fields

    private bool        added, init, stopLeft, stopRight, stopRotate;
    private ColorName   color;
    private GameObject  explosion, table;
    private PieceName   type;
    [SerializeField]
    private Rigidbody2D rigidbody2D;
    private Vector2     position;
    #endregion


    #region Properties

    public bool Added => added;
    public bool Init => init;
    public bool StopLeft => stopLeft;
    public bool StopRight => stopRight;
    public bool StopRotate => stopRotate;
    public ColorName MyColor => color;
    public PieceName MyType => type;
    public Rigidbody2D Rigidbody => rigidbody2D;
    #endregion


    #region Private Methods

    private void Awake()
    {   // Asignacion aleatoria del color
        int random = Random.Range(0, ConfigurationUtils.MaxColors);
        color = (ColorName) random;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + color.ToString());
        explosion = Resources.Load<GameObject>("Prefabs/explosion_" + color.ToString());
    }

    private void Start()
    {
        InitializeEvents();

        // Inicializacion de varibales
        added = init = stopLeft = stopRight = stopRotate = false;
        type = Type();
        table = GameObject.FindGameObjectWithTag("Table");        
    }

    /// <summary>
    /// Inicializa todos los metodos oyentes y eventos invocadores
    /// </summary>
    private void InitializeEvents()
    {
        unityEvents.Add(EventName.GameOverEvent, new GameOverEvent());
        EventManager.AddInvoker(EventName.GameOverEvent, this);

        unityEvents.Add(EventName.StopLateralEvent, new StopLateralEvent());
        EventManager.AddInvoker(EventName.StopLateralEvent, this);

        unityEvents.Add(EventName.StopRotateEvent, new StopRotateEvent());
        EventManager.AddInvoker(EventName.StopRotateEvent, this);

        EventManager.AddListener(EventName.DestroyBlockEvent, HandleDestroyBlockEvent);
        EventManager.AddListener(EventName.DownBlockEvent, HandleDownBlocksEvent);
        EventManager.AddListener(EventName.AddToMatriceEvent, HandleAddToMatriceEvent);
    }

    private void Update()
    {
        if (transform.parent == null && added) return;
        
        position = rigidbody2D.position;
        // Comprueba si no ha empezado a moverse (evita llamar en cada frame a Stop)
        if (!init)
        {   // Comprueba si se encuentra superpuesto sobre un bloque de la matriz
            if (Stop(position.x, position.y))
            {   // Estado de GameOver
                unityEvents[EventName.GameOverEvent].Invoke("");
                ConfigurationUtils.GameOver = true;
            }
            init = true;
        }
        else
        {   // Comprueba si tiene bloques hermanos
            if (transform.parent != null)
            {   // Comprueba si se encuentra una posicion por encima de otro bloque de la matriz
                if (Stop(position.x, position.y - 1))
                {   // Abandona al padre junto con sus hermanos
                    transform.parent.DetachChildren();
                }
                else // RCORRIDO NORMAL
                {   // Comprueba si tiene algun bloque a su izquierda en la matriz
                    if (Stop(position.x - 1, position.y))
                    {   // Deshabilita el movimiento hacia la izquierda
                        unityEvents[EventName.StopLateralEvent].Invoke("L"); // Match with class Piece
                        stopLeft = true;
                    }
                    else if (stopLeft)
                    {   // Habilita el movimiento hacia la izquierda
                        unityEvents[EventName.StopLateralEvent].Invoke("Free"); // Match with class Piece
                        stopLeft = false;
                    }

                    // Comprueba si tiene algun bloque a su derecha en la matriz
                    if (Stop(position.x + 1, position.y))
                    {   // Deshabilita el movimiento hacia la derecha
                        unityEvents[EventName.StopLateralEvent].Invoke("R"); // Match with class Piece
                        stopRight = true;
                    }
                    else if (stopRight)
                    {   // Habilita el movimiento hacia la derecha
                        unityEvents[EventName.StopLateralEvent].Invoke("Free"); // Match with class Piece
                        stopRight = false;
                    }

                    if (CanRotate())
                    {   // Deshabilita la opcion de rotar la Piece
                        stopRotate = true;
                        unityEvents[EventName.StopRotateEvent].Invoke(stopRotate.ToString()); // Match with class Piece
                    }
                    else if (stopRotate)
                    {   // Habilita la opcion de rotar la opcion
                        stopRotate = false;
                        unityEvents[EventName.StopRotateEvent].Invoke(stopRotate.ToString()); // Match with class Piece
                    }
                }
            }
        }
    }

    /// <summary>
    /// Comprueba si la posicion dada se encuentra ocupada
    /// </summary>
    /// <para>
    /// Transforma en posiciones locales de la matriz
    /// </para>
    /// <param name="positionX">Posicion del eje X en el espacio mundo</param>
    /// <param name="positionY">Posicion del eje Y en el spacio mundo</param>
    /// <returns>True si esta ocupado y False en caso contrario</returns>
    private bool Stop(float positionX, float positionY)
    {   // Transforma los valores en valores locales de la matriz
        float y = Mathf.Abs(table.transform.position.y) + positionY;
        float x = Mathf.Abs(table.transform.position.x) + positionX;
        return MatriceUtils.GetPosition(new Vector2(x, y)) != ColorName.empty;
    }

    /// <summary>
    /// Agrega el bloque a la matriz
    /// </summary>
    /// <para>
    /// Toma en cuenta la posicion del objeto
    /// y la transforma a posiciones locales de la matriz
    /// </para>
    private void Add()
    {
        // Transforma los valores en valores locales de la matriz
        float x = Mathf.Abs(table.transform.position.x) + rigidbody2D.position.x;
        float y = Mathf.Abs(table.transform.position.y) + rigidbody2D.position.y;
        MatriceUtils.SetPosition(new Vector2(x, y), color);
    }

    /// <summary>
    /// Instancia el prefab explosion y destruye al bloque
    /// </summary>
    private void Explosion()
    {
        Instantiate(explosion, transform.localPosition, Quaternion.identity);
        Destroy(gameObject);
    }

    /// <summary>
    /// Determina el tipo de pieza (Piece) que es su padre
    /// </summary>
    /// <returns>Tipo de pieza</returns>
    private PieceName Type()
    {
        string[] name = transform.parent.name.Split('(');
        return (PieceName)System.Enum.Parse(typeof(PieceName), name[0]);
    }

    /// <summary>
    /// Determina si puede o no rotar
    /// </summary>
    /// <para>
    /// Depende del tipo de pieza que es su objecto padre
    /// </para>
    /// <returns>True si puede rotar</returns>
    private bool CanRotate()
    {
        switch (type)
        {
            case PieceName.PieceI:
                return (stopLeft && (transform.localEulerAngles.z == 0 || transform.localEulerAngles.z == 180))
                    || ((stopRight || Stop(position.x + 2, position.y))
                        && (transform.localEulerAngles.z == 0 || transform.localEulerAngles.z == 180));

            case PieceName.PieceT:
            case PieceName.PieceS:
            case PieceName.PieceZ:
                return stopLeft && (transform.localEulerAngles.z == 90 || transform.localEulerAngles.z == 270);

            case PieceName.PieceJ:
            case PieceName.PieceL:
                return (stopLeft && (transform.localEulerAngles.z == 270))
                    || (stopRight && transform.localEulerAngles.z == 90);

            default:
                return false;
        }
    }

    private void HandleAddToMatriceEvent(string nothing)
    {
        // Comprueba si tiene padre y si no ha sido agregado a la matriz
        if (transform.parent == null && !added)
        {   // Agrega el bloque a la matriz
            Add();
            added = true;
        }
    }

    /// <summary>
    /// Baja al bloque tantas posiciones se indica
    /// </summary>
    /// <para>
    /// Metodo oyente (Listener)
    /// </para><para>
    /// Llamado despues que explota una fila
    /// </para>
    /// <param name="datos">Valor compuesto por fila y posiciones a bajar</param>
    private void HandleDownBlocksEvent(string datos)
    {
        int row = int.Parse(datos), // Convierte la variable datos de string a int
            bajar = row % 10; // Obtiene la cantidad de posiciones a bajar
        row /= 10; // Obtiene la fila que contiene bloques que deben bajar
        
        // Obtiene la altura con respecto  a la mesa (table/board)
        float positionY = Mathf.Abs(table.transform.position.y) + rigidbody2D.position.y;
        
        // Transforma el valor de la altura en el valor de la fila dentro de la matriz
        // y comprueba si pertenece a la fila que debe bajar
        if (row == Mathf.RoundToInt(positionY) && added)
        {   // Baja al bloque la cantidad de posiciones que indica la variable 'bajar'
            rigidbody2D.position -= new Vector2(0, bajar);
        }
    }

    /// <summary>
    /// Explota el bloque si se encuentra en la fila dada
    /// </summary>
    /// <para>
    /// Metodo oyente (Listener)
    /// </para><para>
    /// Llamado despues que se completa una fila
    /// </para>
    /// <param name="row">Fila completada</param>
    private void HandleDestroyBlockEvent(string row)
    {
        int rowInt = int.Parse(row); // Convierte de string a int

        // Obtiene la altura con respecto  a la mesa (table/board)
        float positionY = Mathf.Abs(table.transform.position.y) + rigidbody2D.position.y;
        // Transforma el valor de la altura en el valor de la fila dentro de la matriz
        // y comprueba si pertenece a la fila que debe explotar
        if (rowInt == Mathf.RoundToInt(positionY))
        {   // BOOM!
            Explosion();
        }
    }
    #endregion
}