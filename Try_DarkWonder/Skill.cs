using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {

    public float skillStandTime = 5f;
    public float skillhurt = 2f;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, skillStandTime);
	}
	
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Monster")
        {
            other.gameObject.GetComponent<Monster1>().life -= skillhurt * Time.deltaTime;
        }
    }
}
