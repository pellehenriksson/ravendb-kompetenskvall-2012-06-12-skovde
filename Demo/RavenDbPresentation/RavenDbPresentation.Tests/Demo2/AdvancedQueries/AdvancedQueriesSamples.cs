using System;
using System.Linq;
using NUnit.Framework;
using Raven.Client.Linq;
using RavenDbPresentation.Core.Documents;

namespace RavenDbPresentation.Tests.AdvancedQueries
{
	[TestFixture]
	public class AdvancedQueriesSamples : RavenTestBase
	{
		//
		// LOAD RELATED DOCUMENTS, THIS WILL ONLY PRODUCE ON REQUEST TO THE SERVER
		//
		[Test]
		public void Include_Other_Documents()
		{
			using (var session = Store.OpenSession())
			{
				var emp = session
						.Include<Employee>(x => x.CompanyId)
						.Load<Employee>("employees/200");

				var company = session.Load<Company>(emp.CompanyId);
			}
		}

		[Test]
		public void Getting_Statistics_From_A_Query()
		{
			using (var session = Store.OpenSession())
			{
				var stats = new RavenQueryStatistics();

				var contacts = session.Query<Employee>()
									.Statistics(out stats)
									.Where(x => x.Email == "bibendum.Donec.felis@et.ca")
									.ToList();

				Console.Out.WriteLine(stats.IndexName);
				Console.Out.WriteLine(stats.IsStale);
			}
		}
		
		[Test]
		public void Handling_Stale_Results()
		{
		    using (var session = Store.OpenSession())
		    {
		    	var contact = session.Query<Employee>()
		    		.Where(x => x.Email == "bibendum.Donec.felis@et.ca")
		    		.Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(5)))
		    		.ToList();
		    }
		}
	}
}
