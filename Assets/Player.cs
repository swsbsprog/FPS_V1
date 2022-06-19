using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        // 로컬축으로 움직이자.
        // w키 앞으로 z증가.
        // s뒤로 z감소
        // a왼, x감소
        // d오른쪽 x 증가
        float forward = 0;
        //float side = 0;
        if (Input.GetKey(KeyCode.W))
            forward = 1;
        if (Input.GetKey(KeyCode.S))
            forward = -1;

        transform.Translate(0, 0, forward);
    }
}
