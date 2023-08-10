using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Draw Call")]
public class UIDrawCall : MonoBehaviour
{
	public enum Clipping
	{
		None = 0,
		SoftClip = 3,
		ConstrainButDontClip = 4
	}

	private static BetterList<UIDrawCall> mActiveList = new BetterList<UIDrawCall>();

	private static BetterList<UIDrawCall> mInactiveList = new BetterList<UIDrawCall>();

	[NonSerialized]
	[HideInInspector]
	public int depthStart = int.MaxValue;

	[NonSerialized]
	[HideInInspector]
	public int depthEnd = int.MinValue;

	[NonSerialized]
	[HideInInspector]
	public UIPanel manager;

	[NonSerialized]
	[HideInInspector]
	public UIPanel panel;

	[NonSerialized]
	[HideInInspector]
	public bool alwaysOnScreen;

	[NonSerialized]
	[HideInInspector]
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	[NonSerialized]
	[HideInInspector]
	public BetterList<Vector3> norms = new BetterList<Vector3>();

	[NonSerialized]
	[HideInInspector]
	public BetterList<Vector4> tans = new BetterList<Vector4>();

	[NonSerialized]
	[HideInInspector]
	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	[NonSerialized]
	[HideInInspector]
	public BetterList<Color32> cols = new BetterList<Color32>();

	private Material mMaterial;

	private Texture mTexture;

	private Shader mShader;

	private int mClipCount;

	private Transform mTrans;

	private Mesh mMesh;

	private MeshFilter mFilter;

	private MeshRenderer mRenderer;

	private Material mDynamicMat;

	private int[] mIndices;

	private bool mRebuildMat = true;

	private bool mLegacyShader;

	private int mRenderQueue = 3000;

	private int mTriangles;

	[NonSerialized]
	public bool isDirty;

	private const int maxIndexBufferCache = 10;

	private static List<int[]> mCache = new List<int[]>(10);

	private static int[] ClipRange = new int[4]
	{
		Shader.PropertyToID("_ClipRange0"),
		Shader.PropertyToID("_ClipRange1"),
		Shader.PropertyToID("_ClipRange2"),
		Shader.PropertyToID("_ClipRange4")
	};

	private static int[] ClipArgs = new int[4]
	{
		Shader.PropertyToID("_ClipArgs0"),
		Shader.PropertyToID("_ClipArgs1"),
		Shader.PropertyToID("_ClipArgs2"),
		Shader.PropertyToID("_ClipArgs3")
	};

	[Obsolete("Use UIDrawCall.activeList")]
	public static BetterList<UIDrawCall> list => mActiveList;

	public static BetterList<UIDrawCall> activeList => mActiveList;

	public static BetterList<UIDrawCall> inactiveList => mInactiveList;

	public int renderQueue
	{
		get
		{
			return mRenderQueue;
		}
		set
		{
			if (mRenderQueue != value)
			{
				mRenderQueue = value;
				if ((Object)(object)mDynamicMat != (Object)null)
				{
					mDynamicMat.renderQueue = value;
				}
			}
		}
	}

	public int sortingOrder
	{
		get
		{
			if (!((Object)(object)mRenderer != (Object)null))
			{
				return 0;
			}
			return ((Renderer)mRenderer).sortingOrder;
		}
		set
		{
			if ((Object)(object)mRenderer != (Object)null && ((Renderer)mRenderer).sortingOrder != value)
			{
				((Renderer)mRenderer).sortingOrder = value;
			}
		}
	}

	public int finalRenderQueue
	{
		get
		{
			if (!((Object)(object)mDynamicMat != (Object)null))
			{
				return mRenderQueue;
			}
			return mDynamicMat.renderQueue;
		}
	}

	public Transform cachedTransform
	{
		get
		{
			if ((Object)(object)mTrans == (Object)null)
			{
				mTrans = ((Component)this).transform;
			}
			return mTrans;
		}
	}

	public Material baseMaterial
	{
		get
		{
			return mMaterial;
		}
		set
		{
			if ((Object)(object)mMaterial != (Object)(object)value)
			{
				mMaterial = value;
				mRebuildMat = true;
			}
		}
	}

	public Material dynamicMaterial => mDynamicMat;

	public Texture mainTexture
	{
		get
		{
			return mTexture;
		}
		set
		{
			mTexture = value;
			if ((Object)(object)mDynamicMat != (Object)null)
			{
				mDynamicMat.mainTexture = value;
			}
		}
	}

	public Shader shader
	{
		get
		{
			return mShader;
		}
		set
		{
			if ((Object)(object)mShader != (Object)(object)value)
			{
				mShader = value;
				mRebuildMat = true;
			}
		}
	}

	public int triangles
	{
		get
		{
			if (!((Object)(object)mMesh != (Object)null))
			{
				return 0;
			}
			return mTriangles;
		}
	}

	public bool isClipped => mClipCount != 0;

	private void CreateMaterial()
	{
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Expected O, but got Unknown
		mLegacyShader = false;
		mClipCount = panel.clipCount;
		string text = (((Object)(object)mShader != (Object)null) ? ((Object)mShader).name : (((Object)(object)mMaterial != (Object)null) ? ((Object)mMaterial.shader).name : "Unlit/Transparent Colored"));
		text = text.Replace("GUI/Text Shader", "Unlit/Text");
		if (text.Length > 2 && text[text.Length - 2] == ' ')
		{
			int num = text[text.Length - 1];
			if (num > 48 && num <= 57)
			{
				text = text.Substring(0, text.Length - 2);
			}
		}
		if (text.StartsWith("Hidden/"))
		{
			text = text.Substring(7);
		}
		text = text.Replace(" (SoftClip)", "");
		if (mClipCount != 0)
		{
			shader = Shader.Find("Hidden/" + text + " " + mClipCount);
			if ((Object)(object)shader == (Object)null)
			{
				Shader.Find(text + " " + mClipCount);
			}
			if ((Object)(object)shader == (Object)null && mClipCount == 1)
			{
				mLegacyShader = true;
				shader = Shader.Find(text + " (SoftClip)");
			}
		}
		else
		{
			shader = Shader.Find(text);
		}
		if ((Object)(object)mMaterial != (Object)null)
		{
			mDynamicMat = new Material(mMaterial);
			((Object)mDynamicMat).hideFlags = (HideFlags)60;
			mDynamicMat.CopyPropertiesFromMaterial(mMaterial);
			string[] shaderKeywords = mMaterial.shaderKeywords;
			for (int i = 0; i < shaderKeywords.Length; i++)
			{
				mDynamicMat.EnableKeyword(shaderKeywords[i]);
			}
			if ((Object)(object)shader != (Object)null)
			{
				mDynamicMat.shader = shader;
			}
			else if (mClipCount != 0)
			{
				Debug.LogError((object)(text + " shader doesn't have a clipped shader version for " + mClipCount + " clip regions"));
			}
		}
		else
		{
			mDynamicMat = new Material(shader);
			((Object)mDynamicMat).hideFlags = (HideFlags)60;
		}
	}

	private Material RebuildMaterial()
	{
		NGUITools.DestroyImmediate((Object)(object)mDynamicMat);
		CreateMaterial();
		mDynamicMat.renderQueue = mRenderQueue;
		if ((Object)(object)mTexture != (Object)null)
		{
			mDynamicMat.mainTexture = mTexture;
		}
		if ((Object)(object)mRenderer != (Object)null)
		{
			((Renderer)mRenderer).sharedMaterials = (Material[])(object)new Material[1] { mDynamicMat };
		}
		return mDynamicMat;
	}

	private void UpdateMaterials()
	{
		if (mRebuildMat || (Object)(object)mDynamicMat == (Object)null || mClipCount != panel.clipCount)
		{
			RebuildMaterial();
			mRebuildMat = false;
		}
		else if ((Object)(object)((Renderer)mRenderer).sharedMaterial != (Object)(object)mDynamicMat)
		{
			((Renderer)mRenderer).sharedMaterials = (Material[])(object)new Material[1] { mDynamicMat };
		}
	}

	public void UpdateGeometry()
	{
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		int size = verts.size;
		if (size > 0 && size == uvs.size && size == cols.size && size % 4 == 0)
		{
			if ((Object)(object)mFilter == (Object)null)
			{
				mFilter = ((Component)this).gameObject.GetComponent<MeshFilter>();
			}
			if ((Object)(object)mFilter == (Object)null)
			{
				mFilter = ((Component)this).gameObject.AddComponent<MeshFilter>();
			}
			if (verts.size < 65000)
			{
				int num = (size >> 1) * 3;
				bool flag = mIndices == null || mIndices.Length != num;
				if ((Object)(object)mMesh == (Object)null)
				{
					mMesh = new Mesh();
					((Object)mMesh).hideFlags = (HideFlags)52;
					((Object)mMesh).name = (((Object)(object)mMaterial != (Object)null) ? ((Object)mMaterial).name : "Mesh");
					mMesh.MarkDynamic();
					flag = true;
				}
				bool flag2 = uvs.buffer.Length != verts.buffer.Length || cols.buffer.Length != verts.buffer.Length || (norms.buffer != null && norms.buffer.Length != verts.buffer.Length) || (tans.buffer != null && tans.buffer.Length != verts.buffer.Length);
				if (!flag2 && panel.renderQueue != 0)
				{
					flag2 = (Object)(object)mMesh == (Object)null || mMesh.vertexCount != verts.buffer.Length;
				}
				if (!flag2 && verts.size << 1 < verts.buffer.Length)
				{
					flag2 = true;
				}
				mTriangles = verts.size >> 1;
				if (flag2 || verts.buffer.Length > 65000)
				{
					if (flag2 || mMesh.vertexCount != verts.size)
					{
						mMesh.Clear();
						flag = true;
					}
					mMesh.vertices = verts.ToArray();
					mMesh.uv = uvs.ToArray();
					mMesh.colors32 = cols.ToArray();
					if (norms != null)
					{
						mMesh.normals = norms.ToArray();
					}
					if (tans != null)
					{
						mMesh.tangents = tans.ToArray();
					}
				}
				else
				{
					if (mMesh.vertexCount != verts.buffer.Length)
					{
						mMesh.Clear();
						flag = true;
					}
					mMesh.vertices = verts.buffer;
					mMesh.uv = uvs.buffer;
					mMesh.colors32 = cols.buffer;
					if (norms != null)
					{
						mMesh.normals = norms.buffer;
					}
					if (tans != null)
					{
						mMesh.tangents = tans.buffer;
					}
				}
				if (flag)
				{
					mIndices = GenerateCachedIndexBuffer(size, num);
					mMesh.triangles = mIndices;
				}
				if (flag2 || !alwaysOnScreen)
				{
					mMesh.RecalculateBounds();
				}
				mFilter.mesh = mMesh;
			}
			else
			{
				mTriangles = 0;
				if ((Object)(object)mFilter.mesh != (Object)null)
				{
					mFilter.mesh.Clear();
				}
				Debug.LogError((object)("Too many vertices on one panel: " + verts.size));
			}
			if ((Object)(object)mRenderer == (Object)null)
			{
				mRenderer = ((Component)this).gameObject.GetComponent<MeshRenderer>();
			}
			if ((Object)(object)mRenderer == (Object)null)
			{
				mRenderer = ((Component)this).gameObject.AddComponent<MeshRenderer>();
			}
			UpdateMaterials();
		}
		else
		{
			if ((Object)(object)mFilter.mesh != (Object)null)
			{
				mFilter.mesh.Clear();
			}
			Debug.LogError((object)("UIWidgets must fill the buffer with 4 vertices per quad. Found " + size));
		}
		verts.Clear();
		uvs.Clear();
		cols.Clear();
		norms.Clear();
		tans.Clear();
	}

	private int[] GenerateCachedIndexBuffer(int vertexCount, int indexCount)
	{
		int i = 0;
		for (int count = mCache.Count; i < count; i++)
		{
			int[] array = mCache[i];
			if (array != null && array.Length == indexCount)
			{
				return array;
			}
		}
		int[] array2 = new int[indexCount];
		int num = 0;
		for (int j = 0; j < vertexCount; j += 4)
		{
			array2[num++] = j;
			array2[num++] = j + 1;
			array2[num++] = j + 2;
			array2[num++] = j + 2;
			array2[num++] = j + 3;
			array2[num++] = j;
		}
		if (mCache.Count > 10)
		{
			mCache.RemoveAt(0);
		}
		mCache.Add(array2);
		return array2;
	}

	private void OnWillRenderObject()
	{
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		UpdateMaterials();
		if ((Object)(object)mDynamicMat == (Object)null || mClipCount == 0)
		{
			return;
		}
		if (!mLegacyShader)
		{
			UIPanel parentPanel = panel;
			int num = 0;
			while ((Object)(object)parentPanel != (Object)null)
			{
				if (parentPanel.hasClipping)
				{
					float angle = 0f;
					Vector4 drawCallClipRange = parentPanel.drawCallClipRange;
					if ((Object)(object)parentPanel != (Object)(object)panel)
					{
						Vector3 val = parentPanel.cachedTransform.InverseTransformPoint(panel.cachedTransform.position);
						drawCallClipRange.x -= val.x;
						drawCallClipRange.y -= val.y;
						Quaternion rotation = panel.cachedTransform.rotation;
						Vector3 eulerAngles = ((Quaternion)(ref rotation)).eulerAngles;
						rotation = parentPanel.cachedTransform.rotation;
						Vector3 val2 = ((Quaternion)(ref rotation)).eulerAngles - eulerAngles;
						val2.x = NGUIMath.WrapAngle(val2.x);
						val2.y = NGUIMath.WrapAngle(val2.y);
						val2.z = NGUIMath.WrapAngle(val2.z);
						if (Mathf.Abs(val2.x) > 0.001f || Mathf.Abs(val2.y) > 0.001f)
						{
							Debug.LogWarning((object)"Panel can only be clipped properly if X and Y rotation is left at 0", (Object)(object)panel);
						}
						angle = val2.z;
					}
					SetClipping(num++, drawCallClipRange, parentPanel.clipSoftness, angle);
				}
				parentPanel = parentPanel.parentPanel;
			}
		}
		else
		{
			Vector2 clipSoftness = panel.clipSoftness;
			Vector4 drawCallClipRange2 = panel.drawCallClipRange;
			Vector2 mainTextureOffset = default(Vector2);
			((Vector2)(ref mainTextureOffset))._002Ector((0f - drawCallClipRange2.x) / drawCallClipRange2.z, (0f - drawCallClipRange2.y) / drawCallClipRange2.w);
			Vector2 mainTextureScale = default(Vector2);
			((Vector2)(ref mainTextureScale))._002Ector(1f / drawCallClipRange2.z, 1f / drawCallClipRange2.w);
			Vector2 val3 = default(Vector2);
			((Vector2)(ref val3))._002Ector(1000f, 1000f);
			if (clipSoftness.x > 0f)
			{
				val3.x = drawCallClipRange2.z / clipSoftness.x;
			}
			if (clipSoftness.y > 0f)
			{
				val3.y = drawCallClipRange2.w / clipSoftness.y;
			}
			mDynamicMat.mainTextureOffset = mainTextureOffset;
			mDynamicMat.mainTextureScale = mainTextureScale;
			mDynamicMat.SetVector("_ClipSharpness", Vector4.op_Implicit(val3));
		}
	}

	private void SetClipping(int index, Vector4 cr, Vector2 soft, float angle)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		angle *= -(float)Math.PI / 180f;
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(1000f, 1000f);
		if (soft.x > 0f)
		{
			val.x = cr.z / soft.x;
		}
		if (soft.y > 0f)
		{
			val.y = cr.w / soft.y;
		}
		if (index < ClipRange.Length)
		{
			mDynamicMat.SetVector(ClipRange[index], new Vector4((0f - cr.x) / cr.z, (0f - cr.y) / cr.w, 1f / cr.z, 1f / cr.w));
			mDynamicMat.SetVector(ClipArgs[index], new Vector4(val.x, val.y, Mathf.Sin(angle), Mathf.Cos(angle)));
		}
	}

	private void OnEnable()
	{
		mRebuildMat = true;
	}

	private void OnDisable()
	{
		depthStart = int.MaxValue;
		depthEnd = int.MinValue;
		panel = null;
		manager = null;
		mMaterial = null;
		mTexture = null;
		NGUITools.DestroyImmediate((Object)(object)mDynamicMat);
		mDynamicMat = null;
		if ((Object)(object)mRenderer != (Object)null)
		{
			((Renderer)mRenderer).sharedMaterials = (Material[])(object)new Material[0];
		}
	}

	private void OnDestroy()
	{
		NGUITools.DestroyImmediate((Object)(object)mMesh);
	}

	public static UIDrawCall Create(UIPanel panel, Material mat, Texture tex, Shader shader)
	{
		return Create(null, panel, mat, tex, shader);
	}

	private static UIDrawCall Create(string name, UIPanel pan, Material mat, Texture tex, Shader shader)
	{
		UIDrawCall uIDrawCall = Create(name);
		((Component)uIDrawCall).gameObject.layer = pan.cachedGameObject.layer;
		uIDrawCall.baseMaterial = mat;
		uIDrawCall.mainTexture = tex;
		uIDrawCall.shader = shader;
		uIDrawCall.renderQueue = pan.startingRenderQueue;
		uIDrawCall.sortingOrder = pan.sortingOrder;
		uIDrawCall.manager = pan;
		return uIDrawCall;
	}

	private static UIDrawCall Create(string name)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		if (mInactiveList.size > 0)
		{
			UIDrawCall uIDrawCall = mInactiveList.Pop();
			mActiveList.Add(uIDrawCall);
			if (name != null)
			{
				((Object)uIDrawCall).name = name;
			}
			NGUITools.SetActive(((Component)uIDrawCall).gameObject, state: true);
			return uIDrawCall;
		}
		GameObject val = new GameObject(name);
		Object.DontDestroyOnLoad((Object)val);
		UIDrawCall uIDrawCall2 = val.AddComponent<UIDrawCall>();
		mActiveList.Add(uIDrawCall2);
		return uIDrawCall2;
	}

	public static void ClearAll()
	{
		bool isPlaying = Application.isPlaying;
		int num = mActiveList.size;
		while (num > 0)
		{
			UIDrawCall uIDrawCall = mActiveList[--num];
			if (Object.op_Implicit((Object)(object)uIDrawCall))
			{
				if (isPlaying)
				{
					NGUITools.SetActive(((Component)uIDrawCall).gameObject, state: false);
				}
				else
				{
					NGUITools.DestroyImmediate((Object)(object)((Component)uIDrawCall).gameObject);
				}
			}
		}
		mActiveList.Clear();
	}

	public static void ReleaseAll()
	{
		ClearAll();
		ReleaseInactive();
	}

	public static void ReleaseInactive()
	{
		int num = mInactiveList.size;
		while (num > 0)
		{
			UIDrawCall uIDrawCall = mInactiveList[--num];
			if (Object.op_Implicit((Object)(object)uIDrawCall))
			{
				NGUITools.DestroyImmediate((Object)(object)((Component)uIDrawCall).gameObject);
			}
		}
		mInactiveList.Clear();
	}

	public static int Count(UIPanel panel)
	{
		int num = 0;
		for (int i = 0; i < mActiveList.size; i++)
		{
			if ((Object)(object)mActiveList[i].manager == (Object)(object)panel)
			{
				num++;
			}
		}
		return num;
	}

	public static void Destroy(UIDrawCall dc)
	{
		if (!Object.op_Implicit((Object)(object)dc))
		{
			return;
		}
		if (Application.isPlaying)
		{
			if (mActiveList.Remove(dc))
			{
				NGUITools.SetActive(((Component)dc).gameObject, state: false);
				mInactiveList.Add(dc);
			}
		}
		else
		{
			mActiveList.Remove(dc);
			NGUITools.DestroyImmediate((Object)(object)((Component)dc).gameObject);
		}
	}
}
