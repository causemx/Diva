using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Controls
{
	interface IDialog
	{
		void Title(string title);
		void Message(string message);
	}
}
