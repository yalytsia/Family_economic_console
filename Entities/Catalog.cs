using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Catalog
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public static string ListToCsv(List<Catalog> catalogs)
        {
            string line = string.Empty;
            foreach (var item in catalogs)
            {
                line = line + item.Id.ToString() + Constant.Delimiter + item.Name + Environment.NewLine;

            }
            line = line.TrimEnd(Environment.NewLine.ToCharArray());
            return line;
        }
    }
}
