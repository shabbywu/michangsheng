using System;

namespace script.ItemSource.Interface;

[Serializable]
public abstract class ABItemSourceData
{
	public int Id;

	public int Count;

	public int UpdateTime;

	public int HasCostTime;

	public virtual void Init()
	{
	}
}
