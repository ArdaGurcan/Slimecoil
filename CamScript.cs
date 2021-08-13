using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public Transform player1;
    
    public Transform player2;
    private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    transform.position = new Vector3(Mathf.SmoothStep(transform.position.x, (player1.transform.position.x + player2.transform.position.x) / 2, 10f * Time.deltaTime), transform.position.y, transform.position.z);
    //}
    private void LateUpdate()
    {
        
        transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, (player1.transform.position.x + player2.transform.position.x) / 2, ref velocity, 30f * Time.deltaTime), transform.position.y, transform.position.z);

    }
}
