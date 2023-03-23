using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2d;
    public int maxHealth = 5; //  最大生命值
    public AudioClip rubyHitAudioClip;
    public AudioClip rubyLaunchAudioClip;
    // public AudioClip rubyWalkAudioClip;
    public int CurrentHealth { get; private set; } // 当前生命值
    public int speed = 4;
    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int LookX = Animator.StringToHash("Look X");
    private static readonly int LookY = Animator.StringToHash("Look Y");
    private Vector2 _lookDirection = new Vector2(0, -1);
    public GameObject projectilePrefab;
    private static readonly int LaunchHash = Animator.StringToHash("Launch");
    public AudioSource audioSource;
    public AudioSource walkAudioSource;
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private bool _rubyIsHiting;
    private Vector3 _initRubyPosition;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Test.Show();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        _rigidbody2d = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60; // 强制设置Update帧率
        CurrentHealth = maxHealth;
        _animator = GetComponent<Animator>();
        _initRubyPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var move = new Vector2(h, v);
        // Mathf.Approximately 比较两个浮点值，如果它们相似，则返回 true --> https://docs.unity3d.com/cn/2017.4/ScriptReference/Mathf.Approximately.html
        // 当前玩家输入某个轴向值不为0
        if ((!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0)) && !_rubyIsHiting)
        {
            _lookDirection.Set(h, v);
            _lookDirection.Normalize(); // 不要浮点数，直接设置成1或-1
            if (!walkAudioSource.isPlaying)
            {
                // walkAudioSource.clip = rubyWalkAudioClip;
                walkAudioSource.Play();
            }
        }
        else
        {
            walkAudioSource.Stop();
        }
        _animator.SetFloat(LookX, _lookDirection.x);
        _animator.SetFloat(LookY, _lookDirection.y);
        _animator.SetFloat(Speed, move.magnitude); // magnitude: 向量的长度
        // 个人写法
        // float x = h > 0 ? 1 : h < 0 ? -1 : 0;
        // float y = v > 0 ? 1 : v < 0 ? -1 : 0;
        // _animator.SetFloat(Speed, Mathf.Abs(x != 0 ? x : y));
        // if (y != 0)
        // {
        //     _animator.SetFloat(LookY, y);
        //     _animator.SetFloat(LookX, 0);
        // }
        // if (x != 0)
        // {
        //     _animator.SetFloat(LookX, x);
        //     _animator.SetFloat(LookY, 0);
        // }
        
        Vector2 position = transform.position;
        // position.x = position.x + 3 * h * Time.deltaTime;
        // position.y = position.y + 3 * v * Time.deltaTime;
        // 上面两句等于下面这句，向量可以直接计算
        if (!_rubyIsHiting)
        {
            position = position + move * (speed * Time.deltaTime);
            _rigidbody2d.MovePosition(position);   
        }

        // 发射子弹
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
        
        // 检测是否与NPC对话
        if (Input.GetKeyDown(KeyCode.T))
        {
            var hit = Physics2D.Raycast(
                _rigidbody2d.position + Vector2.up * 0.2f,
                _lookDirection,
                1,
                LayerMask.GetMask("NPC")
            );
            Debug.Log(hit.collider);
            if (hit.collider != null)
            {
                var npcDialog = hit.collider.gameObject.GetComponent<NpcDialog>();
                npcDialog.DialogShow(true);
            }
        }
    }

    // 血量改变
    public void ChangeHealth(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);
        // Debug.Log(CurrentHealth+"/"+maxHealth);
        UIHealthBar.Instance.SetValue((float)CurrentHealth/maxHealth);
        if (amount < 0)
        {
            PlaySound(rubyHitAudioClip);
            _animator.SetTrigger(HitHash);
            _rubyIsHiting = true;
            Invoke(nameof(RubyHitExit), 0.2f);
        }

        if (CurrentHealth <= 0)
        {
            transform.position = _initRubyPosition;
            ChangeHealth(maxHealth);
        }
    }

    private void RubyHitExit()
    {
        _rubyIsHiting = false;
    }

    // 发射子弹
    private void Launch()
    {
        if (!UIHealthBar.Instance.canUseLaunch)
        {
            return;
        }
        PlaySound(rubyLaunchAudioClip);
        GameObject projectileObject = Instantiate(
            projectilePrefab,
            _rigidbody2d.position+Vector2.up * 0.5f,
            Quaternion.identity
        );
        var projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(_lookDirection, 300);
        _animator.SetTrigger(LaunchHash);
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
