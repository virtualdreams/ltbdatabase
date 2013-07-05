<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<EditCategoryViewModel>" %>
<%@ Import Namespace="LTB_Database.Core" %>
<%@ Import Namespace="LTB_Database.Models" %>
<%@ Import Namespace="LTB_Database.ViewModels" %>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
	<% if(Model.Category.Id != 0) { %>
	<div class="content-block">
		<h3>Kategorie...</h3>
		<ul class="link-list">
            <li style="margin-bottom: 3px;"><%= Html.ActionLink("Details", "view", "category", new { id = Model.Category.Id }, new { @class = "link" }) %></li>
            <li style="margin-bottom: 3px;"><%= Html.ActionLink("Löschen", "delete", "category", new { id = Model.Category.Id }, new { @class = "link" }) %></li>
        </ul>
	</div>
	<% } %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	Lustiges Taschenbuch Datenbank - Kategorie bearbeiten: <%= (String.IsNullOrEmpty(Model.Category.Name) ? "Neue Kategorie" : Model.Category.Name ) %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<% using(Html.BeginForm("edit", "category", FormMethod.Post, new { enctype = "multipart/form-data", id = "edit-form" })) { %>
	<div class="content-block">
		<h3>Kategorie</h3>
		<div class="indent">
			<label for="name" >Kategorie</label><br />
			<input type="text" name="name" id="name" value="<%= Model.Category.Name %>" maxlength="100" /><%= Html.ValidationMessage("name") %><br />
		</div>
	</div>
	<div class="content-block">
		<div class="indent">
			<input type="hidden" name="id" value="<%= Model.Category.Id %>" />
			<input type="submit" name="submit" value="Speichern" />
		</div>
	</div>
	<% } %>
</asp:Content>
