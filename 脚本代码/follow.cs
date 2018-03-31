using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour {
   
    public Transform m_player;
    Vector3 pos;
	// Use this for initialization
	void Start () {
        pos = this.transform.position - m_player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = m_player.transform.position + pos;
	}
}
