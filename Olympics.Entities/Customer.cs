using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Entities
{
	public class Customer
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual int Age { get; set; }
	}
}
