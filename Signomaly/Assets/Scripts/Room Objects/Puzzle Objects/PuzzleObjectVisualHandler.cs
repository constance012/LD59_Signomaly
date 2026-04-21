using System;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public class PuzzleObjectVisualHandler : MonoBehaviour
	{
		[Header("References"), Space]
		[SerializeField] private Transform _objectVisualTransform;
		[SerializeField] private MeshFilter _meshFilter;
		[SerializeField] private MeshRenderer _meshRenderer;

		[Header("Visual State Data"), Space]
		[SerializeField] private VisualStateData _normalStateData;
		[SerializeField] private VisualStateData _anomalyStateData;

		public Material[] RendererMaterials { get; private set; }
		public VisualState CurrentState { get; private set; } = VisualState.Normal;
		public bool IsInAnomalyState => CurrentState == VisualState.Anomaly;

		private Vector3 _originalVisualLocalPosition;

		private void Awake()
		{
			RendererMaterials = _meshRenderer.materials;
			_originalVisualLocalPosition = _objectVisualTransform.localPosition;
		}

		public void SwitchState(VisualState newState)
		{
			CurrentState = newState;
			
			var newStateData = CurrentState switch
			{
				VisualState.Normal => _normalStateData,
				VisualState.Anomaly => _anomalyStateData,
				_ => _normalStateData
			};

			UpdateVisual(newStateData);

		}

		private void UpdateVisual(VisualStateData stateData)
		{
			_meshFilter.mesh = stateData.Mesh;
			_objectVisualTransform.localEulerAngles = stateData.LocalRotation;
			_objectVisualTransform.localScale = stateData.LocalScale;

			if (stateData.LocalPositionOffset != Vector3.zero)
			{
				_objectVisualTransform.localPosition += stateData.LocalPositionOffset;
			}
			else
			{
				_objectVisualTransform.localPosition = _originalVisualLocalPosition;
			}

			if (stateData.OverrideMaterials != null && stateData.OverrideMaterials.Length > 0)
			{
				_meshRenderer.materials = stateData.OverrideMaterials;
			}
			else
			{
				_meshRenderer.materials = RendererMaterials;
			}
		}
		
		public enum VisualState
		{
			Normal,
			Anomaly
		}

		[Serializable]
		public class VisualStateData
		{
			public Mesh Mesh;
			public Vector3 LocalPositionOffset;
			public Vector3 LocalRotation;
			public Vector3 LocalScale = Vector3.one;
			public Material[] OverrideMaterials;
		}
	}
}