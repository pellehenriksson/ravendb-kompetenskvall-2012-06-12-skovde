using System;
using System.Linq;
using NUnit.Framework;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using RavenDbPresentation.Core.Documents;

namespace RavenDbPresentation.Tests.Demo2.FullTextSearch
{
	[TestFixture]
	public class FullTextSample : RavenTestBase
	{
		[Test]
		public void Test()
		{
			using (var session = Store.OpenSession())
			{
				var emps = session.Query<Employee, Employees_ByName_FullTextSearch>()
					.Search(x => x.Lastname, "Hickman OR Kline")
					.Search(x => x.Firstname, "Olympia")
					.ToList();

				foreach (var e in emps)
				{
					Console.Out.WriteLine(string.Format("{0} {1}", e.Firstname, e.Lastname));
				}
			}
		}

		public class Employee_Search : AbstractIndexCreationTask<Employee>
		{
			public Employee_Search()
			{
				Map = emps => from e in emps
				              select new
								{
									Query = new[]
									{
										e.Firstname,
										e.Lastname,
										e.Email
									}
								};

				
			}
		}

		public class Employees_ByName_FullTextSearch : AbstractIndexCreationTask<Employee>
		{
			public Employees_ByName_FullTextSearch()
			{
				Map = emps => from emp in emps
				              select new
				                     	{
				                     		emp.Firstname,
				                     		emp.Lastname
				                     	};

				Index(x => x.Firstname, FieldIndexing.Analyzed);
				Index(x => x.Lastname, FieldIndexing.Analyzed);
			}
		}
	}
}