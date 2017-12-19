﻿using UnityEngine;
using System.Collections;

public class UseRay : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject.Destroy(hitInfo.collider.gameObject);
            }
        }
	}
}
