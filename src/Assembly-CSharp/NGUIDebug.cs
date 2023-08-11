using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
	private static bool mRayDebug = false;

	private static List<string> mLines = new List<string>();

	private static NGUIDebug mInstance = null;

	public static bool debugRaycast
	{
		get
		{
			return mRayDebug;
		}
		set
		{
			if (Application.isPlaying)
			{
				mRayDebug = value;
				if (value)
				{
					CreateInstance();
				}
			}
		}
	}

	public static void CreateInstance()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		if ((Object)(object)mInstance == (Object)null)
		{
			GameObject val = new GameObject("_NGUI Debug");
			mInstance = val.AddComponent<NGUIDebug>();
			Object.DontDestroyOnLoad((Object)val);
		}
	}

	private static void LogString(string text)
	{
		if (Application.isPlaying)
		{
			if (mLines.Count > 20)
			{
				mLines.RemoveAt(0);
			}
			mLines.Add(text);
			CreateInstance();
		}
		else
		{
			Debug.Log((object)text);
		}
	}

	public static void Log(params object[] objs)
	{
		string text = "";
		for (int i = 0; i < objs.Length; i++)
		{
			text = ((i != 0) ? (text + ", " + objs[i].ToString()) : (text + objs[i].ToString()));
		}
		LogString(text);
	}

	public static void DrawBounds(Bounds b)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		Vector3 center = ((Bounds)(ref b)).center;
		Vector3 val = ((Bounds)(ref b)).center - ((Bounds)(ref b)).extents;
		Vector3 val2 = ((Bounds)(ref b)).center + ((Bounds)(ref b)).extents;
		Debug.DrawLine(new Vector3(val.x, val.y, center.z), new Vector3(val2.x, val.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(val.x, val.y, center.z), new Vector3(val.x, val2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(val2.x, val.y, center.z), new Vector3(val2.x, val2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(val.x, val2.y, center.z), new Vector3(val2.x, val2.y, center.z), Color.red);
	}

	private void OnGUI()
	{
		if (mLines.Count == 0)
		{
			if (mRayDebug && (Object)(object)UICamera.hoveredObject != (Object)null && Application.isPlaying)
			{
				GUILayout.Label("Last Hit: " + NGUITools.GetHierarchy(UICamera.hoveredObject).Replace("\"", ""), Array.Empty<GUILayoutOption>());
			}
			return;
		}
		int i = 0;
		for (int count = mLines.Count; i < count; i++)
		{
			GUILayout.Label(mLines[i], Array.Empty<GUILayoutOption>());
		}
	}
}
