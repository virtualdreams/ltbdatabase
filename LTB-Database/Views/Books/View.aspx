<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<LTB_Database.ViewModels.ViewBookModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
	<div class="content-block">
		<h3>Buch...</h3>
		<ul class="link-list">
            <li style="margin-bottom: 3px;"><%= Html.ActionLink("Bearbeiten", "Edit", "Books", new { id = Model.Book.id }, new { @class = "link" }) %></li>
        	<li style="margin-bottom: 3px;"><%= Html.ActionLink("Löschen", "Delete", "Books", new { id = Model.Book.id }, new { @class = "link" }) %></li>
        </ul>
	</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<title><%= Model.Book.name %> - LTB-Database</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-block">          
        <h3 style="color: #78BA91;">Nr. <%= Model.Book.number %> - <%= Model.Book.name%></h3>
        <div class="description">
        	<div style="float: left; width: 450px;">
    			<ul class="stories">
    			<% foreach(var story in Model.Stories) { %>
    				<li class="stories"><%= story.name %></li>
    			<% } %>
    			</ul>
        	</div>
        	<div style="float: right; height: 200px; border: 1px solid #EEEEEE; border-radius: 5px; margin-right: 26px; background-color: #EEE;">
        		<a id="fancybox" title="Nr. <%= Model.Book.number %> - <%= Model.Book.name %>" href="<%= Url.Content(GlobalConfig.Get().ImagePath + Model.Book.image) %>">
        		<% if(!String.IsNullOrEmpty(Model.Book.image)) { %>		
        			<img src="<%= Url.Content(GlobalConfig.Get().ImagePath + Model.Book.image) %>" height="200" alt="<%= Model.Book.name %>" style="border: none; display: block; margin-left: auto; margin-right: auto;" />
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
    		<dd><%= Model.Book.number %></dd>
    		<dt>Kategorie</dt>
    		<dd><a class="link" href="<%= Url.Action("Category", "Books", new { id = Model.Book.catid }) %>"><%= (String.IsNullOrEmpty(Model.Book.category) ? "&nbsp;" : Model.Book.category) %></a></dd>
    		<dt>Hinzugefügt</dt>
    		<dd><%= String.Format("{0:dddd, d MMM, yyyy}", Model.Book.added) %></dd>
    	</dl>
    </div>
</asp:Content>