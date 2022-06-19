using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTest : MonoBehaviour
{
    void Start()
    {
        ////무한 루프 테스트
        // 콜스택으로 남아서 해결하기 쉬운상황
        //InfiniteFn(1);

        // 문제가 되는 심각한 상황!
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
