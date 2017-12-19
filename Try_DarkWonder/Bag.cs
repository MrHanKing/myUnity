using UnityEngine;
using System.Collections;

public class Bag{

    private static Bag instance;
    private Hashtable bag;

    private Bag()
    {
        bag = new Hashtable();
    }

    public static Bag GetInstance()
    {
        if (instance == null)
        {
            instance = new Bag();
        }
        return instance;
    }

    public void AddRewardToBag(string name, int addNum)
    {
        if (addNum == 0)
        {
            return;
        }
        if (bag.ContainsKey(name))
        {
            bag[name] = (int)bag[name] + addNum;
        }
        else
        {
            bag.Add(name, addNum);
        }
        Message.GetInstance().ShowMessageTime("获得" + name + addNum.ToString() + "个", 1f);
    }

    public int ReduceRewardToBag(string name, int reduceNum)
    {
        if (bag.ContainsKey(name))
        {
            if ((int)bag[name] >= reduceNum)
            {
                bag[name] = (int)bag[name] - reduceNum;
                return 0;
            }
            else
            {
                int lackNum = reduceNum - (int)bag[name];
                bag[name] = 0;
                Message.GetInstance().ShowMessageTime(name.ToString() + "还缺少" + lackNum.ToString(), 2f);
                return lackNum;
            }
        }
        else
        {
            Message.GetInstance().ShowMessageTime("消耗的物品不存在！", 2f);
            return reduceNum;
        }
    }

    public int GetItemNum(string name)
    {
        if (bag.ContainsKey(name))
        {
            return (int)bag[name];
        }
        else
        {
            return 0;
        }
    }
}
