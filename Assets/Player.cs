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


    public GameObject bulletHole;
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
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
                var newGo =Instantiate(bulletHole);
                newGo.transform.position = hit.point;
                //hit.normal x = 90, y, z = 0;
                //newGo.transform.rotation = Quaternion.Euler(hit.normal); // 범위가 다름
                var bulletTr = newGo.transform;
                bulletTr.rotation = Quaternion.FromToRotation(bulletTr.up, hit.normal) * bulletTr.rotation;
                bulletTr.Translate(0, bulletOffset, 0);
            }
        }

        UpdateMove();
    }
    public float bulletOffset = 0.001f;

    private void UpdateMove()
    {
        // 로컬축으로 움직이자.
        // w키 앞으로 z증가.
        // s뒤로 z감소
        // a왼, x감소
        // d오른쪽 x 증가
        //Vector3  x,y,z float 이 3개

        ////Vector3 move = new Vector3(0, 0, 0); ///Vector3.zero;
        Vector3 move = Vector3.zero;
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

        //animator.SetFloat("Speed", move.magnitude);
        animator.SetFloat("forward", forward);
        animator.SetFloat("side", side);


        move = move * speed * Time.deltaTime;

        transform.Translate(move);
    }
}
