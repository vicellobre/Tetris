using System;
using System.IO;
using UnityEngine;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    const string ConfigurationDataFileName = "ConfigurationData.csv";

    // configuration data
    static float angle = 90f;
    static float easyWait = 1f;
    static float mediumWait = 0.75f;
    static float hardWait = 0.5f;
    static float lineRendererWidth = 0.05f;
    static float movePerUnits = 1f;
    static float timeDownBlocks = 0.3f;
    static float timeExplosion = 1f;
    static float tolerance = 0.5f;
    static float yPositionPlaying = 8f;
    static int bonusPerColors = 30;
    static int bonusPerRow = 20;
    static int lineRendererCount = 2;
    static int numPieces = 7;
    static int points = 100;
    static int easyMaxColors = 4;
    static int mediumMaxColors = 6;
    static int hardMaxColors = 9;

    #endregion

    #region Properties

    public float Angle { get { return angle; } }
    public float EasyWait { get { return easyWait; } }
    public float MediumWait { get { return mediumWait; } }
    public float HardWait { get { return hardWait; } }
    public float LineRendererWidth { get { return lineRendererWidth; } }
    public float MovePerUnits { get { return movePerUnits; } }
    public float TimeDownBlocks { get { return timeDownBlocks; } }
    public float TimeExplosion { get { return timeExplosion; } }
    public float Tolerance { get { return tolerance; } }
    public float YPositionPlaying { get { return yPositionPlaying; } }
    public int BonusPerColors { get { return bonusPerColors; } }
    public int BonusPerRow { get { return bonusPerRow; } }
    public int LineRendererCount { get { return lineRendererCount; } }
    public int NumPieces { get { return numPieces; } }
    public int Points { get { return points; } }
    public int EasyMaxColors { get { return easyMaxColors; } }
    public int MediumMaxColors { get { return mediumMaxColors; } }
    public int HardMaxColors { get { return hardMaxColors; } }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        StreamReader file = null;
        try
        {
            file = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));
            string names = file.ReadLine();
            string values = file.ReadLine();

            SetConfigurationDataFields(values);
        }
        catch (Exception) {}
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    /// <summary>
    /// Sets the configuration data fields from the provided
    /// csv string
    /// </summary>
    /// <param name="csvValues">csv string of values</param>
    static void SetConfigurationDataFields(string csvValues)
    {
        string[] values = csvValues.Split(';');
        easyWait = float.Parse(values[0]);
        mediumWait = float.Parse(values[1]);
        hardWait = float.Parse(values[2]);
        timeExplosion = float.Parse(values[3]);
        tolerance = float.Parse(values[4]);
        angle = float.Parse(values[5]);
        yPositionPlaying = float.Parse(values[6]);
        lineRendererWidth = float.Parse(values[7]);
        lineRendererCount = int.Parse(values[8]);
        bonusPerRow = int.Parse(values[9]);
        bonusPerColors = int.Parse(values[10]);
        points = int.Parse(values[11]);
        numPieces = int.Parse(values[12]);
        easyMaxColors = int.Parse(values[13]);
        mediumMaxColors = int.Parse(values[14]);
        hardMaxColors = int.Parse(values[15]);
        movePerUnits = float.Parse(values[16]);
        timeDownBlocks = float.Parse(values[17]);

    }
    #endregion
}