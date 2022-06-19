using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1;

    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // 로컬축으로 움직이자.
        // w키 앞으로 z증가.
        // s뒤로 z감소
        // a왼, x감소
        // d오른쪽 x 증가
        //Vector3  x,y,z float 이 3개

        ////Vector3 move = new Vector3(0, 0, 0); ///Vector3.zero;
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) // z
            move.z = 1;
        if (Input.GetKey(KeyCode.S))
            move.z = -1;
        if (Input.GetKey(KeyCode.A)) // x
            move.x = -1;
        if (Input.GetKey(KeyCode.D))
            move.x = 1;

        // move 움직임이 있다면 런 애니메이션 해라.
        // 움직임이 없다면 idle애니메이션 해라.
        //if (move.x != 0 || move.z != 0) 
        //if (move.magnitude > 0)
        if (move.sqrMagnitude> 0)
        {
            animator.Play("run");
        }
        else
            animator.Play("idle");


        move = move * speed * Time.deltaTime;

        transform.Translate(move);
    }
}
