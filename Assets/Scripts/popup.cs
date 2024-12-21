using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    // Reference to the GameObject you want to hide/show
    public GameObject objectToToggle;

    // Method to make the object appear
    public void ShowObject()
    {
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(true);
            Debug.Log("Object is now visible: " + objectToToggle.name);
        }
        else
        {
            Debug.LogWarning("Object reference is missing!");
        }
    }

    // Method to make the object disappear
    public void HideObject()
    {
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(false);
            Debug.Log("Object is now hidden: " + objectToToggle.name);
        }
        else
        {
            Debug.LogWarning("Object reference is missing!");
        }
    }
}