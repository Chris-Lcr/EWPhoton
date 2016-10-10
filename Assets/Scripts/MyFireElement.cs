﻿using UnityEngine;
using System.Collections;
using CnControls;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class MyFireElement: Photon.MonoBehaviour
{

	public float speed;
	public float bulletSpeed = 70f;
	public float scale = 0.01f;
	public GameObject fireBallPrefab;
	public float timeBetweenShots = 1f;

	private float nextShotTime = 0.0f;
	private Rigidbody2D rb2d;
	private Transform fireBallInstance;

	#region MONOBEHAVIOUR MESSAGES

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		if (photonView.isMine == false && PhotonNetwork.connected == true) {
			return;
		}
//		Vector2 shootVec = new Vector2 (CnInputManager.GetAxis ("Horizontal"), CnInputManager.GetAxis ("Vertical"));
//		if (shootVec != Vector2.zero) {
//			if (nextShotTime <= Time.time) {
//				CmdShoot (shootVec, rb2d.position);
//				nextShotTime = Time.time + timeBetweenShots;
//			}
//		}
	
	}

	void FixedUpdate ()
	{
		if (photonView.isMine == false && PhotonNetwork.connected == true) {
			return;
		}

		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis ("LeftHorizontal"), CrossPlatformInputManager.GetAxis ("LeftVertical")) * speed; 
		rb2d.position += moveVec.normalized * speed * scale;
		Vector2 shootVec = new Vector2 (CnInputManager.GetAxis ("Horizontal"), CnInputManager.GetAxis ("Vertical"));
		if (shootVec.magnitude > 0) {
			float angle = Mathf.Atan2 (shootVec.y, shootVec.x) * Mathf.Rad2Deg - 90f;
			rb2d.rotation = angle;
		}

	}

	#endregion
}
