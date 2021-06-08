<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="AboutCircuit.aspx.cs" Inherits="School_Website.AboutCircuit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h4>Info about <span id="circuitName" runat="server"></span></h4>
    <table class="table">
        <thead>
            <th>Circuit No</th>
            <th>Name</th>
            <th>Manager</th>
            <th>Cellphone</th>
            <th>Telephone</th>
            <th>No. of Schools</th>
        </thead>
        <tbody id="circuit" runat="server">
            
        </tbody>
    </table>
    <h4>Schools under <span id="areaName" runat="server"></span></h4>
    <table class="our-table schools-table summary-table report-summaty-table ">
        <thead>
            <th>EMIS</th>
            <th>Name</th>
            <th>Principal</th>
            <th>Cellphone</th>
            <th>Telephone</th>
            <th>No. of Learners</th>
            <th>Avg Budget</th>
        </thead>
        <tbody id="schools" runat="server"></tbody>
    </table>

</asp:Content>
