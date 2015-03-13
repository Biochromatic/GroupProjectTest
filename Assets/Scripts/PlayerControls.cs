using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour 
{
	public float rotateSpeed = 500f;
	public float acceleration = 50f;
	public float shotCooldownTime = 0.33f;
	public float waitTimeUntilLaserCleanup = 2f;
	public GameObject laserPrefab;

	private float timeUntilFire = 0f;
	private Transform leftTurret;
	private Transform rightTurret;
	private ParticleSystem thruster;

	void Awake()
	{
		leftTurret = this.transform.FindChild("leftTurret");
		rightTurret = this.transform.FindChild("rightTurret");
	}

	// Use this for initialization
	void Start () 
	{
		thruster = gameObject.GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		captureAngularInput();
		captureForwardInput();
		captureAttackInput();
		if (timeUntilFire > 0f)
			timeUntilFire -= Time.deltaTime;
	}

	private void captureAngularInput()
	{
		this.GetComponent<Rigidbody2D>().angularVelocity = -Input.GetAxis("Horizontal") * rotateSpeed;
	}

	private void captureForwardInput()
	{
		float verticalInput = Mathf.Max (0,Input.GetAxis("Vertical"));
		thruster.enableEmission = verticalInput > 0;
		this.GetComponent<Rigidbody2D>().AddForce(verticalInput*this.transform.up.normalized*acceleration*Time.deltaTime);
	}

	private void captureAttackInput()
	{
		if (Input.GetButton ("Fire1") && timeUntilFire <= 0f)
		{
			timeUntilFire = shotCooldownTime;
			GameObject leftLaser = Instantiate(laserPrefab, leftTurret.position, this.transform.rotation) as GameObject;
			GameObject rightLaser = Instantiate(laserPrefab, rightTurret.position, this.transform.rotation) as GameObject;
			GameObject.Destroy(leftLaser, waitTimeUntilLaserCleanup);
			GameObject.Destroy(rightLaser, waitTimeUntilLaserCleanup);
		}
	}
}
