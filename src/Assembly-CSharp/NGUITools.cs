using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public static class NGUITools
{
	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06000765 RID: 1893 RVA: 0x0000A3C1 File Offset: 0x000085C1
	// (set) Token: 0x06000766 RID: 1894 RVA: 0x0000A3E9 File Offset: 0x000085E9
	public static float soundVolume
	{
		get
		{
			if (!NGUITools.mLoaded)
			{
				NGUITools.mLoaded = true;
				NGUITools.mGlobalVolume = PlayerPrefs.GetFloat("Sound", 1f);
			}
			return NGUITools.mGlobalVolume;
		}
		set
		{
			if (NGUITools.mGlobalVolume != value)
			{
				NGUITools.mLoaded = true;
				NGUITools.mGlobalVolume = value;
				PlayerPrefs.SetFloat("Sound", value);
			}
		}
	}

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06000767 RID: 1895 RVA: 0x00004050 File Offset: 0x00002250
	public static bool fileAccess
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x0000A40A File Offset: 0x0000860A
	public static AudioSource PlaySound(AudioClip clip)
	{
		return NGUITools.PlaySound(clip, 1f, 1f);
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x0000A41C File Offset: 0x0000861C
	public static AudioSource PlaySound(AudioClip clip, float volume)
	{
		return NGUITools.PlaySound(clip, volume, 1f);
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x0007EAB8 File Offset: 0x0007CCB8
	public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
	{
		volume *= NGUITools.soundVolume;
		if (clip != null && volume > 0.01f)
		{
			if (NGUITools.mListener == null || !NGUITools.GetActive(NGUITools.mListener))
			{
				AudioListener[] array = Object.FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (NGUITools.GetActive(array[i]))
						{
							NGUITools.mListener = array[i];
							break;
						}
					}
				}
				if (NGUITools.mListener == null)
				{
					Camera camera = Camera.main;
					if (camera == null)
					{
						camera = (Object.FindObjectOfType(typeof(Camera)) as Camera);
					}
					if (camera != null)
					{
						NGUITools.mListener = camera.gameObject.AddComponent<AudioListener>();
					}
				}
			}
			if (NGUITools.mListener != null && NGUITools.mListener.enabled && NGUITools.GetActive(NGUITools.mListener.gameObject))
			{
				AudioSource audioSource = NGUITools.mListener.GetComponent<AudioSource>();
				if (audioSource == null)
				{
					audioSource = NGUITools.mListener.gameObject.AddComponent<AudioSource>();
				}
				audioSource.pitch = pitch;
				audioSource.PlayOneShot(clip, volume);
				return audioSource;
			}
		}
		return null;
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x0000A42A File Offset: 0x0000862A
	public static int RandomRange(int min, int max)
	{
		if (min == max)
		{
			return min;
		}
		return Random.Range(min, max + 1);
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x0007EBE8 File Offset: 0x0007CDE8
	public static string GetHierarchy(GameObject obj)
	{
		if (obj == null)
		{
			return "";
		}
		string text = obj.name;
		while (obj.transform.parent != null)
		{
			obj = obj.transform.parent.gameObject;
			text = obj.name + "\\" + text;
		}
		return text;
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x0000A43B File Offset: 0x0000863B
	public static T[] FindActive<T>() where T : Component
	{
		return Object.FindObjectsOfType(typeof(T)) as T[];
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x0007EC48 File Offset: 0x0007CE48
	public static Camera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		Camera camera;
		for (int i = 0; i < UICamera.list.size; i++)
		{
			camera = UICamera.list.buffer[i].cachedCamera;
			if (camera && (camera.cullingMask & num) != 0)
			{
				return camera;
			}
		}
		camera = Camera.main;
		if (camera && (camera.cullingMask & num) != 0)
		{
			return camera;
		}
		Camera[] array = new Camera[Camera.allCamerasCount];
		int allCameras = Camera.GetAllCameras(array);
		for (int j = 0; j < allCameras; j++)
		{
			camera = array[j];
			if (camera && camera.enabled && (camera.cullingMask & num) != 0)
			{
				return camera;
			}
		}
		return null;
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x0000A451 File Offset: 0x00008651
	public static void AddWidgetCollider(GameObject go)
	{
		NGUITools.AddWidgetCollider(go, false);
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x0007ECFC File Offset: 0x0007CEFC
	public static void AddWidgetCollider(GameObject go, bool considerInactive)
	{
		if (go != null)
		{
			Collider component = go.GetComponent<Collider>();
			BoxCollider boxCollider = component as BoxCollider;
			if (boxCollider != null)
			{
				NGUITools.UpdateWidgetCollider(boxCollider, considerInactive);
				return;
			}
			if (component != null)
			{
				return;
			}
			BoxCollider2D boxCollider2D = go.GetComponent<BoxCollider2D>();
			if (boxCollider2D != null)
			{
				NGUITools.UpdateWidgetCollider(boxCollider2D, considerInactive);
				return;
			}
			UICamera uicamera = UICamera.FindCameraForLayer(go.layer);
			if (uicamera != null && (uicamera.eventType == UICamera.EventType.World_2D || uicamera.eventType == UICamera.EventType.UI_2D))
			{
				boxCollider2D = go.AddComponent<BoxCollider2D>();
				boxCollider2D.isTrigger = true;
				UIWidget component2 = go.GetComponent<UIWidget>();
				if (component2 != null)
				{
					component2.autoResizeBoxCollider = true;
				}
				NGUITools.UpdateWidgetCollider(boxCollider2D, considerInactive);
				return;
			}
			boxCollider = go.AddComponent<BoxCollider>();
			boxCollider.isTrigger = true;
			UIWidget component3 = go.GetComponent<UIWidget>();
			if (component3 != null)
			{
				component3.autoResizeBoxCollider = true;
			}
			NGUITools.UpdateWidgetCollider(boxCollider, considerInactive);
		}
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0000A45A File Offset: 0x0000865A
	public static void UpdateWidgetCollider(GameObject go)
	{
		NGUITools.UpdateWidgetCollider(go, false);
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x0007EDDC File Offset: 0x0007CFDC
	public static void UpdateWidgetCollider(GameObject go, bool considerInactive)
	{
		if (go != null)
		{
			BoxCollider component = go.GetComponent<BoxCollider>();
			if (component != null)
			{
				NGUITools.UpdateWidgetCollider(component, considerInactive);
				return;
			}
			BoxCollider2D component2 = go.GetComponent<BoxCollider2D>();
			if (component2 != null)
			{
				NGUITools.UpdateWidgetCollider(component2, considerInactive);
			}
		}
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0007EE24 File Offset: 0x0007D024
	public static void UpdateWidgetCollider(BoxCollider box, bool considerInactive)
	{
		if (box != null)
		{
			GameObject gameObject = box.gameObject;
			UIWidget component = gameObject.GetComponent<UIWidget>();
			if (component != null)
			{
				Vector4 drawRegion = component.drawRegion;
				if (drawRegion.x != 0f || drawRegion.y != 0f || drawRegion.z != 1f || drawRegion.w != 1f)
				{
					Vector4 drawingDimensions = component.drawingDimensions;
					box.center = new Vector3((drawingDimensions.x + drawingDimensions.z) * 0.5f, (drawingDimensions.y + drawingDimensions.w) * 0.5f);
					box.size = new Vector3(drawingDimensions.z - drawingDimensions.x, drawingDimensions.w - drawingDimensions.y);
					return;
				}
				Vector3[] localCorners = component.localCorners;
				box.center = Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
				box.size = localCorners[2] - localCorners[0];
				return;
			}
			else
			{
				Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(gameObject.transform, considerInactive);
				box.center = bounds.center;
				box.size = new Vector3(bounds.size.x, bounds.size.y, 0f);
			}
		}
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x0007EF78 File Offset: 0x0007D178
	public static void UpdateWidgetCollider(BoxCollider2D box, bool considerInactive)
	{
		if (box != null)
		{
			GameObject gameObject = box.gameObject;
			UIWidget component = gameObject.GetComponent<UIWidget>();
			if (component != null)
			{
				Vector3[] localCorners = component.localCorners;
				box.offset = Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
				box.size = localCorners[2] - localCorners[0];
				return;
			}
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(gameObject.transform, considerInactive);
			box.offset = bounds.center;
			box.size = new Vector2(bounds.size.x, bounds.size.y);
		}
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x0007F034 File Offset: 0x0007D234
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

	// Token: 0x06000776 RID: 1910 RVA: 0x0007F080 File Offset: 0x0007D280
	public static string GetTypeName(Object obj)
	{
		if (obj == null)
		{
			return "Null";
		}
		string text = obj.GetType().ToString();
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

	// Token: 0x06000777 RID: 1911 RVA: 0x000042DD File Offset: 0x000024DD
	public static void RegisterUndo(Object obj, string name)
	{
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x000042DD File Offset: 0x000024DD
	public static void SetDirty(Object obj)
	{
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0000A463 File Offset: 0x00008663
	public static GameObject AddChild(GameObject parent)
	{
		return NGUITools.AddChild(parent, true);
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x0007F0D8 File Offset: 0x0007D2D8
	public static GameObject AddChild(GameObject parent, bool undo)
	{
		GameObject gameObject = new GameObject();
		if (parent != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = parent.layer;
		}
		return gameObject;
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x0007F134 File Offset: 0x0007D334
	public static GameObject AddChild(GameObject parent, GameObject prefab)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(prefab);
		if (gameObject != null && parent != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = parent.layer;
		}
		return gameObject;
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x0007F19C File Offset: 0x0007D39C
	public static int CalculateRaycastDepth(GameObject go)
	{
		UIWidget component = go.GetComponent<UIWidget>();
		if (component != null)
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
		int num2 = componentsInChildren.Length;
		while (i < num2)
		{
			if (componentsInChildren[i].enabled)
			{
				num = Mathf.Min(num, componentsInChildren[i].raycastDepth);
			}
			i++;
		}
		return num;
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x0007F200 File Offset: 0x0007D400
	public static int CalculateNextDepth(GameObject go)
	{
		int num = -1;
		UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
		int i = 0;
		int num2 = componentsInChildren.Length;
		while (i < num2)
		{
			num = Mathf.Max(num, componentsInChildren[i].depth);
			i++;
		}
		return num + 1;
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0007F238 File Offset: 0x0007D438
	public static int CalculateNextDepth(GameObject go, bool ignoreChildrenWithColliders)
	{
		if (ignoreChildrenWithColliders)
		{
			int num = -1;
			UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
			int i = 0;
			int num2 = componentsInChildren.Length;
			while (i < num2)
			{
				UIWidget uiwidget = componentsInChildren[i];
				if (!(uiwidget.cachedGameObject != go) || (!(uiwidget.GetComponent<Collider>() != null) && !(uiwidget.GetComponent<Collider2D>() != null)))
				{
					num = Mathf.Max(num, uiwidget.depth);
				}
				i++;
			}
			return num + 1;
		}
		return NGUITools.CalculateNextDepth(go);
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x0007F2AC File Offset: 0x0007D4AC
	public static int AdjustDepth(GameObject go, int adjustment)
	{
		if (!(go != null))
		{
			return 0;
		}
		if (go.GetComponent<UIPanel>() != null)
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
		int num = componentsInChildren2.Length;
		while (j < num)
		{
			componentsInChildren2[j].depth += adjustment;
			j++;
		}
		return 2;
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0007F328 File Offset: 0x0007D528
	public static void BringForward(GameObject go)
	{
		int num = NGUITools.AdjustDepth(go, 1000);
		if (num == 1)
		{
			NGUITools.NormalizePanelDepths();
			return;
		}
		if (num == 2)
		{
			NGUITools.NormalizeWidgetDepths();
		}
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x0007F354 File Offset: 0x0007D554
	public static void PushBack(GameObject go)
	{
		int num = NGUITools.AdjustDepth(go, -1000);
		if (num == 1)
		{
			NGUITools.NormalizePanelDepths();
			return;
		}
		if (num == 2)
		{
			NGUITools.NormalizeWidgetDepths();
		}
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0000A46C File Offset: 0x0000866C
	public static void NormalizeDepths()
	{
		NGUITools.NormalizeWidgetDepths();
		NGUITools.NormalizePanelDepths();
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0007F380 File Offset: 0x0007D580
	public static void NormalizeWidgetDepths()
	{
		UIWidget[] array = NGUITools.FindActive<UIWidget>();
		int num = array.Length;
		if (num > 0)
		{
			Array.Sort<UIWidget>(array, new Comparison<UIWidget>(UIWidget.FullCompareFunc));
			int num2 = 0;
			int depth = array[0].depth;
			for (int i = 0; i < num; i++)
			{
				UIWidget uiwidget = array[i];
				if (uiwidget.depth == depth)
				{
					uiwidget.depth = num2;
				}
				else
				{
					depth = uiwidget.depth;
					num2 = (uiwidget.depth = num2 + 1);
				}
			}
		}
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0007F3F8 File Offset: 0x0007D5F8
	public static void NormalizePanelDepths()
	{
		UIPanel[] array = NGUITools.FindActive<UIPanel>();
		int num = array.Length;
		if (num > 0)
		{
			Array.Sort<UIPanel>(array, new Comparison<UIPanel>(UIPanel.CompareFunc));
			int num2 = 0;
			int depth = array[0].depth;
			for (int i = 0; i < num; i++)
			{
				UIPanel uipanel = array[i];
				if (uipanel.depth == depth)
				{
					uipanel.depth = num2;
				}
				else
				{
					depth = uipanel.depth;
					num2 = (uipanel.depth = num2 + 1);
				}
			}
		}
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x0000A478 File Offset: 0x00008678
	public static UIPanel CreateUI(bool advanced3D)
	{
		return NGUITools.CreateUI(null, advanced3D, -1);
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0000A482 File Offset: 0x00008682
	public static UIPanel CreateUI(bool advanced3D, int layer)
	{
		return NGUITools.CreateUI(null, advanced3D, layer);
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x0007F470 File Offset: 0x0007D670
	public static UIPanel CreateUI(Transform trans, bool advanced3D, int layer)
	{
		UIRoot uiroot = (trans != null) ? NGUITools.FindInParents<UIRoot>(trans.gameObject) : null;
		if (uiroot == null && UIRoot.list.Count > 0)
		{
			uiroot = UIRoot.list[0];
		}
		if (uiroot != null)
		{
			UICamera componentInChildren = uiroot.GetComponentInChildren<UICamera>();
			if (componentInChildren != null && componentInChildren.GetComponent<Camera>().orthographic == advanced3D)
			{
				trans = null;
				uiroot = null;
			}
		}
		if (uiroot == null)
		{
			GameObject gameObject = NGUITools.AddChild(null, false);
			uiroot = gameObject.AddComponent<UIRoot>();
			if (layer == -1)
			{
				layer = LayerMask.NameToLayer("UI");
			}
			if (layer == -1)
			{
				layer = LayerMask.NameToLayer("2D UI");
			}
			gameObject.layer = layer;
			if (advanced3D)
			{
				gameObject.name = "UI Root (3D)";
				uiroot.scalingStyle = UIRoot.Scaling.FixedSize;
			}
			else
			{
				gameObject.name = "UI Root";
				uiroot.scalingStyle = UIRoot.Scaling.PixelPerfect;
			}
		}
		UIPanel uipanel = uiroot.GetComponentInChildren<UIPanel>();
		if (uipanel == null)
		{
			Camera[] array = NGUITools.FindActive<Camera>();
			float num = -1f;
			bool flag = false;
			int num2 = 1 << uiroot.gameObject.layer;
			foreach (Camera camera in array)
			{
				if (camera.clearFlags == 2 || camera.clearFlags == 1)
				{
					flag = true;
				}
				num = Mathf.Max(num, camera.depth);
				camera.cullingMask &= ~num2;
			}
			Camera camera2 = NGUITools.AddChild<Camera>(uiroot.gameObject, false);
			camera2.gameObject.AddComponent<UICamera>();
			camera2.clearFlags = (flag ? 3 : 2);
			camera2.backgroundColor = Color.grey;
			camera2.cullingMask = num2;
			camera2.depth = num + 1f;
			if (advanced3D)
			{
				camera2.nearClipPlane = 0.1f;
				camera2.farClipPlane = 4f;
				camera2.transform.localPosition = new Vector3(0f, 0f, -700f);
			}
			else
			{
				camera2.orthographic = true;
				camera2.orthographicSize = 1f;
				camera2.nearClipPlane = -10f;
				camera2.farClipPlane = 10f;
			}
			AudioListener[] array2 = NGUITools.FindActive<AudioListener>();
			if (array2 == null || array2.Length == 0)
			{
				camera2.gameObject.AddComponent<AudioListener>();
			}
			uipanel = uiroot.gameObject.AddComponent<UIPanel>();
		}
		if (trans != null)
		{
			while (trans.parent != null)
			{
				trans = trans.parent;
			}
			if (NGUITools.IsChild(trans, uipanel.transform))
			{
				uipanel = trans.gameObject.AddComponent<UIPanel>();
			}
			else
			{
				trans.parent = uipanel.transform;
				trans.localScale = Vector3.one;
				trans.localPosition = Vector3.zero;
				NGUITools.SetChildLayer(uipanel.cachedTransform, uipanel.cachedGameObject.layer);
			}
		}
		return uipanel;
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x0007F72C File Offset: 0x0007D92C
	public static void SetChildLayer(Transform t, int layer)
	{
		for (int i = 0; i < t.childCount; i++)
		{
			Transform child = t.GetChild(i);
			child.gameObject.layer = layer;
			NGUITools.SetChildLayer(child, layer);
		}
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x0000A48C File Offset: 0x0000868C
	public static T AddChild<T>(GameObject parent) where T : Component
	{
		GameObject gameObject = NGUITools.AddChild(parent);
		gameObject.name = NGUITools.GetTypeName<T>();
		return gameObject.AddComponent<T>();
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0000A4A4 File Offset: 0x000086A4
	public static T AddChild<T>(GameObject parent, bool undo) where T : Component
	{
		GameObject gameObject = NGUITools.AddChild(parent, undo);
		gameObject.name = NGUITools.GetTypeName<T>();
		return gameObject.AddComponent<T>();
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x0007F764 File Offset: 0x0007D964
	public static T AddWidget<T>(GameObject go) where T : UIWidget
	{
		int depth = NGUITools.CalculateNextDepth(go);
		T t = NGUITools.AddChild<T>(go);
		t.width = 100;
		t.height = 100;
		t.depth = depth;
		t.gameObject.layer = go.layer;
		return t;
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x0007F7BC File Offset: 0x0007D9BC
	public static UISprite AddSprite(GameObject go, UIAtlas atlas, string spriteName)
	{
		UISpriteData uispriteData = (atlas != null) ? atlas.GetSprite(spriteName) : null;
		UISprite uisprite = NGUITools.AddWidget<UISprite>(go);
		uisprite.type = ((uispriteData == null || !uispriteData.hasBorder) ? UIBasicSprite.Type.Simple : UIBasicSprite.Type.Sliced);
		uisprite.atlas = atlas;
		uisprite.spriteName = spriteName;
		return uisprite;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0007F808 File Offset: 0x0007DA08
	public static GameObject GetRoot(GameObject go)
	{
		Transform transform = go.transform;
		for (;;)
		{
			Transform parent = transform.parent;
			if (parent == null)
			{
				break;
			}
			transform = parent;
		}
		return transform.gameObject;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x0007F838 File Offset: 0x0007DA38
	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null)
		{
			return default(T);
		}
		T component = go.GetComponent<T>();
		if (component == null)
		{
			Transform parent = go.transform.parent;
			while (parent != null && component == null)
			{
				component = parent.gameObject.GetComponent<T>();
				parent = parent.parent;
			}
		}
		return component;
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0007F8A8 File Offset: 0x0007DAA8
	public static T FindInParents<T>(Transform trans) where T : Component
	{
		if (trans == null)
		{
			return default(T);
		}
		T component = trans.GetComponent<T>();
		if (component == null)
		{
			Transform parent = trans.transform.parent;
			while (parent != null && component == null)
			{
				component = parent.gameObject.GetComponent<T>();
				parent = parent.parent;
			}
		}
		return component;
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x0000A4BD File Offset: 0x000086BD
	public static void Destroy(Object obj)
	{
		if (obj != null)
		{
			if (Application.isPlaying)
			{
				if (obj is GameObject)
				{
					(obj as GameObject).transform.parent = null;
				}
				Object.Destroy(obj);
				return;
			}
			Object.DestroyImmediate(obj);
		}
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x0000A4F5 File Offset: 0x000086F5
	public static void DestroyImmediate(Object obj)
	{
		if (obj != null)
		{
			if (Application.isEditor)
			{
				Object.DestroyImmediate(obj);
				return;
			}
			Object.Destroy(obj);
		}
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x0007F918 File Offset: 0x0007DB18
	public static void Broadcast(string funcName)
	{
		GameObject[] array = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			array[i].SendMessage(funcName, 1);
			i++;
		}
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x0007F954 File Offset: 0x0007DB54
	public static void Broadcast(string funcName, object param)
	{
		GameObject[] array = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			array[i].SendMessage(funcName, param, 1);
			i++;
		}
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x0000A514 File Offset: 0x00008714
	public static bool IsChild(Transform parent, Transform child)
	{
		if (parent == null || child == null)
		{
			return false;
		}
		while (child != null)
		{
			if (child == parent)
			{
				return true;
			}
			child = child.parent;
		}
		return false;
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x0000A547 File Offset: 0x00008747
	private static void Activate(Transform t)
	{
		NGUITools.Activate(t, false);
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x0007F994 File Offset: 0x0007DB94
	private static void Activate(Transform t, bool compatibilityMode)
	{
		NGUITools.SetActiveSelf(t.gameObject, true);
		if (compatibilityMode)
		{
			int i = 0;
			int childCount = t.childCount;
			while (i < childCount)
			{
				if (t.GetChild(i).gameObject.activeSelf)
				{
					return;
				}
				i++;
			}
			int j = 0;
			int childCount2 = t.childCount;
			while (j < childCount2)
			{
				NGUITools.Activate(t.GetChild(j), true);
				j++;
			}
		}
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x0000A550 File Offset: 0x00008750
	private static void Deactivate(Transform t)
	{
		NGUITools.SetActiveSelf(t.gameObject, false);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x0000A55E File Offset: 0x0000875E
	public static void SetActive(GameObject go, bool state)
	{
		NGUITools.SetActive(go, state, true);
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x0000A568 File Offset: 0x00008768
	public static void SetActive(GameObject go, bool state, bool compatibilityMode)
	{
		if (go)
		{
			if (state)
			{
				NGUITools.Activate(go.transform, compatibilityMode);
				NGUITools.CallCreatePanel(go.transform);
				return;
			}
			NGUITools.Deactivate(go.transform);
		}
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x0007F9F8 File Offset: 0x0007DBF8
	[DebuggerHidden]
	[DebuggerStepThrough]
	private static void CallCreatePanel(Transform t)
	{
		UIWidget component = t.GetComponent<UIWidget>();
		if (component != null)
		{
			component.CreatePanel();
		}
		int i = 0;
		int childCount = t.childCount;
		while (i < childCount)
		{
			NGUITools.CallCreatePanel(t.GetChild(i));
			i++;
		}
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x0007FA3C File Offset: 0x0007DC3C
	public static void SetActiveChildren(GameObject go, bool state)
	{
		Transform transform = go.transform;
		if (state)
		{
			int i = 0;
			int childCount = transform.childCount;
			while (i < childCount)
			{
				NGUITools.Activate(transform.GetChild(i));
				i++;
			}
			return;
		}
		int j = 0;
		int childCount2 = transform.childCount;
		while (j < childCount2)
		{
			NGUITools.Deactivate(transform.GetChild(j));
			j++;
		}
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x0000A598 File Offset: 0x00008798
	[Obsolete("Use NGUITools.GetActive instead")]
	public static bool IsActive(Behaviour mb)
	{
		return mb != null && mb.enabled && mb.gameObject.activeInHierarchy;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0000A5B8 File Offset: 0x000087B8
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static bool GetActive(Behaviour mb)
	{
		return mb && mb.enabled && mb.gameObject.activeInHierarchy;
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0000A5D7 File Offset: 0x000087D7
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static bool GetActive(GameObject go)
	{
		return go && go.activeInHierarchy;
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x0000A5E9 File Offset: 0x000087E9
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static void SetActiveSelf(GameObject go, bool state)
	{
		go.SetActive(state);
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0007FA94 File Offset: 0x0007DC94
	public static void SetLayer(GameObject go, int layer)
	{
		go.layer = layer;
		Transform transform = go.transform;
		int i = 0;
		int childCount = transform.childCount;
		while (i < childCount)
		{
			NGUITools.SetLayer(transform.GetChild(i).gameObject, layer);
			i++;
		}
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0000A5F2 File Offset: 0x000087F2
	public static Vector3 Round(Vector3 v)
	{
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
		return v;
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0007FAD4 File Offset: 0x0007DCD4
	public static void MakePixelPerfect(Transform t)
	{
		UIWidget component = t.GetComponent<UIWidget>();
		if (component != null)
		{
			component.MakePixelPerfect();
		}
		if (t.GetComponent<UIAnchor>() == null && t.GetComponent<UIRoot>() == null)
		{
			t.localPosition = NGUITools.Round(t.localPosition);
			t.localScale = NGUITools.Round(t.localScale);
		}
		int i = 0;
		int childCount = t.childCount;
		while (i < childCount)
		{
			NGUITools.MakePixelPerfect(t.GetChild(i));
			i++;
		}
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0007FB54 File Offset: 0x0007DD54
	public static bool Save(string fileName, byte[] bytes)
	{
		if (!NGUITools.fileAccess)
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
			Debug.LogError(ex.Message);
			return false;
		}
		fileStream.Write(bytes, 0, bytes.Length);
		fileStream.Close();
		return true;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0007FBCC File Offset: 0x0007DDCC
	public static byte[] Load(string fileName)
	{
		if (!NGUITools.fileAccess)
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

	// Token: 0x060007A5 RID: 1957 RVA: 0x0007FC04 File Offset: 0x0007DE04
	public static Color ApplyPMA(Color c)
	{
		if (c.a != 1f)
		{
			c.r *= c.a;
			c.g *= c.a;
			c.b *= c.a;
		}
		return c;
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0007FC54 File Offset: 0x0007DE54
	public static void MarkParentAsChanged(GameObject go)
	{
		UIRect[] componentsInChildren = go.GetComponentsInChildren<UIRect>();
		int i = 0;
		int num = componentsInChildren.Length;
		while (i < num)
		{
			componentsInChildren[i].ParentHasChanged();
			i++;
		}
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0000A62B File Offset: 0x0000882B
	// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0000A643 File Offset: 0x00008843
	public static string clipboard
	{
		get
		{
			TextEditor textEditor = new TextEditor();
			textEditor.Paste();
			return textEditor.content.text;
		}
		set
		{
			TextEditor textEditor = new TextEditor();
			textEditor.content = new GUIContent(value);
			textEditor.OnFocus();
			textEditor.Copy();
		}
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0000A356 File Offset: 0x00008556
	[Obsolete("Use NGUIText.EncodeColor instead")]
	public static string EncodeColor(Color c)
	{
		return NGUIText.EncodeColor24(c);
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0000A34D File Offset: 0x0000854D
	[Obsolete("Use NGUIText.ParseColor instead")]
	public static Color ParseColor(string text, int offset)
	{
		return NGUIText.ParseColor24(text, offset);
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0000A661 File Offset: 0x00008861
	[Obsolete("Use NGUIText.StripSymbols instead")]
	public static string StripSymbols(string text)
	{
		return NGUIText.StripSymbols(text);
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x0007FC80 File Offset: 0x0007DE80
	public static T AddMissingComponent<T>(this GameObject go) where T : Component
	{
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		return t;
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0000A669 File Offset: 0x00008869
	public static Vector3[] GetSides(this Camera cam)
	{
		return cam.GetSides(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), null);
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x0000A688 File Offset: 0x00008888
	public static Vector3[] GetSides(this Camera cam, float depth)
	{
		return cam.GetSides(depth, null);
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x0000A692 File Offset: 0x00008892
	public static Vector3[] GetSides(this Camera cam, Transform relativeTo)
	{
		return cam.GetSides(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), relativeTo);
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0007FCAC File Offset: 0x0007DEAC
	public static Vector3[] GetSides(this Camera cam, float depth, Transform relativeTo)
	{
		Rect rect = cam.rect;
		Vector2 screenSize = NGUITools.screenSize;
		float num = -0.5f;
		float num2 = 0.5f;
		float num3 = -0.5f;
		float num4 = 0.5f;
		float num5 = rect.width / rect.height;
		num *= num5;
		num2 *= num5;
		num *= screenSize.x;
		num2 *= screenSize.x;
		num3 *= screenSize.y;
		num4 *= screenSize.y;
		Transform transform = cam.transform;
		NGUITools.mSides[0] = transform.TransformPoint(new Vector3(num, 0f, depth));
		NGUITools.mSides[1] = transform.TransformPoint(new Vector3(0f, num4, depth));
		NGUITools.mSides[2] = transform.TransformPoint(new Vector3(num2, 0f, depth));
		NGUITools.mSides[3] = transform.TransformPoint(new Vector3(0f, num3, depth));
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				NGUITools.mSides[i] = relativeTo.InverseTransformPoint(NGUITools.mSides[i]);
			}
		}
		return NGUITools.mSides;
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0000A6B1 File Offset: 0x000088B1
	public static Vector3[] GetWorldCorners(this Camera cam)
	{
		return cam.GetWorldCorners(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), null);
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0000A6D0 File Offset: 0x000088D0
	public static Vector3[] GetWorldCorners(this Camera cam, float depth)
	{
		return cam.GetWorldCorners(depth, null);
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0000A6DA File Offset: 0x000088DA
	public static Vector3[] GetWorldCorners(this Camera cam, Transform relativeTo)
	{
		return cam.GetWorldCorners(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), relativeTo);
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0007FDE4 File Offset: 0x0007DFE4
	public static Vector3[] GetWorldCorners(this Camera cam, float depth, Transform relativeTo)
	{
		Rect rect = cam.rect;
		Vector2 screenSize = NGUITools.screenSize;
		float num = -0.5f;
		float num2 = 0.5f;
		float num3 = -0.5f;
		float num4 = 0.5f;
		float num5 = rect.width / rect.height;
		num *= num5;
		num2 *= num5;
		num *= screenSize.x;
		num2 *= screenSize.x;
		num3 *= screenSize.y;
		num4 *= screenSize.y;
		Transform transform = cam.transform;
		NGUITools.mSides[0] = transform.TransformPoint(new Vector3(num, num3, depth));
		NGUITools.mSides[1] = transform.TransformPoint(new Vector3(num, num4, depth));
		NGUITools.mSides[2] = transform.TransformPoint(new Vector3(num2, num4, depth));
		NGUITools.mSides[3] = transform.TransformPoint(new Vector3(num2, num3, depth));
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				NGUITools.mSides[i] = relativeTo.InverseTransformPoint(NGUITools.mSides[i]);
			}
		}
		return NGUITools.mSides;
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x0007FF0C File Offset: 0x0007E10C
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

	// Token: 0x060007B6 RID: 1974 RVA: 0x0007FF5C File Offset: 0x0007E15C
	public static void Execute<T>(GameObject go, string funcName) where T : Component
	{
		foreach (T t in go.GetComponents<T>())
		{
			MethodInfo method = t.GetType().GetMethod(funcName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method != null)
			{
				method.Invoke(t, null);
			}
		}
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0007FFB4 File Offset: 0x0007E1B4
	public static void ExecuteAll<T>(GameObject root, string funcName) where T : Component
	{
		NGUITools.Execute<T>(root, funcName);
		Transform transform = root.transform;
		int i = 0;
		int childCount = transform.childCount;
		while (i < childCount)
		{
			NGUITools.ExecuteAll<T>(transform.GetChild(i).gameObject, funcName);
			i++;
		}
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0000A6F9 File Offset: 0x000088F9
	public static void ImmediatelyCreateDrawCalls(GameObject root)
	{
		NGUITools.ExecuteAll<UIWidget>(root, "Start");
		NGUITools.ExecuteAll<UIPanel>(root, "Start");
		NGUITools.ExecuteAll<UIWidget>(root, "Update");
		NGUITools.ExecuteAll<UIPanel>(root, "Update");
		NGUITools.ExecuteAll<UIPanel>(root, "LateUpdate");
	}

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0000A732 File Offset: 0x00008932
	public static Vector2 screenSize
	{
		get
		{
			return new Vector2((float)Screen.width, (float)Screen.height);
		}
	}

	// Token: 0x04000566 RID: 1382
	private static AudioListener mListener;

	// Token: 0x04000567 RID: 1383
	private static bool mLoaded = false;

	// Token: 0x04000568 RID: 1384
	private static float mGlobalVolume = 1f;

	// Token: 0x04000569 RID: 1385
	private static Vector3[] mSides = new Vector3[4];
}
