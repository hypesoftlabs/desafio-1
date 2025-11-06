using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Domain.Entity
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Category(int id, string name, string description)
        {
            Id = id;
            Name = name;

        }
    }
}
