<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<LTB_Database.ViewModels.ViewCategoriesModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<title>Alle Kategorien - LTB-Database</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-block">          
        <h3>Kategorien</h3>
		<ul class="book">
            <% foreach(var category in Model.Category) { %>
	            <li class="book-item">
	                <a href="<%= Url.Action("View", "Categories", new { id = category.id }) %>">
	                    <span>
	                        <div class="book-item-image">
	                        
	                        </div>
	                    </span>
	                    <span class="book-item-footer"><%= category.name %></span>
	                </a>
	            </li>
            <% } %>
        </ul>
    </div>
</asp:Content>


