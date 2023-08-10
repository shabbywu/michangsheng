using UnityEngine;

namespace WXB;

[ExecuteInEditMode]
public class OffsetDraw : EffectDrawObjec
{
	public override DrawType type => DrawType.Offset;

	protected override void Init()
	{
		m_Effects[0] = new OffsetEffect();
	}

	public void Set(Rect rect)
	{
		Set(((Rect)(ref rect)).xMin, ((Rect)(ref rect)).yMin, ((Rect)(ref rect)).xMax, ((Rect)(ref rect)).yMax);
	}

	public void Set(float xMin, float yMin, float xMax, float yMax)
	{
		OffsetEffect obj = m_Effects[0] as OffsetEffect;
		obj.xMin = xMin;
		obj.yMin = yMin;
		obj.xMax = xMax;
		obj.yMax = yMax;
	}
}
