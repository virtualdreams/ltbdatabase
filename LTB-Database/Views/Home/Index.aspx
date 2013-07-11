<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<CategoryViewModel>" %>
<%@ Import Namespace="LTB_Database.Core" %>
<%@ Import Namespace="LTB_Database.Models" %>
<%@ Import Namespace="LTB_Database.ViewModels" %>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	Lustiges Taschenbuch Datenbank - Home
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-block">          
        <h3>Letzte Eintragungen</h3>
        <ul class="book">
            <% foreach(var book in Model.Books) { %>
	            <li class="book-item">
	                <a href="<%= Url.Action("view", "book", new { id = book.Id }) %>">
	                    <span class="book-item-header">Nr. <%= book.Number %></span>
                        <div class="book-item-image">
                            <% if(!String.IsNullOrEmpty(book.Image)) { %>
                            <img src="<%= Url.Content(GlobalConfig.Get().ImagePath + book.Image) %>" alt="<%= book.Name %>" style="border: none; display: block; margin-left: auto; margin-right: auto; height: 120px;" />
							<% } else { %>
							<img src="/Content/no-image.png" alt="<%= book.Name %>" style="border: none; display: block; margin-left: auto; margin-right: auto; height: 120px;" />
							<% } %>
                        </div>
	                    <span class="book-item-footer"><%= book.Name %></span>
	                </a>
	            </li>
            <% } %>
        </ul>       
    </div>
    <div style="clear: both;"></div>
</asp:Content>


