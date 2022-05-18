using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012BB RID: 4795
	public class CameraManager : MonoBehaviour
	{
		// Token: 0x0600741A RID: 29722 RVA: 0x002AD10C File Offset: 0x002AB30C
		protected virtual void OnGUI()
		{
			if (this.swipePanActive && this.swipePanIcon)
			{
				float num = (float)Screen.width * this.swipeIconPosition.x;
				float num2 = (float)Screen.height * this.swipeIconPosition.y;
				float num3 = (float)this.swipePanIcon.width;
				float num4 = (float)this.swipePanIcon.height;
				num = Mathf.Max(num, 0f);
				num2 = Mathf.Max(num2, 0f);
				num = Mathf.Min(num, (float)Screen.width - num3);
				num2 = Mathf.Min(num2, (float)Screen.height - num4);
				GUI.DrawTexture(new Rect(num, num2, num3, num4), this.swipePanIcon);
			}
			if (this.fadeAlpha > 0f && this.screenFadeTexture != null)
			{
				GUI.color = new Color(1f, 1f, 1f, this.fadeAlpha);
				GUI.depth = -1000;
				GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.screenFadeTexture);
			}
		}

		// Token: 0x0600741B RID: 29723 RVA: 0x0004F467 File Offset: 0x0004D667
		protected virtual IEnumerator FadeInternal(float targetAlpha, float fadeDuration, Action fadeAction)
		{
			float startAlpha = this.fadeAlpha;
			float timer = 0f;
			if (Mathf.Approximately(startAlpha, targetAlpha))
			{
				yield return null;
			}
			else
			{
				while (timer < fadeDuration)
				{
					float num = timer / fadeDuration;
					timer += Time.deltaTime;
					num = Mathf.Clamp01(num);
					this.fadeAlpha = Mathf.Lerp(startAlpha, targetAlpha, num);
					yield return null;
				}
			}
			this.fadeAlpha = targetAlpha;
			if (fadeAction != null)
			{
				fadeAction();
			}
			yield break;
		}

		// Token: 0x0600741C RID: 29724 RVA: 0x0004F48B File Offset: 0x0004D68B
		protected virtual IEnumerator PanInternal(Camera camera, Vector3 targetPos, Quaternion targetRot, float targetSize, float duration, Action arriveAction)
		{
			if (camera == null)
			{
				Debug.LogWarning("Camera is null");
				yield break;
			}
			float timer = 0f;
			float startSize = camera.orthographicSize;
			Vector3 startPos = camera.transform.position;
			Quaternion startRot = camera.transform.rotation;
			bool arrived = false;
			while (!arrived)
			{
				timer += Time.deltaTime;
				if (timer > duration)
				{
					arrived = true;
					timer = duration;
				}
				float num = 1f;
				if (duration > 0f)
				{
					num = timer / duration;
				}
				if (camera != null)
				{
					camera.orthographicSize = Mathf.Lerp(startSize, targetSize, Mathf.SmoothStep(0f, 1f, num));
					camera.transform.position = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0f, 1f, num));
					camera.transform.rotation = Quaternion.Lerp(startRot, targetRot, Mathf.SmoothStep(0f, 1f, num));
					this.SetCameraZ(camera);
				}
				if (arrived && arriveAction != null)
				{
					arriveAction();
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600741D RID: 29725 RVA: 0x0004F4C7 File Offset: 0x0004D6C7
		protected virtual IEnumerator PanToPathInternal(Camera camera, float duration, Action arriveAction, Vector3[] path)
		{
			if (camera == null)
			{
				Debug.LogWarning("Camera is null");
				yield break;
			}
			float timer = 0f;
			while (timer < duration)
			{
				timer += Time.deltaTime;
				timer = Mathf.Min(timer, duration);
				float percent = timer / duration;
				Vector3 vector = iTween.PointOnPath(path, percent);
				camera.transform.position = new Vector3(vector.x, vector.y, 0f);
				camera.orthographicSize = vector.z;
				this.SetCameraZ(camera);
				yield return null;
			}
			if (arriveAction != null)
			{
				arriveAction();
			}
			yield break;
		}

		// Token: 0x0600741E RID: 29726 RVA: 0x002AD228 File Offset: 0x002AB428
		protected virtual void SetCameraZ(Camera camera)
		{
			if (!this.setCameraZ)
			{
				return;
			}
			if (camera == null)
			{
				Debug.LogWarning("Camera is null");
				return;
			}
			camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, this.cameraZ);
		}

		// Token: 0x0600741F RID: 29727 RVA: 0x002AD288 File Offset: 0x002AB488
		protected virtual void Update()
		{
			if (!this.swipePanActive)
			{
				return;
			}
			if (this.swipeCamera == null)
			{
				Debug.LogWarning("Camera is null");
				return;
			}
			Vector3 vector = Vector3.zero;
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == 1)
			{
				vector = Input.GetTouch(0).deltaPosition;
			}
			if (Input.GetMouseButtonDown(0))
			{
				this.previousMousePos = Input.mousePosition;
			}
			else if (Input.GetMouseButton(0))
			{
				vector = Input.mousePosition - this.previousMousePos;
				this.previousMousePos = Input.mousePosition;
			}
			Vector3 vector2 = this.swipeCamera.ScreenToViewportPoint(vector);
			vector2.x *= -2f * this.swipeSpeedMultiplier;
			vector2.y *= -2f * this.swipeSpeedMultiplier;
			vector2.z = 0f;
			Vector3 vector3 = this.swipeCamera.transform.position;
			vector3 += vector2;
			this.swipeCamera.transform.position = this.CalcCameraPosition(vector3, this.swipePanViewA, this.swipePanViewB);
			this.swipeCamera.orthographicSize = this.CalcCameraSize(vector3, this.swipePanViewA, this.swipePanViewB);
		}

		// Token: 0x06007420 RID: 29728 RVA: 0x002AD3C4 File Offset: 0x002AB5C4
		protected virtual Vector3 CalcCameraPosition(Vector3 pos, View viewA, View viewB)
		{
			Vector3 vector = pos;
			vector.x = Mathf.Max(vector.x, Mathf.Min(viewA.transform.position.x, viewB.transform.position.x));
			vector.x = Mathf.Min(vector.x, Mathf.Max(viewA.transform.position.x, viewB.transform.position.x));
			vector.y = Mathf.Max(vector.y, Mathf.Min(viewA.transform.position.y, viewB.transform.position.y));
			vector.y = Mathf.Min(vector.y, Mathf.Max(viewA.transform.position.y, viewB.transform.position.y));
			return vector;
		}

		// Token: 0x06007421 RID: 29729 RVA: 0x002AD4B0 File Offset: 0x002AB6B0
		protected virtual float CalcCameraSize(Vector3 pos, View viewA, View viewB)
		{
			Vector3 vector = viewB.transform.position - viewA.transform.position;
			Vector3 vector2 = pos - viewA.transform.position;
			float magnitude = vector.magnitude;
			vector /= magnitude;
			vector2 /= magnitude;
			float num = Vector3.Dot(vector, vector2);
			num = Mathf.Clamp01(num);
			return Mathf.Lerp(viewA.ViewSize, viewB.ViewSize, num);
		}

		// Token: 0x06007422 RID: 29730 RVA: 0x002AD524 File Offset: 0x002AB724
		public virtual void PanToPath(Camera camera, View[] viewList, float duration, Action arriveAction)
		{
			if (camera == null)
			{
				Debug.LogWarning("Camera is null");
				return;
			}
			this.swipePanActive = false;
			List<Vector3> list = new List<Vector3>();
			Vector3 item;
			item..ctor(camera.transform.position.x, camera.transform.position.y, camera.orthographicSize);
			list.Add(item);
			foreach (View view in viewList)
			{
				Vector3 item2;
				item2..ctor(view.transform.position.x, view.transform.position.y, view.ViewSize);
				list.Add(item2);
			}
			base.StartCoroutine(this.panCoroutine = this.PanToPathInternal(camera, duration, arriveAction, list.ToArray()));
		}

		// Token: 0x06007423 RID: 29731 RVA: 0x002AD5F0 File Offset: 0x002AB7F0
		public static Texture2D CreateColorTexture(Color color, int width, int height)
		{
			Color[] array = new Color[width * height];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = color;
			}
			Texture2D texture2D = new Texture2D(width, height, 5, false);
			texture2D.SetPixels(array);
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x17000A97 RID: 2711
		// (set) Token: 0x06007424 RID: 29732 RVA: 0x0004F4F3 File Offset: 0x0004D6F3
		public Texture2D ScreenFadeTexture
		{
			set
			{
				this.screenFadeTexture = value;
			}
		}

		// Token: 0x06007425 RID: 29733 RVA: 0x002AD634 File Offset: 0x002AB834
		public virtual void Fade(float targetAlpha, float fadeDuration, Action fadeAction)
		{
			base.StartCoroutine(this.fadeCoroutine = this.FadeInternal(targetAlpha, fadeDuration, fadeAction));
		}

		// Token: 0x06007426 RID: 29734 RVA: 0x002AD65C File Offset: 0x002AB85C
		public virtual void FadeToView(Camera camera, View view, float fadeDuration, bool fadeOut, Action fadeAction)
		{
			this.swipePanActive = false;
			this.fadeAlpha = 0f;
			float inDuration;
			float fadeDuration2;
			if (fadeOut)
			{
				fadeDuration2 = fadeDuration / 2f;
				inDuration = fadeDuration / 2f;
			}
			else
			{
				fadeDuration2 = 0f;
				inDuration = fadeDuration;
			}
			this.Fade(1f, fadeDuration2, delegate
			{
				this.PanToPosition(camera, view.transform.position, view.transform.rotation, view.ViewSize, 0f, null);
				this.Fade(0f, inDuration, delegate
				{
					if (fadeAction != null)
					{
						fadeAction();
					}
				});
			});
		}

		// Token: 0x06007427 RID: 29735 RVA: 0x0004F4FC File Offset: 0x0004D6FC
		public virtual void Stop()
		{
			base.StopAllCoroutines();
			this.panCoroutine = null;
			this.fadeCoroutine = null;
		}

		// Token: 0x06007428 RID: 29736 RVA: 0x002AD6E0 File Offset: 0x002AB8E0
		public virtual void PanToPosition(Camera camera, Vector3 targetPosition, Quaternion targetRotation, float targetSize, float duration, Action arriveAction)
		{
			if (camera == null)
			{
				Debug.LogWarning("Camera is null");
				return;
			}
			if (this.panCoroutine != null)
			{
				base.StopCoroutine(this.panCoroutine);
				this.panCoroutine = null;
			}
			this.swipePanActive = false;
			if (Mathf.Approximately(duration, 0f))
			{
				camera.orthographicSize = targetSize;
				camera.transform.position = targetPosition;
				camera.transform.rotation = targetRotation;
				this.SetCameraZ(camera);
				if (arriveAction != null)
				{
					arriveAction();
					return;
				}
			}
			else
			{
				base.StartCoroutine(this.panCoroutine = this.PanInternal(camera, targetPosition, targetRotation, targetSize, duration, arriveAction));
			}
		}

		// Token: 0x06007429 RID: 29737 RVA: 0x002AD784 File Offset: 0x002AB984
		public virtual void StartSwipePan(Camera camera, View viewA, View viewB, float duration, float speedMultiplier, Action arriveAction)
		{
			if (camera == null)
			{
				Debug.LogWarning("Camera is null");
				return;
			}
			this.swipePanViewA = viewA;
			this.swipePanViewB = viewB;
			this.swipeSpeedMultiplier = speedMultiplier;
			Vector3 position = camera.transform.position;
			Vector3 targetPosition = this.CalcCameraPosition(position, this.swipePanViewA, this.swipePanViewB);
			float targetSize = this.CalcCameraSize(position, this.swipePanViewA, this.swipePanViewB);
			this.PanToPosition(camera, targetPosition, Quaternion.identity, targetSize, duration, delegate
			{
				this.swipePanActive = true;
				this.swipeCamera = camera;
				if (arriveAction != null)
				{
					arriveAction();
				}
			});
		}

		// Token: 0x0600742A RID: 29738 RVA: 0x0004F512 File Offset: 0x0004D712
		public virtual void StopSwipePan()
		{
			this.swipePanActive = false;
			this.swipePanViewA = null;
			this.swipePanViewB = null;
			this.swipeCamera = null;
		}

		// Token: 0x040065E2 RID: 26082
		[Tooltip("Full screen texture used for screen fade effect.")]
		[SerializeField]
		protected Texture2D screenFadeTexture;

		// Token: 0x040065E3 RID: 26083
		[Tooltip("Icon to display when swipe pan mode is active.")]
		[SerializeField]
		protected Texture2D swipePanIcon;

		// Token: 0x040065E4 RID: 26084
		[Tooltip("Position of continue and swipe icons in normalized screen space coords. (0,0) = top left, (1,1) = bottom right")]
		[SerializeField]
		protected Vector2 swipeIconPosition = new Vector2(1f, 0f);

		// Token: 0x040065E5 RID: 26085
		[Tooltip("Set the camera z coordinate to a fixed value every frame.")]
		[SerializeField]
		protected bool setCameraZ = true;

		// Token: 0x040065E6 RID: 26086
		[Tooltip("Fixed Z coordinate of main camera.")]
		[SerializeField]
		protected float cameraZ = -10f;

		// Token: 0x040065E7 RID: 26087
		[Tooltip("Camera to use when in swipe mode")]
		[SerializeField]
		protected Camera swipeCamera;

		// Token: 0x040065E8 RID: 26088
		protected float fadeAlpha;

		// Token: 0x040065E9 RID: 26089
		protected bool swipePanActive;

		// Token: 0x040065EA RID: 26090
		protected float swipeSpeedMultiplier = 1f;

		// Token: 0x040065EB RID: 26091
		protected View swipePanViewA;

		// Token: 0x040065EC RID: 26092
		protected View swipePanViewB;

		// Token: 0x040065ED RID: 26093
		protected Vector3 previousMousePos;

		// Token: 0x040065EE RID: 26094
		protected IEnumerator panCoroutine;

		// Token: 0x040065EF RID: 26095
		protected IEnumerator fadeCoroutine;

		// Token: 0x040065F0 RID: 26096
		protected Dictionary<string, CameraManager.CameraView> storedViews = new Dictionary<string, CameraManager.CameraView>();

		// Token: 0x020012BC RID: 4796
		protected class CameraView
		{
			// Token: 0x040065F1 RID: 26097
			public Vector3 cameraPos;

			// Token: 0x040065F2 RID: 26098
			public Quaternion cameraRot;

			// Token: 0x040065F3 RID: 26099
			public float cameraSize;
		}
	}
}
