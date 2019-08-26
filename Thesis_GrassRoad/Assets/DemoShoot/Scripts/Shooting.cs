using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour 
{
	//Public variables
	public GameObject bulletPrefab;
	public ParticleSystem sparkParticle;
	public Transform bodyTransform;
	public float offset = 2;
	public float shootingInterval = 0.2f;
	public float bulletSpeed = 500f;
	public float recoil = 3;
	public float recoverTime = 2;
	public float fov = 60;
	public float zoomInScale = 6;
	public int atk = 1;

	//Private variables
	float intervalTimer = 0;
	float recoverTimer = 0;
	bool isCooling = false;
	bool isRecovering = false;
	bool isFocus = false;
	AudioSource shootingAudio;
	Camera shootingCamera;


	/*-----------------------
	Unity: Start
	-----------------------*/
	void Start () 
	{
		shootingAudio = GetComponent<AudioSource>();
		shootingCamera = GetComponent<Camera>();
	}
	

	/*-----------------------
	Unity: Update
	-----------------------*/
	void Update () 
	{
		//1. Shooting interval timer
		if(isCooling)
		{
			intervalTimer += Time.deltaTime;

			if(intervalTimer >= shootingInterval)
			{
				isCooling = false;
				intervalTimer = 0;
			}
		}

		//2. Recovering timer
		if(isRecovering)
		{
			recoverTimer += Time.deltaTime;
			recover(recoverTimer);

			if(recoverTimer >= recoverTime)
			{
				isRecovering = false;
				recoverTimer = 0;
			}
		}
	}


	/*-----------------------
	Recover from recoil
	-----------------------*/
	void recover(float t)
	{
		//1. Check time
		if(t >= recoverTime)
			t = recoverTime;

		//2. Linear interpolation between the camera rotation & the body rotation
		transform.rotation = Quaternion.Lerp(
			transform.rotation,
			bodyTransform.rotation,
			t / recoverTime
		);
	}


	/*-----------------------
	Fire (shoot a bullet)
	-----------------------*/
	public bool fire()
	{
		//1. Check shooting interval
		if(isCooling) return false;

		//2. Play spark & audio
		sparkParticle.Play();
		shootingAudio.Play();

		//3. Create a bullet
		GameObject obj = Instantiate(
			bulletPrefab,
			transform.position + offset * transform.forward,
			transform.rotation
		);
		obj.GetComponent<Rigidbody>().velocity = bulletSpeed * transform.forward;
		obj.GetComponent<Bullet>().atk = atk;

		//4. Start timer
		isCooling = true;

		//5. Start recovering
		if(shootingCamera != null)
		{
			Vector3 angles = transform.rotation.eulerAngles;
			angles.x -= recoil;
			transform.rotation = Quaternion.Euler(angles);
			isRecovering = true;
			recoverTimer = 0;

			setFocus(false);
		}

		return true;
	}


	/*-----------------------
	Set focus
	-----------------------*/
	public void setFocus(bool f)
	{
		isFocus = f;

		if(isFocus) shootingCamera.fieldOfView = fov / zoomInScale;
		else shootingCamera.fieldOfView = fov;
	}


	/*-----------------------
	Toggle focus
	-----------------------*/
	public void toggleFocus()
	{
		setFocus(!isFocus);
	}
}