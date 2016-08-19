<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<CachingDemo.Web.Models.Data.Vehicle>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	
	<h2><%: ViewData["Message"] %></h2>
	
	<p>
		To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
	</p>

	<h2>Car data:</h2>

	<p><%: Html.ActionLink("Add a new car", "Create") %></p>

	<table cellpadding="0" cellspacing="0" border="0">
		<tr>
			<th>Id</th>
			<th>Name</th>
			<th>Price</th>
		</tr>

		<% foreach (var vehicle in Model) { %>

			<tr>
				<td><%: Html.ActionLink("Edit", "Edit", new {id=vehicle.Id}) %></td>
				<td><%: vehicle.Name %></td>
				<td><%: string.Format("{0:c0}", vehicle.Price) %></td>
			</tr>

		<% } %>

	</table>

	<% using (Html.BeginForm()) {  %>
		<input type="submit" value="Invalidate Cache" id="InvalidateButton" name="InvalidateButton" />
	<% } %>

</asp:Content>
