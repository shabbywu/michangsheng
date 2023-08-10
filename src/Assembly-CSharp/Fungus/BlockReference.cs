using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct BlockReference
{
	public Block block;

	public void Execute()
	{
		if ((Object)(object)block != (Object)null)
		{
			block.StartExecution();
		}
	}
}
