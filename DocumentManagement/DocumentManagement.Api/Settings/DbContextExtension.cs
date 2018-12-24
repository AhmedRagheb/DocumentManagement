using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using DocumentManagement.DataAccess.Abstractions.Entities;
using DocumentManagement.DataAccess.DBContext;

namespace DocumentManagement.Api.Settings
{
	public static class DbContextExtension
	{
		public static void EnsureSeeded(this DocumentDbContext context)
		{
			if (!context.DocumentsTypes.Any())
			{
				var types = JsonConvert.DeserializeObject<List<DocumentType>>(File.ReadAllText("Seeds" + Path.DirectorySeparatorChar + "DocumentTypes.json"));
				context.AddRange(types);
				context.SaveChanges();
			}
		}
	}
}
