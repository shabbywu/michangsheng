using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WXB
{
	// Token: 0x020006B5 RID: 1717
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/SymbolText")]
	public class SymbolText : Text, Owner
	{
		// Token: 0x0600361A RID: 13850 RVA: 0x00172F0E File Offset: 0x0017110E
		static SymbolText()
		{
			Font.textureRebuilt += SymbolText.RebuildForFont;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600361B RID: 13851 RVA: 0x00172F3F File Offset: 0x0017113F
		public static Mesh WorkerMesh
		{
			get
			{
				return Graphic.workerMesh;
			}
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x00172F48 File Offset: 0x00171148
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

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600361D RID: 13853 RVA: 0x00172FA5 File Offset: 0x001711A5
		protected TextParser Parser
		{
			get
			{
				return SymbolText.sTextParser;
			}
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x00172FAC File Offset: 0x001711AC
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

		// Token: 0x0600361F RID: 13855 RVA: 0x0017301C File Offset: 0x0017121C
		public void SetRenderDirty()
		{
			this.FreeDraws();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
		}

		// Token: 0x17000501 RID: 1281
		// (set) Token: 0x06003620 RID: 13856 RVA: 0x00173030 File Offset: 0x00171230
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

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06003621 RID: 13857 RVA: 0x00173092 File Offset: 0x00171292
		public Anchor anchor
		{
			get
			{
				return base.alignment;
			}
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x0017309A File Offset: 0x0017129A
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

		// Token: 0x06003623 RID: 13859 RVA: 0x001730B8 File Offset: 0x001712B8
		public void Clear()
		{
			foreach (NodeBase node in this.mNodeList)
			{
				SymbolText.FreeNode(node);
			}
			this.mNodeList.Clear();
			this.FreeDraws();
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x0017311C File Offset: 0x0017131C
		// (set) Token: 0x06003625 RID: 13861 RVA: 0x00173124 File Offset: 0x00171324
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

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x0017313C File Offset: 0x0017133C
		// (set) Token: 0x06003627 RID: 13863 RVA: 0x00173144 File Offset: 0x00171344
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

		// Token: 0x06003628 RID: 13864 RVA: 0x0017315C File Offset: 0x0017135C
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

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x00173268 File Offset: 0x00171468
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

		// Token: 0x0600362A RID: 13866 RVA: 0x00173284 File Offset: 0x00171484
		protected override void Awake()
		{
			this.mRenderCache = new RenderCache(this);
			base.Awake();
			this.m_textDirty = true;
			this.m_renderNodeDirty = true;
			this.DestroyDrawChild();
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x001732AC File Offset: 0x001714AC
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

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x00173325 File Offset: 0x00171525
		// (set) Token: 0x0600362D RID: 13869 RVA: 0x0017332D File Offset: 0x0017152D
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

		// Token: 0x0600362E RID: 13870 RVA: 0x00173348 File Offset: 0x00171548
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

		// Token: 0x0600362F RID: 13871 RVA: 0x0017355C File Offset: 0x0017175C
		public float getNodeHeight()
		{
			float num = 0f;
			for (int i = 0; i < this.mLines.Count; i++)
			{
				num += this.mLines[i].y;
			}
			return num;
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x0017359C File Offset: 0x0017179C
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

		// Token: 0x06003631 RID: 13873 RVA: 0x001735FA File Offset: 0x001717FA
		protected override void OnEnable()
		{
			base.OnEnable();
			SymbolText.ActiveList.Add(this);
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x0017360D File Offset: 0x0017180D
		protected override void OnDisable()
		{
			base.OnDisable();
			this.FreeDraws();
			SymbolText.ActiveList.Remove(this);
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x00173627 File Offset: 0x00171827
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

		// Token: 0x06003634 RID: 13876 RVA: 0x00173650 File Offset: 0x00171850
		protected void LateUpdate()
		{
			float deltaTime = Time.deltaTime;
			for (int i = 0; i < this.m_UsedDraws.Count; i++)
			{
				this.m_UsedDraws[i].UpdateSelf(deltaTime);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x0017368B File Offset: 0x0017188B
		public override float preferredWidth
		{
			get
			{
				this.UpdateByDirty();
				return this.getNodeWidth();
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06003636 RID: 13878 RVA: 0x00173699 File Offset: 0x00171899
		public override float preferredHeight
		{
			get
			{
				this.UpdateByDirty();
				return this.getNodeHeight();
			}
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x001736A7 File Offset: 0x001718A7
		public override void SetAllDirty()
		{
			base.SetAllDirty();
			this.SetTextDirty();
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x001736B8 File Offset: 0x001718B8
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

		// Token: 0x06003639 RID: 13881 RVA: 0x0017372B File Offset: 0x0017192B
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

		// Token: 0x0600363A RID: 13882 RVA: 0x0017375A File Offset: 0x0017195A
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

		// Token: 0x0600363B RID: 13883 RVA: 0x00173784 File Offset: 0x00171984
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

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600363C RID: 13884 RVA: 0x00173839 File Offset: 0x00171A39
		public Around around
		{
			get
			{
				return this.d_Around;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x00173841 File Offset: 0x00171A41
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

		// Token: 0x0600363E RID: 13886 RVA: 0x00173860 File Offset: 0x00171A60
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

		// Token: 0x0600363F RID: 13887 RVA: 0x001738BA File Offset: 0x00171ABA
		public override void SetVerticesDirty()
		{
			base.SetVerticesDirty();
			this.SetMaterialDirty();
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x001738C8 File Offset: 0x00171AC8
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

		// Token: 0x06003641 RID: 13889 RVA: 0x0017393D File Offset: 0x00171B3D
		public override void SetLayoutDirty()
		{
			base.SetLayoutDirty();
			this.SetMaterialDirty();
			this.SetRenderDirty();
			this.m_textDirty = true;
			this.m_layoutDirty = true;
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x00173960 File Offset: 0x00171B60
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

		// Token: 0x06003643 RID: 13891 RVA: 0x00173ADC File Offset: 0x00171CDC
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

		// Token: 0x06003644 RID: 13892 RVA: 0x00173B75 File Offset: 0x00171D75
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

		// Token: 0x06003645 RID: 13893 RVA: 0x00173BAD File Offset: 0x00171DAD
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

		// Token: 0x06003646 RID: 13894 RVA: 0x00173BE4 File Offset: 0x00171DE4
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

		// Token: 0x04002F59 RID: 12121
		public static List<SymbolText> ActiveList = new List<SymbolText>();

		// Token: 0x04002F5A RID: 12122
		private static TextParser sTextParser = new TextParser();

		// Token: 0x04002F5B RID: 12123
		[NonSerialized]
		protected LinkedList<NodeBase> mNodeList = new LinkedList<NodeBase>();

		// Token: 0x04002F5C RID: 12124
		[SerializeField]
		private string m_ElementSegment = "Default";

		// Token: 0x04002F5D RID: 12125
		protected bool m_textDirty;

		// Token: 0x04002F5E RID: 12126
		protected bool m_renderNodeDirty;

		// Token: 0x04002F5F RID: 12127
		protected bool m_layoutDirty;

		// Token: 0x04002F60 RID: 12128
		[SerializeField]
		private int m_MinLineHeight = 10;

		// Token: 0x04002F61 RID: 12129
		[SerializeField]
		private LineAlignment m_LineAlignment = LineAlignment.Bottom;

		// Token: 0x04002F62 RID: 12130
		private RenderCache mRenderCache;

		// Token: 0x04002F63 RID: 12131
		private List<Line> mLines = new List<Line>();

		// Token: 0x04002F64 RID: 12132
		[SerializeField]
		private bool m_isCheckFontY;

		// Token: 0x04002F65 RID: 12133
		private Around d_Around = new Around();

		// Token: 0x04002F66 RID: 12134
		protected static List<NodeBase> s_nodebases = new List<NodeBase>();

		// Token: 0x04002F67 RID: 12135
		private Vector2 last_size = new Vector2(-1000f, -1000f);

		// Token: 0x04002F68 RID: 12136
		private List<Draw> m_UsedDraws = new List<Draw>();
	}
}
