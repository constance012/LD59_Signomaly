using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public class MoveCamera : MonoBehaviour
	{
		[Header("References"), Space]
		[SerializeField] private Transform cameraPosition;

		private static MoveCamera _instance;
		private static Transform _cameraFollowTarget;
		private Transform _cameraTransform;

		private void Awake()
		{
			MakeSingleton();

			_cameraFollowTarget = cameraPosition;
			_cameraTransform = Camera.main.transform;
		}

		private void LateUpdate()
		{
			_cameraTransform.position = _cameraFollowTarget.position;
		}

		public static void OverrideCameraFollowTarget(Transform newTarget)
		{
			_cameraFollowTarget = newTarget;
			_instance.ResetCameraTransform();
		}

		public static void ResetCameraFollowTarget()
		{
			_cameraFollowTarget = _instance.cameraPosition;
			_instance.ResetCameraTransform();
		}

		private void ResetCameraTransform()
		{
			_cameraTransform.forward = _cameraFollowTarget.forward;
		}

		private void MakeSingleton()
		{
			if (_instance == null)
			{
				_instance = this;
			}
			else
			{
				Destroy(this.gameObject);
			}
		}
	}
}