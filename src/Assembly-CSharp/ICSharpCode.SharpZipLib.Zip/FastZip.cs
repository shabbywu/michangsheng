using System;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace ICSharpCode.SharpZipLib.Zip;

public class FastZip
{
	public enum Overwrite
	{
		Prompt,
		Never,
		Always
	}

	public delegate bool ConfirmOverwriteDelegate(string fileName);

	private bool continueRunning_;

	private byte[] buffer_;

	private ZipOutputStream outputStream_;

	private ZipFile zipFile_;

	private string sourceDirectory_;

	private NameFilter fileFilter_;

	private NameFilter directoryFilter_;

	private Overwrite overwrite_;

	private ConfirmOverwriteDelegate confirmDelegate_;

	private bool restoreDateTimeOnExtract_;

	private bool restoreAttributesOnExtract_;

	private bool createEmptyDirectories_;

	private FastZipEvents events_;

	private IEntryFactory entryFactory_ = new ZipEntryFactory();

	private INameTransform extractNameTransform_;

	private UseZip64 useZip64_ = UseZip64.Dynamic;

	private Deflater.CompressionLevel compressionLevel_ = Deflater.CompressionLevel.DEFAULT_COMPRESSION;

	private string password_;

	public bool CreateEmptyDirectories
	{
		get
		{
			return createEmptyDirectories_;
		}
		set
		{
			createEmptyDirectories_ = value;
		}
	}

	public string Password
	{
		get
		{
			return password_;
		}
		set
		{
			password_ = value;
		}
	}

	public ZipEncryptionMethod EntryEncryptionMethod { get; set; } = ZipEncryptionMethod.ZipCrypto;


	public INameTransform NameTransform
	{
		get
		{
			return entryFactory_.NameTransform;
		}
		set
		{
			entryFactory_.NameTransform = value;
		}
	}

	public IEntryFactory EntryFactory
	{
		get
		{
			return entryFactory_;
		}
		set
		{
			if (value == null)
			{
				entryFactory_ = new ZipEntryFactory();
			}
			else
			{
				entryFactory_ = value;
			}
		}
	}

	public UseZip64 UseZip64
	{
		get
		{
			return useZip64_;
		}
		set
		{
			useZip64_ = value;
		}
	}

	public bool RestoreDateTimeOnExtract
	{
		get
		{
			return restoreDateTimeOnExtract_;
		}
		set
		{
			restoreDateTimeOnExtract_ = value;
		}
	}

	public bool RestoreAttributesOnExtract
	{
		get
		{
			return restoreAttributesOnExtract_;
		}
		set
		{
			restoreAttributesOnExtract_ = value;
		}
	}

	public Deflater.CompressionLevel CompressionLevel
	{
		get
		{
			return compressionLevel_;
		}
		set
		{
			compressionLevel_ = value;
		}
	}

	public FastZip()
	{
	}

	public FastZip(FastZipEvents events)
	{
		events_ = events;
	}

	public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
	{
		CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter);
	}

	public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter)
	{
		CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, null);
	}

	public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
	{
		CreateZip(outputStream, sourceDirectory, recurse, fileFilter, directoryFilter, leaveOpen: false);
	}

	public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter, bool leaveOpen)
	{
		FileSystemScanner scanner = new FileSystemScanner(fileFilter, directoryFilter);
		CreateZip(outputStream, sourceDirectory, recurse, scanner, leaveOpen);
	}

	public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, IScanFilter fileFilter, IScanFilter directoryFilter)
	{
		CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter);
	}

	public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, IScanFilter fileFilter, IScanFilter directoryFilter, bool leaveOpen = false)
	{
		FileSystemScanner scanner = new FileSystemScanner(fileFilter, directoryFilter);
		CreateZip(outputStream, sourceDirectory, recurse, scanner, leaveOpen);
	}

	private void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, FileSystemScanner scanner, bool leaveOpen)
	{
		NameTransform = new ZipNameTransform(sourceDirectory);
		sourceDirectory_ = sourceDirectory;
		using (outputStream_ = new ZipOutputStream(outputStream))
		{
			outputStream_.SetLevel((int)CompressionLevel);
			outputStream_.IsStreamOwner = !leaveOpen;
			outputStream_.NameTransform = null;
			if (!string.IsNullOrEmpty(password_) && EntryEncryptionMethod != 0)
			{
				outputStream_.Password = password_;
			}
			outputStream_.UseZip64 = UseZip64;
			scanner.ProcessFile = (ProcessFileHandler)Delegate.Combine(scanner.ProcessFile, new ProcessFileHandler(ProcessFile));
			if (CreateEmptyDirectories)
			{
				scanner.ProcessDirectory += ProcessDirectory;
			}
			if (events_ != null)
			{
				if (events_.FileFailure != null)
				{
					scanner.FileFailure = (FileFailureHandler)Delegate.Combine(scanner.FileFailure, events_.FileFailure);
				}
				if (events_.DirectoryFailure != null)
				{
					scanner.DirectoryFailure = (DirectoryFailureHandler)Delegate.Combine(scanner.DirectoryFailure, events_.DirectoryFailure);
				}
			}
			scanner.Scan(sourceDirectory, recurse);
		}
	}

	public void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
	{
		ExtractZip(zipFileName, targetDirectory, Overwrite.Always, null, fileFilter, null, restoreDateTimeOnExtract_);
	}

	public void ExtractZip(string zipFileName, string targetDirectory, Overwrite overwrite, ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime, bool allowParentTraversal = false)
	{
		Stream inputStream = File.Open(zipFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
		ExtractZip(inputStream, targetDirectory, overwrite, confirmDelegate, fileFilter, directoryFilter, restoreDateTime, isStreamOwner: true, allowParentTraversal);
	}

	public void ExtractZip(Stream inputStream, string targetDirectory, Overwrite overwrite, ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime, bool isStreamOwner, bool allowParentTraversal = false)
	{
		if (overwrite == Overwrite.Prompt && confirmDelegate == null)
		{
			throw new ArgumentNullException("confirmDelegate");
		}
		continueRunning_ = true;
		overwrite_ = overwrite;
		confirmDelegate_ = confirmDelegate;
		extractNameTransform_ = new WindowsNameTransform(targetDirectory, allowParentTraversal);
		fileFilter_ = new NameFilter(fileFilter);
		directoryFilter_ = new NameFilter(directoryFilter);
		restoreDateTimeOnExtract_ = restoreDateTime;
		using (zipFile_ = new ZipFile(inputStream, !isStreamOwner))
		{
			if (password_ != null)
			{
				zipFile_.Password = password_;
			}
			IEnumerator enumerator = zipFile_.GetEnumerator();
			while (continueRunning_ && enumerator.MoveNext())
			{
				ZipEntry zipEntry = (ZipEntry)enumerator.Current;
				if (zipEntry.IsFile)
				{
					if (directoryFilter_.IsMatch(Path.GetDirectoryName(zipEntry.Name)) && fileFilter_.IsMatch(zipEntry.Name))
					{
						ExtractEntry(zipEntry);
					}
				}
				else if (zipEntry.IsDirectory && directoryFilter_.IsMatch(zipEntry.Name) && CreateEmptyDirectories)
				{
					ExtractEntry(zipEntry);
				}
			}
		}
	}

	private void ProcessDirectory(object sender, DirectoryEventArgs e)
	{
		if (!e.HasMatchingFiles && CreateEmptyDirectories)
		{
			if (events_ != null)
			{
				events_.OnProcessDirectory(e.Name, e.HasMatchingFiles);
			}
			if (e.ContinueRunning && e.Name != sourceDirectory_)
			{
				ZipEntry entry = entryFactory_.MakeDirectoryEntry(e.Name);
				outputStream_.PutNextEntry(entry);
			}
		}
	}

	private void ProcessFile(object sender, ScanEventArgs e)
	{
		if (events_ != null && events_.ProcessFile != null)
		{
			events_.ProcessFile(sender, e);
		}
		if (!e.ContinueRunning)
		{
			return;
		}
		try
		{
			using FileStream stream = File.Open(e.Name, FileMode.Open, FileAccess.Read, FileShare.Read);
			ZipEntry entry = entryFactory_.MakeFileEntry(e.Name);
			ConfigureEntryEncryption(entry);
			outputStream_.PutNextEntry(entry);
			AddFileContents(e.Name, stream);
		}
		catch (Exception e2)
		{
			if (events_ != null)
			{
				continueRunning_ = events_.OnFileFailure(e.Name, e2);
				return;
			}
			continueRunning_ = false;
			throw;
		}
	}

	private void ConfigureEntryEncryption(ZipEntry entry)
	{
		if (!string.IsNullOrEmpty(Password) && entry.AESEncryptionStrength == 0)
		{
			switch (EntryEncryptionMethod)
			{
			case ZipEncryptionMethod.AES128:
				entry.AESKeySize = 128;
				break;
			case ZipEncryptionMethod.AES256:
				entry.AESKeySize = 256;
				break;
			}
		}
	}

	private void AddFileContents(string name, Stream stream)
	{
		if (stream == null)
		{
			throw new ArgumentNullException("stream");
		}
		if (buffer_ == null)
		{
			buffer_ = new byte[4096];
		}
		if (events_ != null && events_.Progress != null)
		{
			StreamUtils.Copy(stream, outputStream_, buffer_, events_.Progress, events_.ProgressInterval, this, name);
		}
		else
		{
			StreamUtils.Copy(stream, outputStream_, buffer_);
		}
		if (events_ != null)
		{
			continueRunning_ = events_.OnCompletedFile(name);
		}
	}

	private void ExtractFileEntry(ZipEntry entry, string targetName)
	{
		bool flag = true;
		if (overwrite_ != Overwrite.Always && File.Exists(targetName))
		{
			flag = overwrite_ == Overwrite.Prompt && confirmDelegate_ != null && confirmDelegate_(targetName);
		}
		if (!flag)
		{
			return;
		}
		if (events_ != null)
		{
			continueRunning_ = events_.OnProcessFile(entry.Name);
		}
		if (!continueRunning_)
		{
			return;
		}
		try
		{
			using (FileStream destination = File.Create(targetName))
			{
				if (buffer_ == null)
				{
					buffer_ = new byte[4096];
				}
				using (Stream source = zipFile_.GetInputStream(entry))
				{
					if (events_ != null && events_.Progress != null)
					{
						StreamUtils.Copy(source, destination, buffer_, events_.Progress, events_.ProgressInterval, this, entry.Name, entry.Size);
					}
					else
					{
						StreamUtils.Copy(source, destination, buffer_);
					}
				}
				if (events_ != null)
				{
					continueRunning_ = events_.OnCompletedFile(entry.Name);
				}
			}
			if (restoreDateTimeOnExtract_)
			{
				File.SetLastWriteTime(targetName, entry.DateTime);
			}
			if (RestoreAttributesOnExtract && entry.IsDOSEntry && entry.ExternalFileAttributes != -1)
			{
				FileAttributes externalFileAttributes = (FileAttributes)entry.ExternalFileAttributes;
				externalFileAttributes &= FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.Archive | FileAttributes.Normal;
				File.SetAttributes(targetName, externalFileAttributes);
			}
		}
		catch (Exception e)
		{
			if (events_ != null)
			{
				continueRunning_ = events_.OnFileFailure(targetName, e);
				return;
			}
			continueRunning_ = false;
			throw;
		}
	}

	private void ExtractEntry(ZipEntry entry)
	{
		bool flag = entry.IsCompressionMethodSupported();
		string text = entry.Name;
		if (flag)
		{
			if (entry.IsFile)
			{
				text = extractNameTransform_.TransformFile(text);
			}
			else if (entry.IsDirectory)
			{
				text = extractNameTransform_.TransformDirectory(text);
			}
			flag = !string.IsNullOrEmpty(text);
		}
		string text2 = string.Empty;
		if (flag)
		{
			text2 = ((!entry.IsDirectory) ? Path.GetDirectoryName(Path.GetFullPath(text)) : text);
		}
		if (flag && !Directory.Exists(text2) && (!entry.IsDirectory || CreateEmptyDirectories))
		{
			try
			{
				continueRunning_ = events_?.OnProcessDirectory(text2, hasMatchingFiles: true) ?? true;
				if (continueRunning_)
				{
					Directory.CreateDirectory(text2);
					if (entry.IsDirectory && restoreDateTimeOnExtract_)
					{
						Directory.SetLastWriteTime(text2, entry.DateTime);
					}
				}
				else
				{
					flag = false;
				}
			}
			catch (Exception e)
			{
				flag = false;
				if (events_ == null)
				{
					continueRunning_ = false;
					throw;
				}
				if (entry.IsDirectory)
				{
					continueRunning_ = events_.OnDirectoryFailure(text, e);
				}
				else
				{
					continueRunning_ = events_.OnFileFailure(text, e);
				}
			}
		}
		if (flag && entry.IsFile)
		{
			ExtractFileEntry(entry, text);
		}
	}

	private static int MakeExternalAttributes(FileInfo info)
	{
		return (int)info.Attributes;
	}

	private static bool NameIsValid(string name)
	{
		if (!string.IsNullOrEmpty(name))
		{
			return name.IndexOfAny(Path.GetInvalidPathChars()) < 0;
		}
		return false;
	}
}
