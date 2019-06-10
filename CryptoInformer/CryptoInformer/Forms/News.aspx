<%@ Page Title="News" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="Forms_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>News Page</title>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<h2 style="margin-left: 50px">Latest News Articles</h2>--%>
        <h2 style="margin-left: 50px">Latest News Articles </h2>

        <div class="row float-right">            
            <h2 style="margin-right: 5px;">Specific Crypto Currency News: </h2>
            <div class="dropdown show" style="margin-right: 20px">
              <a class="btn btn-secondary btn-lg dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                CryptoCurrency Menu
              </a>
              <div class="dropdown-menu" aria-labelledby="dropdownMenuLink" >
                <a class="dropdown-item" href="News.aspx?currencyName=bitcoin">Bitcoin</a>
                <a class="dropdown-item" href="News.aspx?currencyName=litecoin">Litecoin</a>
                <a class="dropdown-item" href="News.aspx?currencyName=ethereum">Ethereum</a>
                <a class="dropdown-item" href="News.aspx?currencyName=stellar">Stellar</a>
                <a class="dropdown-item" href="News.aspx?currencyName=xrp">XRP</a>
                <a class="dropdown-item" href="News.aspx?currencyName=eos">EOS</a>
                <a class="dropdown-item" href="News.aspx?currencyName=tether">Tether</a>
                <a class="dropdown-item" href="News.aspx?currencyName=cardano">Cardano</a>
                <a class="dropdown-item" href="News.aspx?currencyName=tron">TRON</a>
                <a class="dropdown-item" href="News.aspx?currencyName=monero">Monero</a>
                <a class="dropdown-item" href="News.aspx?currencyName=dash">Dash</a>
                <a class="dropdown-item" href="News.aspx?currencyName=iota">IOTA</a>
                <a class="dropdown-item" href="News.aspx?currencyName=tezos">Tezos</a>
                <a class="dropdown-item" href="News.aspx?currencyName=neo">NEO</a>
                <a class="dropdown-item" href="News.aspx?currencyName=ontology">Ontology</a>
                <a class="dropdown-item" href="News.aspx?currencyName=maker">Maker</a>
                <a class="dropdown-item" href="News.aspx?currencyName=nem">NEM</a>
                <a class="dropdown-item" href="News.aspx?currencyName=zcash">Zcash</a>
                <a class="dropdown-item" href="News.aspx?currencyName=vechain">VeChain</a>
                <a class="dropdown-item" href="News.aspx?currencyName=dogecoin">Dogecoin</a>
                <a class="dropdown-item" href="News.aspx?currencyName=omisego">OmiseGO</a>
                <a class="dropdown-item" href="News.aspx?currencyName=waves">Waves</a>
                <a class="dropdown-item" href="News.aspx?currencyName=qtum">Qtum</a>
                <a class="dropdown-item" href="News.aspx?currencyName=decred">Decred</a>
                <a class="dropdown-item" href="News.aspx?currencyName=lisk">Lisk</a>
                <a class="dropdown-item" href="News.aspx?currencyName=augur">Augur</a>
                <a class="dropdown-item" href="News.aspx?currencyName=nano">Nano</a>
                <a class="dropdown-item" href="News.aspx?currencyName=ravencoin">Ravencoin</a>
                <a class="dropdown-item" href="News.aspx?currencyName=0x">0x</a>
                <a class="dropdown-item" href="News.aspx?currencyName=zilliqa">Zilliqa</a>
                   
              </div>
            </div>
        </div>

    <div class="container" style="margin-top: 60px">
        
    <% foreach (var article in aPIResult) { %>   
        <div class="card-group">                    
              <div class="card">
                <h3 class="card-header"><%= article.title %></h3>
                <img class="card-img-top" src="<%= article.imageURL %>" alt="Card image cap"/>
                <div class="card-body">
                  <h6 class="card-title">Source Name: <%= article.source %></h6>
                  <p class="card-text"><%= article.description %></p>
                  <h4 class="card-text">Similar Articles:</h4>
                  <% foreach (var similarArticle in article.similarArticlesList) { %>  
                    <h5 class="card-text"><%= similarArticle.title %></h5>
                    <p class="card-text"><%= similarArticle.source %> <a href="<%= similarArticle.url %>">Go to source.</a></p>
                                   
                  <% } %>
                </div>
                <div class="card-footer">
                    <a href="<%= article.url %>">Go to primary source.</a>
                </div>
              </div>            
        </div>
    <% } %>
    </div>
</asp:Content>
