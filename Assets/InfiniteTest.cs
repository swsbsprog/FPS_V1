using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTest : MonoBehaviour
{
    void Start()
    {
        ////���� ���� �׽�Ʈ
        // �ݽ������� ���Ƽ� �ذ��ϱ� �����Ȳ
        //InfiniteFn(1);

        // ������ �Ǵ� �ɰ��� ��Ȳ!
        //bool loop = true;
        //while (loop)
        //{
        //    int i = 0;
        //    int j = 1;
        //    if(i > j)
        //        loop = false;
        //}
    }

    private void InfiniteFn(int v)
    {
        InfiniteFn1(v);
    }

    private void InfiniteFn1(int v)
    {
        InfiniteFn(v);
    }
}
