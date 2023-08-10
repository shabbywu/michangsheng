using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace WXB;

[ExecuteInEditMode]
public class OutlineDraw : EffectDrawObjec, ICanvasElement
{
	private DrawLineStruct m_Data;

	private float currentWidth;

	private float maxWidth;

	public override DrawType type => DrawType.Outline;

	public bool isOpenAlpha
	{
		get
		{
			return GetOpen(0);
		}
		set
		{
			SetOpen<AlphaEffect>(0, value);
		}
	}

	public override void UpdateSelf(float deltaTime)
	{
		base.UpdateSelf(deltaTime);
		if (currentWidth >= maxWidth || m_Data == null)
		{
			return;
		}
		float num = currentWidth;
		for (int i = 0; i < m_Data.lines.Count; i++)
		{
			DrawLineStruct.Line line = m_Data.lines[i];
			if (num >= line.width)
			{
				num -= line.width;
				continue;
			}
			float num2 = (line.width - num) / (float)line.dynSpeed;
			if (num2 >= deltaTime)
			{
				currentWidth += deltaTime * (float)line.dynSpeed;
				break;
			}
			currentWidth += (float)line.dynSpeed * num2;
			deltaTime -= num2;
			num -= (float)line.dynSpeed * num2;
		}
		CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild((ICanvasElement)(object)this);
	}

	public void AddLine(TextNode n, Vector2 left, float width, float height, Color color, Vector2 uv, int speed)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		if (m_Data == null)
		{
			m_Data = new DrawLineStruct();
			maxWidth = 0f;
			currentWidth = 0f;
		}
		maxWidth += width;
		m_Data.lines.Add(new DrawLineStruct.Line
		{
			leftPos = left,
			width = width,
			height = height,
			color = color,
			uv = uv,
			node = n,
			dynSpeed = speed
		});
	}

	public override void UpdateMaterial(Material mat)
	{
		base.UpdateMaterial(mat);
		((Transform)base.rectTransform).SetAsLastSibling();
	}

	public void Rebuild(CanvasUpdate executing)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Invalid comparison between Unknown and I4
		if (m_Data != null && (int)executing == 3)
		{
			float width = currentWidth;
			VertexHelper vertexHelper = Tools.vertexHelper;
			vertexHelper.Clear();
			m_Data.Render(width, vertexHelper);
			Mesh workerMesh = SymbolText.WorkerMesh;
			vertexHelper.FillMesh(workerMesh);
			base.canvasRenderer.SetMesh(workerMesh);
		}
	}

	public override void Release()
	{
		base.Release();
		m_Data = null;
	}

	public void GraphicUpdateComplete()
	{
	}

	public bool IsDestroyed()
	{
		return (Object)(object)this == (Object)null;
	}

	public void LayoutComplete()
	{
	}

	[SpecialName]
	Transform ICanvasElement.get_transform()
	{
		return ((Component)this).transform;
	}
}
