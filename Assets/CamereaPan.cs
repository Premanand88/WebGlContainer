using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamereaPan : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveCamera(string Action)
    {
        Vector3 pos = transform.position;
        float moveBy = .25f;
        switch (Action)
        {
            case "UP":
                pos.x = pos.x + moveBy;
                break;
            case "DOWN":
                pos.x = pos.x - moveBy;
                break;
            case "RIGHT":
                pos.z = pos.z - moveBy;
                break;
            case "LEFT":
                pos.z = pos.z + moveBy;
                break;
        }
        transform.position = pos;
    }
}
