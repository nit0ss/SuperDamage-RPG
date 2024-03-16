using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ParallaxBackgroun : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float xPosition;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        
        transform.position = new UnityEngine.Vector2(xPosition + distanceToMove, transform.position.y);
    }
}
