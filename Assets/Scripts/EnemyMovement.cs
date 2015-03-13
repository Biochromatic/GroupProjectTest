using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour 
{
	public Transform targetLocation;
	public float acceleration = 1f;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		// TODO make the enemy catch up faster if he's far away (so he doesn't spend so long off screen)
		this.GetComponent<Rigidbody2D>().AddForce((targetLocation.position - this.transform.position).normalized*acceleration*Time.deltaTime);
	}
}
