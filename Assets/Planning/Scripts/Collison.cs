using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collison : MonoBehaviour {

     WallCreator OrthoCamera;
     ObjectsLoaded UndoList;

    //private static bool isCursor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (gameObject.tag == "FloorPlaced" && col.gameObject.tag == "CursorFloor")
        {
            UndoList = GameObject.FindGameObjectWithTag("Manager").GetComponent<ObjectsLoaded>();
            if (UndoList != null)
            {
                UndoList.RemoveUndoItem(gameObject);
            }
            Destroy(col.gameObject);
        }
        if (gameObject.tag == "FurnPlaced" && col.gameObject.tag != "FloorPlaced")
        {
            UndoList = GameObject.FindGameObjectWithTag("Manager").GetComponent<ObjectsLoaded>();
            if (UndoList != null)
            {
                UndoList.RemoveUndoItem(gameObject);
            }
            Destroy(gameObject);
        }

        if (gameObject.tag == "SideBeam" && col.gameObject.tag == "SideBeam")
        {
            col.gameObject.GetComponent<Renderer>().enabled =false;
            gameObject.GetComponent<Renderer>().enabled = false;
        }

        if (gameObject.tag == "WallPlaced" && col.gameObject.tag == "WallPlaced" )
        {
            float dist = Vector3.Distance(gameObject.transform.position, col.gameObject.transform.position);
            if (dist < 0.28)
            {
                OrthoCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WallCreator>();
                UndoList = GameObject.FindGameObjectWithTag("Manager").GetComponent<ObjectsLoaded>();
                if (OrthoCamera != null)
                {
                    OrthoCamera.RemoveWallItem(gameObject);                    
                }
                if(UndoList != null)
                {
                    UndoList.RemoveUndoItem(gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
    //public void OnTriggerExit(Collider other)
    //{
    //    if (gameObject.tag.Contains("SideBeamL")&& other.pa)
    //    {
    //        gameObject.GetComponent<Renderer>().enabled = true;
    //    }
    //}
    //public void SetAsCursor()
    //{
    //    isCursor = true;
    //}

    //public bool isObjCursor()
    //{
    //    return isCursor;
    //}

}
