using UnityEngine;

namespace WXB;

[ExecuteInEditMode]
public class EffectDrawObjec : DrawObject
{
	protected IEffect[] m_Effects = new IEffect[2];

	public override void UpdateSelf(float deltaTime)
	{
		for (int i = 0; i < m_Effects.Length; i++)
		{
			if (m_Effects[i] != null)
			{
				m_Effects[i].UpdateEffect(this, deltaTime);
			}
		}
	}

	protected bool GetOpen(int index)
	{
		return m_Effects[index] != null;
	}

	protected void SetOpen<T>(int index, bool value) where T : IEffect, new()
	{
		if (GetOpen(index) != value)
		{
			if (value)
			{
				m_Effects[index] = new T();
			}
			else
			{
				m_Effects[index] = null;
			}
		}
	}
}
