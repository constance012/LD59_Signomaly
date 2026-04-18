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

		[Header("Mesh Assets"), Space]
		[SerializeField] private Mesh _normalObjectMesh;
		[SerializeField] private Mesh _anomalyObjectMesh;

		public Material RendererMaterial { get; private set; }
		public VisualState CurrentState { get; private set; } = VisualState.Normal;
		public bool IsInAnomalyState => CurrentState == VisualState.Anomaly;

		private void Awake()
		{
			RendererMaterial = _meshRenderer.material;
		}

		public void SwitchState(VisualState newState)
		{
			CurrentState = newState;
			
			Mesh newMesh = CurrentState switch
			{
				VisualState.Normal => _normalObjectMesh,
				VisualState.Anomaly => _anomalyObjectMesh,
				_ => _normalObjectMesh
			};

			_meshFilter.mesh = newMesh;
		}
		
		public enum VisualState
		{
			Normal,
			Anomaly
		}
	}
}