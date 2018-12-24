using Diva.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Data
{
	public class WPsDataManager
	{

		private List<Locationwp> wpList = new List<Locationwp>();

		public void Set(List<Locationwp> data) => wpList = data;

		public List<Locationwp> Get() => wpList;
	}
}
