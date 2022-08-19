using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000084 RID: 132
[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x0600068A RID: 1674 RVA: 0x00024CC9 File Offset: 0x00022EC9
	// (set) Token: 0x0600068B RID: 1675 RVA: 0x00024CD0 File Offset: 0x00022ED0
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

	// Token: 0x0600068C RID: 1676 RVA: 0x00024CE7 File Offset: 0x00022EE7
	public static void CreateInstance()
	{
		if (NGUIDebug.mInstance == null)
		{
			GameObject gameObject = new GameObject("_NGUI Debug");
			NGUIDebug.mInstance = gameObject.AddComponent<NGUIDebug>();
			Object.DontDestroyOnLoad(gameObject);
		}
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x00024D10 File Offset: 0x00022F10
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

	// Token: 0x0600068E RID: 1678 RVA: 0x00024D4C File Offset: 0x00022F4C
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

	// Token: 0x0600068F RID: 1679 RVA: 0x00024D9C File Offset: 0x00022F9C
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

	// Token: 0x06000690 RID: 1680 RVA: 0x00024EBC File Offset: 0x000230BC
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

	// Token: 0x04000457 RID: 1111
	private static bool mRayDebug = false;

	// Token: 0x04000458 RID: 1112
	private static List<string> mLines = new List<string>();

	// Token: 0x04000459 RID: 1113
	private static NGUIDebug mInstance = null;
}
