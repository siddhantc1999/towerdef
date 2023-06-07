using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Coordinatelabeller : MonoBehaviour
{
    public TextMeshPro GridText;
    Vector2Int coordinates = new Vector2Int();
    float gridUnitSize;
    GridManager gridManager;
    bool isPlacable;
    // Start is called before the first frame update
    void Start()
    {
        GridText = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();
        DisplayCoordinates();
        GridText.enabled = false;
        gridUnitSize = UnityEditor.EditorSnapSettings.move.x;
    }

    
    private void DisplayCoordinates()
    {
        //coordinates.x = Mathf.RoundToInt(transform.position.x / gridUnitSize);
        //coordinates.y = Mathf.RoundToInt(transform.position.z / gridUnitSize);
        //GridText.text = coordinates.x + "," + coordinates.y;
        coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        GridText.text = coordinates.x + "," + coordinates.y;
        UpdateTileName();

    }
    private void UpdateTileName()
    {
        transform.parent.name = GridText.text;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleLabels();
    }

    private void ToggleLabels()
    {
       if(Input.GetKeyDown(KeyCode.C))
        {
            GridText.enabled = !GridText.enabled;
        }
        if (gridManager.getGrid[coordinates].isPath )
        {
            GridText.color = Color.red;
        }
        else
        {
            GridText.color = Color.black;
        }

    }
}
