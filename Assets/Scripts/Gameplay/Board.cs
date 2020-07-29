using UnityEngine;

public class Board : StringEventInvoker
{
    #region Fields

    private float           positionX;
    private float           tolerance;
    [SerializeField]
    private GameObject      square;
    [SerializeField]
    private GameObject[]    pieces = new GameObject[(int) PieceName.PieceZ];
    private int             width, height;
    [SerializeField]
    private RectTransform   hud, container;
    [SerializeField]
    private Vector3         table = new Vector3();
    #endregion


    #region Properties

    public float PositionX => positionX;
    public float Tolerance => tolerance;
    public GameObject Square => square;
    public GameObject[] Pieces => pieces;
    public int Width => width;
    public int Height => height;
    public RectTransform Container => container;
    public RectTransform Hud => hud;
    public Vector3 Table => table;
    #endregion

    #region Public Methods

    public void GetPieces()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i] = Resources.Load<GameObject>("Prefabs/" + ((PieceName) i).ToString());
        }
    }
    #endregion


    #region Private Methods
    private void Awake()
    {
        MatriceUtils.Initialize();

        width = MatriceUtils.Width;
        height = MatriceUtils.Height;
        tolerance = ConfigurationUtils.Tolerance;

        CreateWalls();
        Grid();
    }

    private void Start()
    {
        InitializeEvents();

        float hudPositionX = hud.rect.position.x;
        float containerHalfWidth = container.rect.width / 2;
        float screenLeft = ScreenUtils.ScreenLeft * 2;
        positionX = containerHalfWidth * 100 / hudPositionX;
        positionX *= screenLeft / 100;
        positionX += screenLeft;

        InstantiateFirstPiece("");
        CreatePiece("");
    }

    private void InitializeEvents()
    {
        unityEvents.Add(EventName.DestroyBlockEvent, new DestroyBlockEvent());
        EventManager.AddInvoker(EventName.DestroyBlockEvent, this);

        unityEvents.Add(EventName.DownBlockEvent, new DownBlockEvent());
        EventManager.AddInvoker(EventName.DownBlockEvent, this);

        unityEvents.Add(EventName.PointsEvent, new PointsEvent());
        EventManager.AddInvoker(EventName.PointsEvent, this);

        EventManager.AddListener(EventName.CreatePieceEvent, CreatePiece);
        EventManager.AddListener(EventName.CheckRowsEvent, HandleCheckRowsEvent);
    }

    private void CreatePiece(string str)
    {
        int random = Random.Range(0, ConfigurationUtils.NumPieces);
        Instantiate(pieces[random], new Vector2(positionX, 0), Quaternion.identity);
    }

    private void InstantiateFirstPiece(string str)
    {
        int random = Random.Range(0, ConfigurationUtils.NumPieces);
        GameObject firstPiece = Instantiate(pieces[random], new Vector2(0, ConfigurationUtils.YPositionPlaying), Quaternion.identity);

        if (firstPiece.TryGetComponent<PieceComplexA>(out PieceComplexA pieceA))
        {
            pieceA.enabled = true;
            firstPiece.transform.position += new Vector3(-tolerance, 0, 0);
        }
        else if (firstPiece.TryGetComponent<PieceComplexB>(out PieceComplexB pieceB))
        {
            pieceB.enabled = true;
            firstPiece.transform.position += new Vector3(-tolerance, tolerance, 0);
        }
        else
        {
            firstPiece.GetComponent<Piece>().enabled = true;
        }
        foreach (Transform child in firstPiece.transform)
        {
            child.gameObject.GetComponent<Block>().enabled = true;
        }
        this.enabled = false;
    }

    private void CreateWalls()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0)
                {
                    Vector3 position = new Vector3(x, y);
                    Instantiate(square, transform.position + position,
                        Quaternion.identity, transform);
                    MatriceUtils.SetPosition(position, ColorName.wall);
                }
            }
        }
    }

    private void Grid()
    {
        GameObject lines = new GameObject("Lines");
        
        Vector3 table = this.table;
        table += new Vector3(tolerance, tolerance, 0);
        for (int i = 1; i < width - 2; i++)
        {
            // add line renderer and draw line
            GameObject lineObj = new GameObject("LineObj");
            LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
            CreateLine(lineObj, lineRenderer);

            //Set the postion of both two lines
            lineRenderer.SetPosition(0, table + new Vector3(i, 0, 0));
            lineRenderer.SetPosition(1, table + new Vector3(i, height - 1, 0));
            lineObj.transform.SetParent(lines.transform);
        }

        for (int j = 1; j < height - 1; j++)
        {
            // add line renderer and draw line
            GameObject lineObj = new GameObject("LineObj");
            LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
            CreateLine(lineObj, lineRenderer);

            //Set the postion of both two lines
            lineRenderer.SetPosition(0, table + new Vector3(0, j, 0));
            lineRenderer.SetPosition(1, table + new Vector3(width - 2, j, 0));
            lineObj.transform.SetParent(lines.transform);
        }
    }

    private void CreateLine(GameObject lineObj, LineRenderer lineRenderer)
    {
        lineRenderer.material = new Material(Shader.Find("Hidden/Internal-Colored"));

        //Set color
        lineRenderer.startColor = Color.grey;
        lineRenderer.endColor = Color.grey;

        //Set width
        lineRenderer.startWidth = ConfigurationUtils.LineRendererWidth;
        lineRenderer.endWidth = ConfigurationUtils.LineRendererWidth;

        //Set line count which is 2
        lineRenderer.positionCount = ConfigurationUtils.LineRendererCount;
    }

    private void HandleCheckRowsEvent(string nothing)
    {
        int points = 0,
            multiplicador = -1;
        for (int row = 1; row < width - 1; row++)
        {
            if (MatriceUtils.FullRow(row))
            {
                multiplicador++;
                points += Points(row);
                MatriceUtils.EmptyRow(row);
                unityEvents[EventName.DestroyBlockEvent].Invoke(row.ToString());
            }
            else if (MatriceUtils.RowIsEmpty(row))
            {
                break;
            }
        }

        if (multiplicador >= 0)
        {
            AudioManager.Play(AudioClipName.Points);
            points += multiplicador * ConfigurationUtils.BonusPerRow;
            unityEvents[EventName.PointsEvent].Invoke(points.ToString());
            Invoke("ReajustarMatriz", ConfigurationUtils.TimeDownBlocks);
        }
    }

    private int Points(int row)
    {
        int points = ConfigurationUtils.Points;
        for (int i = 1; i < width - 3; i++)
        {
            int seguidas = 1;
            for (int j = i + 1; j < width - 1; j++)
            {
                if (MatriceUtils.Compare(i, row, j, row))
                {
                    seguidas++;
                }
                else
                {
                    i = j - 1;
                    break;
                }
            }
            if (seguidas >= 3)
            {
                points += seguidas * ConfigurationUtils.BonusPerColors;
            }
        }
        return points;
    }

    private void ReajustarMatriz()
    {
        for (int row = 1; row < height - 2; row++)
        {
            if (MatriceUtils.RowIsEmpty(row))
            {
                int bajar = 1,
                    rowAux = row + 1;
                while (MatriceUtils.RowIsEmpty(rowAux) && rowAux < height - 1)
                {
                    bajar++;
                    rowAux++;
                }
                string datos = rowAux.ToString() + bajar.ToString();
                unityEvents[EventName.DownBlockEvent].Invoke(datos);
                MatriceUtils.DownRows(row, bajar);
            }
        }
    }
    #endregion
}