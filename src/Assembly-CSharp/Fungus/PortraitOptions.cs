using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001364 RID: 4964
	public class PortraitOptions
	{
		// Token: 0x06007879 RID: 30841 RVA: 0x002B67B0 File Offset: 0x002B49B0
		public PortraitOptions(bool useDefaultSettings = true)
		{
			this.character = null;
			this.replacedCharacter = null;
			this.portrait = null;
			this.display = DisplayType.None;
			this.offset = PositionOffset.None;
			this.fromPosition = null;
			this.toPosition = null;
			this.facing = FacingDirection.None;
			this.shiftOffset = new Vector2(0f, 0f);
			this.move = false;
			this.shiftIntoPlace = false;
			this.waitUntilFinished = false;
			this.onComplete = null;
			this.fadeDuration = 0.5f;
			this.moveDuration = 1f;
			this.useDefaultSettings = useDefaultSettings;
		}

		// Token: 0x04006878 RID: 26744
		public Character character;

		// Token: 0x04006879 RID: 26745
		public Character replacedCharacter;

		// Token: 0x0400687A RID: 26746
		public Sprite portrait;

		// Token: 0x0400687B RID: 26747
		public DisplayType display;

		// Token: 0x0400687C RID: 26748
		public PositionOffset offset;

		// Token: 0x0400687D RID: 26749
		public RectTransform fromPosition;

		// Token: 0x0400687E RID: 26750
		public RectTransform toPosition;

		// Token: 0x0400687F RID: 26751
		public FacingDirection facing;

		// Token: 0x04006880 RID: 26752
		public bool useDefaultSettings;

		// Token: 0x04006881 RID: 26753
		public float fadeDuration;

		// Token: 0x04006882 RID: 26754
		public float moveDuration;

		// Token: 0x04006883 RID: 26755
		public Vector2 shiftOffset;

		// Token: 0x04006884 RID: 26756
		public bool move;

		// Token: 0x04006885 RID: 26757
		public bool shiftIntoPlace;

		// Token: 0x04006886 RID: 26758
		public bool waitUntilFinished;

		// Token: 0x04006887 RID: 26759
		public Action onComplete;
	}
}
