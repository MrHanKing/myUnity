using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed = 5f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public float rotationSpeed = 1;
    private Vector3 moveDirection = Vector3.zero;
    private bool hasSkill = false;
    public float skillCD = 1f;
    private float saveSkillCD;
    public GameObject skillPrefab;
    //控制主角中吗？
    public bool controlling = true;
    // 主角的摄像机
    public Camera playerCamera;

	// Use this for initialization
	void Start () {
        saveSkillCD = skillCD;
        skillCD = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (controlling)
        {
            ControlToMove();
            UseSkill();
        }
	}

    public void SetControllToTrue()
    {
        controlling = true;
        playerCamera.gameObject.SetActive(true);
    }

    public void SetControllToFalse()
    {
        controlling = false;
        playerCamera.gameObject.SetActive(false);
    }


    private void UseSkill(){
        if (skillCD > 0)
        {
            skillCD -= Time.deltaTime;
        }
        else if (hasSkill && Input.GetKeyDown(KeyCode.U))
        {
            //生成技能prefab
            Instantiate(skillPrefab, this.transform.position, skillPrefab.transform.rotation);
            //重置技能cd
            resetSkillCD();
        }
    }

    private void ControlToMove()
    {
        CharacterController player = GetComponent<CharacterController>();
        if (Input.GetAxis("Fire2") == 1)
        {
            float rotay = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotay, 0);
        }
        if (player.isGrounded)
        {
            Vector3 movex = Input.GetAxis("Horizontal") * speed * Time.deltaTime * transform.TransformDirection(Vector3.right);
            Vector3 movez = Input.GetAxis("Vertical") * speed * Time.deltaTime * transform.TransformDirection(Vector3.forward);
            Vector3 movey = Input.GetAxis("Jump") * jumpSpeed * transform.TransformDirection(Vector3.up);
            moveDirection = movex + movey + movez;
            player.Move(moveDirection);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        player.Move(moveDirection);
    }

    public void GetSkill()
    {
        hasSkill = true;
    }

    public void resetSkillCD()
    {
        skillCD = saveSkillCD;
    }
}
