using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 v3,mousePos,worldPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        v3 = Camera.main.WorldToScreenPoint(transform.position);
        mousePos = Input.mousePosition;
        mousePos.z = v3.z;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        //Debug.Log(worldPos);
    }
}
