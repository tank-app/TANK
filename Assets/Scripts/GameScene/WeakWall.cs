using System;
using UnityEngine;

public class WeakWall : MonoBehaviour
{
    void OnTriggerEnter(Collider collider) //爆風との衝突を検知
    {
        if (collider.CompareTag("Mine")) Destroy(this.gameObject); //破壊
    }
}