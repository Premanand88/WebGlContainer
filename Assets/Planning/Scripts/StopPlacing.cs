using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlacing : MonoBehaviour {
    private ObjCreator objC;
    private FloorCreator floorC;
    private WallCreator wallC;
    private DoorCreator doorC;
    private WindowCreator windowC;
    private string currentOption;

    // Use this for initialization
    void Start () {
        objC = gameObject.GetComponent<ObjCreator>();
        floorC = gameObject.GetComponent<FloorCreator>();
        wallC = gameObject.GetComponent<WallCreator>();
        doorC = gameObject.GetComponent<DoorCreator>();
        windowC = gameObject.GetComponent<WindowCreator>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void OnEnd(string option)
    {
        Destroy(objC.cursor);
        Destroy(floorC.cursor);
        Destroy(wallC.cursor);
        Destroy(doorC.cursor);
        Destroy(windowC.cursor);
        objC.cursor = null;
        objC.enabled = false;
        floorC.cursor = null;
        floorC.enabled = false;
        wallC.cursor = null;
        wallC.enabled = false;
        doorC.cursor = null;
        doorC.enabled = false;
        windowC.cursor = null;
        windowC.enabled = false;
        if (option.Contains("Furniture") && currentOption !=option)
        {
            currentOption = "Furniture";
            objC.enabled = true;
            objC.UseTile();
        }
        else if (option.Contains("Wall") && currentOption != option)
        {
            currentOption = "Wall";
            wallC.enabled = true;
            wallC.UseTile();
        }

        else if (option.Contains("Floor") && currentOption != option)
        {
            currentOption = "Floor";
            floorC.enabled = true;
            floorC.UseTile();
        }
        else if (option.Contains("Door") && currentOption != option)
        {
            currentOption = "Door";
            doorC.enabled = true;
            doorC.UseTile();
        }
        else if (option.Contains("Window") && currentOption != option)
        {
            currentOption = "WindowCreator";
            windowC.enabled = true;
            windowC.UseTile();
        }
        else
        {
            currentOption = "";
        }
    }

    public void SetCursor(int n,Item cartItem)
    {
        if(objC != null)
        {
            objC.SetTile(n, cartItem);
        }
        if(floorC != null)
        {
            floorC.SetTile(n, cartItem);
        }
        if(wallC != null)
        {
            wallC.SetTile(n, cartItem);
        }
        if (windowC != null)
        {
            windowC.SetTile(n, cartItem);
        }
        if (doorC != null)
        {
            doorC.SetTile(n, cartItem);
        }
    }
}
