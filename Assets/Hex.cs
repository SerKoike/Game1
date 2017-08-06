using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Define grid position, world space position, size
//Neighbours, etc of a hex tile.
//It does not interact with Unity directly


public class Hex
{
    //Q + R + S = 0

    //Readonly : because position should not change after creation
    public readonly int Q;  //Column
    public readonly int R;  //row
    public readonly int S;

    float radius = 1f;
    bool allowWrapEastWest = true;
    bool allowWrapNorthSouth = false;

    static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;

    public Hex(int q, int r)
    {
        this.Q = q;
        this.R = r;
        this.S = -(q + r);
    }

    public Vector3 Position()
    {
        //Parameter
        float height = radius * 2;
        float widht = WIDTH_MULTIPLIER * height;

        //offset
        float vert = height * 0.75f;
        float horiz = widht;

        return new Vector3(horiz * (this.Q + this.R / 2f), 0, vert * this.R);
    }

    public float HexHeight()
    {
        return radius * 2f;
    }
    public float HexWidht()
    {
        return WIDTH_MULTIPLIER * HexHeight();
    }
    public float HexVerticalSpacing()
    {
        return HexHeight() * 0.75f;
    }
    public float HexHorizontalSpacing()
    {
        return HexWidht();
    }

    public Vector3 PositionFromCamera(Vector3 cameraPosition, float numRows, float numColumns)
    {
        float mapHeight = numRows * HexVerticalSpacing();
        float mapWidth = numColumns * HexHorizontalSpacing();

        Vector3 position = Position();

        if (allowWrapEastWest)
        {
            //Should be between -0.5 to 0.5
            float howManyWidthsFromCamera = (position.x - cameraPosition.x) / mapWidth;
            float howManyHeightsFromCamera = (position.z - cameraPosition.z) / mapWidth;

            if (howManyWidthsFromCamera > 0)
                howManyWidthsFromCamera += 0.5f;
            else
                howManyWidthsFromCamera -= 0.5f;

            int howManyWidthToFix = (int)howManyWidthsFromCamera;

            position.x -= howManyWidthToFix * mapWidth;
        }

        if (allowWrapNorthSouth)
        {
            //Should be between -0.5 to 0.5
            float howManyHeightsFromCamera = (position.z - cameraPosition.z) / mapHeight;

            if (howManyHeightsFromCamera > 0)
                howManyHeightsFromCamera += 0.5f;
            else
                howManyHeightsFromCamera -= 0.5f;

            int howManyHeightsToFix = (int)howManyHeightsFromCamera;

            position.z -= howManyHeightsToFix * mapHeight;
        }

        return position;
    }
}
