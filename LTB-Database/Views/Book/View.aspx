<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<ViewBookViewModel>" %>
<%@ Import Namespace="LTB_Database.Core" %>
<%@ Import Namespace="LTB_Database.ViewModels" %>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
	<div class="content-block">
		<h3>Buch...</h3>
		<ul class="link-list">
            <li style="margin-bottom: 3px;"><%= Html.ActionLink("Bearbeiten", "edit", "book", new { id = Model.Book.Id }, new { @class = "link" }) %></li>
        	<li style="margin-bottom: 3px;"><%= Html.ActionLink("Löschen", "delete", "book", new { id = Model.Book.Id }, new { @class = "link" }) %></li>
        </ul>
	</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	Lustiges Taschenbuch Datenbank - <%= Model.Book.Name %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-block">          
        <h3 style="color: #78BA91;">Nr. <%= Model.Book.Number %> - <%= Model.Book.Name%></h3>
        <div class="description">
        	<div style="float: left; width: 450px;">
    			<ul class="stories">
    			<% foreach(var story in Model.Book.Stories) { %>
    				<li class="stories"><%= story %></li>
    			<% } %>
    			</ul>
        	</div>
        	<div style="float: right; height: 200px; border: 1px solid #EEEEEE; border-radius: 5px; margin-right: 26px; background-color: #EEE;">
        		<a id="fancybox" title="Nr. <%= Model.Book.Number %> - <%= Model.Book.Name %>" href="<%= Url.Content(GlobalConfig.Get().ImagePath + Model.Book.Image) %>">
        		<% if(!String.IsNullOrEmpty(Model.Book.Image)) { %>		
        			<img src="<%= Url.Content(GlobalConfig.Get().ImagePath + Model.Book.Image) %>" height="200" alt="<%= Model.Book.Name %>" style="border: none; display: block; margin-left: auto; margin-right: auto;" />
        		<% } %>
        		</a>
        	</div>
        	<div style="clear: both;"></div>
        </div>
    </div>
    <div class="content-block">
    	<h3>Details</h3>
    	<dl>
    		<dt>Nr.</dt>
    		<dd><%= Model.Book.Number %></dd>
    		<dt>Kategorie</dt>
    		<dd><a class="link" href="<%= Url.Action("view", "category", new { id = Model.Book.CategoryId }) %>"><%= (String.IsNullOrEmpty(Model.Category.Name) ? "&nbsp;" : Model.Category.Name) %></a></dd>
    		<dt>Hinzugefügt</dt>
    		<dd><%= String.Format(new System.Globalization.CultureInfo("de-DE"),"{0:dddd, d MMM, yyyy}", Model.Book.Added) %></dd>
    	</dl>
    </div>
</asp:Content>