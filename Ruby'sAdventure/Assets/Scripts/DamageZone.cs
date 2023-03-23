using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private float _invincibleTimer = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 踩针掉血
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _invincibleTimer -= Time.deltaTime;
            if (_invincibleTimer <= 0)
            {
                _invincibleTimer = 1.0f;
                var rubyController = other.GetComponent<RubyController>();
                rubyController.ChangeHealth(-1);
            }
        }
    }
}
