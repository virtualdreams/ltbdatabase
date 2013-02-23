<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<LTB_Database.ViewModels.CategoriesDeleteViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	<title>Kategorie löschen - LTB-Database</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="content-block">          
        <h3>Löschen</h3>
        <% using(Html.BeginForm("Delete", "Categories", FormMethod.Post, new { enctype = "multipart/form-data", id = "del-form", name = "del-form", style="" })) { %>
			<div style="text-align: center;">
				<div style="margin-bottom: 20px; margin-top: 20px;">
					Soll die Kategorie <b>"<%= Model.Category.name %>"</b> wirklich gelöscht werden?
				</div>
				<input type="hidden" name="id" value="<%= Model.Category.id %>" />
				<input type="submit" name="submit" value="Löschen" />
			</div>
		<% } %>
    </div>
</asp:Content>

