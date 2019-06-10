<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CurrencyComments.aspx.cs" Inherits="CurrencyComments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2 style="margin-left: 50px">Cryptocurrency Comments for <%= Request.QueryString["currencySymbol"]%></h2>
    <div class="container">
        
        <% if (aPIResult.Count != 0) { %> 
            <% foreach (var comment in aPIResult) { %>   
                <div class="card-group">                    
                      <div class="card">
                        <h3 class="card-header">Rating: <%= comment.rating %>/5</h3>
                        <div class="card-body">
                          <h5 class="card-title">Comment:</h5>
                          <p class="card-text"><%= comment.comment %></p>
                        </div>
                        <div class="card-footer">
                        </div>
                      </div>            
                </div>
            <% } %>
        <% } else { %>
                <div class="card-group">                    
                      <div class="card">
                        <h3 class="card-header">No Comments or Ratings Saved.</h3>
                      </div>            
                </div>
        <% } %>
    </div>

</asp:Content>

