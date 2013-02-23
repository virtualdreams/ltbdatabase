<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<LTB_Database.ViewModels.CategoriesEditViewModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
	<div class="content-block">
		<h3>Kategorie...</h3>
		<ul class="link-list">
            <li style="margin-bottom: 3px;"><%= Html.ActionLink("Details", "View", "Categories", new { id = Model.Category.id }, new { @class = "link" }) %></li>
            <li style="margin-bottom: 3px;"><%= Html.ActionLink("Löschen", "Delete", "Categories", new { id = Model.Category.id }, new { @class = "link" }) %></li>
        </ul>
	</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<title>Kategorie bearbeiten: "<%= (String.IsNullOrEmpty(Model.Category.name) ? "Neue Kategorie" : Model.Category.name ) %>" - LTB-Database</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="content-block">
		<h3>Kategorie</h3>
		<% using(Html.BeginForm("Edit", "Categories", FormMethod.Post, new { enctype = "multipart/form-data", id = "edit-form", name = "edit-form" })) { %>
			<label for="name" >Kategorie</label><%= Html.ValidationMessage("name") %><br />
			<input type="text" name="name" value="<%= (String.IsNullOrEmpty(Model.Category.name) ? "Neue Kategorie" : Model.Category.name ) %>" maxlength="100" /><br />
			
			<input type="hidden" name="id" value="<%= Model.Category.id %>" />
			
			<input type="submit" name="submit" value="Speichern" />
		<% } %>
	</div>
</asp:Content>
