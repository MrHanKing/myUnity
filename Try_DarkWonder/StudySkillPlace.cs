using UnityEngine;
using System.Collections;

public class StudySkillPlace : MonoBehaviour {

    public float studyTime = 15f;
    private bool isStartStudy = false;
    private bool isEndStudy = false;
    private float resetTime;

    public void Start()
    {
        resetTime = studyTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isStartStudy = true;
            studyTime = resetTime;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (isStartStudy && !isEndStudy)
        {
            studyTime -= Time.deltaTime;
            Message.GetInstance().ShowMessage("学习技能中：" + studyTime.ToString("0.0"));
            if (studyTime <= 0)
            {
                Message.GetInstance().ShowMessage("技能学习成功,按U键释放");
                isEndStudy = true;
                other.gameObject.GetComponent<Player>().GetSkill();
            }
        }

        if (isEndStudy)
        {
            Message.GetInstance().ShowMessage("您已学习技能，按U键释放");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        isStartStudy = false;
        Message.GetInstance().ExitMessage();
    }

}
