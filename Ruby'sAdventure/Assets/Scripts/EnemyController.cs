using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D _rigidbody2D;

    public bool vertical;

    public int direction = 1;

    public float changeTime = 3;

    private float _timer;

    private Animator _animator;

    private bool _broken = true; // 当前机器人是否故障

    private static readonly int MoveX = Animator.StringToHash("MoveX");
    // private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int Fixed = Animator.StringToHash("Fixed");
    public ParticleSystem smokeEffect;

    private AudioSource _audioSource;
    public AudioClip[] hitAudios;
    public AudioClip fixedAudio;
    public GameObject hitEffectParticle;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _timer = changeTime;
        // _animator.SetFloat(MoveX, direction);
        // _animator.SetBool(Vertical, vertical);
        _animator.SetFloat(vertical ? MoveY : MoveX, direction);
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_broken)
        {
            return;
        }
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = changeTime;
            direction *= -1;
            // _animator.SetFloat(MoveX, direction);
            _animator.SetFloat(vertical ? MoveY : MoveX, direction);
        }
        Vector2 position = _rigidbody2D.position; // == transform.position
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        } 
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        _rigidbody2D.MovePosition(position);
    }

    // 机器人撞到ruby
    private void OnCollisionEnter2D(Collision2D col)
    {
        var rubyController = col.gameObject.GetComponent<RubyController>();
        if (col.gameObject.CompareTag("Player"))
        {
            rubyController.ChangeHealth(-1);
        }
    }

    // 机器人被修复
    public void Fix()
    {
        _broken = false;
        _rigidbody2D.simulated = false; // 物理系统不再模拟刚体，碰撞检测失效
        smokeEffect.Stop(); // 停止粒子动画
        _animator.SetTrigger(Fixed);
        int randomNum = Random.Range(0, 2);
        _audioSource.PlayOneShot(hitAudios[randomNum], 1.5f);
        Invoke(nameof(PlayFixedSound), 0.5f);// 延迟调用
        GameObject projectileObject = Instantiate(
            hitEffectParticle,
            _rigidbody2D.position + Vector2.up * 0.5f,
            Quaternion.identity
        );
    }

    private void PlayFixedSound()
    {
        _audioSource.PlayOneShot(fixedAudio, 1.5f);
        Invoke(nameof(StopAudioSourcePlay), 1f);
    }

    private void StopAudioSourcePlay()
    {
        _audioSource.Stop();
    }
}
