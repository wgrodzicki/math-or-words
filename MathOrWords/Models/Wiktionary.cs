using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathOrWords.Models;

class Wiktionary
{
	public string Keyword { get; set; }
	public string[] Results { get; set; }
	public string[] Descriptions { get; set; }
	public string[] Pages { get; set;  }
}
