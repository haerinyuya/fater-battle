using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FaterData : MonoBehaviour
{
    public string faterName { get; set; }
    private float Hp = 1f;
    public List<Transform> EnemyFaterData { get; set; } = new();
    private bool Die = false;

    private Transform target;

    public RoomManager roomManager;

    public FaterManager faterManager;

    public RectTransform canvasRect;

    public FaterNameUI faterNameUi;
    public FaterHpUI faterHpUi;
    private FaterHpUI myHpUi;

    private Animator animator;
    private AudioSource audioSource;
    public AudioClip SE, SE2, SE3;

    private float timeOut;
    private float timeElapsed;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        faterNameUi.UiOffset = faterManager.Faters.IndexOf(this);
        faterHpUi.UiOffset = faterManager.Faters.IndexOf(this);
    }

    void Update()
    {
        if (faterManager.IsGame)
        {
            Homing();
        }

        if (Hp <= 0.01f)
        {
            Die = true;
            faterManager.DieManage(this);
            faterManager.Faters.Remove(this);
            animator.SetTrigger("Die");
        }
    }

    /// <summary>
    /// UIの初期化
    /// </summary>
    public void nameSet()
    {
        var newFaterNameUi = Instantiate(faterNameUi, canvasRect);
        newFaterNameUi.target = transform;
        newFaterNameUi.ShowName(faterName);

        var newFaterHpUi = Instantiate(faterHpUi, canvasRect);
        newFaterHpUi.target = transform;
        newFaterHpUi.HpReset();
        myHpUi = newFaterHpUi;
    }

    /// <summary>
    /// 初期設定
    /// </summary>
    public void GameStart()
    {
        foreach(var enemy in faterManager.Faters)
        {
            if (enemy != this)
            {
                EnemyFaterData.Add(enemy.transform);
            }
        }

        var myNum = faterManager.Faters.IndexOf(this) - faterManager.Faters.Count / 2;

        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        var position = transform.position;
        position.y = roomManager.roomCube.transform.position.y - roomManager.roomCube.transform.localScale.y / 2;
        position.x = roomManager.roomCube.transform.position.x + myNum;
        transform.position = position;
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;

        target = EnemyFaterData[Random.Range(0, EnemyFaterData.Count)];
        timeOut = Random.Range(1f, 2f);
    }

    /// <summary>
    /// ターゲット追跡
    /// </summary>
    private void Homing()
    {
        if (Vector3.Distance(transform.position, target.position) < 0.6f)
        {
            Attack();
            return;
        }

        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(target.position.x, target.position.y, target.position.z),1 * Time.deltaTime);
    }

    /// <summary>
    /// ターゲット攻撃の間隔
    /// </summary>
    private void Attack()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
            int attack = Random.Range(0, 4);
            float damage = 0;

            switch (attack)
            {
                case 0:
                    damage = 0f;
                    break;

                case 1:
                    damage = 0.01f;
                    audioSource.PlayOneShot(SE);
                    animator.SetTrigger("Attack1");
                    break;

                case 2:
                    damage = 0.03f;
                    audioSource.PlayOneShot(SE2);
                    animator.SetTrigger("Attack2");
                    break;

                case 3:
                    damage = 0.1f;
                    audioSource.PlayOneShot(SE3);
                    animator.SetTrigger("Attack3");
                    break;

                default:
                    break;
            }

            var targetNum = faterManager.Faters.IndexOf(target.gameObject.GetComponent<FaterData>());

            faterManager.DamageManage(targetNum, damage);
            faterManager.Message(this.faterName, target.gameObject.GetComponent<FaterData>().faterName, attack);

            target = EnemyFaterData[Random.Range(0, EnemyFaterData.Count)];
            timeOut = Random.Range(1f, 2f);

            timeElapsed = 0.0f;
        }
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damage"></param>
    public void Receive(float damage)
    {
        Hp -= damage;
        myHpUi.HpUpdate(Hp);
    }
}
