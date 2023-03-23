using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mouseIsUp = Input.GetMouseButtonUp(0);
        if (mouseIsUp)
        {
            var transform1 = transform;
            var bullet = GameObject.Instantiate(bulletPrefab, transform1.position, transform1.rotation);
            var rd = bullet.GetComponent<Rigidbody>();
            // rd.AddForce(Vector3.forward * 100);
            rd.velocity = Vector3.back * 30;
        }
    }
}
