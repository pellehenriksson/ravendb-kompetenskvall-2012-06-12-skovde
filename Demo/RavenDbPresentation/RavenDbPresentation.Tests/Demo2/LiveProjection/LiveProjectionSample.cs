using System;
using System.Linq;
using NUnit.Framework;
using Raven.Client.Indexes;
using RavenDbPresentation.Core.Documents;

namespace RavenDbPresentation.Tests.Demo2.LiveProjection
{
	[TestFixture]
	public class LiveProjectionSample : RavenTestBase
	{
		[Test]
		public void Run_Forrest_Run()
		{
			using (var session = Store.OpenSession())
			{
				var users = session.Query<dynamic, Employee_With_Company>()
					.Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(10)))
					.ToList();

				foreach (var e in users)
				{
					Console.Out.WriteLine(e.Name + " " + e.CompanyName);
				}
			}
		}
	}

	public class Employee_With_Company : AbstractIndexCreationTask<Employee>
	{
		public Employee_With_Company()
		{
			Map = emps => from e in emps
			              select new { e.CompanyId };

			TransformResults = (db, emps) => from e in emps
			                                 let cmp = db.Load<Company>(e.CompanyId)
			                                 select new
			                                {
												e.Id,
												Name = e.Firstname + " " + e.Lastname,
												CompanyId = cmp.Id,
												CompanyName = cmp.Name
			                                };

		}
	}

	public class EmployeeWithCompanyProjection
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string CompanyId { get; set; }
		public string CompanyName { get; set; }
	}
}
