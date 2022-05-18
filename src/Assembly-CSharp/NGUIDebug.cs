using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BC RID: 188
[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
	// Token: 0x170000DD RID: 221
	// (get) Token: 0x0600070C RID: 1804 RVA: 0x0000A0FD File Offset: 0x000082FD
	// (set) Token: 0x0600070D RID: 1805 RVA: 0x0000A104 File Offset: 0x00008304
	public static bool debugRaycast
	{
		get
		{
			return NGUIDebug.mRayDebug;
		}
		set
		{
			if (Application.isPlaying)
			{
				NGUIDebug.mRayDebug = value;
				if (value)
				{
					NGUIDebug.CreateInstance();
				}
			}
		}
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0000A11B File Offset: 0x0000831B
	public static void CreateInstance()
	{
		if (NGUIDebug.mInstance == null)
		{
			GameObject gameObject = new GameObject("_NGUI Debug");
			NGUIDebug.mInstance = gameObject.AddComponent<NGUIDebug>();
			Object.DontDestroyOnLoad(gameObject);
		}
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0000A144 File Offset: 0x00008344
	private static void LogString(string text)
	{
		if (Application.isPlaying)
		{
			if (NGUIDebug.mLines.Count > 20)
			{
				NGUIDebug.mLines.RemoveAt(0);
			}
			NGUIDebug.mLines.Add(text);
			NGUIDebug.CreateInstance();
			return;
		}
		Debug.Log(text);
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0007A39C File Offset: 0x0007859C
	public static void Log(params object[] objs)
	{
		string text = "";
		for (int i = 0; i < objs.Length; i++)
		{
			if (i == 0)
			{
				text += objs[i].ToString();
			}
			else
			{
				text = text + ", " + objs[i].ToString();
			}
		}
		NGUIDebug.LogString(text);
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0007A3EC File Offset: 0x000785EC
	public static void DrawBounds(Bounds b)
	{
		Vector3 center = b.center;
		Vector3 vector = b.center - b.extents;
		Vector3 vector2 = b.center + b.extents;
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector2.x, vector.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector2.x, vector.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector2.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x0007A50C File Offset: 0x0007870C
	private void OnGUI()
	{
		if (NGUIDebug.mLines.Count == 0)
		{
			if (NGUIDebug.mRayDebug && UICamera.hoveredObject != null && Application.isPlaying)
			{
				GUILayout.Label("Last Hit: " + NGUITools.GetHierarchy(UICamera.hoveredObject).Replace("\"", ""), Array.Empty<GUILayoutOption>());
				return;
			}
		}
		else
		{
			int i = 0;
			int count = NGUIDebug.mLines.Count;
			while (i < count)
			{
				GUILayout.Label(NGUIDebug.mLines[i], Array.Empty<GUILayoutOption>());
				i++;
			}
		}
	}

	// Token: 0x04000531 RID: 1329
	private static bool mRayDebug = false;

	// Token: 0x04000532 RID: 1330
	private static List<string> mLines = new List<string>();

	// Token: 0x04000533 RID: 1331
	private static NGUIDebug mInstance = null;
}
