using UnityEngine;

namespace UnityUtils.Effects.VisualEffects
{
	public class CanvasComponent : EffectComponent
	{
		[SerializeField]
		private Canvas canvas;

		private void Update()
		{
			transform.LookAt(canvas.worldCamera.transform);
		}

		private void Awake()
		{
			SetupCamera();
		}

		private void SetupCamera()
		{
			if (canvas.renderMode == RenderMode.WorldSpace && !canvas.worldCamera)
				canvas.worldCamera = Camera.main;
		}

		public override T GetValue<T>(int id)
		{
			return default;
		}
		public override void SetValue<T>(int id, T value, bool isOptinal = false) { }

		public override void Play()
		{
			SetupCamera();
		}
		public override void Stop() { }
	}
}
