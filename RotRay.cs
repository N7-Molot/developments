using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  Поворот объекта согласно углу геометрии Terrain. Можно использовать для менеджера появления объектов
/// </summary>
public class RotRay: MonoBehaviour {
	[Tooltip ("Поворот появления"), SerializeField] Quaternion spawnRot;
	[Tooltip ("Кнопка работы"), SerializeField] Button isWork;

	void Start () {
		isWork.onClick.AddListener (CastRay);
	}

	/// <summary>
	/// Изменение поворота объекта согласно нормали точки луча
	/// </summary>
	void CastRay () {
		Ray _newRay = new Ray (transform.position, -transform.up * 100);
		RaycastHit _newRaycastHit;

		if (Physics.Raycast (_newRay, out _newRaycastHit)) {
			if (_newRaycastHit.collider.GetComponent <Terrain> ()) {
				spawnRot = Quaternion.FromToRotation (_newRaycastHit.transform.position + _newRaycastHit.transform.up, _newRaycastHit.normal);
				Debug.Log ($"Угол поворота: {spawnRot}");
			}
		}

		transform.rotation = spawnRot;
	}
}