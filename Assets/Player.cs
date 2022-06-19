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
        // ���������� ��������.
        // wŰ ������ z����.
        // s�ڷ� z����
        // a��, x����
        // d������ x ����
        //Vector3  x,y,z float �� 3��

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

        // move �������� �ִٸ� �� �ִϸ��̼� �ض�.
        // �������� ���ٸ� idle�ִϸ��̼� �ض�.
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
