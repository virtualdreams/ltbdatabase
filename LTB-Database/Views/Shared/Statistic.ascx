<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LTB_Database.ViewModels.StatisticViewModel>" %>

<div class="content-block">
    <h3>Statistik</h3>
    <ul class="link-list" style="padding-left: 14px;">
        <li class="link-item" style="border-right: 1px solid #DDDDDD; padding-right: 10px;">
            <div style="display: block; font-size: 16px;"><%= Model.Books %>
				<span style="display: block; color: #928e82;">Bücher</span>
            </div>
        </li>
        <li class="link-item" style="padding-left: 10px;">
            <div style="display: block; font-size: 16px;"><%= Model.Stories %>
				<span style="display: block; color: #928e82;">Geschichten</span>
            </div>
        </li>
    </ul>
</div>