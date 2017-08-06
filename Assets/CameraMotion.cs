using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour {

    Vector3 oldPosition;
    HexComponent[] hexes;

    // Use this for initialization
    void Start () {
        oldPosition = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        //Move Camera
        //ZQSD
        //Zoom

        CheckIfCameraMoved();
	}

    public void PanToHex (Hex hex)
    {
        //Move cam to hex
    }
    void CheckIfCameraMoved()
    {
        if (oldPosition != this.transform.position)
        {
            oldPosition = this.transform.position;

            // TODO: probably HexMap will have a dictionary of all these later 
            if(hexes == null)
                hexes = GameObject.FindObjectsOfType<HexComponent>();

            // TODO: Cull non updated hexes
            foreach (HexComponent hex in hexes)
            {
                hex.UpdatePosition();
            }
        }
    }
}
