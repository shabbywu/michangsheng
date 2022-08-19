using System;
using System.Collections.Generic;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000B11 RID: 2833
	public class AdRequest
	{
		// Token: 0x06004EDB RID: 20187 RVA: 0x002179F8 File Offset: 0x00215BF8
		private AdRequest(AdRequest.Builder builder)
		{
			this.testDevices = builder.TestDevices;
			this.keywords = builder.Keywords;
			this.birthday = builder.Birthday;
			this.gender = builder.Gender;
			this.tagForChildDirectedTreatment = builder.ChildDirectedTreatmentTag;
			this.extras = builder.Extras;
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06004EDC RID: 20188 RVA: 0x00217A53 File Offset: 0x00215C53
		public List<string> TestDevices
		{
			get
			{
				return this.testDevices;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06004EDD RID: 20189 RVA: 0x00217A5B File Offset: 0x00215C5B
		public HashSet<string> Keywords
		{
			get
			{
				return this.keywords;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06004EDE RID: 20190 RVA: 0x00217A63 File Offset: 0x00215C63
		public DateTime? Birthday
		{
			get
			{
				return this.birthday;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x00217A6B File Offset: 0x00215C6B
		public Gender? Gender
		{
			get
			{
				return this.gender;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06004EE0 RID: 20192 RVA: 0x00217A73 File Offset: 0x00215C73
		public bool? TagForChildDirectedTreatment
		{
			get
			{
				return this.tagForChildDirectedTreatment;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06004EE1 RID: 20193 RVA: 0x00217A7B File Offset: 0x00215C7B
		public Dictionary<string, string> Extras
		{
			get
			{
				return this.extras;
			}
		}

		// Token: 0x04004E3B RID: 20027
		public const string Version = "2.3.1";

		// Token: 0x04004E3C RID: 20028
		public const string TestDeviceSimulator = "SIMULATOR";

		// Token: 0x04004E3D RID: 20029
		private List<string> testDevices;

		// Token: 0x04004E3E RID: 20030
		private HashSet<string> keywords;

		// Token: 0x04004E3F RID: 20031
		private DateTime? birthday;

		// Token: 0x04004E40 RID: 20032
		private Gender? gender;

		// Token: 0x04004E41 RID: 20033
		private bool? tagForChildDirectedTreatment;

		// Token: 0x04004E42 RID: 20034
		private Dictionary<string, string> extras;

		// Token: 0x020015E2 RID: 5602
		public class Builder
		{
			// Token: 0x0600855A RID: 34138 RVA: 0x002E43B0 File Offset: 0x002E25B0
			public Builder()
			{
				this.testDevices = new List<string>();
				this.keywords = new HashSet<string>();
				this.birthday = null;
				this.gender = null;
				this.tagForChildDirectedTreatment = null;
				this.extras = new Dictionary<string, string>();
			}

			// Token: 0x0600855B RID: 34139 RVA: 0x002E4408 File Offset: 0x002E2608
			public AdRequest.Builder AddKeyword(string keyword)
			{
				this.keywords.Add(keyword);
				return this;
			}

			// Token: 0x0600855C RID: 34140 RVA: 0x002E4418 File Offset: 0x002E2618
			public AdRequest.Builder AddTestDevice(string deviceId)
			{
				this.testDevices.Add(deviceId);
				return this;
			}

			// Token: 0x0600855D RID: 34141 RVA: 0x002E4427 File Offset: 0x002E2627
			public AdRequest Build()
			{
				return new AdRequest(this);
			}

			// Token: 0x0600855E RID: 34142 RVA: 0x002E442F File Offset: 0x002E262F
			public AdRequest.Builder SetBirthday(DateTime birthday)
			{
				this.birthday = new DateTime?(birthday);
				return this;
			}

			// Token: 0x0600855F RID: 34143 RVA: 0x002E443E File Offset: 0x002E263E
			public AdRequest.Builder SetGender(Gender gender)
			{
				this.gender = new Gender?(gender);
				return this;
			}

			// Token: 0x06008560 RID: 34144 RVA: 0x002E444D File Offset: 0x002E264D
			public AdRequest.Builder TagForChildDirectedTreatment(bool tagForChildDirectedTreatment)
			{
				this.tagForChildDirectedTreatment = new bool?(tagForChildDirectedTreatment);
				return this;
			}

			// Token: 0x06008561 RID: 34145 RVA: 0x002E445C File Offset: 0x002E265C
			public AdRequest.Builder AddExtra(string key, string value)
			{
				this.extras.Add(key, value);
				return this;
			}

			// Token: 0x17000B69 RID: 2921
			// (get) Token: 0x06008562 RID: 34146 RVA: 0x002E446C File Offset: 0x002E266C
			internal List<string> TestDevices
			{
				get
				{
					return this.testDevices;
				}
			}

			// Token: 0x17000B6A RID: 2922
			// (get) Token: 0x06008563 RID: 34147 RVA: 0x002E4474 File Offset: 0x002E2674
			internal HashSet<string> Keywords
			{
				get
				{
					return this.keywords;
				}
			}

			// Token: 0x17000B6B RID: 2923
			// (get) Token: 0x06008564 RID: 34148 RVA: 0x002E447C File Offset: 0x002E267C
			internal DateTime? Birthday
			{
				get
				{
					return this.birthday;
				}
			}

			// Token: 0x17000B6C RID: 2924
			// (get) Token: 0x06008565 RID: 34149 RVA: 0x002E4484 File Offset: 0x002E2684
			internal Gender? Gender
			{
				get
				{
					return this.gender;
				}
			}

			// Token: 0x17000B6D RID: 2925
			// (get) Token: 0x06008566 RID: 34150 RVA: 0x002E448C File Offset: 0x002E268C
			internal bool? ChildDirectedTreatmentTag
			{
				get
				{
					return this.tagForChildDirectedTreatment;
				}
			}

			// Token: 0x17000B6E RID: 2926
			// (get) Token: 0x06008567 RID: 34151 RVA: 0x002E4494 File Offset: 0x002E2694
			internal Dictionary<string, string> Extras
			{
				get
				{
					return this.extras;
				}
			}

			// Token: 0x040070A0 RID: 28832
			private List<string> testDevices;

			// Token: 0x040070A1 RID: 28833
			private HashSet<string> keywords;

			// Token: 0x040070A2 RID: 28834
			private DateTime? birthday;

			// Token: 0x040070A3 RID: 28835
			private Gender? gender;

			// Token: 0x040070A4 RID: 28836
			private bool? tagForChildDirectedTreatment;

			// Token: 0x040070A5 RID: 28837
			private Dictionary<string, string> extras;
		}
	}
}
