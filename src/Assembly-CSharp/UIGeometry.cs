using UnityEngine;

public class UIGeometry
{
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	public BetterList<Color32> cols = new BetterList<Color32>();

	private BetterList<Vector3> mRtpVerts = new BetterList<Vector3>();

	private Vector3 mRtpNormal;

	private Vector4 mRtpTan;

	public bool hasVertices => verts.size > 0;

	public bool hasTransformed
	{
		get
		{
			if (mRtpVerts != null && mRtpVerts.size > 0)
			{
				return mRtpVerts.size == verts.size;
			}
			return false;
		}
	}

	public void Clear()
	{
		verts.Clear();
		uvs.Clear();
		cols.Clear();
		mRtpVerts.Clear();
	}

	public void ApplyTransform(Matrix4x4 widgetToPanel)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		if (verts.size > 0)
		{
			mRtpVerts.Clear();
			int i = 0;
			for (int size = verts.size; i < size; i++)
			{
				mRtpVerts.Add(((Matrix4x4)(ref widgetToPanel)).MultiplyPoint3x4(verts[i]));
			}
			Vector3 val = ((Matrix4x4)(ref widgetToPanel)).MultiplyVector(Vector3.back);
			mRtpNormal = ((Vector3)(ref val)).normalized;
			val = ((Matrix4x4)(ref widgetToPanel)).MultiplyVector(Vector3.right);
			Vector3 normalized = ((Vector3)(ref val)).normalized;
			mRtpTan = new Vector4(normalized.x, normalized.y, normalized.z, -1f);
		}
		else
		{
			mRtpVerts.Clear();
		}
	}

	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		if (mRtpVerts == null || mRtpVerts.size <= 0)
		{
			return;
		}
		if (n == null)
		{
			for (int i = 0; i < mRtpVerts.size; i++)
			{
				v.Add(mRtpVerts.buffer[i]);
				u.Add(uvs.buffer[i]);
				c.Add(cols.buffer[i]);
			}
			return;
		}
		for (int j = 0; j < mRtpVerts.size; j++)
		{
			v.Add(mRtpVerts.buffer[j]);
			u.Add(uvs.buffer[j]);
			c.Add(cols.buffer[j]);
			n.Add(mRtpNormal);
			t.Add(mRtpTan);
		}
	}
}
