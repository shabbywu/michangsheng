using System;
using System.Collections.Generic;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000E7D RID: 3709
	public class AdRequest
	{
		// Token: 0x060058CF RID: 22735 RVA: 0x00247C30 File Offset: 0x00245E30
		private AdRequest(AdRequest.Builder builder)
		{
			this.testDevices = builder.TestDevices;
			this.keywords = builder.Keywords;
			this.birthday = builder.Birthday;
			this.gender = builder.Gender;
			this.tagForChildDirectedTreatment = builder.ChildDirectedTreatmentTag;
			this.extras = builder.Extras;
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060058D0 RID: 22736 RVA: 0x0003F3A3 File Offset: 0x0003D5A3
		public List<string> TestDevices
		{
			get
			{
				return this.testDevices;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060058D1 RID: 22737 RVA: 0x0003F3AB File Offset: 0x0003D5AB
		public HashSet<string> Keywords
		{
			get
			{
				return this.keywords;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060058D2 RID: 22738 RVA: 0x0003F3B3 File Offset: 0x0003D5B3
		public DateTime? Birthday
		{
			get
			{
				return this.birthday;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060058D3 RID: 22739 RVA: 0x0003F3BB File Offset: 0x0003D5BB
		public Gender? Gender
		{
			get
			{
				return this.gender;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060058D4 RID: 22740 RVA: 0x0003F3C3 File Offset: 0x0003D5C3
		public bool? TagForChildDirectedTreatment
		{
			get
			{
				return this.tagForChildDirectedTreatment;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060058D5 RID: 22741 RVA: 0x0003F3CB File Offset: 0x0003D5CB
		public Dictionary<string, string> Extras
		{
			get
			{
				return this.extras;
			}
		}

		// Token: 0x04005884 RID: 22660
		public const string Version = "2.3.1";

		// Token: 0x04005885 RID: 22661
		public const string TestDeviceSimulator = "SIMULATOR";

		// Token: 0x04005886 RID: 22662
		private List<string> testDevices;

		// Token: 0x04005887 RID: 22663
		private HashSet<string> keywords;

		// Token: 0x04005888 RID: 22664
		private DateTime? birthday;

		// Token: 0x04005889 RID: 22665
		private Gender? gender;

		// Token: 0x0400588A RID: 22666
		private bool? tagForChildDirectedTreatment;

		// Token: 0x0400588B RID: 22667
		private Dictionary<string, string> extras;

		// Token: 0x02000E7E RID: 3710
		public class Builder
		{
			// Token: 0x060058D6 RID: 22742 RVA: 0x00247C8C File Offset: 0x00245E8C
			public Builder()
			{
				this.testDevices = new List<string>();
				this.keywords = new HashSet<string>();
				this.birthday = null;
				this.gender = null;
				this.tagForChildDirectedTreatment = null;
				this.extras = new Dictionary<string, string>();
			}

			// Token: 0x060058D7 RID: 22743 RVA: 0x0003F3D3 File Offset: 0x0003D5D3
			public AdRequest.Builder AddKeyword(string keyword)
			{
				this.keywords.Add(keyword);
				return this;
			}

			// Token: 0x060058D8 RID: 22744 RVA: 0x0003F3E3 File Offset: 0x0003D5E3
			public AdRequest.Builder AddTestDevice(string deviceId)
			{
				this.testDevices.Add(deviceId);
				return this;
			}

			// Token: 0x060058D9 RID: 22745 RVA: 0x0003F3F2 File Offset: 0x0003D5F2
			public AdRequest Build()
			{
				return new AdRequest(this);
			}

			// Token: 0x060058DA RID: 22746 RVA: 0x0003F3FA File Offset: 0x0003D5FA
			public AdRequest.Builder SetBirthday(DateTime birthday)
			{
				this.birthday = new DateTime?(birthday);
				return this;
			}

			// Token: 0x060058DB RID: 22747 RVA: 0x0003F409 File Offset: 0x0003D609
			public AdRequest.Builder SetGender(Gender gender)
			{
				this.gender = new Gender?(gender);
				return this;
			}

			// Token: 0x060058DC RID: 22748 RVA: 0x0003F418 File Offset: 0x0003D618
			public AdRequest.Builder TagForChildDirectedTreatment(bool tagForChildDirectedTreatment)
			{
				this.tagForChildDirectedTreatment = new bool?(tagForChildDirectedTreatment);
				return this;
			}

			// Token: 0x060058DD RID: 22749 RVA: 0x0003F427 File Offset: 0x0003D627
			public AdRequest.Builder AddExtra(string key, string value)
			{
				this.extras.Add(key, value);
				return this;
			}

			// Token: 0x1700083F RID: 2111
			// (get) Token: 0x060058DE RID: 22750 RVA: 0x0003F437 File Offset: 0x0003D637
			internal List<string> TestDevices
			{
				get
				{
					return this.testDevices;
				}
			}

			// Token: 0x17000840 RID: 2112
			// (get) Token: 0x060058DF RID: 22751 RVA: 0x0003F43F File Offset: 0x0003D63F
			internal HashSet<string> Keywords
			{
				get
				{
					return this.keywords;
				}
			}

			// Token: 0x17000841 RID: 2113
			// (get) Token: 0x060058E0 RID: 22752 RVA: 0x0003F447 File Offset: 0x0003D647
			internal DateTime? Birthday
			{
				get
				{
					return this.birthday;
				}
			}

			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x060058E1 RID: 22753 RVA: 0x0003F44F File Offset: 0x0003D64F
			internal Gender? Gender
			{
				get
				{
					return this.gender;
				}
			}

			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x060058E2 RID: 22754 RVA: 0x0003F457 File Offset: 0x0003D657
			internal bool? ChildDirectedTreatmentTag
			{
				get
				{
					return this.tagForChildDirectedTreatment;
				}
			}

			// Token: 0x17000844 RID: 2116
			// (get) Token: 0x060058E3 RID: 22755 RVA: 0x0003F45F File Offset: 0x0003D65F
			internal Dictionary<string, string> Extras
			{
				get
				{
					return this.extras;
				}
			}

			// Token: 0x0400588C RID: 22668
			private List<string> testDevices;

			// Token: 0x0400588D RID: 22669
			private HashSet<string> keywords;

			// Token: 0x0400588E RID: 22670
			private DateTime? birthday;

			// Token: 0x0400588F RID: 22671
			private Gender? gender;

			// Token: 0x04005890 RID: 22672
			private bool? tagForChildDirectedTreatment;

			// Token: 0x04005891 RID: 22673
			private Dictionary<string, string> extras;
		}
	}
}
