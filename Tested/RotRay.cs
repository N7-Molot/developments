using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  Object pointers according to the angle of the Terrain geometries. Can be used for object appearance manager
/// </summary>
public class RotRay: MonoBehaviour {
	[Tooltip ("Поворот появления"), SerializeField] Quaternion spawnRot;
	[Tooltip ("Кнопка работы"), SerializeField] Button isWork;

	void Start () {
		isWork.onClick.AddListener (CastRay);
	}

	/// <summary>
	/// Changing the rotation of the object according to the normal of the ray point
	/// </summary>
	void CastRay () {
		Ray _newRay = new Ray (transform.position, -transform.up * 100);
		RaycastHit _newRaycastHit;

		if (Physics.Raycast (_newRay, out _newRaycastHit)) {
			if (_newRaycastHit.collider.GetComponent <Terrain> ()) {
				spawnRot = Quaternion.FromToRotation (_newRaycastHit.transform.position + _newRaycastHit.transform.up, _newRaycastHit.normal);
				Debug.Log ($"New object rotation: {spawnRot}");
			}
		}

		transform.rotation = spawnRot;
	}
}