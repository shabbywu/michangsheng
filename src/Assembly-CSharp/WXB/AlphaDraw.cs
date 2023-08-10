using UnityEngine;

namespace WXB;

[ExecuteInEditMode]
public class AlphaDraw : EffectDrawObjec
{
	public override DrawType type => DrawType.Alpha;

	protected override void Init()
	{
		m_Effects[0] = new AlphaEffect();
	}

	public override void Release()
	{
		base.Release();
		m_Effects[0].Release();
	}
}
