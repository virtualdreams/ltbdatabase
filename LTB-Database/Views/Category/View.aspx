<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<CategoryViewModel>" %>
<%@ Import Namespace="LTB_Database.Core" %>
<%@ Import Namespace="LTB_Database.ViewModels" %>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
	<div class="content-block">
		<h3>Kategorie...</h3>
		<ul class="link-list">
            <li style="margin-bottom: 3px;"><%= Html.ActionLink("Bearbeiten", "edit", "category", new { id = Model.Category.Id }, new { @class = "link" }) %></li>
        	<li style="margin-bottom: 3px;"><%= Html.ActionLink("Löschen", "delete", "category", new { id = Model.Category.Id }, new { @class = "link" }) %></li>
        </ul>
	</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	Lustiges Taschenbuch Datenbank - <%= Model.Category.Name %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-block">          
        <h3>Kategorie: <%= Model.Category.Name %></h3>
        <%= Html.Pager("view", "category", new { id = Model.Category.Id }, Model.Pager) %>
        <ul class="book">
            <% foreach(var book in Model.Books) { %>
	            <li class="book-item">
	                <a href="<%= Url.Action("view", "book", new { id = book.Id }) %>">
	                    <span class="book-item-header">Nr. <%= book.Number %></span>
	                    <div>
	                        <div class="book-item-image">
	                            <% if(!String.IsNullOrEmpty(book.Image)) { %>
	                            <img src="<%= Url.Content(GlobalConfig.Get().ImagePath + book.Image) %>" height="120" alt="<%= book.Name %>" style="border: none; display: block; margin-left: auto; margin-right: auto;" />
								<% } else { %>
								<img src="/Content/no-image.png" alt="<%= book.Name %>" style="border: none; display: block; margin-left: auto; margin-right: auto; height: 120px;" />
								<% } %>
	                        </div>
	                    </div>
	                    <span class="book-item-footer"><%= book.Name %></span>
	                </a>
	            </li>
            <% } %>
        </ul>
        <%= Html.Pager("view", "category", new { id = Model.Category.Id }, Model.Pager) %>
    </div>
</asp:Content>


