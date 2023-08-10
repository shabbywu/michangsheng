using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public class StarttFightAddBuff
{
	[Tooltip("buff的ID")]
	public int buffID;

	[Tooltip("buff的层数")]
	public int BuffNum = 1;

	public StarttFightAddBuff()
	{
		buffID = 0;
		BuffNum = 1;
	}
}
