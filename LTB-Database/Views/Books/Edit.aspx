<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<LTB_Database.ViewModels.BooksEditViewModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
	<div class="content-block">
		<h3>Buch...</h3>
		<ul class="link-list">
            <li style="margin-bottom: 3px;"><%= Html.ActionLink("Details", "View", "Books", new { id = Model.Book.id }, new { @class = "link" }) %></li>
            <li style="margin-bottom: 3px;"><%= Html.ActionLink("Löschen", "Delete", "Books", new { id = Model.Book.id }, new { @class = "link" }) %></li>
        </ul>
	</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<title>Buch bearbeiten: "<%= Model.Book.name %>" - LTB-Database</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-block">
		<h3>Buch</h3>
		<% using(Html.BeginForm("Edit", "Books", new { id = "" }, FormMethod.Post, new { enctype = "multipart/form-data", id = "edit-form", name = "edit-form" })) { %>
			<label for="name" >Titel</label><%= Html.ValidationMessage("name") %><br />
			<input type="text" name="name" value="<%= (String.IsNullOrEmpty(Model.Book.name) ? "" : Model.Book.name) %>" maxlength="100" /><br />
			
			<label for="number">Nummer</label><%= Html.ValidationMessage("number") %><br />
			<input type="text" name="number" value="<%= (Model.Book.number == 0 ? "" : Model.Book.number.ToString()) %>" maxlength="4" /><br />
			
			<label for="category">Kategorie</label><br />
			<select name="category" id="category" size="1">
				<% foreach(var category in Model.Categories) { %>
					<option value="<%= category.id %>" <%= ((Model.Book.id != 0 && category.id == Model.Book.catid) ? "selected=\"selected\"" : "") %>><%= category.name %></option>
				<% } %>
			</select><br />
			
			<input type="hidden" name="id" value="<%= Model.Book.id %>" />
			
			<input type="submit" name="submit" value="Speichern" />
		<% } %>
	</div>
	<% if(Model.Book.id != 0) { %>
	<div class="content-block">
		<h3>Inhalt</h3>
		<% using(Html.BeginForm("Stories", "Books", FormMethod.Post, new { enctype = "multipart/form-data", id = "edit-form", name = "edit-form" })) { %>
			<label for="stories">Geschichten</label><br />
			<textarea id="stories" name="stories"><% foreach(var story in Model.Stories) { %><%= story.name %><%= "\n" %><% } %></textarea>
			
			<input type="hidden" name="id" value="<%= Model.Book.id %>" />
			
			<input type="submit" name="submit" value="Speichern" />
		<% } %>
	</div>
	<div class="content-block">
		<h3>Cover</h3>
		<% using(Html.BeginForm("Upload", "Books", FormMethod.Post, new { enctype = "multipart/form-data", id = "edit-form", name = "edit-form", style="float: left;" })) { %>
			<label for="image">Bild</label><br />
			<input type="file" name="image" /><br />
			
			<input type="hidden" name="id" value="<%= Model.Book.id %>" />
			
			<input type="submit" name="submit" value="Hochladen" />
		<% } %>
		<div style="float: right; height: 200px; width: 200px; border: 1px solid #EEEEEE; border-radius: 5px; margin-right: 26px; background-color: #EEE;">
    		<% if(!String.IsNullOrEmpty(Model.Book.image)) { %>		
        		<img src="<%= Url.Content(GlobalConfig.Get().ImagePath + Model.Book.image) %>" height="200" alt="<%= Model.Book.name %>" style="border: none; display: block; margin-left: auto; margin-right: auto;" />
        	<% } %>
    	</div>
    	<div style="clear: both;"></div>
    </div>
    <% } %>
</asp:Content>
