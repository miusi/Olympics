using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Core.Repository.EntityFramework
{
	public class EntityFrameworkDbContext : DbContext
	{
		public EntityFrameworkDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}
