using UnityEngine;

namespace Es.InkPainter.Sample
{
	public class MousePainter : MonoBehaviour
	{
		public Camera cam;
		public float timer;
		private bool started = false;
		private float holdDuration;
		private float startTime;
		/// <summary>
		/// Types of methods used to paint.
		/// </summary>
		[System.Serializable]
		private enum UseMethodType
		{
			RaycastHitInfo,
			WorldPoint,
			NearestSurfacePoint,
			DirectUV,
		}

		[SerializeField]
		private Brush brush;

		[SerializeField]
		private UseMethodType useMethodType = UseMethodType.RaycastHitInfo;

		[SerializeField]
		bool erase = false;

		private void Update()
		{
			if (started)
			{
				holdDuration = Time.time - startTime;
				if (holdDuration > timer + 0.5f)
				{
					holdDuration = 0;
				}
			}
			if (Input.GetMouseButton(0))
			{
				if (!started)
				{
					startTime = Time.time;
					started = true;
					Debug.Log("Starting timer");
				}
				var ray = cam.ScreenPointToRay(Input.mousePosition);
				bool success = true;
				RaycastHit hitInfo;
				if (Physics.Raycast(ray, out hitInfo))
				{
					var paintObject = hitInfo.transform.GetComponent<InkCanvas>();

					if (paintObject != null)
					{
						//Debug.Log(paintObject.gameObject.tag);
						if (paintObject.gameObject.tag == "Gum")
						{
							//Debug.Log("Hitting gum");
							brush.Scale = 0.011f;
							brush.Color = Color.yellow;
						}
						else
						{
							brush.Scale = 0.01f;
							brush.Color = Color.white;
						}
						switch (useMethodType)
						{
							case UseMethodType.RaycastHitInfo:
								if (holdDuration > timer)
								{
									success = erase ? paintObject.Erase(brush, hitInfo) : paintObject.Paint(brush, hitInfo);
									//Debug.Log("Painting " + success);
									holdDuration = 0;
								}
								break;

							case UseMethodType.WorldPoint:
								success = erase ? paintObject.Erase(brush, hitInfo.point) : paintObject.Paint(brush, hitInfo.point);
								break;

							case UseMethodType.NearestSurfacePoint:
								success = erase ? paintObject.EraseNearestTriangleSurface(brush, hitInfo.point) : paintObject.PaintNearestTriangleSurface(brush, hitInfo.point);
								break;

							case UseMethodType.DirectUV:
								if (!(hitInfo.collider is MeshCollider))
									Debug.LogWarning("Raycast may be unexpected if you do not use MeshCollider.");
								success = erase ? paintObject.EraseUVDirect(brush, hitInfo.textureCoord) : paintObject.PaintUVDirect(brush, hitInfo.textureCoord);
								break;
						}
						if (!success)
							Debug.LogError("Failed to paint.");
					}
				}
			}
			if (Input.GetMouseButtonUp(0))
			{
				//particles.GetComponent<ParticleSystemRenderer>().enabled = false;
				started = false;
				holdDuration = 0;
			}
		}

		public void OnGUI()
		{
			if (GUILayout.Button("Reset"))
			{
				foreach (var canvas in FindObjectsOfType<InkCanvas>())
					canvas.ResetPaint();
			}
		}
	}
}