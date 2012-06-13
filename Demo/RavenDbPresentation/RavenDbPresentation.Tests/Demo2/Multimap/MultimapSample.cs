using System;
using System.Linq;
using NUnit.Framework;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace RavenDbPresentation.Tests.Demo2.Multimap
{
	public class MultimapSample : RavenTestBase
	{
		[Test]
		public void Search_Multiple_Document_Types()
		{
			using (var session = Store.OpenSession())
			{
				var author = new Author
				{
					Name = "pelle henriksson"
				};

				session.Store(author);

				var book = new Book
				{
				    Name = "some name",
				    AuthorId = author.Id
				};

				session.Store(book);
				session.SaveChanges();
			}

			using (var session = Store.OpenSession())
			{
				var results = session.Advanced
					.LuceneQuery<ISearchbleItem, SearchIndex>()
					.Search("Content", "('some name' OR 'henriksson')")
					.ToList();

				foreach (var searchbleItem in results)
				{
				    Console.WriteLine(searchbleItem.Name);
				}
			}
		}

		public class Author : ISearchbleItem
		{
			public string Id { get; set; }
			public string Name { get; set; }
		}

		public class Book : ISearchbleItem
		{
			public string Id { get; set; }
			public string Name { get; set; }
			public string AuthorId { get; set; }
		}

		public interface ISearchbleItem
		{
			string Id { get; set; }
			string Name { get; set; }
		}

		public class SearchIndex : AbstractMultiMapIndexCreationTask<SearchIndex.Result>
		{
			public class Result
			{
				public object[] Content { get; set; }
			}

			public override string IndexName
			{
				get { return "SearchableItems/ByContent"; }
			}

			public SearchIndex()
			{
				AddMap<Author>(items => from x in items
										  select new Result { Content = new object[] { x.Name } });

				AddMap<Book>(items => from x in items
									   select new Result { Content = new object[] { x.Name } });

				Index(x => x.Content, FieldIndexing.Analyzed);
			}
		}
	}
}
