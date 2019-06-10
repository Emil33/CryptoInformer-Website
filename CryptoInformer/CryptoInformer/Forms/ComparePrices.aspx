<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ComparePrices.aspx.cs" Inherits="Forms_ComparePrices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Comapre Prices Page</title>

    <meta http-equiv="refresh" content="60">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2 style="margin-left: 50px">Latest Prices </h2>

        <div class="row float-right">            
            <h2 style="margin-right: 5px">Select a fiat currency (Default USD): </h2>
            <div class="dropdown show" style="margin-right: 20px">
              <a class="btn btn-secondary btn-lg dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                Currency Menu
              </a>
              <div class="dropdown-menu" aria-labelledby="dropdownMenuLink" >
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=USD">United States Dollar (USD)</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=ARS">Argentine Peso ARS</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=AUD">Australian Dollar AUD</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=BRL">Brazilian Real BRL</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=CAD">Canadian Dollar CAD</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=CNY">Chinese Yuan CNY</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=HRK">Croatian Kuna HRK</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=CUP">Cuban Peso CUP</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=CZK">Czech Koruna CZK</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=DKK">Danish Krone DKK</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=EUR">Euro EUR</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=HKD">Hong Kong Dollar HKD</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=HUF">Hungarian Forint HUF</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=INR">Indian Rupee INR</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=JMD">Jamaican Dollar JMD</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=JPY">Japanese Yen JPY</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=MXN">Mexican Peso MXN</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=NZD">New Zealand Dollar NZD</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=NOK">Norwegian Krone NOK</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=PLN">Polish Złoty PLN</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=GBP">Pound Sterling GBP</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=RUB">Russian Ruble RUB</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=SGD">Singapore Dollar SGD</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=ZAR">South African Rand ZAR</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=KRW">South Korean Won KRW</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=SEK">Swedish Krona SEK</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=CHF">Swiss Franc CHF</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=TRY">Turkish Lira TRY</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=UAH">Ukrainian Hryvnia UAH</a>
				<a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=AED">United Arab Emirates Dirham AED</a>
                <a class="dropdown-item" href="ComparePrices.aspx?currencySymbol=<%= currencySymbol %>&selectedFiatCurrency=VND">Vietnamese Dong VND</a>
                    
              </div>
            </div>
        </div>

    <div class="container">                                   
        <div class="form-group" style="width:100%;height:100%;text-align:center; vertical-align:middle">
            <table class="table" style="width:100%">
                <thead class="thead-dark">
                    <tr>
                    <th scope="col">#</th>  
                    <th scope="col">Exchange Name</th>
                    <th scope="col">Cryptocurrency Symbol</th>
                    <th scope="col">Cryptocurrency Name</th>
                    <th scope="col">Price</th>
                    <th scope="col">Volume Crypto (24h)</th>
                    <th scope="col">Volume Fiat (24h)</th>
                    <th scope="col">Change (24h)</th>
                    </tr>
                </thead>
                <tbody>
                <% foreach (var exchange in aPIResult) { %>
                    <tr>                        
                    <th scope="row"><%= aPIResult.IndexOf(exchange)+1 %></th>
                        <td><%= exchange.exchangeName %></td>
                        <td><%= exchange.cryptoSymbol %></td>
                        <td><%= exchange.currencyName %></td>
                        <td><%= exchange.price + " " + exchange.fiatSymbol %></td>
                        <td><%= exchange.volume24HoursCrypto + " " + exchange.cryptoSymbol %></td>
                        <td><%= exchange.volume24HoursFiat + " " + exchange.fiatSymbol %></td>
                        <td><%= exchange.priceChange24Hours %>%</td>
                    </tr>
                <% } %>
                </tbody>
            </table>
        </div>
    </div>

</asp:Content>

