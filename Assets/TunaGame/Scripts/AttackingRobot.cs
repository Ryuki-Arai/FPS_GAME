using UnityEngine;

/// <summary>
/// 敵キャラクターを制御するコンポーネント。
/// トリガーに Rigidbody が入ったら、攻撃する。
/// 攻撃が Rigidbody に当たったら、弾き飛ばす。
/// 攻撃範囲は _attackRangeCenter, _attackRangeRadius で設定する。
/// </summary>
public class AttackingRobot : MonoBehaviour
{
    /// <summary>攻撃範囲の中心</summary>
    [SerializeField] Vector3 _attackRangeCenter = default;
    /// <summary>攻撃範囲の半径</summary>
    [SerializeField] float _attackRangeRadius = 1f;
    /// <summary></summary>
    [SerializeField] float _attackPower = 10f;
    GameObject _player = default;
    string _playerTag = "Player";
    Animator _anim = default;
    Vector3 vPower;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        this.transform.rotation = Quaternion.Euler(0, Random.Range(-180f, 180f), 0);
    }
    void LateUpdate()
    {
        // プレイヤーの方を見る
        //if (_player && live)
        //{
        //    this.transform.forward = _player.transform.position - this.transform.position;
        //}
    }

    void OnDrawGizmosSelected()
    {
        // 攻撃範囲を赤い線でシーンビューに表示する
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetAttackRangeCenter(), _attackRangeRadius);
    }

    /// <summary>
    /// 攻撃範囲の中心を計算して取得する
    /// </summary>
    /// <returns>攻撃範囲の中心座標</returns>
    Vector3 GetAttackRangeCenter()
    {
        Vector3 center = this.transform.position + this.transform.forward * _attackRangeCenter.z
            + this.transform.up * _attackRangeCenter.y
            + this.transform.right * _attackRangeCenter.x;
        return center;
    }

    private void OnCollisionEnter(Collision collision)
    {
        vPower = collision.relativeVelocity;
        var rb = GetComponent<Rigidbody>();
        if (collision.gameObject.tag == _playerTag && vPower.magnitude > 4.5f)
        {
            Debug.Log(vPower.magnitude);
            _anim.SetTrigger("Dead");
            rb.AddForce(vPower, ForceMode.Impulse);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, Random.Range(-180f, 180f), 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // トリガーに入った GameObject が Rigidbody を持っていたら Animator Controller のパラメータを操作する
        //if (other.gameObject.tag == _playerTag)
        //{
            
        //    Debug.Log("Trigger");
        //}
    }

    private void Destroy()
    {
        Debug.Log("Destroy");
        Destroy(this.gameObject);
    }

    void Move()
    {
        this.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 180f), 0);
        switch (Random.Range(0, 3))
        {
            case 0:
                _anim.SetInteger("Moving",0);
                Debug.Log("Walk");
                break;
            case 1:
                _anim.SetInteger("Moving", 1);
                Debug.Log("Run");
                break;
            case 2:
                _anim.SetInteger("Moving", 2);
                Debug.Log("Idle");
                break;
            case 3:
                _anim.SetInteger("Moving", 3);
                Debug.Log("Attack");
                break;
        }
    }

    void Death()
    {
        Debug.Log("Death");
    }

    /// <summary>
    /// 攻撃をする。アニメーションイベントから呼ばれることを想定している。
    /// </summary>
    void Attack()
    {
        // 攻撃範囲に入っているコライダーを取得する
        var cols = Physics.OverlapSphere(GetAttackRangeCenter(), _attackRangeRadius);
        
        // 各コライダーに対して、それが Rigidbody を持っていたら力を加える
        foreach (var c in cols)
        {
            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();

            if (rb)
            {
                rb.AddForce(this.transform.forward * _attackPower, ForceMode.Impulse);
            }
        }
    }
}
