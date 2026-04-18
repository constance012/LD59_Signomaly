using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public sealed class WorldUIBillboard : MonoBehaviour
	{
		private Camera _camera;
		private Transform _objectTransform;

		private void Awake()
		{
			_camera = Camera.main;
			_objectTransform = transform;
		}
		
		private void LateUpdate()
		{
			_objectTransform.forward = _camera.transform.forward;
		}
	}
}