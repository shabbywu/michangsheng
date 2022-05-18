using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000992 RID: 2450
	public static class ColorConst
	{
		// Token: 0x06003E99 RID: 16025 RVA: 0x0002D1B7 File Offset: 0x0002B3B7
		public static void Set(string name, Color c)
		{
			ColorConst.NameToColors[name] = c;
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x001B77A8 File Offset: 0x001B59A8
		public static bool Get(string name, out Color color)
		{
			if (ColorConst.NameToColors.TryGetValue(name, out color))
			{
				return true;
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 2197550541U)
			{
				if (num <= 1089765596U)
				{
					if (num <= 96429129U)
					{
						if (num != 18738364U)
						{
							if (num != 65090618U)
							{
								if (num == 96429129U)
								{
									if (name == "yellow")
									{
										color = Color.yellow;
										return true;
									}
								}
							}
							else if (name == "fuchsia")
							{
								color = ColorConst.fuchsia;
								return true;
							}
						}
						else if (name == "green")
						{
							color = Color.green;
							return true;
						}
					}
					else if (num <= 135788877U)
					{
						if (num != 132336572U)
						{
							if (num == 135788877U)
							{
								if (name == "darkblue")
								{
									color = ColorConst.darkblue;
									return true;
								}
							}
						}
						else if (name == "lime")
						{
							color = ColorConst.lime;
							return true;
						}
					}
					else if (num != 817772335U)
					{
						if (num == 1089765596U)
						{
							if (name == "red")
							{
								color = Color.red;
								return true;
							}
						}
					}
					else if (name == "brown")
					{
						color = ColorConst.brown;
						return true;
					}
				}
				else if (num <= 1231115066U)
				{
					if (num != 1110921109U)
					{
						if (num != 1169454059U)
						{
							if (num == 1231115066U)
							{
								if (name == "cyan")
								{
									color = Color.cyan;
									return true;
								}
							}
						}
						else if (name == "orange")
						{
							color = ColorConst.orange;
							return true;
						}
					}
					else if (name == "aqua")
					{
						color = ColorConst.aqua;
						return true;
					}
				}
				else if (num <= 1676028392U)
				{
					if (num != 1452231588U)
					{
						if (num == 1676028392U)
						{
							if (name == "magenta")
							{
								color = Color.magenta;
								return true;
							}
						}
					}
					else if (name == "black")
					{
						color = Color.black;
						return true;
					}
				}
				else if (num != 1848823029U)
				{
					if (num == 2197550541U)
					{
						if (name == "blue")
						{
							color = Color.blue;
							return true;
						}
					}
				}
				else if (name == "maroon")
				{
					color = ColorConst.maroon;
					return true;
				}
			}
			else if (num <= 3255563174U)
			{
				if (num <= 2701128145U)
				{
					if (num != 2203898828U)
					{
						if (num != 2590900991U)
						{
							if (num == 2701128145U)
							{
								if (name == "navy")
								{
									color = ColorConst.navy;
									return true;
								}
							}
						}
						else if (name == "purple")
						{
							color = ColorConst.purple;
							return true;
						}
					}
					else if (name == "olive")
					{
						color = ColorConst.olive;
						return true;
					}
				}
				else if (num <= 2995788198U)
				{
					if (num != 2751299231U)
					{
						if (num == 2995788198U)
						{
							if (name == "grey")
							{
								color = Color.grey;
								return true;
							}
						}
					}
					else if (name == "teal")
					{
						color = ColorConst.teal;
						return true;
					}
				}
				else if (num != 3042244896U)
				{
					if (num == 3255563174U)
					{
						if (name == "G")
						{
							color = Color.green;
							return true;
						}
					}
				}
				else if (name == "silver")
				{
					color = ColorConst.silver;
					return true;
				}
			}
			else if (num <= 3456894602U)
			{
				if (num != 3278945851U)
				{
					if (num != 3339451269U)
					{
						if (num == 3456894602U)
						{
							if (name == "K")
							{
								color = Color.black;
								return true;
							}
						}
					}
					else if (name == "B")
					{
						color = Color.blue;
						return true;
					}
				}
				else if (name == "lightblue")
				{
					color = ColorConst.lightblue;
					return true;
				}
			}
			else if (num <= 3607893173U)
			{
				if (num != 3524005078U)
				{
					if (num == 3607893173U)
					{
						if (name == "R")
						{
							color = Color.red;
							return true;
						}
					}
				}
				else if (name == "W")
				{
					color = Color.white;
					return true;
				}
			}
			else if (num != 3691781268U)
			{
				if (num == 3724674918U)
				{
					if (name == "white")
					{
						color = Color.white;
						return true;
					}
				}
			}
			else if (name == "Y")
			{
				color = Color.yellow;
				return true;
			}
			color = Color.white;
			return false;
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x001B7D5C File Offset: 0x001B5F5C
		public static Color Get(string name, Color d)
		{
			Color result;
			if (ColorConst.Get(name, out result))
			{
				return result;
			}
			return d;
		}

		// Token: 0x04003876 RID: 14454
		public static Color aqua = new Color(0f, 1f, 1f, 1f);

		// Token: 0x04003877 RID: 14455
		public static Color brown = new Color(0.64705884f, 0.16470589f, 0.16470589f, 1f);

		// Token: 0x04003878 RID: 14456
		public static Color darkblue = new Color(0f, 0f, 0.627451f, 1f);

		// Token: 0x04003879 RID: 14457
		public static Color fuchsia = new Color(1f, 0f, 1f, 1f);

		// Token: 0x0400387A RID: 14458
		public static Color lightblue = new Color(0.6784314f, 0.84705883f, 0.9019608f, 1f);

		// Token: 0x0400387B RID: 14459
		public static Color lime = new Color(0f, 1f, 0f, 1f);

		// Token: 0x0400387C RID: 14460
		public static Color maroon = new Color(0.5019608f, 0f, 0f, 1f);

		// Token: 0x0400387D RID: 14461
		public static Color navy = new Color(0f, 0f, 0.5019608f, 1f);

		// Token: 0x0400387E RID: 14462
		public static Color olive = new Color(0.5019608f, 0.5019608f, 0f, 1f);

		// Token: 0x0400387F RID: 14463
		public static Color orange = new Color(1f, 0.64705884f, 0f, 1f);

		// Token: 0x04003880 RID: 14464
		public static Color purple = new Color(0.5019608f, 0f, 0.5019608f, 1f);

		// Token: 0x04003881 RID: 14465
		public static Color silver = new Color(0.7529412f, 0.7529412f, 0.7529412f, 1f);

		// Token: 0x04003882 RID: 14466
		public static Color teal = new Color(0f, 0.5019608f, 0.5019608f, 1f);

		// Token: 0x04003883 RID: 14467
		public static Dictionary<string, Color> NameToColors = new Dictionary<string, Color>();
	}
}
