using UnityEngine;
using UnityEngine.UI;

public class StartDialog : MonoBehaviour
{
    public GameObject dialogBox; // Reference to the dialog box UI
    public Button dialogButton; // Reference to the button in the dialog box

    void Start()
    {
        // Ensure the dialog box and button are assigned
        if (dialogBox == null || dialogButton == null)
        {
            Debug.LogError("StartDialog: Dialog box or button is not assigned!");
            return;
        }

        // Show the dialog box at the start of the game
        dialogBox.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;

        // Add a listener to the button to handle the click
        dialogButton.onClick.AddListener(OnDialogClick);
    }

    void OnDialogClick()
    {
        // Hide the dialog box
        dialogBox.SetActive(false);

        // Resume the game
        Time.timeScale = 1f;

        // Optionally, start gameplay mechanics here
        Debug.Log("Game started!");
    }
}