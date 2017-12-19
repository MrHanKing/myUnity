using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuiShowMeat : MonoBehaviour {

    public Text meatText;

    public void OnGUI()
    {
        int meatNum = Bag.GetInstance().GetItemNum("meat");
        if (meatNum > 0)
        {
            meatText.text = "肉：" + meatNum;
        }
        else
        {
            meatText.text = "肉：0";
        }
    }
}
