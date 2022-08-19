using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EC6 RID: 3782
	public class PortraitOptions
	{
		// Token: 0x06006ADE RID: 27358 RVA: 0x00294414 File Offset: 0x00292614
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

		// Token: 0x04005A19 RID: 23065
		public Character character;

		// Token: 0x04005A1A RID: 23066
		public Character replacedCharacter;

		// Token: 0x04005A1B RID: 23067
		public Sprite portrait;

		// Token: 0x04005A1C RID: 23068
		public DisplayType display;

		// Token: 0x04005A1D RID: 23069
		public PositionOffset offset;

		// Token: 0x04005A1E RID: 23070
		public RectTransform fromPosition;

		// Token: 0x04005A1F RID: 23071
		public RectTransform toPosition;

		// Token: 0x04005A20 RID: 23072
		public FacingDirection facing;

		// Token: 0x04005A21 RID: 23073
		public bool useDefaultSettings;

		// Token: 0x04005A22 RID: 23074
		public float fadeDuration;

		// Token: 0x04005A23 RID: 23075
		public float moveDuration;

		// Token: 0x04005A24 RID: 23076
		public Vector2 shiftOffset;

		// Token: 0x04005A25 RID: 23077
		public bool move;

		// Token: 0x04005A26 RID: 23078
		public bool shiftIntoPlace;

		// Token: 0x04005A27 RID: 23079
		public bool waitUntilFinished;

		// Token: 0x04005A28 RID: 23080
		public Action onComplete;
	}
}
