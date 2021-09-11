using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    GameManager gameManager;
    Camera cam;
    TrailRenderer trailRen;
    BoxCollider boxCol;
    
    Vector3 mousePos;

    bool swiping = false;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        trailRen = GetComponent<TrailRenderer>();
        boxCol = GetComponent<BoxCollider>();

        trailRen.enabled = false;
        boxCol.enabled = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsGameOver && !gameManager.IsGamePaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }

            if (swiping)
            {
                UpdateMousePos();
            }
        }
    }

    /**ScreenToWorld will convert the screen position of the mouse to a world position. 
     * The reason we use 10.0f on the z axis, is because the camera has the z position of -10.0f.*/
    void UpdateMousePos()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
    }

    void UpdateComponents()
    {
        trailRen.enabled = swiping;
        boxCol.enabled = swiping;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
}
