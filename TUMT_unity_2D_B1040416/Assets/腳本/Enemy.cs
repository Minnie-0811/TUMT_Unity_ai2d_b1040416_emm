using UnityEngine;

public class Enemy : MonoBehaviour 
{
    [Header("移動速度"), Range(0, 100)]
    public float speed =1.5f;
    [Header("傷害"), Range(0, 100)]
    public float damage = 20;

    public Transform checkPoint;  //檢查地板

    private Rigidbody2D r2D;  //鋼體

    private void Start() 
    {
        r2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {
        Move();
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow;  //圖示 顏色
        Gizmos.DrawRay(checkPoint.position, -checkPoint.up * 2f);  //圖示 繪製射線
    }

    //持續觸發
    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.name == "主角") 
        {
            Track(collision.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.name == "主角") 
        {
            collision.gameObject.GetComponent<Fox>().Damage(damage);
        }
    }

    private void Move()  //一般移動
    {
        //r2D.AddForce(new Vector2(-speed, 0));//世界座標
        r2D.AddForce(-transform.right * speed);

        //物理 設限碰撞
        RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, -checkPoint.up, 2f, 1 << 8);

        if (hit == false) 
        {
            transform.eulerAngles += new Vector3(0, 180, 0);
        }
    }

    private void Track(Vector3 target)   //追蹤
    {
        //左邊0
        //右邊180
        if (target.x < transform.position.x)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else 
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
