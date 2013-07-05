<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<LTB_Database.ViewModels.ViewBookViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	Lustiges Taschenbuch Datenbank - Buch löschen
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="content-block">          
        <h3>Löschen</h3>
        <% using(Html.BeginForm("delete", "book", FormMethod.Post, new { enctype = "multipart/form-data", id = "del-form", style="" })) { %>
			<div style="text-align: center;">
				<div style="margin-bottom: 20px; margin-top: 20px;">
					Soll das Buch <b>"<%= Model.Book.Name %>"</b> wirklich gelöscht werden?
				</div>
			
				<input type="hidden" name="id" value="<%= Model.Book.Id %>" />
				<input type="submit" name="submit" value="Löschen" />
			</div>
		<% } %>
    </div>
</asp:Content>

