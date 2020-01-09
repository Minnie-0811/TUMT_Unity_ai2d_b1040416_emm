using UnityEngine;
using UnityEngine.Events;   //引用 事件 api
using UnityEngine.UI;   //引用 介面

public class Fox : MonoBehaviour 
{
    [Header("血量"), Range(0, 200)]
    public float hp = 100;

    public Image hpBar;
    public GameObject final;

    private float hpMax;

    //成員
    //修飾詞
    public int speed = 50;  //整數
    public float jump = 2.5f;  //浮點數
    public string foxName = "主角";  //字串
    public bool pass = false;  //布林值
    public bool isGround;

    public UnityEvent onEat;

    private Rigidbody2D r2d;

    //事件
    //開始事件
    private void Start()
    {
        //泛型
        r2d = GetComponent<Rigidbody2D>();
        AudioSource aud = GetComponent<AudioSource>();
        Animator ani = GetComponent<Animator>();
        hpMax = hp;
    }

    //更新事件
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) TurnRight();
        if (Input.GetKeyDown(KeyCode.A)) TurnLeft();
    }

    //固定更新
    private void FixedUpdate()
    {
        Walk(); //呼叫方式
        Jump();
    }

    //碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("碰到東西了" + collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "墓碑")
        {
            Destroy(collision.gameObject);
            onEat.Invoke();
        }
    }

    //走路
    private void Walk()
    {
        r2d.AddForce(new Vector2(speed * Input.GetAxis("Horizontal"), 0));
    }

    //跳
    private void Jump() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
            r2d.AddForce(new Vector2(0, jump));
    }

    //右轉
    private void TurnRight()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    //左轉
    private void TurnLeft()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public void Damage(float damage)
    {
        hp -= damage;
        hpBar.fillAmount = hp / hpMax;

        if (hp <= 0) final.SetActive(true);
    }
}
