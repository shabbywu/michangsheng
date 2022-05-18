using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CF RID: 207
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Draw Call")]
public class UIDrawCall : MonoBehaviour
{
	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x06000802 RID: 2050 RVA: 0x0000AA19 File Offset: 0x00008C19
	[Obsolete("Use UIDrawCall.activeList")]
	public static BetterList<UIDrawCall> list
	{
		get
		{
			return UIDrawCall.mActiveList;
		}
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x06000803 RID: 2051 RVA: 0x0000AA19 File Offset: 0x00008C19
	public static BetterList<UIDrawCall> activeList
	{
		get
		{
			return UIDrawCall.mActiveList;
		}
	}

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x06000804 RID: 2052 RVA: 0x0000AA20 File Offset: 0x00008C20
	public static BetterList<UIDrawCall> inactiveList
	{
		get
		{
			return UIDrawCall.mInactiveList;
		}
	}

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x06000805 RID: 2053 RVA: 0x0000AA27 File Offset: 0x00008C27
	// (set) Token: 0x06000806 RID: 2054 RVA: 0x0000AA2F File Offset: 0x00008C2F
	public int renderQueue
	{
		get
		{
			return this.mRenderQueue;
		}
		set
		{
			if (this.mRenderQueue != value)
			{
				this.mRenderQueue = value;
				if (this.mDynamicMat != null)
				{
					this.mDynamicMat.renderQueue = value;
				}
			}
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x06000807 RID: 2055 RVA: 0x0000AA5B File Offset: 0x00008C5B
	// (set) Token: 0x06000808 RID: 2056 RVA: 0x0000AA78 File Offset: 0x00008C78
	public int sortingOrder
	{
		get
		{
			if (!(this.mRenderer != null))
			{
				return 0;
			}
			return this.mRenderer.sortingOrder;
		}
		set
		{
			if (this.mRenderer != null && this.mRenderer.sortingOrder != value)
			{
				this.mRenderer.sortingOrder = value;
			}
		}
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x06000809 RID: 2057 RVA: 0x0000AAA2 File Offset: 0x00008CA2
	public int finalRenderQueue
	{
		get
		{
			if (!(this.mDynamicMat != null))
			{
				return this.mRenderQueue;
			}
			return this.mDynamicMat.renderQueue;
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x0600080A RID: 2058 RVA: 0x0000AAC4 File Offset: 0x00008CC4
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x0600080B RID: 2059 RVA: 0x0000AAE6 File Offset: 0x00008CE6
	// (set) Token: 0x0600080C RID: 2060 RVA: 0x0000AAEE File Offset: 0x00008CEE
	public Material baseMaterial
	{
		get
		{
			return this.mMaterial;
		}
		set
		{
			if (this.mMaterial != value)
			{
				this.mMaterial = value;
				this.mRebuildMat = true;
			}
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x0600080D RID: 2061 RVA: 0x0000AB0C File Offset: 0x00008D0C
	public Material dynamicMaterial
	{
		get
		{
			return this.mDynamicMat;
		}
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x0600080E RID: 2062 RVA: 0x0000AB14 File Offset: 0x00008D14
	// (set) Token: 0x0600080F RID: 2063 RVA: 0x0000AB1C File Offset: 0x00008D1C
	public Texture mainTexture
	{
		get
		{
			return this.mTexture;
		}
		set
		{
			this.mTexture = value;
			if (this.mDynamicMat != null)
			{
				this.mDynamicMat.mainTexture = value;
			}
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000810 RID: 2064 RVA: 0x0000AB3F File Offset: 0x00008D3F
	// (set) Token: 0x06000811 RID: 2065 RVA: 0x0000AB47 File Offset: 0x00008D47
	public Shader shader
	{
		get
		{
			return this.mShader;
		}
		set
		{
			if (this.mShader != value)
			{
				this.mShader = value;
				this.mRebuildMat = true;
			}
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06000812 RID: 2066 RVA: 0x0000AB65 File Offset: 0x00008D65
	public int triangles
	{
		get
		{
			if (!(this.mMesh != null))
			{
				return 0;
			}
			return this.mTriangles;
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06000813 RID: 2067 RVA: 0x0000AB7D File Offset: 0x00008D7D
	public bool isClipped
	{
		get
		{
			return this.mClipCount != 0;
		}
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00082B5C File Offset: 0x00080D5C
	private void CreateMaterial()
	{
		this.mLegacyShader = false;
		this.mClipCount = this.panel.clipCount;
		string text = (this.mShader != null) ? this.mShader.name : ((this.mMaterial != null) ? this.mMaterial.shader.name : "Unlit/Transparent Colored");
		text = text.Replace("GUI/Text Shader", "Unlit/Text");
		if (text.Length > 2 && text[text.Length - 2] == ' ')
		{
			int num = (int)text[text.Length - 1];
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
		if (this.mClipCount != 0)
		{
			this.shader = Shader.Find(string.Concat(new object[]
			{
				"Hidden/",
				text,
				" ",
				this.mClipCount
			}));
			if (this.shader == null)
			{
				Shader.Find(text + " " + this.mClipCount);
			}
			if (this.shader == null && this.mClipCount == 1)
			{
				this.mLegacyShader = true;
				this.shader = Shader.Find(text + " (SoftClip)");
			}
		}
		else
		{
			this.shader = Shader.Find(text);
		}
		if (this.mMaterial != null)
		{
			this.mDynamicMat = new Material(this.mMaterial);
			this.mDynamicMat.hideFlags = 60;
			this.mDynamicMat.CopyPropertiesFromMaterial(this.mMaterial);
			string[] shaderKeywords = this.mMaterial.shaderKeywords;
			for (int i = 0; i < shaderKeywords.Length; i++)
			{
				this.mDynamicMat.EnableKeyword(shaderKeywords[i]);
			}
			if (this.shader != null)
			{
				this.mDynamicMat.shader = this.shader;
				return;
			}
			if (this.mClipCount != 0)
			{
				Debug.LogError(string.Concat(new object[]
				{
					text,
					" shader doesn't have a clipped shader version for ",
					this.mClipCount,
					" clip regions"
				}));
				return;
			}
		}
		else
		{
			this.mDynamicMat = new Material(this.shader);
			this.mDynamicMat.hideFlags = 60;
		}
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x00082DD0 File Offset: 0x00080FD0
	private Material RebuildMaterial()
	{
		NGUITools.DestroyImmediate(this.mDynamicMat);
		this.CreateMaterial();
		this.mDynamicMat.renderQueue = this.mRenderQueue;
		if (this.mTexture != null)
		{
			this.mDynamicMat.mainTexture = this.mTexture;
		}
		if (this.mRenderer != null)
		{
			this.mRenderer.sharedMaterials = new Material[]
			{
				this.mDynamicMat
			};
		}
		return this.mDynamicMat;
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00082E4C File Offset: 0x0008104C
	private void UpdateMaterials()
	{
		if (this.mRebuildMat || this.mDynamicMat == null || this.mClipCount != this.panel.clipCount)
		{
			this.RebuildMaterial();
			this.mRebuildMat = false;
			return;
		}
		if (this.mRenderer.sharedMaterial != this.mDynamicMat)
		{
			this.mRenderer.sharedMaterials = new Material[]
			{
				this.mDynamicMat
			};
		}
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00082EC4 File Offset: 0x000810C4
	public void UpdateGeometry()
	{
		int size = this.verts.size;
		if (size > 0 && size == this.uvs.size && size == this.cols.size && size % 4 == 0)
		{
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.GetComponent<MeshFilter>();
			}
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (this.verts.size < 65000)
			{
				int num = (size >> 1) * 3;
				bool flag = this.mIndices == null || this.mIndices.Length != num;
				if (this.mMesh == null)
				{
					this.mMesh = new Mesh();
					this.mMesh.hideFlags = 52;
					this.mMesh.name = ((this.mMaterial != null) ? this.mMaterial.name : "Mesh");
					this.mMesh.MarkDynamic();
					flag = true;
				}
				bool flag2 = this.uvs.buffer.Length != this.verts.buffer.Length || this.cols.buffer.Length != this.verts.buffer.Length || (this.norms.buffer != null && this.norms.buffer.Length != this.verts.buffer.Length) || (this.tans.buffer != null && this.tans.buffer.Length != this.verts.buffer.Length);
				if (!flag2 && this.panel.renderQueue != UIPanel.RenderQueue.Automatic)
				{
					flag2 = (this.mMesh == null || this.mMesh.vertexCount != this.verts.buffer.Length);
				}
				if (!flag2 && this.verts.size << 1 < this.verts.buffer.Length)
				{
					flag2 = true;
				}
				this.mTriangles = this.verts.size >> 1;
				if (flag2 || this.verts.buffer.Length > 65000)
				{
					if (flag2 || this.mMesh.vertexCount != this.verts.size)
					{
						this.mMesh.Clear();
						flag = true;
					}
					this.mMesh.vertices = this.verts.ToArray();
					this.mMesh.uv = this.uvs.ToArray();
					this.mMesh.colors32 = this.cols.ToArray();
					if (this.norms != null)
					{
						this.mMesh.normals = this.norms.ToArray();
					}
					if (this.tans != null)
					{
						this.mMesh.tangents = this.tans.ToArray();
					}
				}
				else
				{
					if (this.mMesh.vertexCount != this.verts.buffer.Length)
					{
						this.mMesh.Clear();
						flag = true;
					}
					this.mMesh.vertices = this.verts.buffer;
					this.mMesh.uv = this.uvs.buffer;
					this.mMesh.colors32 = this.cols.buffer;
					if (this.norms != null)
					{
						this.mMesh.normals = this.norms.buffer;
					}
					if (this.tans != null)
					{
						this.mMesh.tangents = this.tans.buffer;
					}
				}
				if (flag)
				{
					this.mIndices = this.GenerateCachedIndexBuffer(size, num);
					this.mMesh.triangles = this.mIndices;
				}
				if (flag2 || !this.alwaysOnScreen)
				{
					this.mMesh.RecalculateBounds();
				}
				this.mFilter.mesh = this.mMesh;
			}
			else
			{
				this.mTriangles = 0;
				if (this.mFilter.mesh != null)
				{
					this.mFilter.mesh.Clear();
				}
				Debug.LogError("Too many vertices on one panel: " + this.verts.size);
			}
			if (this.mRenderer == null)
			{
				this.mRenderer = base.gameObject.GetComponent<MeshRenderer>();
			}
			if (this.mRenderer == null)
			{
				this.mRenderer = base.gameObject.AddComponent<MeshRenderer>();
			}
			this.UpdateMaterials();
		}
		else
		{
			if (this.mFilter.mesh != null)
			{
				this.mFilter.mesh.Clear();
			}
			Debug.LogError("UIWidgets must fill the buffer with 4 vertices per quad. Found " + size);
		}
		this.verts.Clear();
		this.uvs.Clear();
		this.cols.Clear();
		this.norms.Clear();
		this.tans.Clear();
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x0008339C File Offset: 0x0008159C
	private int[] GenerateCachedIndexBuffer(int vertexCount, int indexCount)
	{
		int i = 0;
		int count = UIDrawCall.mCache.Count;
		while (i < count)
		{
			int[] array = UIDrawCall.mCache[i];
			if (array != null && array.Length == indexCount)
			{
				return array;
			}
			i++;
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
		if (UIDrawCall.mCache.Count > 10)
		{
			UIDrawCall.mCache.RemoveAt(0);
		}
		UIDrawCall.mCache.Add(array2);
		return array2;
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00083458 File Offset: 0x00081658
	private void OnWillRenderObject()
	{
		this.UpdateMaterials();
		if (this.mDynamicMat == null || this.mClipCount == 0)
		{
			return;
		}
		if (!this.mLegacyShader)
		{
			UIPanel parentPanel = this.panel;
			int num = 0;
			while (parentPanel != null)
			{
				if (parentPanel.hasClipping)
				{
					float angle = 0f;
					Vector4 drawCallClipRange = parentPanel.drawCallClipRange;
					if (parentPanel != this.panel)
					{
						Vector3 vector = parentPanel.cachedTransform.InverseTransformPoint(this.panel.cachedTransform.position);
						drawCallClipRange.x -= vector.x;
						drawCallClipRange.y -= vector.y;
						Vector3 eulerAngles = this.panel.cachedTransform.rotation.eulerAngles;
						Vector3 vector2 = parentPanel.cachedTransform.rotation.eulerAngles - eulerAngles;
						vector2.x = NGUIMath.WrapAngle(vector2.x);
						vector2.y = NGUIMath.WrapAngle(vector2.y);
						vector2.z = NGUIMath.WrapAngle(vector2.z);
						if (Mathf.Abs(vector2.x) > 0.001f || Mathf.Abs(vector2.y) > 0.001f)
						{
							Debug.LogWarning("Panel can only be clipped properly if X and Y rotation is left at 0", this.panel);
						}
						angle = vector2.z;
					}
					this.SetClipping(num++, drawCallClipRange, parentPanel.clipSoftness, angle);
				}
				parentPanel = parentPanel.parentPanel;
			}
			return;
		}
		Vector2 clipSoftness = this.panel.clipSoftness;
		Vector4 drawCallClipRange2 = this.panel.drawCallClipRange;
		Vector2 mainTextureOffset;
		mainTextureOffset..ctor(-drawCallClipRange2.x / drawCallClipRange2.z, -drawCallClipRange2.y / drawCallClipRange2.w);
		Vector2 mainTextureScale;
		mainTextureScale..ctor(1f / drawCallClipRange2.z, 1f / drawCallClipRange2.w);
		Vector2 vector3;
		vector3..ctor(1000f, 1000f);
		if (clipSoftness.x > 0f)
		{
			vector3.x = drawCallClipRange2.z / clipSoftness.x;
		}
		if (clipSoftness.y > 0f)
		{
			vector3.y = drawCallClipRange2.w / clipSoftness.y;
		}
		this.mDynamicMat.mainTextureOffset = mainTextureOffset;
		this.mDynamicMat.mainTextureScale = mainTextureScale;
		this.mDynamicMat.SetVector("_ClipSharpness", vector3);
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x000836C8 File Offset: 0x000818C8
	private void SetClipping(int index, Vector4 cr, Vector2 soft, float angle)
	{
		angle *= -0.017453292f;
		Vector2 vector;
		vector..ctor(1000f, 1000f);
		if (soft.x > 0f)
		{
			vector.x = cr.z / soft.x;
		}
		if (soft.y > 0f)
		{
			vector.y = cr.w / soft.y;
		}
		if (index < UIDrawCall.ClipRange.Length)
		{
			this.mDynamicMat.SetVector(UIDrawCall.ClipRange[index], new Vector4(-cr.x / cr.z, -cr.y / cr.w, 1f / cr.z, 1f / cr.w));
			this.mDynamicMat.SetVector(UIDrawCall.ClipArgs[index], new Vector4(vector.x, vector.y, Mathf.Sin(angle), Mathf.Cos(angle)));
		}
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x0000AB88 File Offset: 0x00008D88
	private void OnEnable()
	{
		this.mRebuildMat = true;
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x000837B8 File Offset: 0x000819B8
	private void OnDisable()
	{
		this.depthStart = int.MaxValue;
		this.depthEnd = int.MinValue;
		this.panel = null;
		this.manager = null;
		this.mMaterial = null;
		this.mTexture = null;
		NGUITools.DestroyImmediate(this.mDynamicMat);
		this.mDynamicMat = null;
		if (this.mRenderer != null)
		{
			this.mRenderer.sharedMaterials = new Material[0];
		}
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x0000AB91 File Offset: 0x00008D91
	private void OnDestroy()
	{
		NGUITools.DestroyImmediate(this.mMesh);
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x0000AB9E File Offset: 0x00008D9E
	public static UIDrawCall Create(UIPanel panel, Material mat, Texture tex, Shader shader)
	{
		return UIDrawCall.Create(null, panel, mat, tex, shader);
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00083828 File Offset: 0x00081A28
	private static UIDrawCall Create(string name, UIPanel pan, Material mat, Texture tex, Shader shader)
	{
		UIDrawCall uidrawCall = UIDrawCall.Create(name);
		uidrawCall.gameObject.layer = pan.cachedGameObject.layer;
		uidrawCall.baseMaterial = mat;
		uidrawCall.mainTexture = tex;
		uidrawCall.shader = shader;
		uidrawCall.renderQueue = pan.startingRenderQueue;
		uidrawCall.sortingOrder = pan.sortingOrder;
		uidrawCall.manager = pan;
		return uidrawCall;
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00083888 File Offset: 0x00081A88
	private static UIDrawCall Create(string name)
	{
		if (UIDrawCall.mInactiveList.size > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.mInactiveList.Pop();
			UIDrawCall.mActiveList.Add(uidrawCall);
			if (name != null)
			{
				uidrawCall.name = name;
			}
			NGUITools.SetActive(uidrawCall.gameObject, true);
			return uidrawCall;
		}
		GameObject gameObject = new GameObject(name);
		Object.DontDestroyOnLoad(gameObject);
		UIDrawCall uidrawCall2 = gameObject.AddComponent<UIDrawCall>();
		UIDrawCall.mActiveList.Add(uidrawCall2);
		return uidrawCall2;
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x000838F0 File Offset: 0x00081AF0
	public static void ClearAll()
	{
		bool isPlaying = Application.isPlaying;
		int i = UIDrawCall.mActiveList.size;
		while (i > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.mActiveList[--i];
			if (uidrawCall)
			{
				if (isPlaying)
				{
					NGUITools.SetActive(uidrawCall.gameObject, false);
				}
				else
				{
					NGUITools.DestroyImmediate(uidrawCall.gameObject);
				}
			}
		}
		UIDrawCall.mActiveList.Clear();
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x0000ABAA File Offset: 0x00008DAA
	public static void ReleaseAll()
	{
		UIDrawCall.ClearAll();
		UIDrawCall.ReleaseInactive();
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00083954 File Offset: 0x00081B54
	public static void ReleaseInactive()
	{
		int i = UIDrawCall.mInactiveList.size;
		while (i > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.mInactiveList[--i];
			if (uidrawCall)
			{
				NGUITools.DestroyImmediate(uidrawCall.gameObject);
			}
		}
		UIDrawCall.mInactiveList.Clear();
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x000839A0 File Offset: 0x00081BA0
	public static int Count(UIPanel panel)
	{
		int num = 0;
		for (int i = 0; i < UIDrawCall.mActiveList.size; i++)
		{
			if (UIDrawCall.mActiveList[i].manager == panel)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x000839E4 File Offset: 0x00081BE4
	public static void Destroy(UIDrawCall dc)
	{
		if (dc)
		{
			if (Application.isPlaying)
			{
				if (UIDrawCall.mActiveList.Remove(dc))
				{
					NGUITools.SetActive(dc.gameObject, false);
					UIDrawCall.mInactiveList.Add(dc);
					return;
				}
			}
			else
			{
				UIDrawCall.mActiveList.Remove(dc);
				NGUITools.DestroyImmediate(dc.gameObject);
			}
		}
	}

	// Token: 0x040005A8 RID: 1448
	private static BetterList<UIDrawCall> mActiveList = new BetterList<UIDrawCall>();

	// Token: 0x040005A9 RID: 1449
	private static BetterList<UIDrawCall> mInactiveList = new BetterList<UIDrawCall>();

	// Token: 0x040005AA RID: 1450
	[HideInInspector]
	[NonSerialized]
	public int depthStart = int.MaxValue;

	// Token: 0x040005AB RID: 1451
	[HideInInspector]
	[NonSerialized]
	public int depthEnd = int.MinValue;

	// Token: 0x040005AC RID: 1452
	[HideInInspector]
	[NonSerialized]
	public UIPanel manager;

	// Token: 0x040005AD RID: 1453
	[HideInInspector]
	[NonSerialized]
	public UIPanel panel;

	// Token: 0x040005AE RID: 1454
	[HideInInspector]
	[NonSerialized]
	public bool alwaysOnScreen;

	// Token: 0x040005AF RID: 1455
	[HideInInspector]
	[NonSerialized]
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	// Token: 0x040005B0 RID: 1456
	[HideInInspector]
	[NonSerialized]
	public BetterList<Vector3> norms = new BetterList<Vector3>();

	// Token: 0x040005B1 RID: 1457
	[HideInInspector]
	[NonSerialized]
	public BetterList<Vector4> tans = new BetterList<Vector4>();

	// Token: 0x040005B2 RID: 1458
	[HideInInspector]
	[NonSerialized]
	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	// Token: 0x040005B3 RID: 1459
	[HideInInspector]
	[NonSerialized]
	public BetterList<Color32> cols = new BetterList<Color32>();

	// Token: 0x040005B4 RID: 1460
	private Material mMaterial;

	// Token: 0x040005B5 RID: 1461
	private Texture mTexture;

	// Token: 0x040005B6 RID: 1462
	private Shader mShader;

	// Token: 0x040005B7 RID: 1463
	private int mClipCount;

	// Token: 0x040005B8 RID: 1464
	private Transform mTrans;

	// Token: 0x040005B9 RID: 1465
	private Mesh mMesh;

	// Token: 0x040005BA RID: 1466
	private MeshFilter mFilter;

	// Token: 0x040005BB RID: 1467
	private MeshRenderer mRenderer;

	// Token: 0x040005BC RID: 1468
	private Material mDynamicMat;

	// Token: 0x040005BD RID: 1469
	private int[] mIndices;

	// Token: 0x040005BE RID: 1470
	private bool mRebuildMat = true;

	// Token: 0x040005BF RID: 1471
	private bool mLegacyShader;

	// Token: 0x040005C0 RID: 1472
	private int mRenderQueue = 3000;

	// Token: 0x040005C1 RID: 1473
	private int mTriangles;

	// Token: 0x040005C2 RID: 1474
	[NonSerialized]
	public bool isDirty;

	// Token: 0x040005C3 RID: 1475
	private const int maxIndexBufferCache = 10;

	// Token: 0x040005C4 RID: 1476
	private static List<int[]> mCache = new List<int[]>(10);

	// Token: 0x040005C5 RID: 1477
	private static int[] ClipRange = new int[]
	{
		Shader.PropertyToID("_ClipRange0"),
		Shader.PropertyToID("_ClipRange1"),
		Shader.PropertyToID("_ClipRange2"),
		Shader.PropertyToID("_ClipRange4")
	};

	// Token: 0x040005C6 RID: 1478
	private static int[] ClipArgs = new int[]
	{
		Shader.PropertyToID("_ClipArgs0"),
		Shader.PropertyToID("_ClipArgs1"),
		Shader.PropertyToID("_ClipArgs2"),
		Shader.PropertyToID("_ClipArgs3")
	};

	// Token: 0x020000D0 RID: 208
	public enum Clipping
	{
		// Token: 0x040005C8 RID: 1480
		None,
		// Token: 0x040005C9 RID: 1481
		SoftClip = 3,
		// Token: 0x040005CA RID: 1482
		ConstrainButDontClip
	}
}
