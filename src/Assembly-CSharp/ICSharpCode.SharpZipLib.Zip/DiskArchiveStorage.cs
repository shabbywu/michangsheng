using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip;

public class DiskArchiveStorage : BaseArchiveStorage
{
	private Stream temporaryStream_;

	private readonly string fileName_;

	private string temporaryName_;

	public DiskArchiveStorage(ZipFile file, FileUpdateMode updateMode)
		: base(updateMode)
	{
		if (file.Name == null)
		{
			throw new ZipException("Cant handle non file archives");
		}
		fileName_ = file.Name;
	}

	public DiskArchiveStorage(ZipFile file)
		: this(file, FileUpdateMode.Safe)
	{
	}

	public override Stream GetTemporaryOutput()
	{
		temporaryName_ = PathUtils.GetTempFileName(temporaryName_);
		temporaryStream_ = File.Open(temporaryName_, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		return temporaryStream_;
	}

	public override Stream ConvertTemporaryToFinal()
	{
		if (temporaryStream_ == null)
		{
			throw new ZipException("No temporary stream has been created");
		}
		Stream stream = null;
		string tempFileName = PathUtils.GetTempFileName(fileName_);
		bool flag = false;
		try
		{
			temporaryStream_.Dispose();
			File.Move(fileName_, tempFileName);
			File.Move(temporaryName_, fileName_);
			flag = true;
			File.Delete(tempFileName);
			return File.Open(fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
		}
		catch (Exception)
		{
			stream = null;
			if (!flag)
			{
				File.Move(tempFileName, fileName_);
				File.Delete(temporaryName_);
			}
			throw;
		}
	}

	public override Stream MakeTemporaryCopy(Stream stream)
	{
		stream.Dispose();
		temporaryName_ = PathUtils.GetTempFileName(fileName_);
		File.Copy(fileName_, temporaryName_, overwrite: true);
		temporaryStream_ = new FileStream(temporaryName_, FileMode.Open, FileAccess.ReadWrite);
		return temporaryStream_;
	}

	public override Stream OpenForDirectUpdate(Stream stream)
	{
		if (stream == null || !stream.CanWrite)
		{
			stream?.Dispose();
			return new FileStream(fileName_, FileMode.Open, FileAccess.ReadWrite);
		}
		return stream;
	}

	public override void Dispose()
	{
		if (temporaryStream_ != null)
		{
			temporaryStream_.Dispose();
		}
	}
}
