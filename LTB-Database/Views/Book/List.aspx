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
	<%= Model.Category.name %> - LTB-Database
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="content-block">          
        <h3>Kategorie: <%= Model.Category.name %> (<a class="h3link" href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, layout = "shelf" }) %>">Kacheln</a>)</h3>
        <div class="pager">
	        <ul class="pager-list">
				<li class="pager-item-active">Seite <%= Model.Pager.PageIndex %> von <%= Model.Pager.TotalPages %></li>
	        	<% if(Model.Pager.HasPrevPage) { %>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = 1, layout = "list" }) %>">Erste</a></li>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = Model.Pager.PageIndex - 1, layout = "list" }) %>">« Zurück</a></li>
	        	<% } else { %>
	        		
	        	<% } %>
	        	<% for(long page = ((Model.Pager.PageIndex - 3) <= 0 ? 1 : Model.Pager.PageIndex - 3) ; page <= ((Model.Pager.PageIndex + 3) >= Model.Pager.TotalPages ? Model.Pager.TotalPages : Model.Pager.PageIndex + 3); ++page) { %>
	        		<% if(page == Model.Pager.PageIndex) { %>
	        			<li class="pager-item-active"><%= page.ToString() %></li>
	        		<% } else { %>
	        			<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = page, layout = "list" }) %>"><%= page.ToString() %></a></li>
	        		<% } %>
	        	<% } %>
	        	<% if(Model.Pager.HasNextPage) { %>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = Model.Pager.PageIndex + 1, layout = "list" }) %>">Weiter »</a></li>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = Model.Pager.TotalPages, layout = "list" }) %>">Letzte</a></li>
	        	<% } else { %>
	        		
	        	<% } %>
	        </ul>
        </div>
        <table border="0" cellspacing="0" cellpadding="0">
        	<thead>
	        	<tr>
	        		<th class="left-column">Nr.</th>
	        		<th class="right-column">Name</th>
	        	</tr>
        	</thead>
        	<% int mod = 0; %>
        	<% foreach(var book in Model.Books) { %>
    		<% mod++; %>
    		<tbody>
	    		<tr <% if(mod % 2 == 1) { %>class="darker-row"<% } %>>
	    			<td class="left-column"><a class="link" href="<%= Url.Action("View", "Books", new { id = book.id }) %>"><%= book.number %></a></td>
	    			<td class="right-column"><a class="link" href="<%= Url.Action("View", "Books", new { id = book.id }) %>"><%= book.name %></a></td>
	    		</tr>
    		</tbody>
        	<% } %>
        </table>
        <div class="pager">
	        <ul class="pager-list">
				<li class="pager-item-active">Seite <%= Model.Pager.PageIndex %> von <%= Model.Pager.TotalPages %></li>
	        	<% if(Model.Pager.HasPrevPage) { %>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = 1, layout = "list" }) %>">Erste</a></li>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = Model.Pager.PageIndex - 1, layout = "list" }) %>">« Zurück</a></li>
	        	<% } else { %>
	        		
	        	<% } %>
	        	<% for(long page = ((Model.Pager.PageIndex - 3) <= 0 ? 1 : Model.Pager.PageIndex - 3) ; page <= ((Model.Pager.PageIndex + 3) >= Model.Pager.TotalPages ? Model.Pager.TotalPages : Model.Pager.PageIndex + 3); ++page) { %>
	        		<% if(page == Model.Pager.PageIndex) { %>
	        			<li class="pager-item-active"><%= page.ToString() %></li>
	        		<% } else { %>
	        			<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = page, layout = "list" }) %>"><%= page.ToString() %></a></li>
	        		<% } %>
	        	<% } %>
	        	<% if(Model.Pager.HasNextPage) { %>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = Model.Pager.PageIndex + 1, layout = "list" }) %>">Weiter »</a></li>
	        		<li class="pager-item"><a href="<%= Url.Action("Category", "Books", new { id = Model.Category.id, page = Model.Pager.TotalPages, layout = "list" }) %>">Letzte</a></li>
	        	<% } else { %>
	        		
	        	<% } %>
	        </ul>
        </div>
    </div>
</asp:Content>
