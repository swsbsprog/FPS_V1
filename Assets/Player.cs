using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        // ���������� ��������.
        // wŰ ������ z����.
        // s�ڷ� z����
        // a��, x����
        // d������ x ����
        float forward = 0;
        //float side = 0;
        if (Input.GetKey(KeyCode.W))
            forward = 1;
        if (Input.GetKey(KeyCode.S))
            forward = -1;

        transform.Translate(0, 0, forward);
    }
}
