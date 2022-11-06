using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Rigidbody2D rb2d;
    private GameMaster gm;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;

    // Update is called once per frame
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
    }
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        verticalMove = Input.GetAxisRaw("Vertical");
        
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }
    private void FixedUpdate()
    {
        if (transform.position.x < -11.80 && horizontalMove <0)
        {
            Debug.Log("Limite sinistro superato!!!");
            horizontalMove = 0;
        }
        else if (verticalMove > 0)
        {
            Debug.Log("Salta");
            jump = true;
            animator.SetBool("isJumping", true);
        }
        
        controller.Move(horizontalMove *  Time.fixedDeltaTime, false, jump);
        jump = false;
        
    }

}
