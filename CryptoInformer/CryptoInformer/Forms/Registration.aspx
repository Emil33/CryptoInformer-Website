<%@ Page Title="Registration" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Registration Page</title>

    <% if(registerSuccess) { %>
        <meta http-equiv = "refresh" content = "3; url = Login.aspx" />
    <% } %>

        <link href="../UserFormStyles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <main class="user-form">
    <div class="cotainer">
            <div class="row justify-content-center">
                <div class="col-md-8">
                    <div class="card">
                        <div class="card-header">Sign Up</div>
                        <div class="card-body">
                            <div class="form">
                                <div class="form-group row">
                                    <label for="email_address" class="col-md-4 col-form-label text-md-right">E-Mail Address</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label for="password" class="col-md-4 col-form-label text-md-right">Password</label>
                                    <div class="col-md-6">
                                        <asp:TextBox TextMode="Password" ID="passwordTextBox" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="confirmPpassword" class="col-md-4 col-form-label text-md-right">Confirm Password</label>
                                    <div class="col-md-6">
                                        <asp:TextBox TextMode="Password" ID="confirmPasswordTextBox" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6 offset-md-4">
                                    <asp:Button  CssClass="btn btn-dark" ID="registerButton" Text="Sign Up" onclick="registerEventMethod" runat="server"/>                                    
                                </div>
                                <asp:Label ID="notificationLabel" runat="server" BorderColor="Red"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>    
    </main>
</asp:Content>
