using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] GameObject plane;
    Texture2D wallTexture;
    Vector2 lastPoint = Vector2.zero;
    BresenhamsLine bresenhamsLine;

    private void Awake()
    {
        bresenhamsLine = new BresenhamsLine();
        wallTexture = Instantiate(plane.GetComponent<Renderer>().material.mainTexture) as Texture2D;
        plane.GetComponent<Renderer>().material.mainTexture = wallTexture;
        wallTexture.Apply();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            RaycastHit ray;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ray))
            {
                lastPoint = new Vector2((int)(ray.textureCoord.x * wallTexture.width),
                                        (int)(ray.textureCoord.y * wallTexture.height));
            }
        }
        //draw a line between the last known location of the mouse and the current location
        if (Input.GetMouseButton(0))
        {
            RaycastHit ray;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ray))
            {
                plane.GetComponent<Renderer>().material.mainTexture = wallTexture;

                bresenhamsLine.DrawPixelLine((int)(ray.textureCoord.x * wallTexture.width),
                                   (int)(ray.textureCoord.y * wallTexture.height),
                                   (int)lastPoint.x, (int)lastPoint.y, Color.yellow, wallTexture);
                wallTexture.Apply();
                lastPoint = new Vector2((int)(ray.textureCoord.x * wallTexture.width),
                                   (int)(ray.textureCoord.y * wallTexture.height));
            }
        }

        if (Input.GetMouseButton(1))
        {
            RaycastHit ray;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ray))
            {
                plane.GetComponent<Renderer>().material.mainTexture = wallTexture;
                wallTexture.SetPixel((int)(ray.textureCoord.x * wallTexture.width),
                                   (int)(ray.textureCoord.y * wallTexture.height), Color.white);
                wallTexture.Apply();
            }
        }
    }
}
