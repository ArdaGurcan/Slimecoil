using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    static List<PlayerScript> players = new List<PlayerScript>();
    public LayerMask layerMask;
    static int activeIndex = 0;
    static bool switched = false;
    float speed = 250f;
    float jumpForce = 8.5f;
    LineRenderer lineRenderer;
    [SerializeField]
    bool sticky = false;
    Animator animator;
    SpriteRenderer rend;
    static Vector3 p1;
    static Vector3 p2;
    bool die = false;
    GameObject[] boxes;
    //float k = 0.7f;

    void Start()
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");

        rend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        players.Add(GetComponent<PlayerScript>());
        activeIndex = 0;
        if(GetComponent<LineRenderer>())
        {
            p1 = transform.position;
            lineRenderer = GetComponent<LineRenderer>();
        }
        else
        {
            p2 = transform.position;
        }
    }

    
    void Update()
    {
        

        transform.GetChild(0).transform.rotation = Quaternion.identity;
        if (lineRenderer)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, players[0].transform.position);
        }

        if(Input.GetKeyDown(KeyCode.R) || die || transform.position.y < -10f)
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].transform.position = boxes[i].GetComponent<BoxScript>().position;
                boxes[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            Camera.main.GetComponent<CamScript>().player1.position = p1;
            Camera.main.GetComponent<CamScript>().player2.position = p2;
            sticky = false;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.constraints = 0;
            die = false;
            animator.enabled = true;

        }

        if(activeIndex == players.IndexOf(this))
        {
            

            rend.color = Color.HSVToRGB(0, 0, 1);
            
            Debug.Log(activeIndex);
            rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime * rb.mass, 0));
            if(Input.GetKeyDown(KeyCode.Space) && Physics2D.OverlapCircle(transform.position - Vector3.up * 0.1f, 0.35f, layerMask))
            {
                rb.AddForce(Vector2.up * jumpForce * rb.mass, ForceMode2D.Impulse);
            }
            if(Input.GetKeyDown(KeyCode.Tab) && !switched)
            {
                switched = true;
                activeIndex++;
                if(activeIndex >= players.Count)
                {
                    activeIndex = 0;
                }
                //players[players.IndexOf(this) < players.Count - 1 ? players.IndexOf(this) + 1 : 0].isActive = true;
                //Debug.Log(players.Count);
            }
            else if(Input.GetKeyUp(KeyCode.Tab))
            {
                switched = false;
            }
            if(Input.GetKeyDown(KeyCode.LeftShift) && Physics2D.OverlapCircle(transform.position - Vector3.up * 0.1f, 0.34f, layerMask))
            {
                sticky = !sticky;
                animator.enabled = !animator.enabled;
                if (sticky)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                }
                else
                {
                    rb.constraints = 0;
                }

            }
        }
        else
        {
            rend.color = Color.HSVToRGB(0, 0, 0.8f);
            
            //{
            //    rb.AddForce(-k * (transform.position - players[activeIndex].transform.position + Vector3.Normalize(transform.position - players[activeIndex].transform.position) * -2f));

            //    players[activeIndex].rb.AddForce(k * (transform.position - players[activeIndex].transform.position + Vector3.Normalize(transform.position - players[activeIndex].transform.position) * -2f));
            //}
            
        }



        //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, rb.velocity.y);
    }

    private void LateUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.R) || die || transform.position.y < -10f)
        //{
        //    die = false;
        //}
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position - Vector3.up * 0.1f, 0.34f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Checkpoint")
        {
            p1 = Camera.main.GetComponent<CamScript>().player1.position;
            p2 = Camera.main.GetComponent<CamScript>().player2.position;
        }
        else if(collision.tag == "Death")
        {
            die = true;
            Camera.main.GetComponent<CamScript>().player1.GetComponent<PlayerScript>().die = true;
            Camera.main.GetComponent<CamScript>().player2.GetComponent<PlayerScript>().die = true;
        }
    }
}
