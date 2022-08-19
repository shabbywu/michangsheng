using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200061D RID: 1565
	public class Gunshot
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x001618A1 File Offset: 0x0015FAA1
		// (set) Token: 0x060031DC RID: 12764 RVA: 0x001618A9 File Offset: 0x0015FAA9
		public Vector3 Position { get; private set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x001618B2 File Offset: 0x0015FAB2
		// (set) Token: 0x060031DE RID: 12766 RVA: 0x001618BA File Offset: 0x0015FABA
		public EntityEventHandler EntityThatShot { get; private set; }

		// Token: 0x060031DF RID: 12767 RVA: 0x001618C3 File Offset: 0x0015FAC3
		public Gunshot(Vector3 position, EntityEventHandler entityThatShot = null)
		{
			this.Position = position;
			this.EntityThatShot = entityThatShot;
		}
	}
}
