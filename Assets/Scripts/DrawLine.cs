using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour, BresenhamsLine
{
    [SerializeField] GameObject plane;
    Texture2D wallTexture;
    Vector2 lastPoint = Vector2.zero;


    private void Awake()
    {

        wallTexture = Instantiate(plane.GetComponent<Renderer>().material.mainTexture) as Texture2D;
        plane.GetComponent<Renderer>().material.mainTexture = wallTexture;
        wallTexture.Apply();
    }

    void Update()
    {
        RaycastHit ray;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ray)) return;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            lastPoint = new Vector2((int)(ray.textureCoord.x * wallTexture.width),
                                        (int)(ray.textureCoord.y * wallTexture.height));
        }

        if (Input.GetMouseButton(0))
        {
            Draw(ray);
        }

        if (Input.GetMouseButton(1))
        {
            Erase(ray);
        }
    }

    private void Draw(RaycastHit ray)
    {
        plane.GetComponent<Renderer>().material.mainTexture = wallTexture;

        BresenhamsLine.DrawPixelLine((int)(ray.textureCoord.x * wallTexture.width),
                           (int)(ray.textureCoord.y * wallTexture.height),
                           (int)lastPoint.x, (int)lastPoint.y, Color.yellow, wallTexture);
        wallTexture.Apply();
        lastPoint = new Vector2((int)(ray.textureCoord.x * wallTexture.width),
                           (int)(ray.textureCoord.y * wallTexture.height));
    }
    
    private void Erase(RaycastHit ray)
    {
        plane.GetComponent<Renderer>().material.mainTexture = wallTexture;
        wallTexture.SetPixel((int)(ray.textureCoord.x * wallTexture.width),
                           (int)(ray.textureCoord.y * wallTexture.height), Color.white);
        wallTexture.Apply();
    }
}
