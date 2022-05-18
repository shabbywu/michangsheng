using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200089E RID: 2206
	public class ItemProperty
	{
		// Token: 0x0200089F RID: 2207
		public enum Type
		{
			// Token: 0x0400331A RID: 13082
			None,
			// Token: 0x0400331B RID: 13083
			Bool,
			// Token: 0x0400331C RID: 13084
			Int,
			// Token: 0x0400331D RID: 13085
			IntRange,
			// Token: 0x0400331E RID: 13086
			RandomInt,
			// Token: 0x0400331F RID: 13087
			Float,
			// Token: 0x04003320 RID: 13088
			FloatRange,
			// Token: 0x04003321 RID: 13089
			RandomFloat,
			// Token: 0x04003322 RID: 13090
			String,
			// Token: 0x04003323 RID: 13091
			Sound
		}

		// Token: 0x020008A0 RID: 2208
		[Serializable]
		public struct Int
		{
			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x060038DE RID: 14558 RVA: 0x00029523 File Offset: 0x00027723
			// (set) Token: 0x060038DF RID: 14559 RVA: 0x0002952B File Offset: 0x0002772B
			public int Current
			{
				get
				{
					return this.m_Current;
				}
				set
				{
					this.m_Current = value;
				}
			}

			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x060038E0 RID: 14560 RVA: 0x00029534 File Offset: 0x00027734
			public int Default
			{
				get
				{
					return this.m_Default;
				}
			}

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x060038E1 RID: 14561 RVA: 0x0002953C File Offset: 0x0002773C
			public float Ratio
			{
				get
				{
					return (float)this.m_Current / (float)this.m_Default;
				}
			}

			// Token: 0x060038E2 RID: 14562 RVA: 0x0002954D File Offset: 0x0002774D
			public override string ToString()
			{
				return this.m_Current.ToString();
			}

			// Token: 0x04003324 RID: 13092
			[SerializeField]
			private int m_Current;

			// Token: 0x04003325 RID: 13093
			[SerializeField]
			private int m_Default;
		}

		// Token: 0x020008A1 RID: 2209
		[Serializable]
		public struct IntRange
		{
			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x060038E3 RID: 14563 RVA: 0x0002955A File Offset: 0x0002775A
			// (set) Token: 0x060038E4 RID: 14564 RVA: 0x00029562 File Offset: 0x00027762
			public int Current
			{
				get
				{
					return this.m_Current;
				}
				set
				{
					this.m_Current = Mathf.Clamp(value, this.m_Min, this.m_Max);
				}
			}

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x060038E5 RID: 14565 RVA: 0x0002957C File Offset: 0x0002777C
			public float Ratio
			{
				get
				{
					return (float)this.m_Current / (float)this.m_Max;
				}
			}

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x060038E6 RID: 14566 RVA: 0x0002958D File Offset: 0x0002778D
			// (set) Token: 0x060038E7 RID: 14567 RVA: 0x00029595 File Offset: 0x00027795
			public int Min
			{
				get
				{
					return this.m_Min;
				}
				set
				{
					this.m_Min = value;
				}
			}

			// Token: 0x170005E0 RID: 1504
			// (get) Token: 0x060038E8 RID: 14568 RVA: 0x0002959E File Offset: 0x0002779E
			// (set) Token: 0x060038E9 RID: 14569 RVA: 0x000295A6 File Offset: 0x000277A6
			public int Max
			{
				get
				{
					return this.m_Max;
				}
				set
				{
					this.m_Max = value;
				}
			}

			// Token: 0x060038EA RID: 14570 RVA: 0x000295AF File Offset: 0x000277AF
			public override string ToString()
			{
				return string.Format("{0} / {1}", this.Current, this.Max);
			}

			// Token: 0x04003326 RID: 13094
			[SerializeField]
			private int m_Current;

			// Token: 0x04003327 RID: 13095
			[SerializeField]
			private int m_Min;

			// Token: 0x04003328 RID: 13096
			[SerializeField]
			private int m_Max;
		}

		// Token: 0x020008A2 RID: 2210
		[Serializable]
		public struct RandomInt
		{
			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x060038EB RID: 14571 RVA: 0x000295D1 File Offset: 0x000277D1
			public int RandomValue
			{
				get
				{
					return Random.Range(this.m_Min, this.m_Max);
				}
			}

			// Token: 0x060038EC RID: 14572 RVA: 0x000295E4 File Offset: 0x000277E4
			public override string ToString()
			{
				return string.Format("{0} - {1}", this.m_Min, this.m_Max);
			}

			// Token: 0x04003329 RID: 13097
			[SerializeField]
			private int m_Min;

			// Token: 0x0400332A RID: 13098
			[SerializeField]
			private int m_Max;
		}

		// Token: 0x020008A3 RID: 2211
		[Serializable]
		public struct Float
		{
			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x060038ED RID: 14573 RVA: 0x00029606 File Offset: 0x00027806
			// (set) Token: 0x060038EE RID: 14574 RVA: 0x0002960E File Offset: 0x0002780E
			public float Current
			{
				get
				{
					return this.m_Current;
				}
				set
				{
					this.m_Current = value;
				}
			}

			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x060038EF RID: 14575 RVA: 0x00029617 File Offset: 0x00027817
			public float Default
			{
				get
				{
					return this.m_Default;
				}
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x060038F0 RID: 14576 RVA: 0x0002961F File Offset: 0x0002781F
			public float Ratio
			{
				get
				{
					return this.m_Current / this.m_Default;
				}
			}

			// Token: 0x060038F1 RID: 14577 RVA: 0x0002962E File Offset: 0x0002782E
			public override string ToString()
			{
				return this.m_Current.ToString();
			}

			// Token: 0x0400332B RID: 13099
			[SerializeField]
			private float m_Current;

			// Token: 0x0400332C RID: 13100
			[SerializeField]
			private float m_Default;
		}

		// Token: 0x020008A4 RID: 2212
		[Serializable]
		public struct FloatRange
		{
			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x060038F2 RID: 14578 RVA: 0x0002963B File Offset: 0x0002783B
			// (set) Token: 0x060038F3 RID: 14579 RVA: 0x00029643 File Offset: 0x00027843
			public float Current
			{
				get
				{
					return this.m_Current;
				}
				set
				{
					this.m_Current = Mathf.Clamp(value, this.m_Min, this.m_Max);
				}
			}

			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x060038F4 RID: 14580 RVA: 0x0002965D File Offset: 0x0002785D
			public float Ratio
			{
				get
				{
					return this.m_Current / this.m_Max;
				}
			}

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x060038F5 RID: 14581 RVA: 0x0002966C File Offset: 0x0002786C
			// (set) Token: 0x060038F6 RID: 14582 RVA: 0x00029674 File Offset: 0x00027874
			public float Min
			{
				get
				{
					return this.m_Min;
				}
				set
				{
					this.m_Min = value;
				}
			}

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x060038F7 RID: 14583 RVA: 0x0002967D File Offset: 0x0002787D
			// (set) Token: 0x060038F8 RID: 14584 RVA: 0x00029685 File Offset: 0x00027885
			public float Max
			{
				get
				{
					return this.m_Max;
				}
				set
				{
					this.m_Max = value;
				}
			}

			// Token: 0x060038F9 RID: 14585 RVA: 0x0002968E File Offset: 0x0002788E
			public override string ToString()
			{
				return string.Format("{0} / {1}", this.Current, this.Max);
			}

			// Token: 0x0400332D RID: 13101
			[SerializeField]
			private float m_Current;

			// Token: 0x0400332E RID: 13102
			[SerializeField]
			private float m_Min;

			// Token: 0x0400332F RID: 13103
			[SerializeField]
			private float m_Max;
		}

		// Token: 0x020008A5 RID: 2213
		[Serializable]
		public struct RandomFloat
		{
			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x060038FA RID: 14586 RVA: 0x000296B0 File Offset: 0x000278B0
			public float RandomValue
			{
				get
				{
					return Random.Range(this.m_Min, this.m_Max);
				}
			}

			// Token: 0x060038FB RID: 14587 RVA: 0x000296C3 File Offset: 0x000278C3
			public override string ToString()
			{
				return string.Format("{0} - {1}", this.m_Min, this.m_Max);
			}

			// Token: 0x04003330 RID: 13104
			[SerializeField]
			private float m_Min;

			// Token: 0x04003331 RID: 13105
			[SerializeField]
			private float m_Max;
		}

		// Token: 0x020008A6 RID: 2214
		[Serializable]
		public struct Definition
		{
			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x060038FC RID: 14588 RVA: 0x000296E5 File Offset: 0x000278E5
			public string Name
			{
				get
				{
					return this.m_Name;
				}
			}

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x060038FD RID: 14589 RVA: 0x000296ED File Offset: 0x000278ED
			public ItemProperty.Type Type
			{
				get
				{
					return this.m_Type;
				}
			}

			// Token: 0x04003332 RID: 13106
			[SerializeField]
			private string m_Name;

			// Token: 0x04003333 RID: 13107
			[SerializeField]
			private ItemProperty.Type m_Type;
		}

		// Token: 0x020008A7 RID: 2215
		[Serializable]
		public class Value
		{
			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x060038FE RID: 14590 RVA: 0x000296F5 File Offset: 0x000278F5
			public string Name
			{
				get
				{
					return this.m_Name;
				}
			}

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x060038FF RID: 14591 RVA: 0x000296FD File Offset: 0x000278FD
			// (set) Token: 0x06003900 RID: 14592 RVA: 0x00029705 File Offset: 0x00027905
			public ItemProperty.Type Type { get; private set; }

			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x06003901 RID: 14593 RVA: 0x0002970E File Offset: 0x0002790E
			public bool Bool
			{
				get
				{
					return this.m_Bool;
				}
			}

			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x06003902 RID: 14594 RVA: 0x00029716 File Offset: 0x00027916
			public ItemProperty.Int Int
			{
				get
				{
					return this.m_Int;
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x06003903 RID: 14595 RVA: 0x0002971E File Offset: 0x0002791E
			public ItemProperty.IntRange IntRange
			{
				get
				{
					return this.m_IntRange;
				}
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x06003904 RID: 14596 RVA: 0x00029726 File Offset: 0x00027926
			public ItemProperty.RandomInt RandomInt
			{
				get
				{
					return this.m_RandomInt;
				}
			}

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x06003905 RID: 14597 RVA: 0x0002972E File Offset: 0x0002792E
			public ItemProperty.Float Float
			{
				get
				{
					return this.m_Float;
				}
			}

			// Token: 0x170005F3 RID: 1523
			// (get) Token: 0x06003906 RID: 14598 RVA: 0x00029736 File Offset: 0x00027936
			public ItemProperty.FloatRange FloatRange
			{
				get
				{
					return this.m_FloatRange;
				}
			}

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x06003907 RID: 14599 RVA: 0x0002973E File Offset: 0x0002793E
			public ItemProperty.RandomFloat RandomFloat
			{
				get
				{
					return this.m_RandomFloat;
				}
			}

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x06003908 RID: 14600 RVA: 0x00029746 File Offset: 0x00027946
			public string String
			{
				get
				{
					return this.m_String;
				}
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x06003909 RID: 14601 RVA: 0x0002974E File Offset: 0x0002794E
			public AudioClip Sound
			{
				get
				{
					return this.m_Sound;
				}
			}

			// Token: 0x0600390A RID: 14602 RVA: 0x00029756 File Offset: 0x00027956
			public ItemProperty.Value GetClone()
			{
				return (ItemProperty.Value)base.MemberwiseClone();
			}

			// Token: 0x0600390B RID: 14603 RVA: 0x001A3DD8 File Offset: 0x001A1FD8
			public void SetValue(ItemProperty.Type type, object value)
			{
				if (type == ItemProperty.Type.Bool)
				{
					this.m_Bool = (bool)value;
				}
				else if (type == ItemProperty.Type.Int)
				{
					this.m_Int = (ItemProperty.Int)value;
				}
				else if (type == ItemProperty.Type.Float)
				{
					this.m_Float = (ItemProperty.Float)value;
				}
				else if (type == ItemProperty.Type.FloatRange)
				{
					this.m_FloatRange = (ItemProperty.FloatRange)value;
				}
				else if (type == ItemProperty.Type.IntRange)
				{
					this.m_IntRange = (ItemProperty.IntRange)value;
				}
				else if (type == ItemProperty.Type.RandomFloat)
				{
					this.m_RandomFloat = (ItemProperty.RandomFloat)value;
				}
				else if (type == ItemProperty.Type.RandomInt)
				{
					this.m_RandomInt = (ItemProperty.RandomInt)value;
				}
				else if (type == ItemProperty.Type.String)
				{
					this.m_String = (string)value;
				}
				else if (type == ItemProperty.Type.None)
				{
					return;
				}
				this.Changed.Send(this);
			}

			// Token: 0x0600390C RID: 14604 RVA: 0x001A3E88 File Offset: 0x001A2088
			public override string ToString()
			{
				if (this.m_Type == ItemProperty.Type.Bool)
				{
					return this.m_Bool.ToString();
				}
				if (this.m_Type == ItemProperty.Type.Int)
				{
					return this.m_Int.ToString();
				}
				if (this.m_Type == ItemProperty.Type.Float)
				{
					return this.m_Float.ToString();
				}
				if (this.m_Type == ItemProperty.Type.FloatRange)
				{
					return this.m_FloatRange.ToString();
				}
				if (this.m_Type == ItemProperty.Type.IntRange)
				{
					return this.m_IntRange.ToString();
				}
				if (this.m_Type == ItemProperty.Type.RandomFloat)
				{
					return this.m_RandomFloat.ToString();
				}
				if (this.m_Type == ItemProperty.Type.RandomInt)
				{
					return this.m_RandomInt.ToString();
				}
				if (this.m_Type == ItemProperty.Type.String)
				{
					return this.m_String;
				}
				return this.m_Name;
			}

			// Token: 0x04003334 RID: 13108
			public Message<ItemProperty.Value> Changed = new Message<ItemProperty.Value>();

			// Token: 0x04003336 RID: 13110
			[SerializeField]
			private string m_Name;

			// Token: 0x04003337 RID: 13111
			[SerializeField]
			private ItemProperty.Type m_Type;

			// Token: 0x04003338 RID: 13112
			[SerializeField]
			private bool m_Bool;

			// Token: 0x04003339 RID: 13113
			[SerializeField]
			private ItemProperty.Int m_Int;

			// Token: 0x0400333A RID: 13114
			[SerializeField]
			private ItemProperty.IntRange m_IntRange;

			// Token: 0x0400333B RID: 13115
			[SerializeField]
			private ItemProperty.RandomInt m_RandomInt;

			// Token: 0x0400333C RID: 13116
			[SerializeField]
			private ItemProperty.Float m_Float;

			// Token: 0x0400333D RID: 13117
			[SerializeField]
			private ItemProperty.FloatRange m_FloatRange;

			// Token: 0x0400333E RID: 13118
			[SerializeField]
			private ItemProperty.RandomFloat m_RandomFloat;

			// Token: 0x0400333F RID: 13119
			[SerializeField]
			private string m_String;

			// Token: 0x04003340 RID: 13120
			[SerializeField]
			private AudioClip m_Sound;
		}
	}
}
