using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008D RID: 141
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Draw Call")]
public class UIDrawCall : MonoBehaviour
{
	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x0600077B RID: 1915 RVA: 0x0002DD98 File Offset: 0x0002BF98
	[Obsolete("Use UIDrawCall.activeList")]
	public static BetterList<UIDrawCall> list
	{
		get
		{
			return UIDrawCall.mActiveList;
		}
	}

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x0600077C RID: 1916 RVA: 0x0002DD98 File Offset: 0x0002BF98
	public static BetterList<UIDrawCall> activeList
	{
		get
		{
			return UIDrawCall.mActiveList;
		}
	}

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x0600077D RID: 1917 RVA: 0x0002DD9F File Offset: 0x0002BF9F
	public static BetterList<UIDrawCall> inactiveList
	{
		get
		{
			return UIDrawCall.mInactiveList;
		}
	}

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x0600077E RID: 1918 RVA: 0x0002DDA6 File Offset: 0x0002BFA6
	// (set) Token: 0x0600077F RID: 1919 RVA: 0x0002DDAE File Offset: 0x0002BFAE
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

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06000780 RID: 1920 RVA: 0x0002DDDA File Offset: 0x0002BFDA
	// (set) Token: 0x06000781 RID: 1921 RVA: 0x0002DDF7 File Offset: 0x0002BFF7
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

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06000782 RID: 1922 RVA: 0x0002DE21 File Offset: 0x0002C021
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

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x06000783 RID: 1923 RVA: 0x0002DE43 File Offset: 0x0002C043
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

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x06000784 RID: 1924 RVA: 0x0002DE65 File Offset: 0x0002C065
	// (set) Token: 0x06000785 RID: 1925 RVA: 0x0002DE6D File Offset: 0x0002C06D
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

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x06000786 RID: 1926 RVA: 0x0002DE8B File Offset: 0x0002C08B
	public Material dynamicMaterial
	{
		get
		{
			return this.mDynamicMat;
		}
	}

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x06000787 RID: 1927 RVA: 0x0002DE93 File Offset: 0x0002C093
	// (set) Token: 0x06000788 RID: 1928 RVA: 0x0002DE9B File Offset: 0x0002C09B
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

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x06000789 RID: 1929 RVA: 0x0002DEBE File Offset: 0x0002C0BE
	// (set) Token: 0x0600078A RID: 1930 RVA: 0x0002DEC6 File Offset: 0x0002C0C6
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

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x0600078B RID: 1931 RVA: 0x0002DEE4 File Offset: 0x0002C0E4
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

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x0600078C RID: 1932 RVA: 0x0002DEFC File Offset: 0x0002C0FC
	public bool isClipped
	{
		get
		{
			return this.mClipCount != 0;
		}
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0002DF08 File Offset: 0x0002C108
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

	// Token: 0x0600078E RID: 1934 RVA: 0x0002E17C File Offset: 0x0002C37C
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

	// Token: 0x0600078F RID: 1935 RVA: 0x0002E1F8 File Offset: 0x0002C3F8
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

	// Token: 0x06000790 RID: 1936 RVA: 0x0002E270 File Offset: 0x0002C470
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

	// Token: 0x06000791 RID: 1937 RVA: 0x0002E748 File Offset: 0x0002C948
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

	// Token: 0x06000792 RID: 1938 RVA: 0x0002E804 File Offset: 0x0002CA04
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

	// Token: 0x06000793 RID: 1939 RVA: 0x0002EA74 File Offset: 0x0002CC74
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

	// Token: 0x06000794 RID: 1940 RVA: 0x0002EB64 File Offset: 0x0002CD64
	private void OnEnable()
	{
		this.mRebuildMat = true;
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x0002EB70 File Offset: 0x0002CD70
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

	// Token: 0x06000796 RID: 1942 RVA: 0x0002EBE0 File Offset: 0x0002CDE0
	private void OnDestroy()
	{
		NGUITools.DestroyImmediate(this.mMesh);
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x0002EBED File Offset: 0x0002CDED
	public static UIDrawCall Create(UIPanel panel, Material mat, Texture tex, Shader shader)
	{
		return UIDrawCall.Create(null, panel, mat, tex, shader);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x0002EBFC File Offset: 0x0002CDFC
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

	// Token: 0x06000799 RID: 1945 RVA: 0x0002EC5C File Offset: 0x0002CE5C
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

	// Token: 0x0600079A RID: 1946 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
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

	// Token: 0x0600079B RID: 1947 RVA: 0x0002ED26 File Offset: 0x0002CF26
	public static void ReleaseAll()
	{
		UIDrawCall.ClearAll();
		UIDrawCall.ReleaseInactive();
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x0002ED34 File Offset: 0x0002CF34
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

	// Token: 0x0600079D RID: 1949 RVA: 0x0002ED80 File Offset: 0x0002CF80
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

	// Token: 0x0600079E RID: 1950 RVA: 0x0002EDC4 File Offset: 0x0002CFC4
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

	// Token: 0x0400049F RID: 1183
	private static BetterList<UIDrawCall> mActiveList = new BetterList<UIDrawCall>();

	// Token: 0x040004A0 RID: 1184
	private static BetterList<UIDrawCall> mInactiveList = new BetterList<UIDrawCall>();

	// Token: 0x040004A1 RID: 1185
	[HideInInspector]
	[NonSerialized]
	public int depthStart = int.MaxValue;

	// Token: 0x040004A2 RID: 1186
	[HideInInspector]
	[NonSerialized]
	public int depthEnd = int.MinValue;

	// Token: 0x040004A3 RID: 1187
	[HideInInspector]
	[NonSerialized]
	public UIPanel manager;

	// Token: 0x040004A4 RID: 1188
	[HideInInspector]
	[NonSerialized]
	public UIPanel panel;

	// Token: 0x040004A5 RID: 1189
	[HideInInspector]
	[NonSerialized]
	public bool alwaysOnScreen;

	// Token: 0x040004A6 RID: 1190
	[HideInInspector]
	[NonSerialized]
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	// Token: 0x040004A7 RID: 1191
	[HideInInspector]
	[NonSerialized]
	public BetterList<Vector3> norms = new BetterList<Vector3>();

	// Token: 0x040004A8 RID: 1192
	[HideInInspector]
	[NonSerialized]
	public BetterList<Vector4> tans = new BetterList<Vector4>();

	// Token: 0x040004A9 RID: 1193
	[HideInInspector]
	[NonSerialized]
	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	// Token: 0x040004AA RID: 1194
	[HideInInspector]
	[NonSerialized]
	public BetterList<Color32> cols = new BetterList<Color32>();

	// Token: 0x040004AB RID: 1195
	private Material mMaterial;

	// Token: 0x040004AC RID: 1196
	private Texture mTexture;

	// Token: 0x040004AD RID: 1197
	private Shader mShader;

	// Token: 0x040004AE RID: 1198
	private int mClipCount;

	// Token: 0x040004AF RID: 1199
	private Transform mTrans;

	// Token: 0x040004B0 RID: 1200
	private Mesh mMesh;

	// Token: 0x040004B1 RID: 1201
	private MeshFilter mFilter;

	// Token: 0x040004B2 RID: 1202
	private MeshRenderer mRenderer;

	// Token: 0x040004B3 RID: 1203
	private Material mDynamicMat;

	// Token: 0x040004B4 RID: 1204
	private int[] mIndices;

	// Token: 0x040004B5 RID: 1205
	private bool mRebuildMat = true;

	// Token: 0x040004B6 RID: 1206
	private bool mLegacyShader;

	// Token: 0x040004B7 RID: 1207
	private int mRenderQueue = 3000;

	// Token: 0x040004B8 RID: 1208
	private int mTriangles;

	// Token: 0x040004B9 RID: 1209
	[NonSerialized]
	public bool isDirty;

	// Token: 0x040004BA RID: 1210
	private const int maxIndexBufferCache = 10;

	// Token: 0x040004BB RID: 1211
	private static List<int[]> mCache = new List<int[]>(10);

	// Token: 0x040004BC RID: 1212
	private static int[] ClipRange = new int[]
	{
		Shader.PropertyToID("_ClipRange0"),
		Shader.PropertyToID("_ClipRange1"),
		Shader.PropertyToID("_ClipRange2"),
		Shader.PropertyToID("_ClipRange4")
	};

	// Token: 0x040004BD RID: 1213
	private static int[] ClipArgs = new int[]
	{
		Shader.PropertyToID("_ClipArgs0"),
		Shader.PropertyToID("_ClipArgs1"),
		Shader.PropertyToID("_ClipArgs2"),
		Shader.PropertyToID("_ClipArgs3")
	};

	// Token: 0x02001206 RID: 4614
	public enum Clipping
	{
		// Token: 0x04006459 RID: 25689
		None,
		// Token: 0x0400645A RID: 25690
		SoftClip = 3,
		// Token: 0x0400645B RID: 25691
		ConstrainButDontClip
	}
}
