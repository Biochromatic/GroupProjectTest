using UnityEngine;
using System.Collections;

public class LaserEffects : MonoBehaviour {

	public float absoluteMoveSpeed;

	// Use this for initialization
	void Start () {
		this.rigidbody2D.velocity = this.transform.up.normalized*absoluteMoveSpeed;
	}
	
	// Update is called once per frame
	void Update () {}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy")
		{
			Debug.Log("hit enemy!");
		}
	}
}
