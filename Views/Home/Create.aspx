<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CachingDemo.Web.Models.Data.Vehicle>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Create</h2>

<% Html.RenderPartial("VehicleForm", Model); %>

<p><%: Html.ActionLink("Back to list", "Index") %></p>

</asp:Content>
