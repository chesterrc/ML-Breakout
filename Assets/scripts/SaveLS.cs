using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLS
{
    public static int SavedScore {get; set; }
    public static int SavedLives {get; set; }

    public static void InitGame()
    {
        SavedScore = 0;
        SavedLives = 5;
    }
}

