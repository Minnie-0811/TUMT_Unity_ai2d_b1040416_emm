using UnityEngine;
using UnityEngine.UI; //引用 介面 API
using System.Collections;


public class NPC : MonoBehaviour 
{
    //欄位
    //定義列舉
    //修飾詞
    public enum state 
    {
        //一般
        normal,notComplete,complete
    }
    //列舉
    //修飾詞 類型 名稱
    public state _state;

    [Header("對話")]
    public string sayStart = "偉大的碼農啊，你想成為傳說中的碼農嗎，那就去後面尋找其他碼濃的墓...心得吧";
    public string sayNotComplete = "碼農呀~碼農，你還沒收集到10個碼農的心得啊，快去吧，別浪費時間";
    public string sayComplete = "恭喜啊，這位碼農，你成功獲得的碼農們的肝，部過想成為傳說中的碼農呢，還是不夠的，還需要的是無限的肝，加油吧~";
    [Header("對話速度")]
    public float speed = 1.5f;
    [Header("任務相關")]
    public bool complete;
    public int countPlayer;
    public int countFinish = 10;
    [Header("介面")]
    public GameObject objCanvas;
    public Text textSay;


    //2D 觸發事件
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        //碰到物件為主角
        if (collision.name == "主角")
            Say();
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.name == "主角")
            SayClose();
    }

    //對話
    private void Say() 
    {
        //畫布.顯示
        objCanvas.SetActive(true);
        StopAllCoroutines();

        if (countPlayer >= countFinish) _state = state.complete;

        //判斷式(狀態)
        switch (_state)
        {
            case state.normal:
                StartCoroutine(ShowDialog(sayStart));  //開始
                _state = state.notComplete;
                break;
            case state.notComplete:
                StartCoroutine(ShowDialog(sayNotComplete));   //未完成
                break;
            case state.complete:
                StartCoroutine(ShowDialog(sayComplete));   //完成
                break;
                
        }
    }

    private IEnumerator ShowDialog(string say) 
    {
        textSay.text = "";  //清空文字

        for (int i = 0; i < say.Length; i++)   //迴圈跑對話 長度
        {
            textSay.text += say[i].ToString();  //累加
            yield return new WaitForSeconds(speed);  //等待
        }
    }

    //關閉對話
    private void SayClose()
    {
        StopAllCoroutines();
        objCanvas.SetActive(false);
    }

    //取得墓碑
    public void PlayGet() 
    {
        countPlayer++;
    }
}



