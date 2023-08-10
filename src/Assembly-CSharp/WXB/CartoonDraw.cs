using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace WXB;

[ExecuteInEditMode]
public class CartoonDraw : EffectDrawObjec, ICanvasElement
{
	private class Data
	{
		public Vector2 leftPos;

		public Color color;

		public float width;

		public float height;

		public void Gen(VertexHelper vh, Vector4 uv)
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			//IL_0107: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Unknown result type (might be due to invalid IL or missing references)
			int currentVertCount = vh.currentVertCount;
			vh.AddVert(new Vector3(leftPos.x, leftPos.y), Color32.op_Implicit(color), new Vector2(uv.x, uv.y));
			vh.AddVert(new Vector3(leftPos.x, leftPos.y + height), Color32.op_Implicit(color), new Vector2(uv.x, uv.w));
			vh.AddVert(new Vector3(leftPos.x + width, leftPos.y + height), Color32.op_Implicit(color), new Vector2(uv.z, uv.w));
			vh.AddVert(new Vector3(leftPos.x + width, leftPos.y), Color32.op_Implicit(color), new Vector2(uv.z, uv.y));
			vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}
	}

	private int frameIndex;

	private float mDelta;

	private List<Data> mData = new List<Data>();

	public override DrawType type => DrawType.Cartoon;

	public Cartoon cartoon { get; set; }

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

	public bool isOpenOffset
	{
		get
		{
			return GetOpen(1);
		}
		set
		{
			SetOpen<OffsetEffect>(1, value);
		}
	}

	private void UpdateAnim(float deltaTime)
	{
		mDelta += Mathf.Min(1f, deltaTime);
		float num = 1f / cartoon.fps;
		while (num < mDelta)
		{
			mDelta = ((num > 0f) ? (mDelta - num) : 0f);
			if (++frameIndex >= cartoon.sprites.Length)
			{
				frameIndex = 0;
			}
		}
	}

	public void Add(Vector2 leftPos, float width, float height, Color color)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		mData.Add(new Data
		{
			leftPos = leftPos,
			color = color,
			width = width,
			height = height
		});
	}

	public override void UpdateSelf(float deltaTime)
	{
		base.UpdateSelf(deltaTime);
		int num = frameIndex;
		UpdateAnim(deltaTime);
		if (num != frameIndex)
		{
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild((ICanvasElement)(object)this);
		}
	}

	public void Rebuild(CanvasUpdate executing)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Invalid comparison between Unknown and I4
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if ((int)executing == 3 && mData != null)
		{
			Sprite val = cartoon.sprites[frameIndex];
			Vector4 outerUV = DataUtility.GetOuterUV(cartoon.sprites[frameIndex]);
			VertexHelper vertexHelper = Tools.vertexHelper;
			vertexHelper.Clear();
			for (int i = 0; i < mData.Count; i++)
			{
				mData[i].Gen(vertexHelper, outerUV);
			}
			Mesh workerMesh = SymbolText.WorkerMesh;
			vertexHelper.FillMesh(workerMesh);
			base.canvasRenderer.SetMesh(workerMesh);
			base.canvasRenderer.SetTexture((Texture)(object)val.texture);
		}
	}

	public override void Release()
	{
		base.Release();
		mData.Clear();
		frameIndex = 0;
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
