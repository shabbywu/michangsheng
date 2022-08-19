using System;

namespace UltimateSurvival
{
	// Token: 0x02000599 RID: 1433
	public class ET
	{
		// Token: 0x02001497 RID: 5271
		public enum ActionRepeatType
		{
			// Token: 0x04006C90 RID: 27792
			Single,
			// Token: 0x04006C91 RID: 27793
			Repetitive
		}

		// Token: 0x02001498 RID: 5272
		public enum PointOrder
		{
			// Token: 0x04006C93 RID: 27795
			Sequenced,
			// Token: 0x04006C94 RID: 27796
			Random
		}

		// Token: 0x02001499 RID: 5273
		public enum AIMovementState
		{
			// Token: 0x04006C96 RID: 27798
			Idle,
			// Token: 0x04006C97 RID: 27799
			Walking,
			// Token: 0x04006C98 RID: 27800
			Running
		}

		// Token: 0x0200149A RID: 5274
		public enum BuildableType
		{
			// Token: 0x04006C9A RID: 27802
			Foundation,
			// Token: 0x04006C9B RID: 27803
			Wall,
			// Token: 0x04006C9C RID: 27804
			Floor
		}

		// Token: 0x0200149B RID: 5275
		public enum MaterialType
		{
			// Token: 0x04006C9E RID: 27806
			Wood,
			// Token: 0x04006C9F RID: 27807
			Stone,
			// Token: 0x04006CA0 RID: 27808
			Metal
		}

		// Token: 0x0200149C RID: 5276
		public enum InputType
		{
			// Token: 0x04006CA2 RID: 27810
			Standalone,
			// Token: 0x04006CA3 RID: 27811
			Mobile
		}

		// Token: 0x0200149D RID: 5277
		public enum InputMode
		{
			// Token: 0x04006CA5 RID: 27813
			Buttons,
			// Token: 0x04006CA6 RID: 27814
			Axes
		}

		// Token: 0x0200149E RID: 5278
		public enum StandaloneAxisType
		{
			// Token: 0x04006CA8 RID: 27816
			Unity,
			// Token: 0x04006CA9 RID: 27817
			Custom
		}

		// Token: 0x0200149F RID: 5279
		public enum MobileAxisType
		{
			// Token: 0x04006CAB RID: 27819
			Custom
		}

		// Token: 0x020014A0 RID: 5280
		public enum ButtonState
		{
			// Token: 0x04006CAD RID: 27821
			Down,
			// Token: 0x04006CAE RID: 27822
			Up
		}

		// Token: 0x020014A1 RID: 5281
		public enum CharacterType
		{
			// Token: 0x04006CB0 RID: 27824
			Player
		}

		// Token: 0x020014A2 RID: 5282
		public enum FireMode
		{
			// Token: 0x04006CB2 RID: 27826
			SemiAuto,
			// Token: 0x04006CB3 RID: 27827
			Burst,
			// Token: 0x04006CB4 RID: 27828
			FullAuto
		}

		// Token: 0x020014A3 RID: 5283
		public enum FileCreatorMode
		{
			// Token: 0x04006CB6 RID: 27830
			ScriptableObject,
			// Token: 0x04006CB7 RID: 27831
			ScriptFile,
			// Token: 0x04006CB8 RID: 27832
			Both
		}

		// Token: 0x020014A4 RID: 5284
		public enum TimeOfDay
		{
			// Token: 0x04006CBA RID: 27834
			Day,
			// Token: 0x04006CBB RID: 27835
			Night
		}

		// Token: 0x020014A5 RID: 5285
		public enum InventoryState
		{
			// Token: 0x04006CBD RID: 27837
			Closed,
			// Token: 0x04006CBE RID: 27838
			Normal,
			// Token: 0x04006CBF RID: 27839
			Loot,
			// Token: 0x04006CC0 RID: 27840
			Furnace,
			// Token: 0x04006CC1 RID: 27841
			Anvil,
			// Token: 0x04006CC2 RID: 27842
			Campfire = 6
		}
	}
}
