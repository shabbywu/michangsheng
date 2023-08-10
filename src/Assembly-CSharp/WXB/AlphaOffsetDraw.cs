using UnityEngine;

namespace WXB;

[ExecuteInEditMode]
public class AlphaOffsetDraw : OffsetDraw
{
	public override DrawType type => DrawType.OffsetAndAlpha;

	protected override void Init()
	{
		base.Init();
		m_Effects[1] = new AlphaEffect();
	}
}
