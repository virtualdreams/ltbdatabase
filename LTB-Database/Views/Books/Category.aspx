<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<LTB_Database.ViewModels.ViewCategoryModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
	<div class="content-block">
		<h3>Kategorie...</h3>
		<ul class="link-list">
            <li style="margin-bottom: 3px;"><%= Html.ActionLink("Bearbeiten", "Edit", "Categories", new { id = Model.Category.id }, new { @class = "link" }) %></li>
        	<li style="margin-bottom: 3px;"><%= Html.ActionLink("Löschen", "Delete", "Categories", new { id = Model.Category.id }, new { @class = "link" }) %></li>
        </ul>
	</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<title><%= Model.Category.name %> - LTB-Database</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-block">          
        <h3>Kategorie: <%= Model.Category.name %> (<a class="h3link" href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, layout = "list" }) %>">Liste</a>)</h3>
        <div class="pager">
	        <ul class="pager-list">
	        	<li class="pager-item-active">Seite <%= Model.Pager.PageIndex %> von <%= Model.Pager.TotalPages %></li>
				<% if(Model.Pager.HasPrevPage) { %>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = 1 }) %>">Erste</a></li>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = Model.Pager.PageIndex - 1 }) %>">« Zurück</a></li>
	        	<% } else { %>
	        		
	        	<% } %>
	        	<% for(long page = ((Model.Pager.PageIndex - 3) <= 0 ? 1 : Model.Pager.PageIndex - 3) ; page <= ((Model.Pager.PageIndex + 3) >= Model.Pager.TotalPages ? Model.Pager.TotalPages : Model.Pager.PageIndex + 3); ++page) { %>
	        		<% if(page == Model.Pager.PageIndex) { %>
	        			<li class="pager-item-active"><%= page.ToString() %></li>
	        		<% } else { %>
	        			<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = page }) %>"><%= page.ToString() %></a></li>
	        		<% } %>
	        	<% } %>
	        	<% if(Model.Pager.HasNextPage) { %>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = Model.Pager.PageIndex + 1 }) %>">Weiter »</a></li>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = Model.Pager.TotalPages }) %>">Letzte</a></li>
	        	<% } else { %>
	        		
	        	<% } %>
	        </ul>
        </div>
        <ul class="book">
            <% foreach(var book in Model.Books) { %>
	            <li class="book-item">
	                <a href="<%= Url.Action("View", "Books", new { id = book.id }) %>">
	                    <span class="book-item-header">Nr. <%= book.number %></span>
	                    <span>
	                        <div class="book-item-image">
	                            <% if(!String.IsNullOrEmpty(book.image)) { %>
	                            <img src="<%= Url.Content(GlobalConfig.Get().ImagePath + book.image) %>" height="120" alt="<%= book.name %>" style="border: none; display: block; margin-left: auto; margin-right: auto;" />
								<% } %>
	                        </div>
	                    </span>
	                    <span class="book-item-footer"><%= book.name %></span>
	                </a>
	            </li>
            <% } %>
        </ul>
        <div class="pager">
	        <ul class="pager-list">
				<li class="pager-item-active">Seite <%= Model.Pager.PageIndex %> von <%= Model.Pager.TotalPages %></li>
	        	<% if(Model.Pager.HasPrevPage) { %>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = 1 }) %>">Erste</a></li>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = Model.Pager.PageIndex - 1 }) %>">« Zurück</a></li>
	        	<% } else { %>
	        		
	        	<% } %>
	        	<% for(long page = ((Model.Pager.PageIndex - 3) <= 0 ? 1 : Model.Pager.PageIndex - 3) ; page <= ((Model.Pager.PageIndex + 3) >= Model.Pager.TotalPages ? Model.Pager.TotalPages : Model.Pager.PageIndex + 3); ++page) { %>
	        		<% if(page == Model.Pager.PageIndex) { %>
	        			<li class="pager-item-active"><%= page.ToString() %></li>
	        		<% } else { %>
	        			<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = page }) %>"><%= page.ToString() %></a></li>
	        		<% } %>
	        	<% } %>
	        	<% if(Model.Pager.HasNextPage) { %>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = Model.Pager.PageIndex + 1 }) %>">Weiter »</a></li>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = Model.Pager.TotalPages }) %>">Letzte</a></li>
	        	<% } else { %>
	        		
	        	<% } %>
	        </ul>
        </div>
    </div>
</asp:Content>


