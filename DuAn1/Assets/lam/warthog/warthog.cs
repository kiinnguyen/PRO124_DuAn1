using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warthog : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator myAnim;
    private Transform target;
    public Transform homePositonWarthog;
    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    private float maxrange = 0;
    [SerializeField]
    private float minrange = 0;
    void Start()
    {
        myAnim = GetComponent<Animator>();
        target = FindAnyObjectByType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= maxrange && Vector3.Distance(target.position, transform.position) >= minrange)
        {
            FollowPlayer();
        }
        else if (Vector3.Distance(target.position, transform.position) >= maxrange)
        {
            Gohome();
        }


    }

    public void FollowPlayer()
    {
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("MoveX", target.position.x - transform.position.x);
        myAnim.SetFloat("MoveY", target.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void Gohome()
    {
        myAnim.SetFloat("MoveX", homePositonWarthog.position.x - transform.position.x);
        myAnim.SetFloat("MoveY", homePositonWarthog.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, homePositonWarthog.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, homePositonWarthog.position) == 0)
        {
            myAnim.SetBool("isMoving", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Attack")
        {
            Vector2 difference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }
}
