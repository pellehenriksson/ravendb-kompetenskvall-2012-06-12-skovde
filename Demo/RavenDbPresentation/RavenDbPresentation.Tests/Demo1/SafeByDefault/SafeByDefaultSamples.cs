using System.Linq;
using NUnit.Framework;
using RavenDbPresentation.Core.Documents;

namespace RavenDbPresentation.Tests.SafeByDefault
{
	[TestFixture]
	public class SafeByDefaultSamples : RavenTestBase
	{

		[Test]
		public void Request_Per_Session_Limit()
		{
			using (var session = Store.OpenSession())
			{
				for(var counter = 0; counter < 100; counter++)
				{
					session.Store(new { Name = "Nisse hult " + counter});
					session.SaveChanges();
				}
			}
		}
		
		[Test]
		public void Bound_Result_Sets()
		{
			using (var session = Store.OpenSession())
			{
				var emps = session.Query<Employee>().ToList();
				Assert.That(emps.Count, Is.EqualTo(128));
			}
		}
	}
}
