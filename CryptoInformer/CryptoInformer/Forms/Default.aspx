<%@ Page Title="Default" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        
    <title>Home Page</title>

    <meta http-equiv="refresh" content="60">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div class="row float-right">
            <h2 style="margin-right: 5px">Select a fiat currency (Default USD): </h2>
            <div class="dropdown show" style="margin-right: 20px">
              <a class="btn btn-secondary btn-lg dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                Currency Menu
              </a>
              <div class="dropdown-menu" aria-labelledby="dropdownMenuLink" >
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=USD">United States Dollar (USD)</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=ARS">Argentine Peso ARS</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=AUD">Australian Dollar AUD</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=BRL">Brazilian Real BRL</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=CAD">Canadian Dollar CAD</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=CNY">Chinese Yuan CNY</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=HRK">Croatian Kuna HRK</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=CUP">Cuban Peso CUP</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=CZK">Czech Koruna CZK</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=DKK">Danish Krone DKK</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=EUR">Euro EUR</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=HKD">Hong Kong Dollar HKD</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=HUF">Hungarian Forint HUF</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=INR">Indian Rupee INR</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=JMD">Jamaican Dollar JMD</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=JPY">Japanese Yen JPY</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=MXN">Mexican Peso MXN</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=NZD">New Zealand Dollar NZD</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=NOK">Norwegian Krone NOK</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=PLN">Polish Złoty PLN</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=GBP">Pound Sterling GBP</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=RUB">Russian Ruble RUB</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=SGD">Singapore Dollar SGD</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=ZAR">South African Rand ZAR</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=KRW">South Korean Won KRW</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=SEK">Swedish Krona SEK</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=CHF">Swiss Franc CHF</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=TRY">Turkish Lira TRY</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=UAH">Ukrainian Hryvnia UAH</a>
				<a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=AED">United Arab Emirates Dirham AED</a>
                <a class="dropdown-item" href="Default.aspx?selectedFiatCurrency=VND">Vietnamese Dong VND</a>
                    
              </div>
            </div>
        </div>
    
        <div class="container">   
            <div class="form-group" style="width:100%;height:100%;text-align:center; vertical-align:middle">

                <table class="table" style="width:100%">
                  <thead class="thead-dark">
                       <tr class="thead-light" style="font-size:13px">
                        <th scope="col"><span>Cryptocurrencies: <%= APIGeneralResult.activeCryptocurrencies %></span></th>
                        <th scope="col"><span>Markets: <%= APIGeneralResult.activeExchanges %></span> </th>
                        <th scope="col" colspan="2"><span>Market Cap: <%= APIGeneralResult.totalMarketCap + " " + aPIResult[0].currencySymbol %></span></th> 
                        <th scope="col" colspan="2"><span>24h Volume: <%= APIGeneralResult.totalVolume24h + " " + aPIResult[0].currencySymbol %></span></th>
                        <th scope="col" colspan="2"><span>BTC Dominance: <%= APIGeneralResult.btcDominance + "%"%></span></th>
                      </tr>
                      <tr>
                        <th scope="col">#</th>  
                        <th scope="col"><a class="table-dark" href="Default.aspx?selectedSortMethod=name">Name</a></th>
                        <th scope="col"><a class="table-dark" href="Default.aspx?selectedSortMethod=symbol">Symbol</a></th>
                        <th scope="col"><a class="table-dark" href="Default.aspx?selectedSortMethod=market_cap">Market Cap</a></th> 
                        <th scope="col"><a class="table-dark" href="Default.aspx?selectedSortMethod=price">Price</a></th>
                        <th scope="col"><a class="table-dark" href="Default.aspx?selectedSortMethod=circulating_supply">Circulating Supply</a></th>
                        <th scope="col"><a class="table-dark" href="Default.aspx?selectedSortMethod=percent_change_24h">Change (24h)</a></th>
                        <th scope="col">Rating</th>
                      </tr>
                  </thead>
                  <tbody>

                    <% foreach (var coin in aPIResult) { %>
                      <tr>                        
                        <th scope="row"><%= aPIResult.IndexOf(coin)+1 %></th>
                          <td><a href="CurrencyProfile.aspx?currencySymbol=<%= coin.symbol %>"><%= coin.name %></a></td>
                          <td><%= coin.symbol %></td><td><%= coin.marketCap + " " + coin.currencySymbol %></td>
                          <td><%= coin.price + " " + coin.currencySymbol %></td>
                          <td><%= coin.circulatingSupply + " " + coin.currencySymbol %></td>
                          <td><%= coin.priceChange24Hours %>%</td>
                          <td>   
                              
                          <% ratingPresent = false; %>
                          <% foreach (var rating in aPIRatingsResult) { %>
                              <% if(rating.cryptocurrencySymbol.Equals(coin.symbol)) { %>
                                <h3><%= rating.rating %>/5</h3>
                                <% ratingPresent = true; %>
                              <% }%>
                          <% } %>
                        <% if (!ratingPresent)
                            {%>
                            <h3>0/5</h3>
                        <% }%>
                          </td>
                      </tr>
                    <% } %>
                  </tbody>
                </table>
            </div>
            <div class="text-center">
            <%--<nav aria-label="...">--%>
              <ul class="pagination pagination-lg">
<%--                <li class="page-item">
                  <a class="page-link" href="Default.aspx?numberOfCurrencies=50" >50</a>
                </li>--%>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=100">100</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=200">200</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=300">300</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=400">400</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=500">500</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=600">600</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=700">700</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=800">800</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=900">900</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=1000">1000</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=1300">1300</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=1600">1600</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="Default.aspx?numberOfCurrencies=2000">2000</a>
                </li>
              </ul>
            <%--</nav>--%>
            </div>
        </div>
</asp:Content>

