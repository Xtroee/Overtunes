using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverShow : MonoBehaviour
{
    public void ShowGameOverUI()
    {
        GameObject gameOverUI = GameObject.Find("GameOverUI"); // Replace with your GameObject's name
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }
}
