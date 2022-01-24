using System.Collections.Generic;
using UnityEngine;
using FMB;

namespace GuildCheramor.System {
	/// <summary>
	/// Launching a projectile into the target position. The projectile transfers damage when it reaches the target zone. The old script.
	/// </summary>
	[AddComponentMenu ("Guild Cheramor/System/Shell", order: 309)]
	public class System_Shell: MonoBehaviour {
		/// <summary>
		/// Warrior - as the owner of the projectile
		/// </summary>
		[HideInInspector] public System_Unit unit;
		/// <summary>
		/// Target - warrior
		/// </summary>
		[HideInInspector] public System_Unit target;
		/// <summary>
		/// Base - as the owner of the projectile
		/// </summary>
		[HideInInspector] public System_Base_AttackPoint attackPoint;

		[Tooltip ("Rigidbody of the shell"), SerializeField] Rigidbody thisRigidbody;
		[Tooltip ("Shell light"), SerializeField] Light thisLight;
		[Tooltip ("Explosion"), SerializeField] ParticleSystem explosion;
		[Tooltip ("Shell velocity (1 = 1000 units)"), SerializeField] float speed;
		[Tooltip ("Zone-target of the shell"), SerializeField] Collider targetZone;
		[Tooltip ("Destroy when hit"), SerializeField] bool hitDestroy;
		/// <summary>
		/// Time to zone-target
		/// </summary>
		float timeToTaraget;

		[Space, Header ("Sound")]
		[Tooltip ("Audio player object"), SerializeField] GameObject audioPlayer;
		[Tooltip ("List of audio clips"), SerializeField] List <AudioClip> audioClips;
		RandomNextValue randomNextValue = new RandomNextValue ();

		/// <summary>
		/// Launching the shell to the target
		/// </summary>
		/// <param name="tartgetPosition">
		/// Target position
		/// </param>
		public void StartShell (Vector3 tartgetPosition) {
			if (Constants.mobilePlatform && thisLight != null) thisLight.shadows = LightShadows.None;
			transform.LookAt (tartgetPosition);

			targetZone.transform.position = tartgetPosition;
			targetZone.enabled = true;

			thisRigidbody.AddForce (transform.forward * speed * 1000);

			//time to target
			timeToTaraget = Vector3.Distance (transform.position, tartgetPosition) / (speed * 20);
			Destroy (transform.parent.gameObject, timeToTaraget + 5);
			//play audio
			int _curNumberClip = randomNextValue.NextRandom (0, audioClips.Count);
			GameObject _newAudioPlayer = Instantiate (audioPlayer);
			_newAudioPlayer.transform.position = tartgetPosition;
			Destroy (_newAudioPlayer, 3);

			System_AudioPlayer _curAudioPlayer = _newAudioPlayer.GetComponent<System_AudioPlayer> ();
			_curAudioPlayer.volume = .25f;
			_curAudioPlayer.AudioPlay (audioClips [_curNumberClip]);
		}

		void OnTriggerEnter (Collider thisBall) {
			if (thisBall.transform == targetZone.transform) {
				thisRigidbody.velocity = Vector3.zero;
				if (thisLight != null) thisLight.enabled = false;

				if (unit != null && unit.aITarget.targetTransform != null && target == unit.aITarget.target) unit.Damage ();
				if (attackPoint != null && attackPoint.targetTransform != null && target == attackPoint.target) attackPoint.Damage ();

				if (hitDestroy) Destroy (gameObject, .3f);
				else Destroy (transform.parent.gameObject, 5);

				if (explosion != null) explosion.Play ();
			}
		}
	}
}
