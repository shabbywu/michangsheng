using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WXB
{
	// Token: 0x020009DA RID: 2522
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/SymbolText")]
	public class SymbolText : Text, Owner
	{
		// Token: 0x0600401E RID: 16414 RVA: 0x0002E086 File Offset: 0x0002C286
		static SymbolText()
		{
			Font.textureRebuilt += SymbolText.RebuildForFont;
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x0002E0B7 File Offset: 0x0002C2B7
		public static Mesh WorkerMesh
		{
			get
			{
				return Graphic.workerMesh;
			}
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x001BBAE4 File Offset: 0x001B9CE4
		private static void RebuildForFont(Font f)
		{
			for (int i = 0; i < SymbolText.ActiveList.Count; i++)
			{
				if (!(SymbolText.ActiveList[i].font == f) && SymbolText.ActiveList[i].isUsedFont(f))
				{
					SymbolText.ActiveList[i].FontTextureChangedOther();
				}
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06004021 RID: 16417 RVA: 0x0002E0BE File Offset: 0x0002C2BE
		protected TextParser Parser
		{
			get
			{
				return SymbolText.sTextParser;
			}
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x001BBB44 File Offset: 0x001B9D44
		public bool isUsedFont(Font f)
		{
			foreach (NodeBase nodeBase in this.mNodeList)
			{
				if (nodeBase is TextNode && ((TextNode)nodeBase).d_font == f)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x0002E0C5 File Offset: 0x0002C2C5
		public void SetRenderDirty()
		{
			this.FreeDraws();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
		}

		// Token: 0x1700072C RID: 1836
		// (set) Token: 0x06004024 RID: 16420 RVA: 0x001BBBB4 File Offset: 0x001B9DB4
		public override string text
		{
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					if (this.m_Text != value)
					{
						this.m_Text = value;
						this.SetVerticesDirty();
						this.SetLayoutDirty();
						this.SetTextDirty();
					}
					return;
				}
				if (string.IsNullOrEmpty(this.m_Text))
				{
					return;
				}
				this.m_Text = "";
				this.SetVerticesDirty();
				this.SetTextDirty();
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06004025 RID: 16421 RVA: 0x0002E0D9 File Offset: 0x0002C2D9
		public Anchor anchor
		{
			get
			{
				return base.alignment;
			}
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x0002E0E1 File Offset: 0x0002C2E1
		private static void FreeNode(NodeBase node)
		{
			if (node == null)
			{
				Debug.LogErrorFormat("FreeNode error! node == null", Array.Empty<object>());
				return;
			}
			node.Release();
			node = null;
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x001BBC18 File Offset: 0x001B9E18
		public void Clear()
		{
			foreach (NodeBase node in this.mNodeList)
			{
				SymbolText.FreeNode(node);
			}
			this.mNodeList.Clear();
			this.FreeDraws();
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x0002E0FF File Offset: 0x0002C2FF
		// (set) Token: 0x06004029 RID: 16425 RVA: 0x0002E107 File Offset: 0x0002C307
		public int minLineHeight
		{
			get
			{
				return this.m_MinLineHeight;
			}
			set
			{
				if (this.m_MinLineHeight != value)
				{
					this.m_MinLineHeight = value;
					this.SetAllDirty();
				}
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x0002E11F File Offset: 0x0002C31F
		// (set) Token: 0x0600402B RID: 16427 RVA: 0x0002E127 File Offset: 0x0002C327
		public LineAlignment lineAlignment
		{
			get
			{
				return this.m_LineAlignment;
			}
			set
			{
				if (this.m_LineAlignment != value)
				{
					this.m_LineAlignment = value;
					this.SetAllDirty();
				}
			}
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x001BBC7C File Offset: 0x001B9E7C
		protected TextParser.Config CreateConfig()
		{
			TextParser.Config result = default(TextParser.Config);
			result.anchor = Anchor.Null;
			result.font = base.font;
			result.fontStyle = base.fontStyle;
			result.fontSize = base.fontSize;
			result.fontColor = this.color;
			result.isBlink = false;
			result.isStrickout = false;
			result.isUnderline = false;
			result.dyncSpeed = 320;
			result.lineAlignment = this.lineAlignment;
			BaseMeshEffect component = base.GetComponent<BaseMeshEffect>();
			if (component != null)
			{
				if (component is Outline)
				{
					Outline outline = component as Outline;
					result.effectType = EffectType.Outline;
					result.effectColor = outline.effectColor;
					result.effectDistance = outline.effectDistance;
				}
				else if (component is Shadow)
				{
					Shadow shadow = component as Shadow;
					result.effectType = EffectType.Shadow;
					result.effectColor = shadow.effectColor;
					result.effectDistance = shadow.effectDistance;
				}
				else
				{
					result.effectType = EffectType.Null;
				}
			}
			else
			{
				result.effectType = EffectType.Null;
			}
			return result;
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600402D RID: 16429 RVA: 0x0002E13F File Offset: 0x0002C33F
		public RenderCache renderCache
		{
			get
			{
				if (this.mRenderCache == null)
				{
					this.mRenderCache = new RenderCache(this);
				}
				return this.mRenderCache;
			}
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x0002E15B File Offset: 0x0002C35B
		protected override void Awake()
		{
			this.mRenderCache = new RenderCache(this);
			base.Awake();
			this.m_textDirty = true;
			this.m_renderNodeDirty = true;
			this.DestroyDrawChild();
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x001BBD88 File Offset: 0x001B9F88
		public void DestroyDrawChild()
		{
			int childCount = base.rectTransform.childCount;
			List<Component> list = ListPool<Component>.Get();
			for (int i = 0; i < childCount; i++)
			{
				Draw component;
				if ((component = base.rectTransform.GetChild(i).GetComponent<Draw>()) != null)
				{
					list.Add(component as Component);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				Tools.Destroy(list[j].gameObject);
			}
			ListPool<Component>.Release(list);
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06004030 RID: 16432 RVA: 0x0002E183 File Offset: 0x0002C383
		// (set) Token: 0x06004031 RID: 16433 RVA: 0x0002E18B File Offset: 0x0002C38B
		public bool isCheckFontY
		{
			get
			{
				return this.m_isCheckFontY;
			}
			set
			{
				if (this.m_isCheckFontY == value)
				{
					return;
				}
				this.m_isCheckFontY = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x001BBE04 File Offset: 0x001BA004
		protected override void UpdateGeometry()
		{
			VertexHelper vertexHelper = Tools.vertexHelper;
			if (base.rectTransform != null && base.rectTransform.rect.width >= 0f && base.rectTransform.rect.height >= 0f && !this.renderCache.isEmpty)
			{
				Rect rect = base.rectTransform.rect;
				Vector2 zero;
				zero..ctor(-base.rectTransform.pivot.x * rect.width, base.rectTransform.pivot.y * rect.height);
				float nodeHeight = this.getNodeHeight();
				Vector2 vector = Vector2.zero;
				TextAnchor alignment = base.alignment;
				if (alignment > 2)
				{
					if (alignment - 3 <= 2)
					{
						vector.y = (rect.height - nodeHeight) / 2f;
					}
					else
					{
						vector.y = rect.height - nodeHeight;
					}
				}
				vector.x += zero.x;
				vector.y += zero.y;
				vertexHelper.Clear();
				float pixelsPerUnit = base.pixelsPerUnit;
				this.m_DisableFontTextureRebuiltCallback = true;
				if (base.alignByGeometry)
				{
					zero = Vector2.zero;
					this.mRenderCache.OnAlignByGeometry(ref zero, pixelsPerUnit, this.mLines[0].y);
					vector -= zero;
				}
				for (int i = 0; i < this.mLines.Count; i++)
				{
					this.mLines[i].minY = float.MaxValue;
					this.mLines[i].maxY = float.MinValue;
				}
				this.mRenderCache.OnCheckLineY(pixelsPerUnit);
				this.mRenderCache.Render(vertexHelper, rect, vector, pixelsPerUnit, Graphic.workerMesh, this.material);
				this.m_DisableFontTextureRebuiltCallback = false;
			}
			else
			{
				vertexHelper.Clear();
				base.canvasRenderer.Clear();
			}
			this.last_size = base.rectTransform.rect.size;
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x001BC018 File Offset: 0x001BA218
		public float getNodeHeight()
		{
			float num = 0f;
			for (int i = 0; i < this.mLines.Count; i++)
			{
				num += this.mLines[i].y;
			}
			return num;
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x001BC058 File Offset: 0x001BA258
		public float getNodeWidth()
		{
			float num = 0f;
			float num2 = 0f;
			LinkedListNode<NodeBase> linkedListNode = this.mNodeList.First;
			while (linkedListNode != null)
			{
				NodeBase value = linkedListNode.Value;
				linkedListNode = linkedListNode.Next;
				num += value.getWidth();
				if (value.isNewLine())
				{
					if (num2 < num)
					{
						num2 = num;
					}
					num = 0f;
				}
			}
			if (num2 < num)
			{
				num2 = num;
			}
			return num2;
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x0002E1A4 File Offset: 0x0002C3A4
		protected override void OnEnable()
		{
			base.OnEnable();
			SymbolText.ActiveList.Add(this);
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x0002E1B7 File Offset: 0x0002C3B7
		protected override void OnDisable()
		{
			base.OnDisable();
			this.FreeDraws();
			SymbolText.ActiveList.Remove(this);
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x0002E1D1 File Offset: 0x0002C3D1
		protected override void OnDestroy()
		{
			if (this.mRenderCache != null)
			{
				this.mRenderCache.Release();
			}
			this.FreeDraws();
			base.OnDestroy();
			this.Clear();
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x001BC0B8 File Offset: 0x001BA2B8
		protected void LateUpdate()
		{
			float deltaTime = Time.deltaTime;
			for (int i = 0; i < this.m_UsedDraws.Count; i++)
			{
				this.m_UsedDraws[i].UpdateSelf(deltaTime);
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06004039 RID: 16441 RVA: 0x0002E1F8 File Offset: 0x0002C3F8
		public override float preferredWidth
		{
			get
			{
				this.UpdateByDirty();
				return this.getNodeWidth();
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600403A RID: 16442 RVA: 0x0002E206 File Offset: 0x0002C406
		public override float preferredHeight
		{
			get
			{
				this.UpdateByDirty();
				return this.getNodeHeight();
			}
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x0002E214 File Offset: 0x0002C414
		public override void SetAllDirty()
		{
			base.SetAllDirty();
			this.SetTextDirty();
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x001BC0F4 File Offset: 0x001BA2F4
		private void UpdateByDirty()
		{
			if (this.m_textDirty)
			{
				this.UpdateByTextDirty();
				this.m_textDirty = false;
				this.UpdateTextHeight();
				this.m_layoutDirty = false;
				this.UpdateRenderElement();
				this.m_renderNodeDirty = false;
			}
			if (this.m_layoutDirty)
			{
				this.UpdateTextHeight();
				this.m_layoutDirty = false;
				this.UpdateRenderElement();
				this.m_renderNodeDirty = false;
			}
			if (this.m_renderNodeDirty)
			{
				this.UpdateRenderElement();
				this.m_renderNodeDirty = false;
			}
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x0002E222 File Offset: 0x0002C422
		public override void Rebuild(CanvasUpdate update)
		{
			if (base.canvasRenderer.cull)
			{
				return;
			}
			if (base.pixelsPerUnit >= 10f)
			{
				return;
			}
			if (update == 3)
			{
				this.UpdateByDirty();
				base.Rebuild(update);
			}
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x0002E251 File Offset: 0x0002C451
		protected virtual void SetTextDirty()
		{
			this.m_textDirty = true;
			this.m_renderNodeDirty = true;
			this.SetMaterialDirty();
			this.FreeDraws();
			if (base.isActiveAndEnabled)
			{
				CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			}
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x001BC168 File Offset: 0x001BA368
		protected override void UpdateMaterial()
		{
			if (!this.IsActive())
			{
				return;
			}
			if (this.m_UsedDraws.Count == 0)
			{
				return;
			}
			List<Component> list = ListPool<Component>.Get();
			base.GetComponents(typeof(IMaterialModifier), list);
			for (int i = 0; i < this.m_UsedDraws.Count; i++)
			{
				Draw draw = this.m_UsedDraws[i];
				if (draw.srcMat == null)
				{
					draw.srcMat = this.material;
				}
				Material material = draw.srcMat;
				for (int j = 0; j < list.Count; j++)
				{
					material = (list[j] as IMaterialModifier).GetModifiedMaterial(material);
				}
				draw.UpdateMaterial(material);
			}
			ListPool<Component>.Release(list);
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06004040 RID: 16448 RVA: 0x0002E27B File Offset: 0x0002C47B
		public Around around
		{
			get
			{
				return this.d_Around;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06004041 RID: 16449 RVA: 0x0002E283 File Offset: 0x0002C483
		public ElementSegment elementSegment
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_ElementSegment))
				{
					return null;
				}
				return ESFactory.Get(this.m_ElementSegment);
			}
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x001BC220 File Offset: 0x001BA420
		public void UpdateByTextDirty()
		{
			this.Clear();
			SymbolText.s_nodebases.Clear();
			this.Parser.parser(this, this.text, this.CreateConfig(), SymbolText.s_nodebases);
			SymbolText.s_nodebases.ForEach(delegate(NodeBase nb)
			{
				this.mNodeList.AddLast(nb);
			});
			SymbolText.s_nodebases.Clear();
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x0002E29F File Offset: 0x0002C49F
		public override void SetVerticesDirty()
		{
			base.SetVerticesDirty();
			this.SetMaterialDirty();
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x001BC27C File Offset: 0x001BA47C
		protected override void OnRectTransformDimensionsChange()
		{
			if (base.gameObject.activeInHierarchy)
			{
				if (CanvasUpdateRegistry.IsRebuildingLayout())
				{
					if (this.last_size == base.rectTransform.rect.size)
					{
						return;
					}
					this.SetVerticesDirty();
					return;
				}
				else
				{
					if (this.last_size != base.rectTransform.rect.size)
					{
						this.SetVerticesDirty();
					}
					this.SetLayoutDirty();
				}
			}
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x0002E2AD File Offset: 0x0002C4AD
		public override void SetLayoutDirty()
		{
			base.SetLayoutDirty();
			this.SetMaterialDirty();
			this.SetRenderDirty();
			this.m_textDirty = true;
			this.m_layoutDirty = true;
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x001BC2F4 File Offset: 0x001BA4F4
		public void UpdateTextHeight()
		{
			if (base.pixelsPerUnit <= 0f)
			{
				return;
			}
			this.renderCache.Release();
			float x = base.rectTransform.rect.size.x;
			this.mLines.Clear();
			if (x <= 0f)
			{
				return;
			}
			this.d_Around.Clear();
			foreach (NodeBase nodeBase in this.mNodeList)
			{
				if (nodeBase is RectSpriteNode)
				{
					RectSpriteNode rectSpriteNode = nodeBase as RectSpriteNode;
					this.d_Around.Add(rectSpriteNode.rect);
				}
			}
			this.mLines.Add(new Line(Vector2.zero));
			Vector2 zero = Vector2.zero;
			float pixelsPerUnit = base.pixelsPerUnit;
			foreach (NodeBase nodeBase2 in this.mNodeList)
			{
				nodeBase2.fill(ref zero, this.mLines, x, pixelsPerUnit);
			}
			for (int i = 0; i < this.mLines.Count; i++)
			{
				this.mLines[i].y = Mathf.Max(this.mLines[i].y, (float)this.m_MinLineHeight);
			}
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x001BC470 File Offset: 0x001BA670
		private void UpdateRenderElement()
		{
			if (base.pixelsPerUnit <= 0f)
			{
				return;
			}
			this.FreeDraws();
			this.renderCache.Release();
			float x = base.rectTransform.rect.size.x;
			if (x <= 0f)
			{
				return;
			}
			float num = 0f;
			uint num2 = 0U;
			for (LinkedListNode<NodeBase> linkedListNode = this.mNodeList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value.render(x, this.renderCache, ref num, ref num2, this.mLines, 0f, 0f);
			}
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x0002E2CF File Offset: 0x0002C4CF
		public void FontTextureChangedOther()
		{
			if (!this)
			{
				return;
			}
			if (this.m_DisableFontTextureRebuiltCallback)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			if (CanvasUpdateRegistry.IsRebuildingGraphics() || CanvasUpdateRegistry.IsRebuildingLayout())
			{
				this.UpdateGeometry();
				return;
			}
			this.SetAllDirty();
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x0002E307 File Offset: 0x0002C507
		protected void FreeDraws()
		{
			this.m_UsedDraws.ForEach(delegate(Draw d)
			{
				if (d != null)
				{
					DrawFactory.Free(d);
				}
			});
			this.m_UsedDraws.Clear();
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x001BC50C File Offset: 0x001BA70C
		public Draw GetDraw(DrawType type, long key, Action<Draw, object> oncreate, object p = null)
		{
			for (int i = 0; i < this.m_UsedDraws.Count; i++)
			{
				Draw draw = this.m_UsedDraws[i];
				if (draw.type == type && draw.key == key)
				{
					return this.m_UsedDraws[i];
				}
			}
			Draw draw2 = DrawFactory.Create(base.gameObject, type);
			draw2.key = key;
			this.m_UsedDraws.Add(draw2);
			oncreate(draw2, p);
			return draw2;
		}

		// Token: 0x04003949 RID: 14665
		public static List<SymbolText> ActiveList = new List<SymbolText>();

		// Token: 0x0400394A RID: 14666
		private static TextParser sTextParser = new TextParser();

		// Token: 0x0400394B RID: 14667
		[NonSerialized]
		protected LinkedList<NodeBase> mNodeList = new LinkedList<NodeBase>();

		// Token: 0x0400394C RID: 14668
		[SerializeField]
		private string m_ElementSegment = "Default";

		// Token: 0x0400394D RID: 14669
		protected bool m_textDirty;

		// Token: 0x0400394E RID: 14670
		protected bool m_renderNodeDirty;

		// Token: 0x0400394F RID: 14671
		protected bool m_layoutDirty;

		// Token: 0x04003950 RID: 14672
		[SerializeField]
		private int m_MinLineHeight = 10;

		// Token: 0x04003951 RID: 14673
		[SerializeField]
		private LineAlignment m_LineAlignment = LineAlignment.Bottom;

		// Token: 0x04003952 RID: 14674
		private RenderCache mRenderCache;

		// Token: 0x04003953 RID: 14675
		private List<Line> mLines = new List<Line>();

		// Token: 0x04003954 RID: 14676
		[SerializeField]
		private bool m_isCheckFontY;

		// Token: 0x04003955 RID: 14677
		private Around d_Around = new Around();

		// Token: 0x04003956 RID: 14678
		protected static List<NodeBase> s_nodebases = new List<NodeBase>();

		// Token: 0x04003957 RID: 14679
		private Vector2 last_size = new Vector2(-1000f, -1000f);

		// Token: 0x04003958 RID: 14680
		private List<Draw> m_UsedDraws = new List<Draw>();
	}
}
