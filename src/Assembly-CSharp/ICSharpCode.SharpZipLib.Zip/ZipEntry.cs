using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip;

public class ZipEntry
{
	[Flags]
	private enum Known : byte
	{
		None = 0,
		Size = 1,
		CompressedSize = 2,
		Crc = 4,
		Time = 8,
		ExternalAttributes = 0x10
	}

	private Known known;

	private int externalFileAttributes = -1;

	private ushort versionMadeBy;

	private string name;

	private ulong size;

	private ulong compressedSize;

	private ushort versionToExtract;

	private uint crc;

	private DateTime dateTime;

	private CompressionMethod method = CompressionMethod.Deflated;

	private byte[] extra;

	private string comment;

	private int flags;

	private long zipFileIndex = -1L;

	private long offset;

	private bool forceZip64_;

	private byte cryptoCheckValue_;

	private int _aesVer;

	private int _aesEncryptionStrength;

	public bool HasCrc => (known & Known.Crc) != 0;

	public bool IsCrypted
	{
		get
		{
			return (flags & 1) != 0;
		}
		set
		{
			if (value)
			{
				flags |= 1;
			}
			else
			{
				flags &= -2;
			}
		}
	}

	public bool IsUnicodeText
	{
		get
		{
			return (flags & 0x800) != 0;
		}
		set
		{
			if (value)
			{
				flags |= 2048;
			}
			else
			{
				flags &= -2049;
			}
		}
	}

	internal byte CryptoCheckValue
	{
		get
		{
			return cryptoCheckValue_;
		}
		set
		{
			cryptoCheckValue_ = value;
		}
	}

	public int Flags
	{
		get
		{
			return flags;
		}
		set
		{
			flags = value;
		}
	}

	public long ZipFileIndex
	{
		get
		{
			return zipFileIndex;
		}
		set
		{
			zipFileIndex = value;
		}
	}

	public long Offset
	{
		get
		{
			return offset;
		}
		set
		{
			offset = value;
		}
	}

	public int ExternalFileAttributes
	{
		get
		{
			if ((known & Known.ExternalAttributes) == 0)
			{
				return -1;
			}
			return externalFileAttributes;
		}
		set
		{
			externalFileAttributes = value;
			known |= Known.ExternalAttributes;
		}
	}

	public int VersionMadeBy => versionMadeBy & 0xFF;

	public bool IsDOSEntry
	{
		get
		{
			if (HostSystem != 0)
			{
				return HostSystem == 10;
			}
			return true;
		}
	}

	public int HostSystem
	{
		get
		{
			return (versionMadeBy >> 8) & 0xFF;
		}
		set
		{
			versionMadeBy &= 255;
			versionMadeBy |= (ushort)((value & 0xFF) << 8);
		}
	}

	public int Version
	{
		get
		{
			if (versionToExtract != 0)
			{
				return versionToExtract & 0xFF;
			}
			int result = 10;
			if (AESKeySize > 0)
			{
				result = 51;
			}
			else if (CentralHeaderRequiresZip64)
			{
				result = 45;
			}
			else if (CompressionMethod.Deflated == method)
			{
				result = 20;
			}
			else if (CompressionMethod.BZip2 == method)
			{
				result = 46;
			}
			else if (IsDirectory)
			{
				result = 20;
			}
			else if (IsCrypted)
			{
				result = 20;
			}
			else if (HasDosAttributes(8))
			{
				result = 11;
			}
			return result;
		}
	}

	public bool CanDecompress
	{
		get
		{
			if (Version <= 51 && (Version == 10 || Version == 11 || Version == 20 || Version == 45 || Version == 46 || Version == 51))
			{
				return IsCompressionMethodSupported();
			}
			return false;
		}
	}

	public bool LocalHeaderRequiresZip64
	{
		get
		{
			bool flag = forceZip64_;
			if (!flag)
			{
				ulong num = compressedSize;
				if (versionToExtract == 0 && IsCrypted)
				{
					num += (ulong)EncryptionOverheadSize;
				}
				flag = (size >= uint.MaxValue || num >= uint.MaxValue) && (versionToExtract == 0 || versionToExtract >= 45);
			}
			return flag;
		}
	}

	public bool CentralHeaderRequiresZip64
	{
		get
		{
			if (!LocalHeaderRequiresZip64)
			{
				return offset >= uint.MaxValue;
			}
			return true;
		}
	}

	public long DosTime
	{
		get
		{
			if ((known & Known.Time) == 0)
			{
				return 0L;
			}
			uint num = (uint)DateTime.Year;
			uint num2 = (uint)DateTime.Month;
			uint num3 = (uint)DateTime.Day;
			uint num4 = (uint)DateTime.Hour;
			uint num5 = (uint)DateTime.Minute;
			uint num6 = (uint)DateTime.Second;
			if (num < 1980)
			{
				num = 1980u;
				num2 = 1u;
				num3 = 1u;
				num4 = 0u;
				num5 = 0u;
				num6 = 0u;
			}
			else if (num > 2107)
			{
				num = 2107u;
				num2 = 12u;
				num3 = 31u;
				num4 = 23u;
				num5 = 59u;
				num6 = 59u;
			}
			return (((num - 1980) & 0x7F) << 25) | (num2 << 21) | (num3 << 16) | (num4 << 11) | (num5 << 5) | (num6 >> 1);
		}
		set
		{
			uint num = (uint)value;
			uint second = Math.Min(59u, 2 * (num & 0x1F));
			uint minute = Math.Min(59u, (num >> 5) & 0x3Fu);
			uint hour = Math.Min(23u, (num >> 11) & 0x1Fu);
			uint month = Math.Max(1u, Math.Min(12u, (uint)(int)(value >> 21) & 0xFu));
			uint year = ((num >> 25) & 0x7F) + 1980;
			int day = Math.Max(1, Math.Min(DateTime.DaysInMonth((int)year, (int)month), (int)((value >> 16) & 0x1F)));
			DateTime = new DateTime((int)year, (int)month, day, (int)hour, (int)minute, (int)second, DateTimeKind.Unspecified);
		}
	}

	public DateTime DateTime
	{
		get
		{
			return dateTime;
		}
		set
		{
			dateTime = value;
			known |= Known.Time;
		}
	}

	public string Name
	{
		get
		{
			return name;
		}
		internal set
		{
			name = value;
		}
	}

	public long Size
	{
		get
		{
			if ((known & Known.Size) == 0)
			{
				return -1L;
			}
			return (long)size;
		}
		set
		{
			size = (ulong)value;
			known |= Known.Size;
		}
	}

	public long CompressedSize
	{
		get
		{
			if ((known & Known.CompressedSize) == 0)
			{
				return -1L;
			}
			return (long)compressedSize;
		}
		set
		{
			compressedSize = (ulong)value;
			known |= Known.CompressedSize;
		}
	}

	public long Crc
	{
		get
		{
			if ((known & Known.Crc) == 0)
			{
				return -1L;
			}
			return (long)crc & 0xFFFFFFFFL;
		}
		set
		{
			if ((crc & -4294967296L) != 0L)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			crc = (uint)value;
			known |= Known.Crc;
		}
	}

	public CompressionMethod CompressionMethod
	{
		get
		{
			return method;
		}
		set
		{
			if (!IsCompressionMethodSupported(value))
			{
				throw new NotSupportedException("Compression method not supported");
			}
			method = value;
		}
	}

	internal CompressionMethod CompressionMethodForHeader
	{
		get
		{
			if (AESKeySize <= 0)
			{
				return method;
			}
			return CompressionMethod.WinZipAES;
		}
	}

	public byte[] ExtraData
	{
		get
		{
			return extra;
		}
		set
		{
			if (value == null)
			{
				extra = null;
				return;
			}
			if (value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			extra = new byte[value.Length];
			Array.Copy(value, 0, extra, 0, value.Length);
		}
	}

	public int AESKeySize
	{
		get
		{
			return _aesEncryptionStrength switch
			{
				0 => 0, 
				1 => 128, 
				2 => 192, 
				3 => 256, 
				_ => throw new ZipException("Invalid AESEncryptionStrength " + _aesEncryptionStrength), 
			};
		}
		set
		{
			switch (value)
			{
			case 0:
				_aesEncryptionStrength = 0;
				break;
			case 128:
				_aesEncryptionStrength = 1;
				break;
			case 256:
				_aesEncryptionStrength = 3;
				break;
			default:
				throw new ZipException("AESKeySize must be 0, 128 or 256: " + value);
			}
		}
	}

	internal byte AESEncryptionStrength => (byte)_aesEncryptionStrength;

	internal int AESSaltLen => AESKeySize / 16;

	internal int AESOverheadSize => 12 + AESSaltLen;

	internal int EncryptionOverheadSize
	{
		get
		{
			if (!IsCrypted)
			{
				return 0;
			}
			if (_aesEncryptionStrength == 0)
			{
				return 12;
			}
			return AESOverheadSize;
		}
	}

	public string Comment
	{
		get
		{
			return comment;
		}
		set
		{
			if (value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", "cannot exceed 65535");
			}
			comment = value;
		}
	}

	public bool IsDirectory
	{
		get
		{
			int length = name.Length;
			if (length <= 0 || (name[length - 1] != '/' && name[length - 1] != '\\'))
			{
				return HasDosAttributes(16);
			}
			return true;
		}
	}

	public bool IsFile
	{
		get
		{
			if (!IsDirectory)
			{
				return !HasDosAttributes(8);
			}
			return false;
		}
	}

	public ZipEntry(string name)
		: this(name, 0, 51, CompressionMethod.Deflated)
	{
	}

	internal ZipEntry(string name, int versionRequiredToExtract)
		: this(name, versionRequiredToExtract, 51, CompressionMethod.Deflated)
	{
	}

	internal ZipEntry(string name, int versionRequiredToExtract, int madeByInfo, CompressionMethod method)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (name.Length > 65535)
		{
			throw new ArgumentException("Name is too long", "name");
		}
		if (versionRequiredToExtract != 0 && versionRequiredToExtract < 10)
		{
			throw new ArgumentOutOfRangeException("versionRequiredToExtract");
		}
		DateTime = DateTime.Now;
		this.name = name;
		versionMadeBy = (ushort)madeByInfo;
		versionToExtract = (ushort)versionRequiredToExtract;
		this.method = method;
		IsUnicodeText = ZipStrings.UseUnicode;
	}

	[Obsolete("Use Clone instead")]
	public ZipEntry(ZipEntry entry)
	{
		if (entry == null)
		{
			throw new ArgumentNullException("entry");
		}
		known = entry.known;
		name = entry.name;
		size = entry.size;
		compressedSize = entry.compressedSize;
		crc = entry.crc;
		dateTime = entry.DateTime;
		method = entry.method;
		comment = entry.comment;
		versionToExtract = entry.versionToExtract;
		versionMadeBy = entry.versionMadeBy;
		externalFileAttributes = entry.externalFileAttributes;
		flags = entry.flags;
		zipFileIndex = entry.zipFileIndex;
		offset = entry.offset;
		forceZip64_ = entry.forceZip64_;
		if (entry.extra != null)
		{
			extra = new byte[entry.extra.Length];
			Array.Copy(entry.extra, 0, extra, 0, entry.extra.Length);
		}
	}

	private bool HasDosAttributes(int attributes)
	{
		bool flag = false;
		if ((known & Known.ExternalAttributes) != 0)
		{
			flag |= (HostSystem == 0 || HostSystem == 10) && (ExternalFileAttributes & attributes) == attributes;
		}
		return flag;
	}

	public void ForceZip64()
	{
		forceZip64_ = true;
	}

	public bool IsZip64Forced()
	{
		return forceZip64_;
	}

	internal void ProcessExtraData(bool localHeader)
	{
		ZipExtraData zipExtraData = new ZipExtraData(extra);
		if (zipExtraData.Find(1))
		{
			forceZip64_ = true;
			if (zipExtraData.ValueLength < 4)
			{
				throw new ZipException("Extra data extended Zip64 information length is invalid");
			}
			if (size == uint.MaxValue)
			{
				size = (ulong)zipExtraData.ReadLong();
			}
			if (compressedSize == uint.MaxValue)
			{
				compressedSize = (ulong)zipExtraData.ReadLong();
			}
			if (!localHeader && offset == uint.MaxValue)
			{
				offset = zipExtraData.ReadLong();
			}
		}
		else if ((versionToExtract & 0xFF) >= 45 && (size == uint.MaxValue || compressedSize == uint.MaxValue))
		{
			throw new ZipException("Zip64 Extended information required but is missing.");
		}
		DateTime = GetDateTime(zipExtraData) ?? DateTime;
		if (method == CompressionMethod.WinZipAES)
		{
			ProcessAESExtraData(zipExtraData);
		}
	}

	private DateTime? GetDateTime(ZipExtraData extraData)
	{
		ExtendedUnixData data = extraData.GetData<ExtendedUnixData>();
		if (data != null && data.Include.HasFlag(ExtendedUnixData.Flags.ModificationTime))
		{
			return data.ModificationTime;
		}
		return null;
	}

	private void ProcessAESExtraData(ZipExtraData extraData)
	{
		if (extraData.Find(39169))
		{
			versionToExtract = 51;
			int valueLength = extraData.ValueLength;
			if (valueLength < 7)
			{
				throw new ZipException("AES Extra Data Length " + valueLength + " invalid.");
			}
			int aesVer = extraData.ReadShort();
			extraData.ReadShort();
			int aesEncryptionStrength = extraData.ReadByte();
			int num = extraData.ReadShort();
			_aesVer = aesVer;
			_aesEncryptionStrength = aesEncryptionStrength;
			method = (CompressionMethod)num;
			return;
		}
		throw new ZipException("AES Extra Data missing");
	}

	public bool IsCompressionMethodSupported()
	{
		return IsCompressionMethodSupported(CompressionMethod);
	}

	public object Clone()
	{
		ZipEntry zipEntry = (ZipEntry)MemberwiseClone();
		if (extra != null)
		{
			zipEntry.extra = new byte[extra.Length];
			Array.Copy(extra, 0, zipEntry.extra, 0, extra.Length);
		}
		return zipEntry;
	}

	public override string ToString()
	{
		return name;
	}

	public static bool IsCompressionMethodSupported(CompressionMethod method)
	{
		if (method != CompressionMethod.Deflated && method != 0)
		{
			return method == CompressionMethod.BZip2;
		}
		return true;
	}

	public static string CleanName(string name)
	{
		if (name == null)
		{
			return string.Empty;
		}
		if (Path.IsPathRooted(name))
		{
			name = name.Substring(Path.GetPathRoot(name).Length);
		}
		name = name.Replace("\\", "/");
		while (name.Length > 0 && name[0] == '/')
		{
			name = name.Remove(0, 1);
		}
		return name;
	}
}
