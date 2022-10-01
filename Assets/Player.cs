using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1;

    public Animator animator;
    public Rigidbody rb;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //Cursor.visible = false;
        //int myInt = 1;
        //Fn(myInt); //2
        //print(myInt); // 1
        //FnRef(ref myInt); //3
        //print(myInt); //3
    }
    //void Fn(int myInt)
    //{
    //    myInt = 2;
    //    print(myInt);
    //}
    //void FnOut(out int myInt)
    //{
    //    myInt = 1;
    //    print(myInt);
    //}
    //void FnRef(ref int myInt)
    //{
    //    myInt = 3;
    //    print(myInt);
    //}


    public Vector3 dir = new Vector3(0, 0, 1);

    public float range = 1;
    public Transform targetPosition;
    void OnDrawGizmos()
    {
        if(targetPosition != null)
            Gizmos.DrawLine(Camera.main.transform.position, targetPosition.position);
    }


    public GameObject bullet;
    public Transform firePos;
    //public 
    void Update()
    {
        FireMissile();
        UpdateRotation();
        UpdateMove();
        ToggleCursor();
    }

    public bool cursorLocked;
    private void ToggleCursor()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            cursorLocked = !cursorLocked;

        Cursor.visible = !cursorLocked;
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public float xRotation;
    public float mouseSensitivity = 1;
    private void UpdateRotation()
    {
        float mouseX = Input.GetAxis("Horizontal") * mouseSensitivity * Time.deltaTime;
        xRotation += mouseX;
        //xRotation = Mathf.Clamp(xRotation, -180, 180);
        transform.rotation = Quaternion.AngleAxis(xRotation, Vector3.up);
    }

    private void FireMissile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("fire");

            var newBullet = Instantiate(bullet);
            newBullet.transform.position = firePos.position;
            

            float centerX = Screen.width / 2;
            float centerY = Screen.height / 2;
            //print("���콺 Ŭ���ߴ� : " + Input.mousePosition);
            print($"x:{centerX}, y:{centerY}, ���콺 Ŭ���ߴ� : {Input.mousePosition}"); // ��õ
                                                                                  //print(string.Format($"x:{0}, y:{1}, ���콺 Ŭ���ߴ� : {2}", centerX, centerY, Input.mousePosition));

            Vector3 centerPos = new Vector3(centerX, centerY, 0);

            //centerPos = Input.mousePosition; // �׽�Ʈ ���콺 Ŭ���� ��ġ�� ���.


            // //ī�޶� ��ġ���� Ŭ���� ��ġ�� ���̸� ����.
            Ray ray = Camera.main.ScreenPointToRay(centerPos);
            Physics.Raycast(ray, out RaycastHit hit);
            if (hit.transform != null)
            {
                newBullet.transform.LookAt(hit.point);
            }
        }
    }
    Vector3 move;

    private void UpdateMove()
    {
        // ���������� ��������.
        // wŰ ������ z����.
        // s�ڷ� z����
        // a��, x����
        // d������ x ����
        //Vector3  x,y,z float �� 3��

        ////Vector3 move = new Vector3(0, 0, 0); ///Vector3.zero;
        move = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) // z �յ�
            move.z = 1;
        if (Input.GetKey(KeyCode.S))
            move.z = -1;
        if (Input.GetKey(KeyCode.A)) // x ���̵�, // ����.
            move.x = -1;
        if (Input.GetKey(KeyCode.D)) // ������
            move.x = 1;

        // move �������� �ִٸ� �� �ִϸ��̼� �ض�.
        // �������� ���ٸ� idle�ִϸ��̼� �ض�.
        //if (move.x != 0 || move.z != 0) 
        //if (move.magnitude > 0)

        float forward = move.z;
        float side = move.x;

        animator.SetFloat("forward", forward);
        animator.SetFloat("side", side);
    }
    private void FixedUpdate()
    {
        Vector3 localMove = move.z * transform.forward;
        localMove += move.x * transform.right;

        var pos = rb.position;
        localMove = localMove * speed * Time.deltaTime;
        pos += localMove;
        rb.position = pos;
    }
}
