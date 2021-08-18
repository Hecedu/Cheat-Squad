using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float halfHeight;
    private float halfWidth;
    // Start is called before the first frame update
    void Start()
    {

        //adjust and hides edges of map according to screen size.
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth,halfHeight,0f);
        topRightLimit = theMap.localBounds.max + new Vector3 (-halfWidth,-halfHeight,0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
