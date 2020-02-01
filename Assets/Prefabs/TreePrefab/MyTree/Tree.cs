using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    enum STATE {idle=0, dried, chopped, testAnimal};
    private STATE state = STATE.idle;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        // set animation at the end of each frame
        SetAnimation(this.state);
    }

    public void actioned()
    {
        this.state = STATE.testAnimal;
    }

    private void SetAnimation(STATE ani)
    {
        this.animator.SetInteger("state", (int) ani);
    }

    private void OnMouseDown()
    {
        print("OnMouseDown triggered");
        actioned();
    }
}
