using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

public class CameraManager : MonoBehaviour
{
	protected class CameraView
	{
		public Vector3 cameraPos;

		public Quaternion cameraRot;

		public float cameraSize;
	}

	[Tooltip("Full screen texture used for screen fade effect.")]
	[SerializeField]
	protected Texture2D screenFadeTexture;

	[Tooltip("Icon to display when swipe pan mode is active.")]
	[SerializeField]
	protected Texture2D swipePanIcon;

	[Tooltip("Position of continue and swipe icons in normalized screen space coords. (0,0) = top left, (1,1) = bottom right")]
	[SerializeField]
	protected Vector2 swipeIconPosition = new Vector2(1f, 0f);

	[Tooltip("Set the camera z coordinate to a fixed value every frame.")]
	[SerializeField]
	protected bool setCameraZ = true;

	[Tooltip("Fixed Z coordinate of main camera.")]
	[SerializeField]
	protected float cameraZ = -10f;

	[Tooltip("Camera to use when in swipe mode")]
	[SerializeField]
	protected Camera swipeCamera;

	protected float fadeAlpha;

	protected bool swipePanActive;

	protected float swipeSpeedMultiplier = 1f;

	protected View swipePanViewA;

	protected View swipePanViewB;

	protected Vector3 previousMousePos;

	protected IEnumerator panCoroutine;

	protected IEnumerator fadeCoroutine;

	protected Dictionary<string, CameraView> storedViews = new Dictionary<string, CameraView>();

	public Texture2D ScreenFadeTexture
	{
		set
		{
			screenFadeTexture = value;
		}
	}

	protected virtual void OnGUI()
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		if (swipePanActive && Object.op_Implicit((Object)(object)swipePanIcon))
		{
			float num = (float)Screen.width * swipeIconPosition.x;
			float num2 = (float)Screen.height * swipeIconPosition.y;
			float num3 = ((Texture)swipePanIcon).width;
			float num4 = ((Texture)swipePanIcon).height;
			num = Mathf.Max(num, 0f);
			num2 = Mathf.Max(num2, 0f);
			num = Mathf.Min(num, (float)Screen.width - num3);
			num2 = Mathf.Min(num2, (float)Screen.height - num4);
			GUI.DrawTexture(new Rect(num, num2, num3, num4), (Texture)(object)swipePanIcon);
		}
		if (fadeAlpha > 0f && (Object)(object)screenFadeTexture != (Object)null)
		{
			GUI.color = new Color(1f, 1f, 1f, fadeAlpha);
			GUI.depth = -1000;
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), (Texture)(object)screenFadeTexture);
		}
	}

	protected virtual IEnumerator FadeInternal(float targetAlpha, float fadeDuration, Action fadeAction)
	{
		float startAlpha = fadeAlpha;
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
				fadeAlpha = Mathf.Lerp(startAlpha, targetAlpha, num);
				yield return null;
			}
		}
		fadeAlpha = targetAlpha;
		fadeAction?.Invoke();
	}

	protected virtual IEnumerator PanInternal(Camera camera, Vector3 targetPos, Quaternion targetRot, float targetSize, float duration, Action arriveAction)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)camera == (Object)null)
		{
			Debug.LogWarning((object)"Camera is null");
			yield break;
		}
		float timer = 0f;
		float startSize = camera.orthographicSize;
		Vector3 startPos = ((Component)camera).transform.position;
		Quaternion startRot = ((Component)camera).transform.rotation;
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
			if ((Object)(object)camera != (Object)null)
			{
				camera.orthographicSize = Mathf.Lerp(startSize, targetSize, Mathf.SmoothStep(0f, 1f, num));
				((Component)camera).transform.position = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0f, 1f, num));
				((Component)camera).transform.rotation = Quaternion.Lerp(startRot, targetRot, Mathf.SmoothStep(0f, 1f, num));
				SetCameraZ(camera);
			}
			if (arrived)
			{
				arriveAction?.Invoke();
			}
			yield return null;
		}
	}

	protected virtual IEnumerator PanToPathInternal(Camera camera, float duration, Action arriveAction, Vector3[] path)
	{
		if ((Object)(object)camera == (Object)null)
		{
			Debug.LogWarning((object)"Camera is null");
			yield break;
		}
		float timer2 = 0f;
		while (timer2 < duration)
		{
			timer2 += Time.deltaTime;
			timer2 = Mathf.Min(timer2, duration);
			float percent = timer2 / duration;
			Vector3 val = iTween.PointOnPath(path, percent);
			((Component)camera).transform.position = new Vector3(val.x, val.y, 0f);
			camera.orthographicSize = val.z;
			SetCameraZ(camera);
			yield return null;
		}
		arriveAction?.Invoke();
	}

	protected virtual void SetCameraZ(Camera camera)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (setCameraZ)
		{
			if ((Object)(object)camera == (Object)null)
			{
				Debug.LogWarning((object)"Camera is null");
			}
			else
			{
				((Component)camera).transform.position = new Vector3(((Component)camera).transform.position.x, ((Component)camera).transform.position.y, cameraZ);
			}
		}
	}

	protected virtual void Update()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Invalid comparison between Unknown and I4
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		if (!swipePanActive)
		{
			return;
		}
		if ((Object)(object)swipeCamera == (Object)null)
		{
			Debug.LogWarning((object)"Camera is null");
			return;
		}
		Vector3 val = Vector3.zero;
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if ((int)((Touch)(ref touch)).phase == 1)
			{
				touch = Input.GetTouch(0);
				val = Vector2.op_Implicit(((Touch)(ref touch)).deltaPosition);
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			previousMousePos = Input.mousePosition;
		}
		else if (Input.GetMouseButton(0))
		{
			val = Input.mousePosition - previousMousePos;
			previousMousePos = Input.mousePosition;
		}
		Vector3 val2 = swipeCamera.ScreenToViewportPoint(val);
		val2.x *= -2f * swipeSpeedMultiplier;
		val2.y *= -2f * swipeSpeedMultiplier;
		val2.z = 0f;
		Vector3 position = ((Component)swipeCamera).transform.position;
		position += val2;
		((Component)swipeCamera).transform.position = CalcCameraPosition(position, swipePanViewA, swipePanViewB);
		swipeCamera.orthographicSize = CalcCameraSize(position, swipePanViewA, swipePanViewB);
	}

	protected virtual Vector3 CalcCameraPosition(Vector3 pos, View viewA, View viewB)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = pos;
		val.x = Mathf.Max(val.x, Mathf.Min(((Component)viewA).transform.position.x, ((Component)viewB).transform.position.x));
		val.x = Mathf.Min(val.x, Mathf.Max(((Component)viewA).transform.position.x, ((Component)viewB).transform.position.x));
		val.y = Mathf.Max(val.y, Mathf.Min(((Component)viewA).transform.position.y, ((Component)viewB).transform.position.y));
		val.y = Mathf.Min(val.y, Mathf.Max(((Component)viewA).transform.position.y, ((Component)viewB).transform.position.y));
		return val;
	}

	protected virtual float CalcCameraSize(Vector3 pos, View viewA, View viewB)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = ((Component)viewB).transform.position - ((Component)viewA).transform.position;
		Vector3 val2 = pos - ((Component)viewA).transform.position;
		float magnitude = ((Vector3)(ref val)).magnitude;
		val /= magnitude;
		val2 /= magnitude;
		float num = Vector3.Dot(val, val2);
		num = Mathf.Clamp01(num);
		return Mathf.Lerp(viewA.ViewSize, viewB.ViewSize, num);
	}

	public virtual void PanToPath(Camera camera, View[] viewList, float duration, Action arriveAction)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)camera == (Object)null)
		{
			Debug.LogWarning((object)"Camera is null");
			return;
		}
		swipePanActive = false;
		List<Vector3> list = new List<Vector3>();
		Vector3 item = default(Vector3);
		((Vector3)(ref item))._002Ector(((Component)camera).transform.position.x, ((Component)camera).transform.position.y, camera.orthographicSize);
		list.Add(item);
		Vector3 item2 = default(Vector3);
		foreach (View view in viewList)
		{
			((Vector3)(ref item2))._002Ector(((Component)view).transform.position.x, ((Component)view).transform.position.y, view.ViewSize);
			list.Add(item2);
		}
		((MonoBehaviour)this).StartCoroutine(panCoroutine = PanToPathInternal(camera, duration, arriveAction, list.ToArray()));
	}

	public static Texture2D CreateColorTexture(Color color, int width, int height)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		Color[] array = (Color[])(object)new Color[width * height];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = color;
		}
		Texture2D val = new Texture2D(width, height, (TextureFormat)5, false);
		val.SetPixels(array);
		val.Apply();
		return val;
	}

	public virtual void Fade(float targetAlpha, float fadeDuration, Action fadeAction)
	{
		((MonoBehaviour)this).StartCoroutine(fadeCoroutine = FadeInternal(targetAlpha, fadeDuration, fadeAction));
	}

	public virtual void FadeToView(Camera camera, View view, float fadeDuration, bool fadeOut, Action fadeAction)
	{
		swipePanActive = false;
		fadeAlpha = 0f;
		float fadeDuration2;
		float inDuration;
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
		Fade(1f, fadeDuration2, delegate
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			PanToPosition(camera, ((Component)view).transform.position, ((Component)view).transform.rotation, view.ViewSize, 0f, null);
			Fade(0f, inDuration, delegate
			{
				if (fadeAction != null)
				{
					fadeAction();
				}
			});
		});
	}

	public virtual void Stop()
	{
		((MonoBehaviour)this).StopAllCoroutines();
		panCoroutine = null;
		fadeCoroutine = null;
	}

	public virtual void PanToPosition(Camera camera, Vector3 targetPosition, Quaternion targetRotation, float targetSize, float duration, Action arriveAction)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)camera == (Object)null)
		{
			Debug.LogWarning((object)"Camera is null");
			return;
		}
		if (panCoroutine != null)
		{
			((MonoBehaviour)this).StopCoroutine(panCoroutine);
			panCoroutine = null;
		}
		swipePanActive = false;
		if (Mathf.Approximately(duration, 0f))
		{
			camera.orthographicSize = targetSize;
			((Component)camera).transform.position = targetPosition;
			((Component)camera).transform.rotation = targetRotation;
			SetCameraZ(camera);
			arriveAction?.Invoke();
		}
		else
		{
			((MonoBehaviour)this).StartCoroutine(panCoroutine = PanInternal(camera, targetPosition, targetRotation, targetSize, duration, arriveAction));
		}
	}

	public virtual void StartSwipePan(Camera camera, View viewA, View viewB, float duration, float speedMultiplier, Action arriveAction)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)camera == (Object)null)
		{
			Debug.LogWarning((object)"Camera is null");
			return;
		}
		swipePanViewA = viewA;
		swipePanViewB = viewB;
		swipeSpeedMultiplier = speedMultiplier;
		Vector3 position = ((Component)camera).transform.position;
		Vector3 targetPosition = CalcCameraPosition(position, swipePanViewA, swipePanViewB);
		float targetSize = CalcCameraSize(position, swipePanViewA, swipePanViewB);
		PanToPosition(camera, targetPosition, Quaternion.identity, targetSize, duration, delegate
		{
			swipePanActive = true;
			swipeCamera = camera;
			if (arriveAction != null)
			{
				arriveAction();
			}
		});
	}

	public virtual void StopSwipePan()
	{
		swipePanActive = false;
		swipePanViewA = null;
		swipePanViewB = null;
		swipeCamera = null;
	}
}
