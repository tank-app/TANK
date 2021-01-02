using System;
using UnityEngine;

public class WeakWall : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision) //壁との衝突をみる
    {
        if (collision.gameObject.tag == "Bullet") Destroy(this.gameObject); //破壊
    }

}