using System;

namespace UltimateSurvival
{
	// Token: 0x02000846 RID: 2118
	public class ET
	{
		// Token: 0x02000847 RID: 2119
		public enum ActionRepeatType
		{
			// Token: 0x040031B5 RID: 12725
			Single,
			// Token: 0x040031B6 RID: 12726
			Repetitive
		}

		// Token: 0x02000848 RID: 2120
		public enum PointOrder
		{
			// Token: 0x040031B8 RID: 12728
			Sequenced,
			// Token: 0x040031B9 RID: 12729
			Random
		}

		// Token: 0x02000849 RID: 2121
		public enum AIMovementState
		{
			// Token: 0x040031BB RID: 12731
			Idle,
			// Token: 0x040031BC RID: 12732
			Walking,
			// Token: 0x040031BD RID: 12733
			Running
		}

		// Token: 0x0200084A RID: 2122
		public enum BuildableType
		{
			// Token: 0x040031BF RID: 12735
			Foundation,
			// Token: 0x040031C0 RID: 12736
			Wall,
			// Token: 0x040031C1 RID: 12737
			Floor
		}

		// Token: 0x0200084B RID: 2123
		public enum MaterialType
		{
			// Token: 0x040031C3 RID: 12739
			Wood,
			// Token: 0x040031C4 RID: 12740
			Stone,
			// Token: 0x040031C5 RID: 12741
			Metal
		}

		// Token: 0x0200084C RID: 2124
		public enum InputType
		{
			// Token: 0x040031C7 RID: 12743
			Standalone,
			// Token: 0x040031C8 RID: 12744
			Mobile
		}

		// Token: 0x0200084D RID: 2125
		public enum InputMode
		{
			// Token: 0x040031CA RID: 12746
			Buttons,
			// Token: 0x040031CB RID: 12747
			Axes
		}

		// Token: 0x0200084E RID: 2126
		public enum StandaloneAxisType
		{
			// Token: 0x040031CD RID: 12749
			Unity,
			// Token: 0x040031CE RID: 12750
			Custom
		}

		// Token: 0x0200084F RID: 2127
		public enum MobileAxisType
		{
			// Token: 0x040031D0 RID: 12752
			Custom
		}

		// Token: 0x02000850 RID: 2128
		public enum ButtonState
		{
			// Token: 0x040031D2 RID: 12754
			Down,
			// Token: 0x040031D3 RID: 12755
			Up
		}

		// Token: 0x02000851 RID: 2129
		public enum CharacterType
		{
			// Token: 0x040031D5 RID: 12757
			Player
		}

		// Token: 0x02000852 RID: 2130
		public enum FireMode
		{
			// Token: 0x040031D7 RID: 12759
			SemiAuto,
			// Token: 0x040031D8 RID: 12760
			Burst,
			// Token: 0x040031D9 RID: 12761
			FullAuto
		}

		// Token: 0x02000853 RID: 2131
		public enum FileCreatorMode
		{
			// Token: 0x040031DB RID: 12763
			ScriptableObject,
			// Token: 0x040031DC RID: 12764
			ScriptFile,
			// Token: 0x040031DD RID: 12765
			Both
		}

		// Token: 0x02000854 RID: 2132
		public enum TimeOfDay
		{
			// Token: 0x040031DF RID: 12767
			Day,
			// Token: 0x040031E0 RID: 12768
			Night
		}

		// Token: 0x02000855 RID: 2133
		public enum InventoryState
		{
			// Token: 0x040031E2 RID: 12770
			Closed,
			// Token: 0x040031E3 RID: 12771
			Normal,
			// Token: 0x040031E4 RID: 12772
			Loot,
			// Token: 0x040031E5 RID: 12773
			Furnace,
			// Token: 0x040031E6 RID: 12774
			Anvil,
			// Token: 0x040031E7 RID: 12775
			Campfire = 6
		}
	}
}
