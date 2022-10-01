using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public GameObject bulletHole;
    public float bulletOffset = 0.001f;
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var newGo = Instantiate(bulletHole);
        var hit = collision.contacts[0];
        newGo.transform.position = hit.point;
        //hit.normal x = 90, y, z = 0;
        //newGo.transform.rotation = Quaternion.Euler(hit.normal); // 범위가 다름
        var bulletTr = newGo.transform;
        bulletTr.rotation = Quaternion.FromToRotation(bulletTr.up, hit.normal) * bulletTr.rotation;
        bulletTr.Translate(0, bulletOffset, 0);

        Destroy(gameObject);
    }
}
