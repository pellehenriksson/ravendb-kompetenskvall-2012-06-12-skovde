using System;
using System.Linq;
using NUnit.Framework;
using Raven.Client.Linq;
using RavenDbPresentation.Core.Documents;

namespace RavenDbPresentation.Tests.BasicQueries
{
	[TestFixture]
	public class BasicQueriesSamples : RavenTestBase
	{
		//
		// GET A DOCUMENT BY THE ID
		//
		[Test]
		public void Find_By_Id()
		{
			using (var session = Store.OpenSession())
			{
				var emp = session.Load<Employee>("employees/200");
				Assert.That(emp, Is.Not.Null);

				Console.Out.WriteLine(emp);
			}
		}

		//
		// IF YOU GIVE IT THE TYPE YOU CAN QUERY WITH JUST THE NUMERIC PART OF THE ID
		//
		[Test]
		public void Find_By_Id_Alternative()
		{
			using (var session = Store.OpenSession())
			{
				var emp = session.Load<Employee>(200);
				Assert.That(emp, Is.Not.Null);

				Console.Out.WriteLine(emp);
			}
		}

		//
		// GET MANY 
		//
		[Test]
		public void Find_Many_By_Id()
		{
			var ids = new[] { "employees/200", "employees/201", "employees/201" };

			using (var session = Store.OpenSession())
			{
				var emps = session.Load<Employee>(ids);
				Assert.That(emps.Length, Is.EqualTo(3));
				
				foreach(var c in emps)
				{
					Console.Out.WriteLine(c);
				}
			}
	}

		//
		// QUERY A CHILD COLLECTION WITHIN THE DOCUMENT
		//
		[Test]
		public void Find_By_Value_In_Collection()
		{
			using (var session = Store.OpenSession())
			{
				var emps = from c in session.Query<Employee>()
				               where c.Tags.Any(x => x == "Yahoo")
				               select c;

				foreach (var c in emps)
				{
					Console.Out.WriteLine(c);
				}
			}
		}
	}
}
