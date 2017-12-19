using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Message.GetInstance().ShowMessageTime("墙太大了，你得找怪物帮忙！", 2f);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.name == "HelpMan")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject.Destroy(this.gameObject);
                Message.GetInstance().ShowMessageTime("墙拆掉了,游戏胜利，按ESC退出", 2f);
                Game.GetInstance().GameOver();
            }
            else
            {
                Message.GetInstance().ShowMessageTime("怪物按空格键攻击", 2f);
            }
        }
    }

}
