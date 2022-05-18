using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000904 RID: 2308
	public class Gunshot
	{
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06003B0F RID: 15119 RVA: 0x0002AD26 File Offset: 0x00028F26
		// (set) Token: 0x06003B10 RID: 15120 RVA: 0x0002AD2E File Offset: 0x00028F2E
		public Vector3 Position { get; private set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x0002AD37 File Offset: 0x00028F37
		// (set) Token: 0x06003B12 RID: 15122 RVA: 0x0002AD3F File Offset: 0x00028F3F
		public EntityEventHandler EntityThatShot { get; private set; }

		// Token: 0x06003B13 RID: 15123 RVA: 0x0002AD48 File Offset: 0x00028F48
		public Gunshot(Vector3 position, EntityEventHandler entityThatShot = null)
		{
			this.Position = position;
			this.EntityThatShot = entityThatShot;
		}
	}
}
