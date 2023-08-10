using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;

public static class NGUITools
{
	private static AudioListener mListener;

	private static bool mLoaded = false;

	private static float mGlobalVolume = 1f;

	private static Vector3[] mSides = (Vector3[])(object)new Vector3[4];

	public static float soundVolume
	{
		get
		{
			if (!mLoaded)
			{
				mLoaded = true;
				mGlobalVolume = PlayerPrefs.GetFloat("Sound", 1f);
			}
			return mGlobalVolume;
		}
		set
		{
			if (mGlobalVolume != value)
			{
				mLoaded = true;
				mGlobalVolume = value;
				PlayerPrefs.SetFloat("Sound", value);
			}
		}
	}

	public static bool fileAccess => false;

	public static string clipboard
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			TextEditor val = new TextEditor();
			val.Paste();
			return val.content.text;
		}
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			TextEditor val = new TextEditor
			{
				content = new GUIContent(value)
			};
			val.OnFocus();
			val.Copy();
		}
	}

	public static Vector2 screenSize => new Vector2((float)Screen.width, (float)Screen.height);

	public static AudioSource PlaySound(AudioClip clip)
	{
		return PlaySound(clip, 1f, 1f);
	}

	public static AudioSource PlaySound(AudioClip clip, float volume)
	{
		return PlaySound(clip, volume, 1f);
	}

	public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
	{
		volume *= soundVolume;
		if ((Object)(object)clip != (Object)null && volume > 0.01f)
		{
			if ((Object)(object)mListener == (Object)null || !GetActive((Behaviour)(object)mListener))
			{
				if (Object.FindObjectsOfType(typeof(AudioListener)) is AudioListener[] array)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (GetActive((Behaviour)(object)array[i]))
						{
							mListener = array[i];
							break;
						}
					}
				}
				if ((Object)(object)mListener == (Object)null)
				{
					Camera val = Camera.main;
					if ((Object)(object)val == (Object)null)
					{
						Object obj = Object.FindObjectOfType(typeof(Camera));
						val = (Camera)(object)((obj is Camera) ? obj : null);
					}
					if ((Object)(object)val != (Object)null)
					{
						mListener = ((Component)val).gameObject.AddComponent<AudioListener>();
					}
				}
			}
			if ((Object)(object)mListener != (Object)null && ((Behaviour)mListener).enabled && GetActive(((Component)mListener).gameObject))
			{
				AudioSource val2 = ((Component)mListener).GetComponent<AudioSource>();
				if ((Object)(object)val2 == (Object)null)
				{
					val2 = ((Component)mListener).gameObject.AddComponent<AudioSource>();
				}
				val2.pitch = pitch;
				val2.PlayOneShot(clip, volume);
				return val2;
			}
		}
		return null;
	}

	public static int RandomRange(int min, int max)
	{
		if (min == max)
		{
			return min;
		}
		return Random.Range(min, max + 1);
	}

	public static string GetHierarchy(GameObject obj)
	{
		if ((Object)(object)obj == (Object)null)
		{
			return "";
		}
		string text = ((Object)obj).name;
		while ((Object)(object)obj.transform.parent != (Object)null)
		{
			obj = ((Component)obj.transform.parent).gameObject;
			text = ((Object)obj).name + "\\" + text;
		}
		return text;
	}

	public static T[] FindActive<T>() where T : Component
	{
		return Object.FindObjectsOfType(typeof(T)) as T[];
	}

	public static Camera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		Camera cachedCamera;
		for (int i = 0; i < UICamera.list.size; i++)
		{
			cachedCamera = UICamera.list.buffer[i].cachedCamera;
			if (Object.op_Implicit((Object)(object)cachedCamera) && (cachedCamera.cullingMask & num) != 0)
			{
				return cachedCamera;
			}
		}
		cachedCamera = Camera.main;
		if (Object.op_Implicit((Object)(object)cachedCamera) && (cachedCamera.cullingMask & num) != 0)
		{
			return cachedCamera;
		}
		Camera[] array = (Camera[])(object)new Camera[Camera.allCamerasCount];
		int allCameras = Camera.GetAllCameras(array);
		for (int j = 0; j < allCameras; j++)
		{
			cachedCamera = array[j];
			if (Object.op_Implicit((Object)(object)cachedCamera) && ((Behaviour)cachedCamera).enabled && (cachedCamera.cullingMask & num) != 0)
			{
				return cachedCamera;
			}
		}
		return null;
	}

	public static void AddWidgetCollider(GameObject go)
	{
		AddWidgetCollider(go, considerInactive: false);
	}

	public static void AddWidgetCollider(GameObject go, bool considerInactive)
	{
		if (!((Object)(object)go != (Object)null))
		{
			return;
		}
		Collider component = go.GetComponent<Collider>();
		BoxCollider val = (BoxCollider)(object)((component is BoxCollider) ? component : null);
		if ((Object)(object)val != (Object)null)
		{
			UpdateWidgetCollider(val, considerInactive);
		}
		else
		{
			if ((Object)(object)component != (Object)null)
			{
				return;
			}
			BoxCollider2D component2 = go.GetComponent<BoxCollider2D>();
			if ((Object)(object)component2 != (Object)null)
			{
				UpdateWidgetCollider(component2, considerInactive);
				return;
			}
			UICamera uICamera = UICamera.FindCameraForLayer(go.layer);
			if ((Object)(object)uICamera != (Object)null && (uICamera.eventType == UICamera.EventType.World_2D || uICamera.eventType == UICamera.EventType.UI_2D))
			{
				component2 = go.AddComponent<BoxCollider2D>();
				((Collider2D)component2).isTrigger = true;
				UIWidget component3 = go.GetComponent<UIWidget>();
				if ((Object)(object)component3 != (Object)null)
				{
					component3.autoResizeBoxCollider = true;
				}
				UpdateWidgetCollider(component2, considerInactive);
			}
			else
			{
				val = go.AddComponent<BoxCollider>();
				((Collider)val).isTrigger = true;
				UIWidget component4 = go.GetComponent<UIWidget>();
				if ((Object)(object)component4 != (Object)null)
				{
					component4.autoResizeBoxCollider = true;
				}
				UpdateWidgetCollider(val, considerInactive);
			}
		}
	}

	public static void UpdateWidgetCollider(GameObject go)
	{
		UpdateWidgetCollider(go, considerInactive: false);
	}

	public static void UpdateWidgetCollider(GameObject go, bool considerInactive)
	{
		if (!((Object)(object)go != (Object)null))
		{
			return;
		}
		BoxCollider component = go.GetComponent<BoxCollider>();
		if ((Object)(object)component != (Object)null)
		{
			UpdateWidgetCollider(component, considerInactive);
			return;
		}
		BoxCollider2D component2 = go.GetComponent<BoxCollider2D>();
		if ((Object)(object)component2 != (Object)null)
		{
			UpdateWidgetCollider(component2, considerInactive);
		}
	}

	public static void UpdateWidgetCollider(BoxCollider box, bool considerInactive)
	{
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)box != (Object)null))
		{
			return;
		}
		GameObject gameObject = ((Component)box).gameObject;
		UIWidget component = gameObject.GetComponent<UIWidget>();
		if ((Object)(object)component != (Object)null)
		{
			Vector4 drawRegion = component.drawRegion;
			if (drawRegion.x != 0f || drawRegion.y != 0f || drawRegion.z != 1f || drawRegion.w != 1f)
			{
				Vector4 drawingDimensions = component.drawingDimensions;
				box.center = new Vector3((drawingDimensions.x + drawingDimensions.z) * 0.5f, (drawingDimensions.y + drawingDimensions.w) * 0.5f);
				box.size = new Vector3(drawingDimensions.z - drawingDimensions.x, drawingDimensions.w - drawingDimensions.y);
			}
			else
			{
				Vector3[] localCorners = component.localCorners;
				box.center = Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
				box.size = localCorners[2] - localCorners[0];
			}
		}
		else
		{
			Bounds val = NGUIMath.CalculateRelativeWidgetBounds(gameObject.transform, considerInactive);
			box.center = ((Bounds)(ref val)).center;
			box.size = new Vector3(((Bounds)(ref val)).size.x, ((Bounds)(ref val)).size.y, 0f);
		}
	}

	public static void UpdateWidgetCollider(BoxCollider2D box, bool considerInactive)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)box != (Object)null)
		{
			GameObject gameObject = ((Component)box).gameObject;
			UIWidget component = gameObject.GetComponent<UIWidget>();
			if ((Object)(object)component != (Object)null)
			{
				Vector3[] localCorners = component.localCorners;
				((Collider2D)box).offset = Vector2.op_Implicit(Vector3.Lerp(localCorners[0], localCorners[2], 0.5f));
				box.size = Vector2.op_Implicit(localCorners[2] - localCorners[0]);
			}
			else
			{
				Bounds val = NGUIMath.CalculateRelativeWidgetBounds(gameObject.transform, considerInactive);
				((Collider2D)box).offset = Vector2.op_Implicit(((Bounds)(ref val)).center);
				box.size = new Vector2(((Bounds)(ref val)).size.x, ((Bounds)(ref val)).size.y);
			}
		}
	}

	public static string GetTypeName<T>()
	{
		string text = typeof(T).ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	public static string GetTypeName(Object obj)
	{
		if (obj == (Object)null)
		{
			return "Null";
		}
		string text = ((object)obj).GetType().ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	public static void RegisterUndo(Object obj, string name)
	{
	}

	public static void SetDirty(Object obj)
	{
	}

	public static GameObject AddChild(GameObject parent)
	{
		return AddChild(parent, undo: true);
	}

	public static GameObject AddChild(GameObject parent, bool undo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = new GameObject();
		if ((Object)(object)parent != (Object)null)
		{
			Transform transform = val.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			val.layer = parent.layer;
		}
		return val;
	}

	public static GameObject AddChild(GameObject parent, GameObject prefab)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = Object.Instantiate<GameObject>(prefab);
		if ((Object)(object)val != (Object)null && (Object)(object)parent != (Object)null)
		{
			Transform transform = val.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			val.layer = parent.layer;
		}
		return val;
	}

	public static int CalculateRaycastDepth(GameObject go)
	{
		UIWidget component = go.GetComponent<UIWidget>();
		if ((Object)(object)component != (Object)null)
		{
			return component.raycastDepth;
		}
		UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
		if (componentsInChildren.Length == 0)
		{
			return 0;
		}
		int num = int.MaxValue;
		int i = 0;
		for (int num2 = componentsInChildren.Length; i < num2; i++)
		{
			if (((Behaviour)componentsInChildren[i]).enabled)
			{
				num = Mathf.Min(num, componentsInChildren[i].raycastDepth);
			}
		}
		return num;
	}

	public static int CalculateNextDepth(GameObject go)
	{
		int num = -1;
		UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
		int i = 0;
		for (int num2 = componentsInChildren.Length; i < num2; i++)
		{
			num = Mathf.Max(num, componentsInChildren[i].depth);
		}
		return num + 1;
	}

	public static int CalculateNextDepth(GameObject go, bool ignoreChildrenWithColliders)
	{
		if (ignoreChildrenWithColliders)
		{
			int num = -1;
			UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
			int i = 0;
			for (int num2 = componentsInChildren.Length; i < num2; i++)
			{
				UIWidget uIWidget = componentsInChildren[i];
				if (!((Object)(object)uIWidget.cachedGameObject != (Object)(object)go) || (!((Object)(object)((Component)uIWidget).GetComponent<Collider>() != (Object)null) && !((Object)(object)((Component)uIWidget).GetComponent<Collider2D>() != (Object)null)))
				{
					num = Mathf.Max(num, uIWidget.depth);
				}
			}
			return num + 1;
		}
		return CalculateNextDepth(go);
	}

	public static int AdjustDepth(GameObject go, int adjustment)
	{
		if ((Object)(object)go != (Object)null)
		{
			if ((Object)(object)go.GetComponent<UIPanel>() != (Object)null)
			{
				UIPanel[] componentsInChildren = go.GetComponentsInChildren<UIPanel>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].depth += adjustment;
				}
				return 1;
			}
			UIWidget[] componentsInChildren2 = go.GetComponentsInChildren<UIWidget>(true);
			int j = 0;
			for (int num = componentsInChildren2.Length; j < num; j++)
			{
				componentsInChildren2[j].depth += adjustment;
			}
			return 2;
		}
		return 0;
	}

	public static void BringForward(GameObject go)
	{
		switch (AdjustDepth(go, 1000))
		{
		case 1:
			NormalizePanelDepths();
			break;
		case 2:
			NormalizeWidgetDepths();
			break;
		}
	}

	public static void PushBack(GameObject go)
	{
		switch (AdjustDepth(go, -1000))
		{
		case 1:
			NormalizePanelDepths();
			break;
		case 2:
			NormalizeWidgetDepths();
			break;
		}
	}

	public static void NormalizeDepths()
	{
		NormalizeWidgetDepths();
		NormalizePanelDepths();
	}

	public static void NormalizeWidgetDepths()
	{
		UIWidget[] array = NGUITools.FindActive<UIWidget>();
		int num = array.Length;
		if (num <= 0)
		{
			return;
		}
		Array.Sort(array, UIWidget.FullCompareFunc);
		int num2 = 0;
		int depth = array[0].depth;
		for (int i = 0; i < num; i++)
		{
			UIWidget uIWidget = array[i];
			if (uIWidget.depth == depth)
			{
				uIWidget.depth = num2;
				continue;
			}
			depth = uIWidget.depth;
			num2 = (uIWidget.depth = num2 + 1);
		}
	}

	public static void NormalizePanelDepths()
	{
		UIPanel[] array = NGUITools.FindActive<UIPanel>();
		int num = array.Length;
		if (num <= 0)
		{
			return;
		}
		Array.Sort(array, UIPanel.CompareFunc);
		int num2 = 0;
		int depth = array[0].depth;
		for (int i = 0; i < num; i++)
		{
			UIPanel uIPanel = array[i];
			if (uIPanel.depth == depth)
			{
				uIPanel.depth = num2;
				continue;
			}
			depth = uIPanel.depth;
			num2 = (uIPanel.depth = num2 + 1);
		}
	}

	public static UIPanel CreateUI(bool advanced3D)
	{
		return CreateUI(null, advanced3D, -1);
	}

	public static UIPanel CreateUI(bool advanced3D, int layer)
	{
		return CreateUI(null, advanced3D, layer);
	}

	public static UIPanel CreateUI(Transform trans, bool advanced3D, int layer)
	{
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Invalid comparison between Unknown and I4
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Invalid comparison between Unknown and I4
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		UIRoot uIRoot = (((Object)(object)trans != (Object)null) ? NGUITools.FindInParents<UIRoot>(((Component)trans).gameObject) : null);
		if ((Object)(object)uIRoot == (Object)null && UIRoot.list.Count > 0)
		{
			uIRoot = UIRoot.list[0];
		}
		if ((Object)(object)uIRoot != (Object)null)
		{
			UICamera componentInChildren = ((Component)uIRoot).GetComponentInChildren<UICamera>();
			if ((Object)(object)componentInChildren != (Object)null && ((Component)componentInChildren).GetComponent<Camera>().orthographic == advanced3D)
			{
				trans = null;
				uIRoot = null;
			}
		}
		if ((Object)(object)uIRoot == (Object)null)
		{
			GameObject val = AddChild(null, undo: false);
			uIRoot = val.AddComponent<UIRoot>();
			if (layer == -1)
			{
				layer = LayerMask.NameToLayer("UI");
			}
			if (layer == -1)
			{
				layer = LayerMask.NameToLayer("2D UI");
			}
			val.layer = layer;
			if (advanced3D)
			{
				((Object)val).name = "UI Root (3D)";
				uIRoot.scalingStyle = UIRoot.Scaling.FixedSize;
			}
			else
			{
				((Object)val).name = "UI Root";
				uIRoot.scalingStyle = UIRoot.Scaling.PixelPerfect;
			}
		}
		UIPanel uIPanel = ((Component)uIRoot).GetComponentInChildren<UIPanel>();
		if ((Object)(object)uIPanel == (Object)null)
		{
			Camera[] array = NGUITools.FindActive<Camera>();
			float num = -1f;
			bool flag = false;
			int num2 = 1 << ((Component)uIRoot).gameObject.layer;
			foreach (Camera val2 in array)
			{
				if ((int)val2.clearFlags == 2 || (int)val2.clearFlags == 1)
				{
					flag = true;
				}
				num = Mathf.Max(num, val2.depth);
				val2.cullingMask &= ~num2;
			}
			Camera val3 = NGUITools.AddChild<Camera>(((Component)uIRoot).gameObject, undo: false);
			((Component)val3).gameObject.AddComponent<UICamera>();
			val3.clearFlags = (CameraClearFlags)(flag ? 3 : 2);
			val3.backgroundColor = Color.grey;
			val3.cullingMask = num2;
			val3.depth = num + 1f;
			if (advanced3D)
			{
				val3.nearClipPlane = 0.1f;
				val3.farClipPlane = 4f;
				((Component)val3).transform.localPosition = new Vector3(0f, 0f, -700f);
			}
			else
			{
				val3.orthographic = true;
				val3.orthographicSize = 1f;
				val3.nearClipPlane = -10f;
				val3.farClipPlane = 10f;
			}
			AudioListener[] array2 = NGUITools.FindActive<AudioListener>();
			if (array2 == null || array2.Length == 0)
			{
				((Component)val3).gameObject.AddComponent<AudioListener>();
			}
			uIPanel = ((Component)uIRoot).gameObject.AddComponent<UIPanel>();
		}
		if ((Object)(object)trans != (Object)null)
		{
			while ((Object)(object)trans.parent != (Object)null)
			{
				trans = trans.parent;
			}
			if (IsChild(trans, ((Component)uIPanel).transform))
			{
				uIPanel = ((Component)trans).gameObject.AddComponent<UIPanel>();
			}
			else
			{
				trans.parent = ((Component)uIPanel).transform;
				trans.localScale = Vector3.one;
				trans.localPosition = Vector3.zero;
				SetChildLayer(uIPanel.cachedTransform, uIPanel.cachedGameObject.layer);
			}
		}
		return uIPanel;
	}

	public static void SetChildLayer(Transform t, int layer)
	{
		for (int i = 0; i < t.childCount; i++)
		{
			Transform child = t.GetChild(i);
			((Component)child).gameObject.layer = layer;
			SetChildLayer(child, layer);
		}
	}

	public static T AddChild<T>(GameObject parent) where T : Component
	{
		GameObject obj = AddChild(parent);
		((Object)obj).name = GetTypeName<T>();
		return obj.AddComponent<T>();
	}

	public static T AddChild<T>(GameObject parent, bool undo) where T : Component
	{
		GameObject obj = AddChild(parent, undo);
		((Object)obj).name = GetTypeName<T>();
		return obj.AddComponent<T>();
	}

	public static T AddWidget<T>(GameObject go) where T : UIWidget
	{
		int depth = CalculateNextDepth(go);
		T val = NGUITools.AddChild<T>(go);
		val.width = 100;
		val.height = 100;
		val.depth = depth;
		((Component)val).gameObject.layer = go.layer;
		return val;
	}

	public static UISprite AddSprite(GameObject go, UIAtlas atlas, string spriteName)
	{
		UISpriteData uISpriteData = (((Object)(object)atlas != (Object)null) ? atlas.GetSprite(spriteName) : null);
		UISprite uISprite = AddWidget<UISprite>(go);
		uISprite.type = ((uISpriteData != null && uISpriteData.hasBorder) ? UIBasicSprite.Type.Sliced : UIBasicSprite.Type.Simple);
		uISprite.atlas = atlas;
		uISprite.spriteName = spriteName;
		return uISprite;
	}

	public static GameObject GetRoot(GameObject go)
	{
		Transform val = go.transform;
		while (true)
		{
			Transform parent = val.parent;
			if ((Object)(object)parent == (Object)null)
			{
				break;
			}
			val = parent;
		}
		return ((Component)val).gameObject;
	}

	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if ((Object)(object)go == (Object)null)
		{
			return default(T);
		}
		T component = go.GetComponent<T>();
		if ((Object)(object)component == (Object)null)
		{
			Transform parent = go.transform.parent;
			while ((Object)(object)parent != (Object)null && (Object)(object)component == (Object)null)
			{
				component = ((Component)parent).gameObject.GetComponent<T>();
				parent = parent.parent;
			}
		}
		return component;
	}

	public static T FindInParents<T>(Transform trans) where T : Component
	{
		if ((Object)(object)trans == (Object)null)
		{
			return default(T);
		}
		T component = ((Component)trans).GetComponent<T>();
		if ((Object)(object)component == (Object)null)
		{
			Transform parent = ((Component)trans).transform.parent;
			while ((Object)(object)parent != (Object)null && (Object)(object)component == (Object)null)
			{
				component = ((Component)parent).gameObject.GetComponent<T>();
				parent = parent.parent;
			}
		}
		return component;
	}

	public static void Destroy(Object obj)
	{
		if (!(obj != (Object)null))
		{
			return;
		}
		if (Application.isPlaying)
		{
			if (obj is GameObject)
			{
				((GameObject)((obj is GameObject) ? obj : null)).transform.parent = null;
			}
			Object.Destroy(obj);
		}
		else
		{
			Object.DestroyImmediate(obj);
		}
	}

	public static void DestroyImmediate(Object obj)
	{
		if (obj != (Object)null)
		{
			if (Application.isEditor)
			{
				Object.DestroyImmediate(obj);
			}
			else
			{
				Object.Destroy(obj);
			}
		}
	}

	public static void Broadcast(string funcName)
	{
		GameObject[] array = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			array[i].SendMessage(funcName, (SendMessageOptions)1);
		}
	}

	public static void Broadcast(string funcName, object param)
	{
		GameObject[] array = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			array[i].SendMessage(funcName, param, (SendMessageOptions)1);
		}
	}

	public static bool IsChild(Transform parent, Transform child)
	{
		if ((Object)(object)parent == (Object)null || (Object)(object)child == (Object)null)
		{
			return false;
		}
		while ((Object)(object)child != (Object)null)
		{
			if ((Object)(object)child == (Object)(object)parent)
			{
				return true;
			}
			child = child.parent;
		}
		return false;
	}

	private static void Activate(Transform t)
	{
		Activate(t, compatibilityMode: false);
	}

	private static void Activate(Transform t, bool compatibilityMode)
	{
		SetActiveSelf(((Component)t).gameObject, state: true);
		if (!compatibilityMode)
		{
			return;
		}
		int i = 0;
		for (int childCount = t.childCount; i < childCount; i++)
		{
			if (((Component)t.GetChild(i)).gameObject.activeSelf)
			{
				return;
			}
		}
		int j = 0;
		for (int childCount2 = t.childCount; j < childCount2; j++)
		{
			Activate(t.GetChild(j), compatibilityMode: true);
		}
	}

	private static void Deactivate(Transform t)
	{
		SetActiveSelf(((Component)t).gameObject, state: false);
	}

	public static void SetActive(GameObject go, bool state)
	{
		SetActive(go, state, compatibilityMode: true);
	}

	public static void SetActive(GameObject go, bool state, bool compatibilityMode)
	{
		if (Object.op_Implicit((Object)(object)go))
		{
			if (state)
			{
				Activate(go.transform, compatibilityMode);
				CallCreatePanel(go.transform);
			}
			else
			{
				Deactivate(go.transform);
			}
		}
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	private static void CallCreatePanel(Transform t)
	{
		UIWidget component = ((Component)t).GetComponent<UIWidget>();
		if ((Object)(object)component != (Object)null)
		{
			component.CreatePanel();
		}
		int i = 0;
		for (int childCount = t.childCount; i < childCount; i++)
		{
			CallCreatePanel(t.GetChild(i));
		}
	}

	public static void SetActiveChildren(GameObject go, bool state)
	{
		Transform transform = go.transform;
		if (state)
		{
			int i = 0;
			for (int childCount = transform.childCount; i < childCount; i++)
			{
				Activate(transform.GetChild(i));
			}
		}
		else
		{
			int j = 0;
			for (int childCount2 = transform.childCount; j < childCount2; j++)
			{
				Deactivate(transform.GetChild(j));
			}
		}
	}

	[Obsolete("Use NGUITools.GetActive instead")]
	public static bool IsActive(Behaviour mb)
	{
		if ((Object)(object)mb != (Object)null && mb.enabled)
		{
			return ((Component)mb).gameObject.activeInHierarchy;
		}
		return false;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static bool GetActive(Behaviour mb)
	{
		if (Object.op_Implicit((Object)(object)mb) && mb.enabled)
		{
			return ((Component)mb).gameObject.activeInHierarchy;
		}
		return false;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static bool GetActive(GameObject go)
	{
		if (Object.op_Implicit((Object)(object)go))
		{
			return go.activeInHierarchy;
		}
		return false;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static void SetActiveSelf(GameObject go, bool state)
	{
		go.SetActive(state);
	}

	public static void SetLayer(GameObject go, int layer)
	{
		go.layer = layer;
		Transform transform = go.transform;
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
		{
			SetLayer(((Component)transform.GetChild(i)).gameObject, layer);
		}
	}

	public static Vector3 Round(Vector3 v)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
		return v;
	}

	public static void MakePixelPerfect(Transform t)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		UIWidget component = ((Component)t).GetComponent<UIWidget>();
		if ((Object)(object)component != (Object)null)
		{
			component.MakePixelPerfect();
		}
		if ((Object)(object)((Component)t).GetComponent<UIAnchor>() == (Object)null && (Object)(object)((Component)t).GetComponent<UIRoot>() == (Object)null)
		{
			t.localPosition = Round(t.localPosition);
			t.localScale = Round(t.localScale);
		}
		int i = 0;
		for (int childCount = t.childCount; i < childCount; i++)
		{
			MakePixelPerfect(t.GetChild(i));
		}
	}

	public static bool Save(string fileName, byte[] bytes)
	{
		if (!fileAccess)
		{
			return false;
		}
		string path = Application.persistentDataPath + "/" + fileName;
		if (bytes == null)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			return true;
		}
		FileStream fileStream = null;
		try
		{
			fileStream = File.Create(path);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex.Message);
			return false;
		}
		fileStream.Write(bytes, 0, bytes.Length);
		fileStream.Close();
		return true;
	}

	public static byte[] Load(string fileName)
	{
		if (!fileAccess)
		{
			return null;
		}
		string path = Application.persistentDataPath + "/" + fileName;
		if (File.Exists(path))
		{
			return File.ReadAllBytes(path);
		}
		return null;
	}

	public static Color ApplyPMA(Color c)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (c.a != 1f)
		{
			c.r *= c.a;
			c.g *= c.a;
			c.b *= c.a;
		}
		return c;
	}

	public static void MarkParentAsChanged(GameObject go)
	{
		UIRect[] componentsInChildren = go.GetComponentsInChildren<UIRect>();
		int i = 0;
		for (int num = componentsInChildren.Length; i < num; i++)
		{
			componentsInChildren[i].ParentHasChanged();
		}
	}

	[Obsolete("Use NGUIText.EncodeColor instead")]
	public static string EncodeColor(Color c)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return NGUIText.EncodeColor24(c);
	}

	[Obsolete("Use NGUIText.ParseColor instead")]
	public static Color ParseColor(string text, int offset)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return NGUIText.ParseColor24(text, offset);
	}

	[Obsolete("Use NGUIText.StripSymbols instead")]
	public static string StripSymbols(string text)
	{
		return NGUIText.StripSymbols(text);
	}

	public static T AddMissingComponent<T>(this GameObject go) where T : Component
	{
		T val = go.GetComponent<T>();
		if ((Object)(object)val == (Object)null)
		{
			val = go.AddComponent<T>();
		}
		return val;
	}

	public static Vector3[] GetSides(this Camera cam)
	{
		return cam.GetSides(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), null);
	}

	public static Vector3[] GetSides(this Camera cam, float depth)
	{
		return cam.GetSides(depth, null);
	}

	public static Vector3[] GetSides(this Camera cam, Transform relativeTo)
	{
		return cam.GetSides(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), relativeTo);
	}

	public static Vector3[] GetSides(this Camera cam, float depth, Transform relativeTo)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		Rect rect = cam.rect;
		Vector2 val = screenSize;
		float num = -0.5f;
		float num2 = 0.5f;
		float num3 = -0.5f;
		float num4 = 0.5f;
		float num5 = ((Rect)(ref rect)).width / ((Rect)(ref rect)).height;
		num *= num5;
		num2 *= num5;
		num *= val.x;
		num2 *= val.x;
		num3 *= val.y;
		num4 *= val.y;
		Transform transform = ((Component)cam).transform;
		mSides[0] = transform.TransformPoint(new Vector3(num, 0f, depth));
		mSides[1] = transform.TransformPoint(new Vector3(0f, num4, depth));
		mSides[2] = transform.TransformPoint(new Vector3(num2, 0f, depth));
		mSides[3] = transform.TransformPoint(new Vector3(0f, num3, depth));
		if ((Object)(object)relativeTo != (Object)null)
		{
			for (int i = 0; i < 4; i++)
			{
				mSides[i] = relativeTo.InverseTransformPoint(mSides[i]);
			}
		}
		return mSides;
	}

	public static Vector3[] GetWorldCorners(this Camera cam)
	{
		return cam.GetWorldCorners(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), null);
	}

	public static Vector3[] GetWorldCorners(this Camera cam, float depth)
	{
		return cam.GetWorldCorners(depth, null);
	}

	public static Vector3[] GetWorldCorners(this Camera cam, Transform relativeTo)
	{
		return cam.GetWorldCorners(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), relativeTo);
	}

	public static Vector3[] GetWorldCorners(this Camera cam, float depth, Transform relativeTo)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		Rect rect = cam.rect;
		Vector2 val = screenSize;
		float num = -0.5f;
		float num2 = 0.5f;
		float num3 = -0.5f;
		float num4 = 0.5f;
		float num5 = ((Rect)(ref rect)).width / ((Rect)(ref rect)).height;
		num *= num5;
		num2 *= num5;
		num *= val.x;
		num2 *= val.x;
		num3 *= val.y;
		num4 *= val.y;
		Transform transform = ((Component)cam).transform;
		mSides[0] = transform.TransformPoint(new Vector3(num, num3, depth));
		mSides[1] = transform.TransformPoint(new Vector3(num, num4, depth));
		mSides[2] = transform.TransformPoint(new Vector3(num2, num4, depth));
		mSides[3] = transform.TransformPoint(new Vector3(num2, num3, depth));
		if ((Object)(object)relativeTo != (Object)null)
		{
			for (int i = 0; i < 4; i++)
			{
				mSides[i] = relativeTo.InverseTransformPoint(mSides[i]);
			}
		}
		return mSides;
	}

	public static string GetFuncName(object obj, string method)
	{
		if (obj == null)
		{
			return "<null>";
		}
		string text = obj.GetType().ToString();
		int num = text.LastIndexOf('/');
		if (num > 0)
		{
			text = text.Substring(num + 1);
		}
		if (!string.IsNullOrEmpty(method))
		{
			return text + "/" + method;
		}
		return text;
	}

	public static void Execute<T>(GameObject go, string funcName) where T : Component
	{
		T[] components = go.GetComponents<T>();
		foreach (T val in components)
		{
			MethodInfo method = ((object)val).GetType().GetMethod(funcName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method != null)
			{
				method.Invoke(val, null);
			}
		}
	}

	public static void ExecuteAll<T>(GameObject root, string funcName) where T : Component
	{
		Execute<T>(root, funcName);
		Transform transform = root.transform;
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
		{
			ExecuteAll<T>(((Component)transform.GetChild(i)).gameObject, funcName);
		}
	}

	public static void ImmediatelyCreateDrawCalls(GameObject root)
	{
		NGUITools.ExecuteAll<UIWidget>(root, "Start");
		NGUITools.ExecuteAll<UIPanel>(root, "Start");
		NGUITools.ExecuteAll<UIWidget>(root, "Update");
		NGUITools.ExecuteAll<UIPanel>(root, "Update");
		NGUITools.ExecuteAll<UIPanel>(root, "LateUpdate");
	}
}
