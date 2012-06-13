using System;
using NUnit.Framework;
using RavenDbPresentation.Core.Documents;

namespace RavenDbPresentation.Tests.Metadata
{
	[TestFixture]
	public class MetadataSamples : RavenTestBase
	{
		[Test]
		public void Get_Document_Metadata()
		{
			using (var session = Store.OpenSession())
			{
				var contact = session.Load<Employee>("employees/100");
				var metadata = session.Advanced.GetMetadataFor(contact);

				Console.Out.WriteLine(metadata.Value<DateTime>("Last-Modified"));
			}
		}
	}
}
