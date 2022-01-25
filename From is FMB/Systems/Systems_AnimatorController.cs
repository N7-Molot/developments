using UnityEngine;
using System.Collections.Generic;
using GPUInstancer;
using GPUInstancer.CrowdAnimations;

namespace GuildCheramor.System {
	/// <summary>
	/// Manager for setting animation parameters for Mecanim and GPUI Animator.
	/// </summary>
	[AddComponentMenu ("Guild Cheramor/System/Animation Controller", order: 307)]
	public class System_Animation_Controller: MonoBehaviour {
		[Tooltip ("Mecanim Animator")] public Animator animator;
		[Tooltip ("GPUI Prefab Manager")] public GPUInstancerPrefab instancerPrefabGPUI;
		/// <summary>
		/// GPUI Events
		/// </summary>
		[HideInInspector] public System_GPUI_Events eventsGPUI;

		//Parameters
		/// <summary>
		/// Warrior Behavior
		/// </summary>
		public AnimationBehaviour animationBehaviour { get; private set; }
		public delegate void ChangeAnimationBehaviour (AnimationBehaviour behaviour);
		public event ChangeAnimationBehaviour OnChangeAnimationBehaviour_Event;
		/// <summary>
		/// Set a new animation behavior
		/// </summary>
		/// <param name="newBehaviour">
		/// New behavior
		/// </param>
		public void SetAnimationBehaviour (AnimationBehaviour newBehaviour) {
			if (animator != null) animator.SetBool (animationBehaviour.ToString (), false);
			if (eventsGPUI != null) eventsGPUI.SetAnimation (instancerPrefabGPUI.prefabPrototype.prefabObject, instancerPrefabGPUI.gpuInstancerID, animationPreferences [0].indexClipGPUI);

			animationBehaviour = newBehaviour;
			OnChangeAnimationBehaviour_Event?.Invoke (animationBehaviour);

			if (animator != null) animator.SetBool (animationBehaviour.ToString (), true);
			if (eventsGPUI != null) eventsGPUI.SetAnimation (instancerPrefabGPUI.prefabPrototype.prefabObject, instancerPrefabGPUI.gpuInstancerID, animationPreferences [(int) animationBehaviour].indexClipGPUI);
		}

		[HideInInspector] public WeaponType weaponType;
		/// <summary>
		/// Warrior is dead
		/// </summary>
		public bool isDead { get; private set; }

		[Space, Tooltip ("List of animation settings")] public List <System_Animation_Preference> animationPreferences;
		[HideInInspector] public System_Animation_Preference attackPreference;

		void Awake () {
			for (int _ap = 0; _ap < animationPreferences.Count; _ap++) {
				if (animationPreferences [_ap].animationBehaviour == AnimationBehaviour.Attack) attackPreference = animationPreferences [_ap];
			}
		}
	}

	/// <summary>
	/// Animated behavior
	/// </summary>
	public enum AnimationBehaviour {
		Idle = 0,
		Run = 1,
		Attack = 2,
		Dead = 3
	}
}
