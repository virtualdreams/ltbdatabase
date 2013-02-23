<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<LTB_Database.ViewModels.HomeIndexModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<title>Home - LTB-Database</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-block">          
        <h3>Letzte Eintragungen</h3>
        <ul class="book">
            <% foreach(var book in Model.Latest) { %>
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
    </div>
    <div style="clear: both;"></div>
</asp:Content>


