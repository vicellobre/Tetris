using UnityEngine;

/// <summary>
/// Provides screen utilities
/// </summary>
public static class MatriceUtils
{
    #region Fields

    // cached for efficient boundary checking
    static int width;
    static int height;
    static ColorName[, ] board;

    #endregion

    #region Properties

    public static int Width { get { return width; } }

    public static int Height { get { return height; } }

    public static ColorName[, ] Matrice { get { return board; } }
    #endregion

    #region Methods

    public static void Initialize()
    {
        width = (int) Camera.main.orthographicSize + 2;
        height = (int) Camera.main.orthographicSize * 2;
        board = new ColorName[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                board[i, j] = ColorName.empty;
            }
        }
    }

    public static ColorName GetPosition(Vector2 position)
    {
        return board[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)];
    }

    public static void SetPosition(Vector2 position, ColorName value)
    {
        board[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] = value;
    }

    public static bool FullRow(int row)
    {
        bool isFull = true;
        for (int x = 1; x < width - 1; x++)
        {
            if (board[x, row] == ColorName.empty)
            {
                isFull = false;
                break;
            }
        }
        return isFull;
    }

    public static void EmptyRow(int row)
    {
        for (int x = 1; x < width - 1; x++)
        {
            board[x, row] = ColorName.empty;
        }
    }

    public static bool RowIsEmpty(int row)
    {
        bool isEmpty = true;
        for (int x = 1; x < width - 1; x++)
        {
            if (board[x, row] != ColorName.empty)
            {
                isEmpty = false;
                break;
            }
        }
        return isEmpty;
    }

    public static bool Compare(int x1, int y1, int x2, int y2)
    {
        return (board[x1, y1] == board[x2, y2]);
    }

    public static void DownRows(int row, int down)
    {
        for (int x = 1; x < width - 1; x++)
        {
            board[x, row] = board[x, row + down];
            board[x, row + down] = ColorName.empty;
        }
    }

    #endregion
}