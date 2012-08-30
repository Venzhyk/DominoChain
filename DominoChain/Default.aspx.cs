using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DominoChain
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e) {

		}

		protected void BuildChain_OnClick(object sender, EventArgs e) {
			string output;
			if (new Domino().BuildChain(InputEdit.Text, out output)) {
				OutputEdit.Text = output;
			} else {
				OutputEdit.Text = "It's impossible to create uninterrupted chain";
			}
		}
		
	}
}