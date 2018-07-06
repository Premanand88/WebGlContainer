using Assets.ReferenceClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallLine : MonoBehaviour {
    bool creating;
    public GameObject tile1;
    public GameObject tile2;
    public GameObject tile3;
    public GameObject cursor;
    GameObject tileAdded;
    public static int TilePicked;
    bool overlapTile;

    public static List<WallAdded> wallsList;

    public GameObject WorldCenter;
    private ObjectsLoaded objLoad;
    private static Item cartItem;
    private ShopScrollList scrollList;
    private static Vector3 prevRot, prevPos;
    bool isDrag = false;
    bool holdZ, holdX, newStart;
    private float posX, posZ;

    //Spawning
    private GameObject[] spawnPrefab;
    private Transform hs;
    private int spawnCount = 0, spawnAmount = 40;
    private float distance = .29f;
    private bool isGood = false;

    // Use this for initialization
    void Start () {
        objLoad = WorldCenter.GetComponent<ObjectsLoaded>();
        scrollList = GameObject.FindGameObjectWithTag("MenuList").GetComponent<ShopScrollList>();
        wallsList = new List<WallAdded>();
    }
	
	// Update is called once per frame
	void Update () {
        if (creating && cursor == null)
        {
            if (TilePicked == 0)
            {
                cursor = (GameObject)Instantiate(tile1, new Vector3(), Quaternion.identity);
                holdX = true;
                holdZ = false;
                newStart = true;
                cursor.SetActive(true);
                cursor.GetComponent<BoxCollider>().enabled = false;
                Collison[] col = cursor.GetComponentsInChildren<Collison>();
                for (int i = 0; i < col.Length; i++)
                {
                    if (col[i].tag.Contains("CursorMode"))
                        col[i].GetComponent<Collison>().enabled = false;
                }
            }
            else if (TilePicked == 1)
            {
                GameObject.FindGameObjectWithTag("Manager").GetComponentInChildren<ObjectsLoaded>().CamAdjust = true;
                creating = false;
                //cursor = (GameObject)Instantiate(tile2, new Vector3(), Quaternion.identity); 
                Vector3 pos = Vector3.zero;
                pos.z = 0.5f;
                pos.x = 0.5f;
                foreach (GameObject chk in GameObject.FindGameObjectsWithTag("WallPlaced"))
                {
                    if (chk.transform.position == pos)
                    {
                        //chk.transform.Rotate(0,90,0);
                        //prevRot.y = chk.transform.rotation.y;
                        return;
                    }
                }
                tileAdded = (GameObject)Instantiate(tile2, pos, tile2.transform.rotation);
                tileAdded.gameObject.tag = "WallPlaced";
                objLoad.PushItem(tileAdded);
                //prevRot.y = tileAdded.transform.rotation.y;
                if (scrollList != null)
                    scrollList.TryTransferItemToOtherShop(cartItem);

                tileAdded.transform.parent = GameObject.FindGameObjectWithTag("Manager").transform;
                WorldCenter.GetComponentInChildren<ObjectsLoaded>().CamAdjust = true;
            }
            else if (TilePicked == 2)
            {
                GameObject.FindGameObjectWithTag("Manager").GetComponentInChildren<ObjectsLoaded>().CamAdjust = true;
                creating = false;
                Vector3 pos = Vector3.zero;
                pos.z = 0.5f;
                pos.x = 0.5f;
                //cursor = (GameObject)Instantiate(tile3, new Vector3(), Quaternion.identity); 
                foreach (GameObject chk in GameObject.FindGameObjectsWithTag("WallPlaced"))
                {
                    if (chk.transform.position == pos)
                    {
                        //chk.transform.Rotate(0,90,0);
                        //prevRot.y = chk.transform.rotation.y;
                        return;
                    }
                }
                tileAdded = (GameObject)Instantiate(tile3, pos, tile3.transform.rotation);
                tileAdded.gameObject.tag = "WallPlaced";
                objLoad.PushItem(tileAdded);
                //prevRot.y = tileAdded.transform.rotation.y;
                if (scrollList != null)
                    scrollList.TryTransferItemToOtherShop(cartItem);

                tileAdded.transform.parent = GameObject.FindGameObjectWithTag("Manager").transform;
                WorldCenter.GetComponentInChildren<ObjectsLoaded>().CamAdjust = true;
            }
            if (cursor != null)
                cursor.tag = "Cursor";
            //cursor.transform.rotation = Quaternion.LookRotation(prevRot, Vector3.up);

        }

        if (cursor != null && EventSystem.current.IsPointerOverGameObject())
        {
            cursor.SetActive(false);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            newStart = true;
            return;
        }
        if (cursor != null && !EventSystem.current.IsPointerOverGameObject())
        {
            cursor.SetActive(true);
            cursor.transform.position = snapPosition(GetWorldPoint());
            GetInput();

        }
    }

    private void GetInput()
    {
        throw new NotImplementedException();
    }

    public Vector3 snapPosition(Vector3 orginal)
    {
        Vector3 snapped;
        //if (isDrag)
        snapped.x = orginal.x;//+ 0.3048f;// Mathf.Floor(orginal.x+0.5f);
        snapped.y = orginal.y; //+ 0.3048f;//Mathf.Floor(orginal.y + 0.5f);
        snapped.z = orginal.z;//+ 0.3048f;//Mathf.Floor(orginal.z + 0.5f);
        List<WallAdded> removedComp = new List<WallAdded>();
        if (isDrag)
        {
            if (holdX)
            {
                orginal.z = posZ;
                orginal.x = snapped.x;// = Mathf.Floor(orginal.x + 0.5f);
            }
            if (holdZ)
            {
                orginal.x = posX;
                orginal.z = snapped.z;
            }
        }
        return orginal;// snapped;
    }

    Vector3 GetWorldPoint()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            overlapTile = false;
            if (hit.collider.tag.Contains("Floor"))
            {
                overlapTile = true;
                return hit.point;
            }
            else
                overlapTile = false;
            return cursor.transform.position;

        }
        overlapTile = true;
        return cursor.transform.position;
    }

    void SpawnItem(Vector3 startPos,Vector3 endPos)
    {
        while (spawnCount < spawnAmount)
        {
            //ex = Random.Range(-200, 201);
            //zee = Random.Range(-200, 201);
            //newPos = Vector3(ex, 400, zee);
       
       for(var i in houses)
            {
                //If new position is too close, don't bother with this random position
                if (Vector3.Distance(i.position, newPosition) < distance)
                    isGood = false;
                break
         else
           isGood = true;
            }
            if (isGood)
            {
                spawnCount++;
                newhs = Instantiate(hs, newPos, transform.rotation);
                houses.Push(newhs);
            }
        }
    }
}
