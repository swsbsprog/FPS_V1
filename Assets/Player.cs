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
            //print("마우스 클릭했다 : " + Input.mousePosition);
            print($"x:{centerX}, y:{centerY}, 마우스 클릭했다 : {Input.mousePosition}"); // 추천
                                                                                  //print(string.Format($"x:{0}, y:{1}, 마우스 클릭했다 : {2}", centerX, centerY, Input.mousePosition));

            Vector3 centerPos = new Vector3(centerX, centerY, 0);

            //centerPos = Input.mousePosition; // 테스트 마우스 클릭한 위치로 쏘기.


            // //카메라 위치에서 클릭한 위치로 레이를 소자.
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
        // 로컬축으로 움직이자.
        // w키 앞으로 z증가.
        // s뒤로 z감소
        // a왼, x감소
        // d오른쪽 x 증가
        //Vector3  x,y,z float 이 3개

        ////Vector3 move = new Vector3(0, 0, 0); ///Vector3.zero;
        move = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) // z 앞뒤
            move.z = 1;
        if (Input.GetKey(KeyCode.S))
            move.z = -1;
        if (Input.GetKey(KeyCode.A)) // x 사이드, // 왼쪽.
            move.x = -1;
        if (Input.GetKey(KeyCode.D)) // 오른쪽
            move.x = 1;

        // move 움직임이 있다면 런 애니메이션 해라.
        // 움직임이 없다면 idle애니메이션 해라.
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
