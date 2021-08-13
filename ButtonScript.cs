using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Animator door;
    int coll = 0;

    private void Update()
    {
        if(coll > 0)
        {

            door.SetBool("open", true);
            GetComponent<Animator>().SetBool("pressed", true);
        }
        else
        {
            door.SetBool("open", false);
            GetComponent<Animator>().SetBool("pressed", false);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        coll++;
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        coll--;
    }
  
}
