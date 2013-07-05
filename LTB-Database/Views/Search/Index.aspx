<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<SearchViewModel>" %>
<%@ Import Namespace="LTB_Database.Core" %>
<%@ Import Namespace="LTB_Database.Models" %>
<%@ Import Namespace="LTB_Database.ViewModels" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	Lustiges Taschenbuch Datenbank - Suche nach "<%= Model.Query %>"
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="content-block">          
        <h3>Suchergebnisse für "<%= Model.Query %>"</h3>
        <% if(Model.Books.Count() == 0) { %>
        <div class="indent">
			Die Suche ergab keine Treffer für "<%= Model.Query %>"
        </div>
        <% } else { %>
        <%= Html.Pager("index", "search", new { q = Model.Query }, Model.Pager) %>
        <ul class="book">
            <% foreach(var book in Model.Books) { %>
	            <li class="book-item">
	                <a href="<%= Url.Action("view", "book", new { id = book.Id }) %>">
	                    <span class="book-item-header">Nr. <%= book.Number %></span>
	                    <div>
	                        <div class="book-item-image">
	                            <% if(!String.IsNullOrEmpty(book.Image)) { %>
	                            <img src="<%= Url.Content(GlobalConfig.Get().ImagePath + book.Image) %>" height="120" alt="<%= book.Name %>" style="border: none; display: block; margin-left: auto; margin-right: auto;" />
								<% } %>
	                        </div>
	                    </div>
	                    <span class="book-item-footer"><%= book.Name %></span>
	                </a>
	            </li>
            <% } %>
        </ul>
        <%= Html.Pager("index", "search", new { q = Model.Query }, Model.Pager)%>
        <% } %>
    </div>
</asp:Content>
