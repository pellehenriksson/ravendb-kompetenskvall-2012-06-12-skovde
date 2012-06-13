using System;
using System.IO;
using NUnit.Framework;
using Raven.Json.Linq;

namespace RavenDbPresentation.Tests.StoringFiles
{
	[TestFixture]
	public class StoringFilesSamples : RavenTestBase
	{
		[Test]
		public void Store_A_File()
		{
			var file = Path.Combine(Directory.GetCurrentDirectory(), "some_file.jpg");
			using (var stream = new FileStream(file, FileMode.Open))
			{
				Store.DatabaseCommands.PutAttachment("photos/1", null, stream, new RavenJObject { { "Description", "Some photo" } }); // RavenJObject = dictionary object
			}
		}

		[Test]
		public void Retrive_A_File()
		{
			var attachment = Store.DatabaseCommands.GetAttachment("photos/1");

			using (var stream = new MemoryStream())
			{
				attachment.Data.Invoke().CopyTo(stream);
				var bytes = stream.ToArray();
			}

			Console.Out.WriteLine(attachment.Metadata["Description"]);
		}
	}
}
