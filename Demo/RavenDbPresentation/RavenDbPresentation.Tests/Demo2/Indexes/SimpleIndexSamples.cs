using System;
using System.Linq;
using NUnit.Framework;
using Raven.Client.Indexes;
using RavenDbPresentation.Core.Documents;

namespace RavenDbPresentation.Tests.Indexes
{
	[TestFixture] 
	public class SimpleIndexSamples : RavenTestBase
	{
		[Test]
		public void Query_With_Index_Implicit()
		{
			using (var session = Store.OpenSession())
			{
				var emps = session.Query<Employee>()
						.Where(x => x.Tags.Any(y => y == "Borland"))
						.ToList();
			}
		}

		[Test]
		public void Query_With_Index_Explicit()
		{
			using (var session =  Store.OpenSession())
			{
				var emps = session.Query<Employee, EmployeeByTag>()
						.Where(x => x.Tags.Any(y => y == "Borland"))
						.ToList();
			}
		}

		[Test]
		public void Query_With_The_Name_Of_The_Index()
		{
			using (var session = Store.OpenSession())
			{
				var emps = session.Query<Employee>("EmployeesThatKnowsAltaVistaAndLiveInNewBrunswick")
						.ToList();
				
				foreach(var e in emps)
				{
					Console.Out.WriteLine(e);
				}
			}
		}
	}

	public class EmployeesThatKnowsAltaVistaAndLiveInNewBrunswick : AbstractIndexCreationTask<Employee>
	{
		public EmployeesThatKnowsAltaVistaAndLiveInNewBrunswick()
		{
			Map = emps => from e in emps
						  where e.Tags.Any(x => x == "Altavista") &&
							e.City == "New Brunswick"
			              select new { e.Id };
		}
	}

	public class EmployeesByTagAndCity : AbstractIndexCreationTask<Employee>
	{
		public EmployeesByTagAndCity()
		{
			Map = emps => from e in emps
			              from t in e.Tags
			              select new
			            {

			                City = e.City,
			                Tag = t
			            };
		}
	}
	
	public class EmployeeByTag : AbstractIndexCreationTask<Employee>
	{
		public EmployeeByTag()
		{
			Map = emps => from e in emps
						  from t in e.Tags
					select new { Tags = t };
		}

		public override string IndexName
		{
			get { return "EmployeesFoundByTagName"; }
		}
	}
}
