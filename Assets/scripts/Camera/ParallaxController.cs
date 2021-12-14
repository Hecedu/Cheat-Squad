using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public float smoothingScale = 0.5f;
    public float parallaxFactor = -1f;
    public Transform[] ParallaxLayers;
    public float[] ParallaxScales;

    public Transform Cam;
    private Vector3 LastCameraPosition;
    // Start is called before the first frame update

    void Awake() {
        Cam = Camera.main.transform;
    }
    void Start()
    {

        LastCameraPosition = Cam.position;
        ParallaxScales = new float[ParallaxLayers.Length];
        for (int i = 0; i<ParallaxScales.Length; i++){
            ParallaxScales[i] = ParallaxLayers[i].position.z*parallaxFactor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i<ParallaxLayers.Length; i++){
            float parallaxX = (LastCameraPosition.x - Cam.position.x) * ParallaxScales[i];
            float backgroundTargetPosX = ParallaxLayers[i].position.x + parallaxX;
            Vector3 BackgroundTargetPosition = new Vector3 (backgroundTargetPosX,ParallaxLayers[i].position.y, ParallaxLayers[i].position.z);
            ParallaxLayers[i].position = Vector3.Lerp(ParallaxLayers[i].position, BackgroundTargetPosition, smoothingScale * Time.unscaledDeltaTime);
        }
        LastCameraPosition = Cam.position;
    }
}
