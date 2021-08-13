using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amogus : MonoBehaviour
{
    public Transform transfor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, transfor.position);
    }
}
