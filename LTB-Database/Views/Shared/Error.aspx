<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/LTBDB.Master" Inherits="System.Web.Mvc.ViewPage<LTB_Database.ViewModels.ErrorModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
	Lustiges Taschenbuch Datenbank - Fehler
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="content-block">
		<h3>Fehler</h3>
		<div class="indent">
			<%= Model.Error %>
		</div>
	</div>
</asp:Content>
