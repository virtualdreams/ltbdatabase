<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CategoriesViewModel>" %>
<%@ Import Namespace="LTB_Database.Core" %>
<%@ Import Namespace="LTB_Database.Models" %>
<%@ Import Namespace="LTB_Database.ViewModels" %>

<div class="content-block">
	<h3>Kategorien</h3>
	<ul class="link-list">
		<li style="margin-bottom: 3px;"><%= Html.ActionLink("Alle", "index", "category", new { }, new { @class = "link" }) %></li>
		<% foreach(var category in Model.Categories) { %>
		<li style="margin-bottom: 3px; white-space: nowrap;"><%= Html.ActionLink(category.Name, "view", "category", new { id = category.Id }, new { @class = "link" }) %></li>
		<% } %>
	</ul>
</div>