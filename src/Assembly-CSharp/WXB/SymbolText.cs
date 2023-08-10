using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WXB;

[RequireComponent(typeof(RectTransform))]
[AddComponentMenu("UI/SymbolText")]
public class SymbolText : Text, Owner
{
	public static List<SymbolText> ActiveList;

	private static TextParser sTextParser;

	[NonSerialized]
	protected LinkedList<NodeBase> mNodeList = new LinkedList<NodeBase>();

	[SerializeField]
	private string m_ElementSegment = "Default";

	protected bool m_textDirty;

	protected bool m_renderNodeDirty;

	protected bool m_layoutDirty;

	[SerializeField]
	private int m_MinLineHeight = 10;

	[SerializeField]
	private LineAlignment m_LineAlignment = LineAlignment.Bottom;

	private RenderCache mRenderCache;

	private List<Line> mLines = new List<Line>();

	[SerializeField]
	private bool m_isCheckFontY;

	private Around d_Around = new Around();

	protected static List<NodeBase> s_nodebases;

	private Vector2 last_size = new Vector2(-1000f, -1000f);

	private List<Draw> m_UsedDraws = new List<Draw>();

	public static Mesh WorkerMesh => Graphic.workerMesh;

	protected TextParser Parser => sTextParser;

	public override string text
	{
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(base.m_Text))
				{
					base.m_Text = "";
					((Graphic)this).SetVerticesDirty();
					SetTextDirty();
				}
			}
			else if (base.m_Text != value)
			{
				base.m_Text = value;
				((Graphic)this).SetVerticesDirty();
				((Graphic)this).SetLayoutDirty();
				SetTextDirty();
			}
		}
	}

	public Anchor anchor => (Anchor)((Text)this).alignment;

	public int minLineHeight
	{
		get
		{
			return m_MinLineHeight;
		}
		set
		{
			if (m_MinLineHeight != value)
			{
				m_MinLineHeight = value;
				((Graphic)this).SetAllDirty();
			}
		}
	}

	public LineAlignment lineAlignment
	{
		get
		{
			return m_LineAlignment;
		}
		set
		{
			if (m_LineAlignment != value)
			{
				m_LineAlignment = value;
				((Graphic)this).SetAllDirty();
			}
		}
	}

	public RenderCache renderCache
	{
		get
		{
			if (mRenderCache == null)
			{
				mRenderCache = new RenderCache(this);
			}
			return mRenderCache;
		}
	}

	public bool isCheckFontY
	{
		get
		{
			return m_isCheckFontY;
		}
		set
		{
			if (m_isCheckFontY != value)
			{
				m_isCheckFontY = value;
				((Graphic)this).SetVerticesDirty();
			}
		}
	}

	public override float preferredWidth
	{
		get
		{
			UpdateByDirty();
			return getNodeWidth();
		}
	}

	public override float preferredHeight
	{
		get
		{
			UpdateByDirty();
			return getNodeHeight();
		}
	}

	public Around around => d_Around;

	public ElementSegment elementSegment
	{
		get
		{
			if (string.IsNullOrEmpty(m_ElementSegment))
			{
				return null;
			}
			return ESFactory.Get(m_ElementSegment);
		}
	}

	static SymbolText()
	{
		ActiveList = new List<SymbolText>();
		sTextParser = new TextParser();
		s_nodebases = new List<NodeBase>();
		Font.textureRebuilt += RebuildForFont;
	}

	private static void RebuildForFont(Font f)
	{
		for (int i = 0; i < ActiveList.Count; i++)
		{
			if (!((Object)(object)((Text)ActiveList[i]).font == (Object)(object)f) && ActiveList[i].isUsedFont(f))
			{
				ActiveList[i].FontTextureChangedOther();
			}
		}
	}

	public bool isUsedFont(Font f)
	{
		foreach (NodeBase mNode in mNodeList)
		{
			if (mNode is TextNode && (Object)(object)((TextNode)mNode).d_font == (Object)(object)f)
			{
				return true;
			}
		}
		return false;
	}

	public void SetRenderDirty()
	{
		FreeDraws();
		((Graphic)this).SetVerticesDirty();
		((Graphic)this).SetMaterialDirty();
	}

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

	public void Clear()
	{
		foreach (NodeBase mNode in mNodeList)
		{
			FreeNode(mNode);
		}
		mNodeList.Clear();
		FreeDraws();
	}

	protected TextParser.Config CreateConfig()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		TextParser.Config result = default(TextParser.Config);
		result.anchor = Anchor.Null;
		result.font = ((Text)this).font;
		result.fontStyle = ((Text)this).fontStyle;
		result.fontSize = ((Text)this).fontSize;
		result.fontColor = ((Graphic)this).color;
		result.isBlink = false;
		result.isStrickout = false;
		result.isUnderline = false;
		result.dyncSpeed = 320;
		result.lineAlignment = lineAlignment;
		BaseMeshEffect component = ((Component)this).GetComponent<BaseMeshEffect>();
		if ((Object)(object)component != (Object)null)
		{
			if (component is Outline)
			{
				Outline val = (Outline)(object)((component is Outline) ? component : null);
				result.effectType = EffectType.Outline;
				result.effectColor = ((Shadow)val).effectColor;
				result.effectDistance = ((Shadow)val).effectDistance;
			}
			else if (component is Shadow)
			{
				Shadow val2 = (Shadow)(object)((component is Shadow) ? component : null);
				result.effectType = EffectType.Shadow;
				result.effectColor = val2.effectColor;
				result.effectDistance = val2.effectDistance;
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

	protected override void Awake()
	{
		mRenderCache = new RenderCache(this);
		((UIBehaviour)this).Awake();
		m_textDirty = true;
		m_renderNodeDirty = true;
		DestroyDrawChild();
	}

	public void DestroyDrawChild()
	{
		int childCount = ((Transform)((Graphic)this).rectTransform).childCount;
		List<Component> list = ListPool<Component>.Get();
		for (int i = 0; i < childCount; i++)
		{
			Draw component;
			if ((component = ((Component)((Transform)((Graphic)this).rectTransform).GetChild(i)).GetComponent<Draw>()) != null)
			{
				list.Add((Component)((component is Component) ? component : null));
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			Tools.Destroy((Object)(object)list[j].gameObject);
		}
		ListPool<Component>.Release(list);
	}

	protected override void UpdateGeometry()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Invalid comparison between Unknown and I4
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Invalid comparison between Unknown and I4
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		VertexHelper vertexHelper = Tools.vertexHelper;
		Rect rect;
		if ((Object)(object)((Graphic)this).rectTransform != (Object)null)
		{
			rect = ((Graphic)this).rectTransform.rect;
			if (((Rect)(ref rect)).width >= 0f)
			{
				rect = ((Graphic)this).rectTransform.rect;
				if (((Rect)(ref rect)).height >= 0f && !renderCache.isEmpty)
				{
					Rect rect2 = ((Graphic)this).rectTransform.rect;
					Vector2 offset = default(Vector2);
					((Vector2)(ref offset))._002Ector((0f - ((Graphic)this).rectTransform.pivot.x) * ((Rect)(ref rect2)).width, ((Graphic)this).rectTransform.pivot.y * ((Rect)(ref rect2)).height);
					float nodeHeight = getNodeHeight();
					Vector2 val = Vector2.zero;
					TextAnchor alignment = ((Text)this).alignment;
					if ((int)alignment > 2)
					{
						if (alignment - 3 <= 2)
						{
							val.y = (((Rect)(ref rect2)).height - nodeHeight) / 2f;
						}
						else
						{
							val.y = ((Rect)(ref rect2)).height - nodeHeight;
						}
					}
					val.x += offset.x;
					val.y += offset.y;
					vertexHelper.Clear();
					float pixelsPerUnit = ((Text)this).pixelsPerUnit;
					base.m_DisableFontTextureRebuiltCallback = true;
					if (((Text)this).alignByGeometry)
					{
						offset = Vector2.zero;
						mRenderCache.OnAlignByGeometry(ref offset, pixelsPerUnit, mLines[0].y);
						val -= offset;
					}
					for (int i = 0; i < mLines.Count; i++)
					{
						mLines[i].minY = float.MaxValue;
						mLines[i].maxY = float.MinValue;
					}
					mRenderCache.OnCheckLineY(pixelsPerUnit);
					mRenderCache.Render(vertexHelper, rect2, val, pixelsPerUnit, Graphic.workerMesh, ((Graphic)this).material);
					base.m_DisableFontTextureRebuiltCallback = false;
					goto IL_01ec;
				}
			}
		}
		vertexHelper.Clear();
		((Graphic)this).canvasRenderer.Clear();
		goto IL_01ec;
		IL_01ec:
		rect = ((Graphic)this).rectTransform.rect;
		last_size = ((Rect)(ref rect)).size;
	}

	public float getNodeHeight()
	{
		float num = 0f;
		for (int i = 0; i < mLines.Count; i++)
		{
			num += mLines[i].y;
		}
		return num;
	}

	public float getNodeWidth()
	{
		float num = 0f;
		float num2 = 0f;
		LinkedListNode<NodeBase> linkedListNode = mNodeList.First;
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
			value = null;
		}
		if (num2 < num)
		{
			num2 = num;
		}
		return num2;
	}

	protected override void OnEnable()
	{
		((Text)this).OnEnable();
		ActiveList.Add(this);
	}

	protected override void OnDisable()
	{
		((Text)this).OnDisable();
		FreeDraws();
		ActiveList.Remove(this);
	}

	protected override void OnDestroy()
	{
		if (mRenderCache != null)
		{
			mRenderCache.Release();
		}
		FreeDraws();
		((UIBehaviour)this).OnDestroy();
		Clear();
	}

	protected void LateUpdate()
	{
		float deltaTime = Time.deltaTime;
		for (int i = 0; i < m_UsedDraws.Count; i++)
		{
			m_UsedDraws[i].UpdateSelf(deltaTime);
		}
	}

	public override void SetAllDirty()
	{
		((Graphic)this).SetAllDirty();
		SetTextDirty();
	}

	private void UpdateByDirty()
	{
		if (m_textDirty)
		{
			UpdateByTextDirty();
			m_textDirty = false;
			UpdateTextHeight();
			m_layoutDirty = false;
			UpdateRenderElement();
			m_renderNodeDirty = false;
		}
		if (m_layoutDirty)
		{
			UpdateTextHeight();
			m_layoutDirty = false;
			UpdateRenderElement();
			m_renderNodeDirty = false;
		}
		if (m_renderNodeDirty)
		{
			UpdateRenderElement();
			m_renderNodeDirty = false;
		}
	}

	public override void Rebuild(CanvasUpdate update)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Invalid comparison between Unknown and I4
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (!((Graphic)this).canvasRenderer.cull && !(((Text)this).pixelsPerUnit >= 10f) && (int)update == 3)
		{
			UpdateByDirty();
			((Graphic)this).Rebuild(update);
		}
	}

	protected virtual void SetTextDirty()
	{
		m_textDirty = true;
		m_renderNodeDirty = true;
		((Graphic)this).SetMaterialDirty();
		FreeDraws();
		if (((Behaviour)this).isActiveAndEnabled)
		{
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild((ICanvasElement)(object)this);
		}
	}

	protected override void UpdateMaterial()
	{
		if (!((UIBehaviour)this).IsActive() || m_UsedDraws.Count == 0)
		{
			return;
		}
		List<Component> list = ListPool<Component>.Get();
		((Component)this).GetComponents(typeof(IMaterialModifier), list);
		for (int i = 0; i < m_UsedDraws.Count; i++)
		{
			Draw draw = m_UsedDraws[i];
			if ((Object)(object)draw.srcMat == (Object)null)
			{
				draw.srcMat = ((Graphic)this).material;
			}
			Material val = draw.srcMat;
			for (int j = 0; j < list.Count; j++)
			{
				Component obj = list[j];
				val = ((IMaterialModifier)((obj is IMaterialModifier) ? obj : null)).GetModifiedMaterial(val);
			}
			draw.UpdateMaterial(val);
		}
		ListPool<Component>.Release(list);
	}

	public void UpdateByTextDirty()
	{
		Clear();
		s_nodebases.Clear();
		Parser.parser(this, ((Text)this).text, CreateConfig(), s_nodebases);
		s_nodebases.ForEach(delegate(NodeBase nb)
		{
			mNodeList.AddLast(nb);
		});
		s_nodebases.Clear();
	}

	public override void SetVerticesDirty()
	{
		((Graphic)this).SetVerticesDirty();
		((Graphic)this).SetMaterialDirty();
	}

	protected override void OnRectTransformDimensionsChange()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (!((Component)this).gameObject.activeInHierarchy)
		{
			return;
		}
		Rect rect;
		if (CanvasUpdateRegistry.IsRebuildingLayout())
		{
			Vector2 val = last_size;
			rect = ((Graphic)this).rectTransform.rect;
			if (!(val == ((Rect)(ref rect)).size))
			{
				((Graphic)this).SetVerticesDirty();
			}
			return;
		}
		Vector2 val2 = last_size;
		rect = ((Graphic)this).rectTransform.rect;
		if (val2 != ((Rect)(ref rect)).size)
		{
			((Graphic)this).SetVerticesDirty();
		}
		((Graphic)this).SetLayoutDirty();
	}

	public override void SetLayoutDirty()
	{
		((Graphic)this).SetLayoutDirty();
		((Graphic)this).SetMaterialDirty();
		SetRenderDirty();
		m_textDirty = true;
		m_layoutDirty = true;
	}

	public void UpdateTextHeight()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		if (((Text)this).pixelsPerUnit <= 0f)
		{
			return;
		}
		renderCache.Release();
		Rect rect = ((Graphic)this).rectTransform.rect;
		float x = ((Rect)(ref rect)).size.x;
		mLines.Clear();
		if (x <= 0f)
		{
			return;
		}
		d_Around.Clear();
		foreach (NodeBase mNode in mNodeList)
		{
			if (mNode is RectSpriteNode)
			{
				RectSpriteNode rectSpriteNode = mNode as RectSpriteNode;
				d_Around.Add(rectSpriteNode.rect);
			}
		}
		mLines.Add(new Line(Vector2.zero));
		Vector2 currentpos = Vector2.zero;
		float pixelsPerUnit = ((Text)this).pixelsPerUnit;
		foreach (NodeBase mNode2 in mNodeList)
		{
			mNode2.fill(ref currentpos, mLines, x, pixelsPerUnit);
		}
		for (int i = 0; i < mLines.Count; i++)
		{
			mLines[i].y = Mathf.Max(mLines[i].y, (float)m_MinLineHeight);
		}
	}

	private void UpdateRenderElement()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (((Text)this).pixelsPerUnit <= 0f)
		{
			return;
		}
		FreeDraws();
		renderCache.Release();
		Rect rect = ((Graphic)this).rectTransform.rect;
		float x = ((Rect)(ref rect)).size.x;
		if (!(x <= 0f))
		{
			float x2 = 0f;
			uint yline = 0u;
			for (LinkedListNode<NodeBase> linkedListNode = mNodeList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value.render(x, renderCache, ref x2, ref yline, mLines, 0f, 0f);
			}
		}
	}

	public void FontTextureChangedOther()
	{
		if (Object.op_Implicit((Object)(object)this) && !base.m_DisableFontTextureRebuiltCallback && ((UIBehaviour)this).IsActive())
		{
			if (CanvasUpdateRegistry.IsRebuildingGraphics() || CanvasUpdateRegistry.IsRebuildingLayout())
			{
				((Graphic)this).UpdateGeometry();
			}
			else
			{
				((Graphic)this).SetAllDirty();
			}
		}
	}

	protected void FreeDraws()
	{
		m_UsedDraws.ForEach(delegate(Draw d)
		{
			if (d != null)
			{
				DrawFactory.Free(d);
			}
		});
		m_UsedDraws.Clear();
	}

	public Draw GetDraw(DrawType type, long key, Action<Draw, object> oncreate, object p = null)
	{
		for (int i = 0; i < m_UsedDraws.Count; i++)
		{
			Draw draw = m_UsedDraws[i];
			if (draw.type == type && draw.key == key)
			{
				return m_UsedDraws[i];
			}
		}
		Draw draw2 = DrawFactory.Create(((Component)this).gameObject, type);
		draw2.key = key;
		m_UsedDraws.Add(draw2);
		oncreate(draw2, p);
		return draw2;
	}
}
