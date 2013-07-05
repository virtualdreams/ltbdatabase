<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<CategoryViewModel>" %>
<%@ Import Namespace="LTB_Database.Core" %>
<%@ Import Namespace="LTB_Database.Models" %>
<%@ Import Namespace="LTB_Database.ViewModels" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	Lustiges Taschenbuch Datenbank - Kategorie löschen
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="content-block">          
        <h3>Löschen</h3>
        <% using(Html.BeginForm("delete", "category", FormMethod.Post, new { enctype = "multipart/form-data", id = "del-form", style="" })) { %>
			<div style="text-align: center;">
				<div style="margin-bottom: 20px; margin-top: 20px;">
					Soll die Kategorie <b>"<%= Model.Category.Name %>"</b> wirklich gelöscht werden?
				</div>
				<input type="hidden" name="id" value="<%= Model.Category.Id %>" />
				<input type="submit" name="submit" value="Löschen" />
			</div>
		<% } %>
    </div>
</asp:Content>

