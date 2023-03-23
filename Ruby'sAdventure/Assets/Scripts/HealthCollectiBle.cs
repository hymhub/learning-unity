using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthCollectiBle : MonoBehaviour
{
    public GameObject effectParticle;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 🍓加血
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rubyController = collision.GetComponent<RubyController>();
        if (collision.CompareTag("Player") && rubyController.CurrentHealth < rubyController.maxHealth)
        {
            rubyController.ChangeHealth(5);
            rubyController.PlaySound(audioClip);
            // gameObject.SetActive(false);
            Destroy(gameObject);
            GameObject projectileObject = Instantiate(
                effectParticle,
                transform.position + Vector3.up * 0.5f,
                Quaternion.identity
            );
        }
    }
}
