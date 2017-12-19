using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelperMan : MonoBehaviour {
    
    // 属性
    public float speed = 5f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public float rotationSpeed = 1;
    private Vector3 moveDirection = Vector3.zero;
    // 可以被控制
    private bool isControll = false;
    // 控制中
    [System.NonSerialized]
    public bool controlling = false;
    public Camera helperManCamera;
    private Player player;
    private int needMeat = 10;
    public GameObject ui;

    public void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

	// Use this for initialization
	void Start () {
        ui.SetActive(false);
        helperManCamera.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (isControll && Input.GetKeyDown(KeyCode.Q))
        {
            if (controlling)
            {
                this.SetControllToFalse();
                player.SetControllToTrue();
            }
            else
            {
                this.SetControllToTrue();
                player.SetControllToFalse();
            }
        }

        if (controlling)
        {
            ControlToMove();
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

    public void SetControllToTrue()
    {
        controlling = true;
        helperManCamera.gameObject.SetActive(true);
    }

    public void SetControllToFalse()
    {
        controlling = false;
        helperManCamera.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && !isControll)
        {
            ui.SetActive(true);
        }

        if (other.name == "Player" && player.controlling && isControll)
        {
            Message.GetInstance().ShowMessageTime("按Q键切换控制角色", 2f);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        ClickButtonNo();
    }

    public void ClickButtonYes()
    {
        ClickButtonNo();
        needMeat = Bag.GetInstance().ReduceRewardToBag("meat", needMeat);
        if (needMeat <= 0)
        {
            isControll = true;
            Message.GetInstance().ShowMessageTime("按Q键切换控制角色", 2f);
        }
    }

    public void ClickButtonNo()
    {
        ui.SetActive(false);
    }
}
