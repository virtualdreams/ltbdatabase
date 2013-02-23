<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LTB_Database.ViewModels.ViewCategoriesModel>" %>

<div class="content-block">
	<h3>Kategorien</h3>
	<ul class="link-list">
		<% foreach(var category in Model.Category) { %>
		<li style="margin-bottom: 3px;"><%= Html.ActionLink(category.name, "Category", "Books", new { id = category.id }, new { @class = "link" }) %></li>
		<% } %>
	</ul>
</div>