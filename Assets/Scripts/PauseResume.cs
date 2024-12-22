using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    public PauseMenu pauseMenu; // Reference to the PauseMenu script

    void Start()
    {
        // Get the Button component and add a listener for the click event
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnResumeButtonClick);
        }
    }

    void OnResumeButtonClick()
    {
        // Call the ResumeGame method from the PauseMenu script
        if (pauseMenu != null)
        {
            pauseMenu.ResumeGame();
        }
    }
}