using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteWalk : MonoBehaviour
{
    
   
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;


        // REFERENCES
    private CharacterController controller;

    private Animator anim;
    
    // Start is called before the first frame update

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {   
        anim.SetFloat("Blend",1.0f);
        float moveX = 0;
        float X = transform.position.x;
        if (X > 728){
            
            // float moveX = transform.position.x;
            // float moveY = transform.position.y;
            
            moveX = moveX - 1;
            
            // moveDirection = transform.position;
            moveDirection = new Vector3(moveX,0, 0);
            moveDirection *= walkSpeed;

            controller.Move(moveDirection * Time.deltaTime);
        }

        else if (X < 728) {
            
            // anim.SetFloat("Blend",0.5f)l

            moveDirection = new Vector3(796,0, 0);
    
            moveDirection *= walkSpeed;

            controller.Move(moveDirection * Time.deltaTime);
        }
        
        

    }
}
