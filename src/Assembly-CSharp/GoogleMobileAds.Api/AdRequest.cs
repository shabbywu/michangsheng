using System;
using System.Collections.Generic;

namespace GoogleMobileAds.Api;

public class AdRequest
{
	public class Builder
	{
		private List<string> testDevices;

		private HashSet<string> keywords;

		private DateTime? birthday;

		private Gender? gender;

		private bool? tagForChildDirectedTreatment;

		private Dictionary<string, string> extras;

		internal List<string> TestDevices => testDevices;

		internal HashSet<string> Keywords => keywords;

		internal DateTime? Birthday => birthday;

		internal Gender? Gender => gender;

		internal bool? ChildDirectedTreatmentTag => tagForChildDirectedTreatment;

		internal Dictionary<string, string> Extras => extras;

		public Builder()
		{
			testDevices = new List<string>();
			keywords = new HashSet<string>();
			birthday = null;
			gender = null;
			tagForChildDirectedTreatment = null;
			extras = new Dictionary<string, string>();
		}

		public Builder AddKeyword(string keyword)
		{
			keywords.Add(keyword);
			return this;
		}

		public Builder AddTestDevice(string deviceId)
		{
			testDevices.Add(deviceId);
			return this;
		}

		public AdRequest Build()
		{
			return new AdRequest(this);
		}

		public Builder SetBirthday(DateTime birthday)
		{
			this.birthday = birthday;
			return this;
		}

		public Builder SetGender(Gender gender)
		{
			this.gender = gender;
			return this;
		}

		public Builder TagForChildDirectedTreatment(bool tagForChildDirectedTreatment)
		{
			this.tagForChildDirectedTreatment = tagForChildDirectedTreatment;
			return this;
		}

		public Builder AddExtra(string key, string value)
		{
			extras.Add(key, value);
			return this;
		}
	}

	public const string Version = "2.3.1";

	public const string TestDeviceSimulator = "SIMULATOR";

	private List<string> testDevices;

	private HashSet<string> keywords;

	private DateTime? birthday;

	private Gender? gender;

	private bool? tagForChildDirectedTreatment;

	private Dictionary<string, string> extras;

	public List<string> TestDevices => testDevices;

	public HashSet<string> Keywords => keywords;

	public DateTime? Birthday => birthday;

	public Gender? Gender => gender;

	public bool? TagForChildDirectedTreatment => tagForChildDirectedTreatment;

	public Dictionary<string, string> Extras => extras;

	private AdRequest(Builder builder)
	{
		testDevices = builder.TestDevices;
		keywords = builder.Keywords;
		birthday = builder.Birthday;
		gender = builder.Gender;
		tagForChildDirectedTreatment = builder.ChildDirectedTreatmentTag;
		extras = builder.Extras;
	}
}
