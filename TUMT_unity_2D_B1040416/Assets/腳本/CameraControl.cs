using UnityEngine;  //跟蹤攝影機

public class CameraControl : MonoBehaviour
{
    [Header("速度"), Range(0, 10)]
    public float speed = 3;

    private Transform target;

    private void Start() 
    {
        target = GameObject.Find("主角").transform;
    }

    //延遲更新
    private void LateUpdate() 
    {
        Vector3 cam = transform.position;
        Vector3 tar = target.position;
        tar.z = -10;
        transform.position = Vector3.Lerp(cam, tar, 0.3f * Time.deltaTime * speed);
    }
}
