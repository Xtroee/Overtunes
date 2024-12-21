using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showCursor : MonoBehaviour
{
    public GameObject crossHair;
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveCrosshair()
    {
        if (crossHair != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = crossHair.transform.position.z;
            crossHair.transform.position = worldPosition;
        }
        else
        {
            Debug.LogError("CrossHair GameObject is not assigned.");
        }
    }
}
