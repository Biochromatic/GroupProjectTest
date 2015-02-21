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

	void Awake()
	{
		leftTurret = this.transform.FindChild("leftTurret");
		rightTurret = this.transform.FindChild("rightTurret");
	}

	// Use this for initialization
	void Start () 
	{
	
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
		if (Input.GetKey(KeyCode.RightArrow))
			this.rigidbody2D.angularVelocity = -rotateSpeed;
		else if (Input.GetKey(KeyCode.LeftArrow))
			this.rigidbody2D.angularVelocity = rotateSpeed;
		else
			this.rigidbody2D.angularVelocity = 0f;
	}

	private void captureForwardInput()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.rigidbody2D.AddForce(this.transform.up.normalized*acceleration*Time.deltaTime);
		}
	}

	private void captureAttackInput()
	{
		if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Space)) && timeUntilFire <= 0f)
		{
			timeUntilFire = shotCooldownTime;
			GameObject leftLaser = Instantiate(laserPrefab, leftTurret.position, this.transform.rotation) as GameObject;
			GameObject rightLaser = Instantiate(laserPrefab, rightTurret.position, this.transform.rotation) as GameObject;
			GameObject.Destroy(leftLaser, waitTimeUntilLaserCleanup);
			GameObject.Destroy(rightLaser, waitTimeUntilLaserCleanup);
		}
	}
}
