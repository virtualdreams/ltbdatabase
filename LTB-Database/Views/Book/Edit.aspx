<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<EditBookViewModel>" %>
<%@ Import Namespace="LTB_Database.Core" %>
<%@ Import Namespace="LTB_Database.Models" %>
<%@ Import Namespace="LTB_Database.ViewModels" %>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
	<% if(Model.Book.Id != 0) { %>
	<div class="content-block">
		<h3>Buch...</h3>
		<ul class="link-list">
            <li style="margin-bottom: 3px;"><%= Html.ActionLink("Details", "view", "book", new { id = Model.Book.Id }, new { @class = "link" }) %></li>
            <li style="margin-bottom: 3px;"><%= Html.ActionLink("Löschen", "delete", "book", new { id = Model.Book.Id }, new { @class = "link" }) %></li>
        </ul>
	</div>
	<% } %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	Lustiges Taschenbuch Datenbank - Buch bearbeiten: <%= String.IsNullOrEmpty(Model.Book.Name) ? "Neues Buch" : Model.Book.Name %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% using(Html.BeginForm("edit", "book", new { id = "" }, FormMethod.Post, new { enctype = "multipart/form-data", id = "edit-form" })) { %>
    <div class="content-block">
		<h3>Buch</h3>
		<div class="indent">
			<label for="name" >Titel</label><br />
			<input type="text" name="name" id="name" value="<%= Model.Book.Name %>" placeholder="Titel" maxlength="100" /><%= Html.ValidationMessage("name") %><br />
			
			<label for="number">Nummer</label><br />
			<input type="text" name="number" id="number" value="<%= (Model.Book.Number == 0 ? "" : Model.Book.Number.ToString()) %>" placeholder="Nummer" maxlength="4" /><%= Html.ValidationMessage("number") %><br />
			
			<label for="category">Kategorie</label><br />
			<select name="categoryid" id="category" size="1">
				<% foreach(var category in Model.Categories) { %>
				<option value="<%= category.Id %>" <%= ((Model.Book.Id != 0 && category.Id == Model.Book.CategoryId) ? "selected=\"selected\"" : "") %>><%= category.Name %></option>
				<% } %>
			</select><%= Html.ValidationMessage("categoryid") %><br />
		</div>
	</div>
	<div class="content-block">
		<h3>Inhalt</h3>
		<noscript>
			<div class="warning">
				Bitte aktiviere JavaScript in deinem Browser, um die Inhalte bearbeiten zu können.
			</div>
		</noscript>
		<div class="indent">
			<div id="dynStories">
				<% foreach(var story in Model.Book.Stories) { %>
				<p>
					<input class="inputStory" type="text" name="stories" value="<%= story %>" placeholder="Geschichte" /> <a href="#" class="remStory" style="margin-left: 10px;"><img src="/Content/cross.png" alt="Entfernen" title="Entfernen" /></a> <a href="#" class="insStory"><img src="/Content/add.png" alt="Einfügen" title="Einfügen" /></a>
				</p>
				<% } %>
			</div>
			<div style="position: relative;">
				 Inhalt hinzufügen&nbsp;<a style="margin-top: 3px;" href="#" id="addStory"><img src="/Content/add.png" alt="Hinzufügen" title="Hinzufügen" style="position: absolute; top: 3px;" /></a>
			</div>
		</div>
	</div>
	<div class="content-block">
		<h3>Cover</h3>
		<noscript>
			<div class="warning">
				Bitte aktiviere JavaScript in deinem Browser, um das Bild löschen zu können.
			</div>
		</noscript>
		<div id="message-box" style="display: none;">
			<p><b>Das Bild konnte aufgrund eines Fehlers nicht gelöscht werden.</b></p>
		</div>
		<div class="indent">
			<% if(!String.IsNullOrEmpty(Model.Book.Image)) { %>		
			<div class="imgdisp" style="height: auto; width: 200px; border: 1px solid #EEEEEE; border-radius: 5px; margin-right: 26px; background-color: #EEE; position: relative; margin-bottom: 26px;">
        		<a id="fancybox" title="Nr. <%= Model.Book.Number %> - <%= Model.Book.Name %>" href="<%= Url.Content(GlobalConfig.Get().ImagePath + Model.Book.Image) %>">
        			<img src="<%= Url.Content(GlobalConfig.Get().ImagePath + Model.Book.Image) %>" title="<%= Model.Book.Name %>" alt="<%= Model.Book.Name %>" style="border: none; display: block; margin-left: auto; margin-right: auto; height: auto; width: 200px;" />
    			</a>
    			<img class="delimg" src="/Content/cross.png" alt="Löschen" title="Löschen" style="position: absolute; top: 10px; left: 174px;" />
    		</div>
    		<% } %>
    		<label for="image">Bild</label><br />
			<input type="file" name="imagefile" id="image" />
    	</div>
    </div>
    <div class="content-block">
		<div class="indent">
			<input type="hidden" id="id" name="id" value="<%= Model.Book.Id %>" />
			<input type="submit" name="submit" value="Speichern" />
			<input type="reset" name="reset" value="Zurücksetzen" />
		</div>
	</div>
    <% } %>
</asp:Content>
