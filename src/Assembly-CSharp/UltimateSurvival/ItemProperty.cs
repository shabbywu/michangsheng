using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005D9 RID: 1497
	public class ItemProperty
	{
		// Token: 0x020014AF RID: 5295
		public enum Type
		{
			// Token: 0x04006CD9 RID: 27865
			None,
			// Token: 0x04006CDA RID: 27866
			Bool,
			// Token: 0x04006CDB RID: 27867
			Int,
			// Token: 0x04006CDC RID: 27868
			IntRange,
			// Token: 0x04006CDD RID: 27869
			RandomInt,
			// Token: 0x04006CDE RID: 27870
			Float,
			// Token: 0x04006CDF RID: 27871
			FloatRange,
			// Token: 0x04006CE0 RID: 27872
			RandomFloat,
			// Token: 0x04006CE1 RID: 27873
			String,
			// Token: 0x04006CE2 RID: 27874
			Sound
		}

		// Token: 0x020014B0 RID: 5296
		[Serializable]
		public struct Int
		{
			// Token: 0x17000AC4 RID: 2756
			// (get) Token: 0x06008178 RID: 33144 RVA: 0x002D8EBC File Offset: 0x002D70BC
			// (set) Token: 0x06008179 RID: 33145 RVA: 0x002D8EC4 File Offset: 0x002D70C4
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

			// Token: 0x17000AC5 RID: 2757
			// (get) Token: 0x0600817A RID: 33146 RVA: 0x002D8ECD File Offset: 0x002D70CD
			public int Default
			{
				get
				{
					return this.m_Default;
				}
			}

			// Token: 0x17000AC6 RID: 2758
			// (get) Token: 0x0600817B RID: 33147 RVA: 0x002D8ED5 File Offset: 0x002D70D5
			public float Ratio
			{
				get
				{
					return (float)this.m_Current / (float)this.m_Default;
				}
			}

			// Token: 0x0600817C RID: 33148 RVA: 0x002D8EE6 File Offset: 0x002D70E6
			public override string ToString()
			{
				return this.m_Current.ToString();
			}

			// Token: 0x04006CE3 RID: 27875
			[SerializeField]
			private int m_Current;

			// Token: 0x04006CE4 RID: 27876
			[SerializeField]
			private int m_Default;
		}

		// Token: 0x020014B1 RID: 5297
		[Serializable]
		public struct IntRange
		{
			// Token: 0x17000AC7 RID: 2759
			// (get) Token: 0x0600817D RID: 33149 RVA: 0x002D8EF3 File Offset: 0x002D70F3
			// (set) Token: 0x0600817E RID: 33150 RVA: 0x002D8EFB File Offset: 0x002D70FB
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

			// Token: 0x17000AC8 RID: 2760
			// (get) Token: 0x0600817F RID: 33151 RVA: 0x002D8F15 File Offset: 0x002D7115
			public float Ratio
			{
				get
				{
					return (float)this.m_Current / (float)this.m_Max;
				}
			}

			// Token: 0x17000AC9 RID: 2761
			// (get) Token: 0x06008180 RID: 33152 RVA: 0x002D8F26 File Offset: 0x002D7126
			// (set) Token: 0x06008181 RID: 33153 RVA: 0x002D8F2E File Offset: 0x002D712E
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

			// Token: 0x17000ACA RID: 2762
			// (get) Token: 0x06008182 RID: 33154 RVA: 0x002D8F37 File Offset: 0x002D7137
			// (set) Token: 0x06008183 RID: 33155 RVA: 0x002D8F3F File Offset: 0x002D713F
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

			// Token: 0x06008184 RID: 33156 RVA: 0x002D8F48 File Offset: 0x002D7148
			public override string ToString()
			{
				return string.Format("{0} / {1}", this.Current, this.Max);
			}

			// Token: 0x04006CE5 RID: 27877
			[SerializeField]
			private int m_Current;

			// Token: 0x04006CE6 RID: 27878
			[SerializeField]
			private int m_Min;

			// Token: 0x04006CE7 RID: 27879
			[SerializeField]
			private int m_Max;
		}

		// Token: 0x020014B2 RID: 5298
		[Serializable]
		public struct RandomInt
		{
			// Token: 0x17000ACB RID: 2763
			// (get) Token: 0x06008185 RID: 33157 RVA: 0x002D8F6A File Offset: 0x002D716A
			public int RandomValue
			{
				get
				{
					return Random.Range(this.m_Min, this.m_Max);
				}
			}

			// Token: 0x06008186 RID: 33158 RVA: 0x002D8F7D File Offset: 0x002D717D
			public override string ToString()
			{
				return string.Format("{0} - {1}", this.m_Min, this.m_Max);
			}

			// Token: 0x04006CE8 RID: 27880
			[SerializeField]
			private int m_Min;

			// Token: 0x04006CE9 RID: 27881
			[SerializeField]
			private int m_Max;
		}

		// Token: 0x020014B3 RID: 5299
		[Serializable]
		public struct Float
		{
			// Token: 0x17000ACC RID: 2764
			// (get) Token: 0x06008187 RID: 33159 RVA: 0x002D8F9F File Offset: 0x002D719F
			// (set) Token: 0x06008188 RID: 33160 RVA: 0x002D8FA7 File Offset: 0x002D71A7
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

			// Token: 0x17000ACD RID: 2765
			// (get) Token: 0x06008189 RID: 33161 RVA: 0x002D8FB0 File Offset: 0x002D71B0
			public float Default
			{
				get
				{
					return this.m_Default;
				}
			}

			// Token: 0x17000ACE RID: 2766
			// (get) Token: 0x0600818A RID: 33162 RVA: 0x002D8FB8 File Offset: 0x002D71B8
			public float Ratio
			{
				get
				{
					return this.m_Current / this.m_Default;
				}
			}

			// Token: 0x0600818B RID: 33163 RVA: 0x002D8FC7 File Offset: 0x002D71C7
			public override string ToString()
			{
				return this.m_Current.ToString();
			}

			// Token: 0x04006CEA RID: 27882
			[SerializeField]
			private float m_Current;

			// Token: 0x04006CEB RID: 27883
			[SerializeField]
			private float m_Default;
		}

		// Token: 0x020014B4 RID: 5300
		[Serializable]
		public struct FloatRange
		{
			// Token: 0x17000ACF RID: 2767
			// (get) Token: 0x0600818C RID: 33164 RVA: 0x002D8FD4 File Offset: 0x002D71D4
			// (set) Token: 0x0600818D RID: 33165 RVA: 0x002D8FDC File Offset: 0x002D71DC
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

			// Token: 0x17000AD0 RID: 2768
			// (get) Token: 0x0600818E RID: 33166 RVA: 0x002D8FF6 File Offset: 0x002D71F6
			public float Ratio
			{
				get
				{
					return this.m_Current / this.m_Max;
				}
			}

			// Token: 0x17000AD1 RID: 2769
			// (get) Token: 0x0600818F RID: 33167 RVA: 0x002D9005 File Offset: 0x002D7205
			// (set) Token: 0x06008190 RID: 33168 RVA: 0x002D900D File Offset: 0x002D720D
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

			// Token: 0x17000AD2 RID: 2770
			// (get) Token: 0x06008191 RID: 33169 RVA: 0x002D9016 File Offset: 0x002D7216
			// (set) Token: 0x06008192 RID: 33170 RVA: 0x002D901E File Offset: 0x002D721E
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

			// Token: 0x06008193 RID: 33171 RVA: 0x002D9027 File Offset: 0x002D7227
			public override string ToString()
			{
				return string.Format("{0} / {1}", this.Current, this.Max);
			}

			// Token: 0x04006CEC RID: 27884
			[SerializeField]
			private float m_Current;

			// Token: 0x04006CED RID: 27885
			[SerializeField]
			private float m_Min;

			// Token: 0x04006CEE RID: 27886
			[SerializeField]
			private float m_Max;
		}

		// Token: 0x020014B5 RID: 5301
		[Serializable]
		public struct RandomFloat
		{
			// Token: 0x17000AD3 RID: 2771
			// (get) Token: 0x06008194 RID: 33172 RVA: 0x002D9049 File Offset: 0x002D7249
			public float RandomValue
			{
				get
				{
					return Random.Range(this.m_Min, this.m_Max);
				}
			}

			// Token: 0x06008195 RID: 33173 RVA: 0x002D905C File Offset: 0x002D725C
			public override string ToString()
			{
				return string.Format("{0} - {1}", this.m_Min, this.m_Max);
			}

			// Token: 0x04006CEF RID: 27887
			[SerializeField]
			private float m_Min;

			// Token: 0x04006CF0 RID: 27888
			[SerializeField]
			private float m_Max;
		}

		// Token: 0x020014B6 RID: 5302
		[Serializable]
		public struct Definition
		{
			// Token: 0x17000AD4 RID: 2772
			// (get) Token: 0x06008196 RID: 33174 RVA: 0x002D907E File Offset: 0x002D727E
			public string Name
			{
				get
				{
					return this.m_Name;
				}
			}

			// Token: 0x17000AD5 RID: 2773
			// (get) Token: 0x06008197 RID: 33175 RVA: 0x002D9086 File Offset: 0x002D7286
			public ItemProperty.Type Type
			{
				get
				{
					return this.m_Type;
				}
			}

			// Token: 0x04006CF1 RID: 27889
			[SerializeField]
			private string m_Name;

			// Token: 0x04006CF2 RID: 27890
			[SerializeField]
			private ItemProperty.Type m_Type;
		}

		// Token: 0x020014B7 RID: 5303
		[Serializable]
		public class Value
		{
			// Token: 0x17000AD6 RID: 2774
			// (get) Token: 0x06008198 RID: 33176 RVA: 0x002D908E File Offset: 0x002D728E
			public string Name
			{
				get
				{
					return this.m_Name;
				}
			}

			// Token: 0x17000AD7 RID: 2775
			// (get) Token: 0x06008199 RID: 33177 RVA: 0x002D9096 File Offset: 0x002D7296
			// (set) Token: 0x0600819A RID: 33178 RVA: 0x002D909E File Offset: 0x002D729E
			public ItemProperty.Type Type { get; private set; }

			// Token: 0x17000AD8 RID: 2776
			// (get) Token: 0x0600819B RID: 33179 RVA: 0x002D90A7 File Offset: 0x002D72A7
			public bool Bool
			{
				get
				{
					return this.m_Bool;
				}
			}

			// Token: 0x17000AD9 RID: 2777
			// (get) Token: 0x0600819C RID: 33180 RVA: 0x002D90AF File Offset: 0x002D72AF
			public ItemProperty.Int Int
			{
				get
				{
					return this.m_Int;
				}
			}

			// Token: 0x17000ADA RID: 2778
			// (get) Token: 0x0600819D RID: 33181 RVA: 0x002D90B7 File Offset: 0x002D72B7
			public ItemProperty.IntRange IntRange
			{
				get
				{
					return this.m_IntRange;
				}
			}

			// Token: 0x17000ADB RID: 2779
			// (get) Token: 0x0600819E RID: 33182 RVA: 0x002D90BF File Offset: 0x002D72BF
			public ItemProperty.RandomInt RandomInt
			{
				get
				{
					return this.m_RandomInt;
				}
			}

			// Token: 0x17000ADC RID: 2780
			// (get) Token: 0x0600819F RID: 33183 RVA: 0x002D90C7 File Offset: 0x002D72C7
			public ItemProperty.Float Float
			{
				get
				{
					return this.m_Float;
				}
			}

			// Token: 0x17000ADD RID: 2781
			// (get) Token: 0x060081A0 RID: 33184 RVA: 0x002D90CF File Offset: 0x002D72CF
			public ItemProperty.FloatRange FloatRange
			{
				get
				{
					return this.m_FloatRange;
				}
			}

			// Token: 0x17000ADE RID: 2782
			// (get) Token: 0x060081A1 RID: 33185 RVA: 0x002D90D7 File Offset: 0x002D72D7
			public ItemProperty.RandomFloat RandomFloat
			{
				get
				{
					return this.m_RandomFloat;
				}
			}

			// Token: 0x17000ADF RID: 2783
			// (get) Token: 0x060081A2 RID: 33186 RVA: 0x002D90DF File Offset: 0x002D72DF
			public string String
			{
				get
				{
					return this.m_String;
				}
			}

			// Token: 0x17000AE0 RID: 2784
			// (get) Token: 0x060081A3 RID: 33187 RVA: 0x002D90E7 File Offset: 0x002D72E7
			public AudioClip Sound
			{
				get
				{
					return this.m_Sound;
				}
			}

			// Token: 0x060081A4 RID: 33188 RVA: 0x002D90EF File Offset: 0x002D72EF
			public ItemProperty.Value GetClone()
			{
				return (ItemProperty.Value)base.MemberwiseClone();
			}

			// Token: 0x060081A5 RID: 33189 RVA: 0x002D90FC File Offset: 0x002D72FC
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

			// Token: 0x060081A6 RID: 33190 RVA: 0x002D91AC File Offset: 0x002D73AC
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

			// Token: 0x04006CF3 RID: 27891
			public Message<ItemProperty.Value> Changed = new Message<ItemProperty.Value>();

			// Token: 0x04006CF5 RID: 27893
			[SerializeField]
			private string m_Name;

			// Token: 0x04006CF6 RID: 27894
			[SerializeField]
			private ItemProperty.Type m_Type;

			// Token: 0x04006CF7 RID: 27895
			[SerializeField]
			private bool m_Bool;

			// Token: 0x04006CF8 RID: 27896
			[SerializeField]
			private ItemProperty.Int m_Int;

			// Token: 0x04006CF9 RID: 27897
			[SerializeField]
			private ItemProperty.IntRange m_IntRange;

			// Token: 0x04006CFA RID: 27898
			[SerializeField]
			private ItemProperty.RandomInt m_RandomInt;

			// Token: 0x04006CFB RID: 27899
			[SerializeField]
			private ItemProperty.Float m_Float;

			// Token: 0x04006CFC RID: 27900
			[SerializeField]
			private ItemProperty.FloatRange m_FloatRange;

			// Token: 0x04006CFD RID: 27901
			[SerializeField]
			private ItemProperty.RandomFloat m_RandomFloat;

			// Token: 0x04006CFE RID: 27902
			[SerializeField]
			private string m_String;

			// Token: 0x04006CFF RID: 27903
			[SerializeField]
			private AudioClip m_Sound;
		}
	}
}
