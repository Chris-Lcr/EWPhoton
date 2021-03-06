﻿using UnityEngine;
using UnityEngine.UI;


using System.Collections;


namespace Com.EW.MyGame
{
	public class PlayerUI : MonoBehaviour
	{


		#region Public Properties


		[Tooltip ("UI Text to display Player's Name")]
		public Text PlayerNameText;


		[Tooltip ("UI Slider to display Player's Health")]
		public Slider PlayerHealthSlider;

		[Tooltip ("Pixel offset from the player target")]
		public Vector3 ScreenOffset = new Vector3 (0f, 30f, 0f);


		#endregion


		#region Private Properties

		PlayerManager _target;

		float _characterControllerHeight = 0f;
		Transform _targetTransform;
		Vector3 _targetPosition;

		#endregion


		#region MonoBehaviour Messages

		void Update ()
		{
			// Reflect the Player Health
			if (PlayerHealthSlider != null) {
				PlayerHealthSlider.value = _target.getHealthPercentage ();
			}

			// Destroy itself if the target is null, It's a fail safe when Photon is destroying Instances of a Player over the network
			if (_target == null) {
				Destroy (this.gameObject);
				return;
			}
		}

		void Awake ()
		{
			this.GetComponent<Transform> ().SetParent (GameObject.Find ("Canvas").GetComponent<Transform> ());
		}

		#endregion


		#region Public Methods

		public void SetTarget (PlayerManager target)
		{
			if (target == null) {
				Debug.LogError ("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
				return;
			}
			// Cache references for efficiency
			_target = target;

			CharacterController _characterController = _target.GetComponent<CharacterController> ();
			// Get data from the Player that won't change during the lifetime of this Component
			if (_characterController != null) {
				_characterControllerHeight = _characterController.height;
			}

			if (PlayerNameText != null) {
				PlayerNameText.text = _target.photonView.owner.name;
			}
		}

		void LateUpdate ()
		{


			this.transform.position = new Vector3 (2f, 2f, 0f);

			// #Critical
			// Follow the Target GameObject on screen.
//			if (_targetTransform!=null)
//			{
//				_targetPosition = _targetTransform.position;
//				_targetPosition.y += _characterControllerHeight;
//				this.transform.position = Camera.main.WorldToScreenPoint (_targetPosition) + ScreenOffset;
//				this.transform.position = _targetPosition + ScreenOffset;
//
//			}
		}

		#endregion


	}
}