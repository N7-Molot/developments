using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  ������� ������� �������� ���� ��������� Terrain. ����� ������������ ��� ��������� ��������� ��������
/// </summary>
public class RotRay: MonoBehaviour {
	[Tooltip ("������� ���������"), SerializeField] Quaternion spawnRot;
	[Tooltip ("������ ������"), SerializeField] Button isWork;

	void Start () {
		isWork.onClick.AddListener (CastRay);
	}

	/// <summary>
	/// ��������� �������� ������� �������� ������� ����� ����
	/// </summary>
	void CastRay () {
		Ray _newRay = new Ray (transform.position, -transform.up * 100);
		RaycastHit _newRaycastHit;

		if (Physics.Raycast (_newRay, out _newRaycastHit)) {
			if (_newRaycastHit.collider.GetComponent <Terrain> ()) {
				spawnRot = Quaternion.FromToRotation (_newRaycastHit.transform.position + _newRaycastHit.transform.up, _newRaycastHit.normal);
				Debug.Log ($"���� ��������: {spawnRot}");
			}
		}

		transform.rotation = spawnRot;
	}
}