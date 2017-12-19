using UnityEngine;
using System.Collections;

public class Monster1 : MonoBehaviour {

    public float moveSpeed = 5f;
    public float maxRotationAngle = 50f;
    public float changeStateCD = 15f;
    public float life = 5f;
    private float timer;
    private CharacterController controll;
    // 状态，0是原地等待，1是移动
    private int state = 0;
    private int statesNum = 2;
    // angle是移动前的要旋转角度，perAngle为angle/20f 每帧处理角度
    private float angle;
    private float perAngle;
    // 敌人生成器
    private MonsterBuilder monsterBuilder;
	// Use this for initialization
	void Start () {
        this.tag = "Monster";
        monsterBuilder = this.transform.parent.gameObject.GetComponent<MonsterBuilder>();
        controll = GetComponent<CharacterController>();
        ResetTimer();
	}
	
	// Update is called once per frame
	void Update () {
        if (life <= 0)
        {
            this.DestroyMy();
            return;
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            state = Random.Range(0, statesNum);
            SetStateAttribute(state);
            ResetTimer();
        }
        StartState(state);
	}

    void ResetTimer()
    {
        timer = changeStateCD;
    }

    void SetStateAttribute(int state){
        if (state == 1)
        {
            angle = Random.Range(-maxRotationAngle, maxRotationAngle);
            perAngle = angle / 20f;
        }
    }

    void StartState(int state)
    {
        switch (state)
        {
            case 0:
                break;
            case 1:
                ThisGameObjectToWalk();
                break;
            default:
                print("undefined Monster States");
                break;
        }
    }

    void ThisGameObjectToWalk()
    {
        if (Mathf.Abs(angle) > 0.2f)
        {
            angle -= perAngle;
            transform.Rotate(new Vector3(0, perAngle, 0));
        }
        else
        {
            controll.SimpleMove(transform.TransformDirection(Vector3.forward) * moveSpeed);
        }
    }

    void DestroyMy()
    {
        monsterBuilder.nowMonsterCount--;
        FallRewardGroup();
        //播放死亡动作,暂时没素材,不加
        Destroy(this.gameObject);
    }

    public void FallRewardGroup()
    {
        Bag.GetInstance().AddRewardToBag("meat", (int)Random.Range(0, 4));
    }
}
