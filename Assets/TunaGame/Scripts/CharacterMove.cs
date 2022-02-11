using UnityEngine;
using TMPro;

/// <summary>
/// キャラクターを操作してジャンプさせる機能を提供するコンポーネント
/// WASD で移動し、Space でジャンプする（Input Manager 使用）
/// 足下にトリガーを設置すること。足下のトリガーが接触していたら「接地している」と判定し、ジャンプできる。
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class CharacterMove : MonoBehaviour
{
    [SerializeField] TextMeshPro userName;
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] float _dashSpeed = 1f;
    [SerializeField] float _jumpPower = 5f;
    Rigidbody _rb = default;
    Animator _anim = default;
    bool _isGrounded;
    bool _isDash;
    bool _isDead;
    int mg;
    float dashTime = 0f;
    float time = 3f;
    [SerializeField] GameObject rsltPanel,GameUI;

    new AudioSource audio = new AudioSource();
    [SerializeField] AudioClip win;
    [SerializeField] AudioClip deth;
    [SerializeField] AudioClip dash;

    public int Magnitude { get => mg; }
    public bool OnDead { set { _isDead = value; } get => _isDead; }

    void Start()
    {
        userName.GetComponent<TextMeshPro>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 入力を受け付ける
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 入力された方向を「カメラを基準とした XZ 平面上のベクトル」に変換する
        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        if (!_isDead)
        {
            // キャラクターを「入力された方向」に向ける
            if (dir != Vector3.zero)
            {
                this.transform.forward = dir;
            }

            // Y 軸方向の速度を保ちながら、速度ベクトルを求めてセットする
            Vector3 velocity;
            if (_isDash)
            {
                dashTime += Time.deltaTime;
                if (dashTime > 5f) _isDash = false;
                velocity = dir.normalized * _dashSpeed;
            }
            else velocity = dir.normalized * _moveSpeed;
            velocity.y = _rb.velocity.y;
            _rb.velocity = velocity;

            // ジャンプ処理
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            }
        }
        else
        {
            OnGameOver();
            time -= Time.deltaTime;
            if (time < 0f)
            {
                OnResult();
            }
        }
    }

    void LateUpdate()
    {
        // アニメーションの処理
        if (_anim)
        {
            _anim.SetBool("IsGrounded", _isGrounded);
            Vector3 walkSpeed = _rb.velocity;
            walkSpeed.y = 0;
            _anim.SetFloat("Speed", walkSpeed.magnitude);
            _anim.SetBool("DeadOrAlive", _isDead);
        }
        mg = (int)_rb.velocity.magnitude;
    }

    void FixedUpdate()
    {
        userName.text = GameManager.Instance.UserName;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Goal") OnCrear();
        else if (other.name == "Field") _isGrounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Field") _isGrounded = false;
    }

    public void OnDash()
    {
        dashTime = 0f;
        audio.PlayOneShot(dash);
        _isDash = true;
    }

    bool b = true;
    void OnGameOver()
    {
        if(b && !audio.isPlaying)
        {
            audio.PlayOneShot(deth);
            b = false;
        }
    }

    void OnCrear()
    {
        audio.PlayOneShot(win);
        OnResult();
    }

    void OnResult()
    {
        GameUI.SetActive(false);
        rsltPanel.SetActive(true);
    }
}
