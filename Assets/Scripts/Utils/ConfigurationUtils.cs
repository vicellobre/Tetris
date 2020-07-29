/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    static ConfigurationData configurationData;
    static bool music = true;
    static bool gameOver = false;
    static float wait = 1f;
    static int maxColors = 4;
    static Difficulty difficulty = Difficulty.Easy;

    #region Properties

    public static float Angle { get { return configurationData.Angle; } }
    public static float EasyWait { get { return configurationData.EasyWait; } }
    public static float MediumWait { get { return configurationData.MediumWait; } }
    public static float HardWait { get { return configurationData.HardWait; } }
    public static float LineRendererWidth { get { return configurationData.LineRendererWidth; } }
    public static float MovePerUnits { get { return configurationData.MovePerUnits; } }
    public static float TimeExplosion { get { return configurationData.TimeExplosion; } }
    public static float TimeDownBlocks { get { return configurationData.TimeDownBlocks; } }
    public static float Tolerance { get { return configurationData.Tolerance; } }
    public static float YPositionPlaying { get { return configurationData.YPositionPlaying; } }
    public static float Wait { get { return wait; } }
    public static int BonusPerColors { get { return configurationData.BonusPerColors; } }
    public static int BonusPerRow { get { return configurationData.BonusPerRow; } }
    public static int LineRendererCount { get { return configurationData.LineRendererCount; } }
    public static int MaxColors { get { return maxColors; } }
    public static int NumPieces { get { return configurationData.NumPieces; } }
    public static int Points { get { return configurationData.Points; } }
    public static int EasyMaxColors { get { return configurationData.EasyMaxColors; } }
    public static int MediumMaxColors { get { return configurationData.MediumMaxColors; } }
    public static int HardMaxColors { get { return configurationData.HardMaxColors; } }
    public static bool Music { get { return music; } set { music = value; } }
    public static bool GameOver { get { return gameOver; } set { gameOver = value; } }
    public static Difficulty Difficulty
    {
        get { return difficulty; }
        set
        {
            difficulty = value;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    wait = EasyWait;
                    maxColors = EasyMaxColors;
                    break;
                case Difficulty.Medium:
                    wait = MediumWait;
                    maxColors = MediumMaxColors;
                    break;
                case Difficulty.Hard:
                    wait = HardWait;
                    maxColors = HardMaxColors;
                    break;
                default:
                    wait = EasyWait;
                    maxColors = EasyMaxColors;
                    break;
            }
        }
    }

    #endregion Properties

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        configurationData = new ConfigurationData();
    }

}