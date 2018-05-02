using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Controls
{

	/// <summary>
	/// The implementor executes some logic on activation, for e.g when moving 
	/// from not selected to selected in a tab control
	/// </summary>
	public interface IActivate
	{
		void Activate();
	}

	/// <summary>
	/// The implementor executes some logic on deactivation, for e.g when moving 
	/// from selected to not selected in a tab control
	/// </summary>
	public interface IDeactivate
	{
		void Deactivate();
	}
}
