using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearShow : MonoBehaviour
{
    public void ShowStageClearUI()
    {
        GameObject gameOverUI = GameObject.Find("StageClearUI"); // Replace with your GameObject's name
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            Cursor.visible = true;
        }
    }
}
