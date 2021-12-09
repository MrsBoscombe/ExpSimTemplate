using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;

    private Rigidbody2D theRB;
    private Animator anim;

    public SpriteRenderer theSR;
    public Sprite[] playerDirectionSprites;

    public Animator wpnAnim;

    enum Facing
    {
        Down,    // 0
        Side,    // 1
        Up       // 2
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
        anim.SetFloat("Speed", theRB.velocity.magnitude);

        if (theRB.velocity != Vector2.zero)
        {
            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                theSR.sprite = playerDirectionSprites[(int)Facing.Side];

                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    theSR.flipX = true;
                    wpnAnim.SetFloat("dirX", -1f);
                    wpnAnim.SetFloat("dirY", 0f);
                }
                else
                {
                    theSR.flipX = false;
                    wpnAnim.SetFloat("dirX", 1f);
                    wpnAnim.SetFloat("dirY", 0f);
                }
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    theSR.sprite = playerDirectionSprites[(int)Facing.Down];

                    wpnAnim.SetFloat("dirX", 0f);
                    wpnAnim.SetFloat("dirY", -1f);
                }
                else
                {
                    theSR.sprite = playerDirectionSprites[(int)Facing.Up];
                    wpnAnim.SetFloat("dirX", 0f);
                    wpnAnim.SetFloat("dirY", 1f);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            wpnAnim.SetTrigger("Attack");
        }

        //transform.position = new Vector3(transform.position.x + (Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime), transform.position.y + (Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime), transform.position.z);
    }
}
