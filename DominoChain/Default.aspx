<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DominoChain.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		Input domino set <br>
		<asp:TextBox runat="server" TextMode="MultiLine" ID="InputEdit"></asp:TextBox>
		<p> Output domino chain <br>
		<asp:TextBox runat="server" TextMode="MultiLine" ID="OutputEdit"></asp:TextBox>
		<p>
		<asp:Button runat="server" Text="Go" ID="BuildChain" OnClick="BuildChain_OnClick"/>
	</div>
	</form>
</body>
</html>
