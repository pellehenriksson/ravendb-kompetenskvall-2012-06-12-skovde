using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RavenDbPresentation.Tests.HandleDocumentStructureChanges
{
	[TestFixture]
	public class HandleDocumentStructureChangesSamples : RavenTestBase
	{
		public class Person
		{
			public string Name { get; set; }
			public string Email { get; set; }
			//public string Phone { get; set; }
		}

		[Test]
		public void Store_With_Original_Structure()
		{
			using (var session = Store.OpenSession())
			{
				var p = new Person { Name = "Niss Hult", Email = "n@h.se" };
				session.Store(p);
				session.SaveChanges();
			}
		}

		//[Test]
		//public void Store_With_New_Structure()
		//{
		//    using (var session = Store.OpenSession())
		//    {
		//        var p = new Person { Name = "Niss Hult", Email = "n@h.se", Phone = "12334555" };
		//        session.Store(p);
		//        session.SaveChanges();
		//    }
		//}

		[Test]
		public void Get_First_Person()
		{
			using (var session = Store.OpenSession())
			{
				var p = session.Load<Person>(1);
				session.SaveChanges();
			}
		}
	}
}
