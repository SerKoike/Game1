using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour {

    public GameObject HexPrefab;

    public Material[] HexMaterials;

    public int numRows = 20;
    public int numColumns = 40;

	// Use this for initialization
	void Start () {
        GenerateMap();
	}


	virtual public void GenerateMap()
    {
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                //Instantiate a Hex
                Hex h = new Hex(column, row);
                Vector3 pos = h.PositionFromCamera(
                    Camera.main.transform.position,
                    numRows,
                    numColumns
                );

                GameObject hexGO = (GameObject)Instantiate
                (
                    HexPrefab,
                    pos,
                    Quaternion.identity,
                    this.transform
                );

                hexGO.GetComponent<HexComponent>().Hex = h;
                hexGO.GetComponent<HexComponent>().HexMap = this;

                hexGO.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1}", column, row);

                MeshRenderer mr = hexGO.GetComponentInChildren<MeshRenderer>();
                mr.material = HexMaterials[ Random.Range(0, HexMaterials.Length) ];
                
            }
        }
    }
}
