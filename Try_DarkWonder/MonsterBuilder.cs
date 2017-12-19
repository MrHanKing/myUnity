using UnityEngine;
using System.Collections;

public class MonsterBuilder : MonoBehaviour {

    public GameObject monsterPrefab;
    public int maxMonsterNum = 10;
    public float buildCD = 20f;
    private float resetCD;
    public int nowMonsterCount = 0;
	// Use this for initialization
	void Start () {
        resetCD = buildCD;
	}
	
	// Update is called once per frame
	void Update () {
        buildCD--;
        if ((buildCD <= 0) && (nowMonsterCount < maxMonsterNum))
        {
            BuildOneMonster();
            ResetBuildMonsterCD();
        }
	}

    void ResetBuildMonsterCD()
    {
        buildCD = resetCD;
    }

    void BuildOneMonster()
    {
        GameObject go = Instantiate(monsterPrefab, transform.position, Quaternion.identity) as GameObject;
        //方便面板管理了和go实例调用此脚本
        go.transform.parent = transform;
        nowMonsterCount++;
    }
}
