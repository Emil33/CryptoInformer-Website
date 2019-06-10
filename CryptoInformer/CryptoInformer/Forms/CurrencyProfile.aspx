<%@ Page Title="Profile" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CurrencyProfile.aspx.cs" Inherits="Forms_CurrencyProfile" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Profile Page</title>
    <link href="../WebsiteStyles.css" rel="stylesheet" />

    <meta http-equiv="refresh" content="60">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-3"> 

            <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>

            <div class="row">
            <ul class="profileLeftColumnUL">
                <li class="profileLeftColumnLI"><h2 style="margin-bottom:50px">Profile Page Of - <%= aPIResultFiat.name %> (<%= cryptoCurrencySymbol %>)</h2></li>
                <li class="profileLeftColumnLI"><img class="logoImg" src="<%= aPICoinDetailsResult.logoURL %>" alt="Card image cap"/></li>
                <li class="profileLeftColumnLI"><a class="btn btn-dark btn-xs btn-block" href="<%= aPICoinDetailsResult.websiteURL %>" role="button">Currency Website</a></li>
                <li class="profileLeftColumnLI"><a class="btn btn-dark btn-xs btn-block" href="<%= aPICoinDetailsResult.messageBoardURL %>" role="button">Message Board</a></li>
                <li class="profileLeftColumnLI"><a class="btn btn-dark btn-xs btn-block" href="<%= aPICoinDetailsResult.explorerURL %>" role="button">Blockchain Explorer</a></li>
                <li class="profileLeftColumnLI"><a class="btn btn-dark btn-xs btn-block" href="<%= aPICoinDetailsResult.sourceCodeURL %>" role="button">Source Code</a></li>   
                <li class="profileLeftColumnLI"><a class="btn btn-dark btn-xs btn-block" href="ComparePrices.aspx?currencySymbol=<%= cryptoCurrencySymbol %>" role="button">Compare Prices</a></li>
                <li class="profileLeftColumnLI"><asp:Button  CssClass="btn btn-dark btn-xs btn-block" ID="subscribeButton" Text="Subscribe" onclick="subscribeUser" runat="server"/></li>
                <li class="profileLeftColumnLI"><asp:Button  CssClass="btn btn-dark btn-xs btn-block" ID="unsubscribeButton" Text="Unsubscribe" onclick="unsubscribeUser" runat="server"/></li>
                <li style="margin-top:30px; margin-bottom:50px">
                    <AjaxControlToolkit:Rating ID="Rating" runat="server" AutoPostBack="true"
                        StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
                        FilledStarCssClass="FilledStar">
                    </AjaxControlToolkit:Rating>
                    <asp:Label ID="lbresult" runat="server" Text=""></asp:Label>
                </li>
                <li><asp:TextBox Width="350px" runat="server" ID="txtreview" TextMode="MultiLine"></asp:TextBox></li>
                <li><asp:Button CssClass="btn btn-dark btn-xs btn-block" runat="server" Text="Submit Rating" ID="btnsubmit" OnClick="btnsubmit_Click" /></li>
                <li style="margin-top:10px"><a Class="btn btn-dark btn-xs btn-block" ID="commentsButton" href="CurrencyComments.aspx?currencySymbol=<%= cryptoCurrencySymbol %>" >View Comments</a></li>
                <li style="margin-top:30px"><asp:Label ID="notificationLabel" runat="server"/></li>

            </ul>
            </div>
        </div>

        <div class="col-md-9">

            <div class="details-panel-item--marketcap-stats flex-container">
                <div class="coin-summary-item">
                    <h5 class="coin-summary-item-header">Market Cap</h5>
                    <div class="coin-summary-item-detail">
                            <span data-currency-market-cap="" data-usd="18977624373.3">
                                <span data-currency-value=""><%=aPIResultFiat.marketCap%></span>
                                <span data-currency-code="">USD</span>
                            </span><br>
                        <span class="text-gray">
                            <span data-format-market-cap="" data-format-value="3628549.67101"><%=aPIResultCrypto.marketCap%></span>
                            <%= cryptoCurrencySymbol %>
                            <br>
                        </span>
                    </div>
                </div>
                    <div class="coin-summary-item">
                        <h5 class="coin-summary-item-header">Volume (24h)</h5>
                        <div class="coin-summary-item-detail">
                            <span data-currency-volume="" data-usd="9594563432.17">
                                <span data-currency-value=""><%=aPIResultFiat.volume24Hours%></span>
                                <span data-currency-code="">USD</span>
                            </span><br>
                            <span class="text-gray">
                                <span data-format-volume-crypto="" data-format-value="1834494.62907"><%=aPIResultCrypto.volume24Hours%></span>
                                <%= cryptoCurrencySymbol %>
                            </span>
                        </div>
                    </div>
                 <div class="coin-summary-item">
                    <h5 class="coin-summary-item-header">Circulating Supply</h5>
                    <div class="coin-summary-item-detail details-text-medium">
                        <span data-format-supply="" data-format-value="105574571.749"><%=aPIResultFiat.circulatingSupply%></span>
                        <%= cryptoCurrencySymbol %>
                    </div>
                </div>
            </div>

            <h4>ShortDescription:</h4>
            <p class="lead">
                <%=aPICoinDetailsResult.description%>
            </p>

            <asp:Chart ID="Chart1" runat="server" Width="1200px" Height="600px">
                <Titles>
                    <asp:Title></asp:Title>
                </Titles>
                <Series>
                    <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="Series1"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisX Title="Month"></AxisX>
                        <AxisY Title="Price"></AxisY>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
    </div>
</asp:Content>

