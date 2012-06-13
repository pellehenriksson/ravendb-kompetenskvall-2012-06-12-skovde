using System;
using System.Linq;
using NUnit.Framework;
using Raven.Client.Indexes;
using RavenDbPresentation.Core.Documents;

namespace RavenDbPresentation.Tests.MapReduce
{
	[TestFixture]
	public class MapReduceSamples : RavenTestBase
	{
		[Test]
		public void Map_Reduce_Sample()
		{
			using (var session = Store.OpenSession())
			{
				var result = session.Query<TagsCount.ReduceResult, TagsCount>();

				foreach (var r in result)
				{
					Console.Out.WriteLine(r.Tag + " " + r.Count);
				}
			}
		}
		
		public class TagsCount : AbstractIndexCreationTask<Employee, TagsCount.ReduceResult>
		{
			public class ReduceResult
			{
				public string Tag { get; set; }
				public int Count { get; set; }
			}

			public TagsCount()
			{
				Map = emps => from e in emps
							  from t in e.Tags
							  select new
							  {
								  Tag = t,
								  Count = 1
							  };

				Reduce = results => from r in results
									group r by r.Tag
										into g
										select new
										{
											Tag = g.Key,
											Count = g.Sum(x => x.Count)
										};
			}
		}
	}
}
