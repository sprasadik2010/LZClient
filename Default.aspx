<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LZClient.Default"  Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SO/DD Registration</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        .header {
            background-color: #f0f0f0;
            padding: 10px;
            margin-bottom: 20px;
        }
        .filter-section {
            display: flex;
            gap: 20px;
            margin-bottom: 20px;
            flex-wrap: wrap;
        }
        .filter-item {
            display: flex;
            flex-direction: column;
        }
        .filter-item label {
            font-weight: bold;
            margin-bottom: 5px;
        }
        .filter-item input, .filter-item select {
            padding: 5px;
            width: 200px;
        }
        .practice-section {
            margin-bottom: 20px;
        }
        .practice-section select {
            padding: 5px;
            width: 200px;
        }
        .grid-container {
            overflow-x: auto;
        }
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        .grid-view th {
            background-color: #4CAF50;
            color: white;
            padding: 8px;
            text-align: left;
        }
        .grid-view td {
            padding: 8px;
            border-bottom: 1px solid #ddd;
        }
        .grid-view tr:hover {
            background-color: #f5f5f5;
        }
        .action-buttons {
            margin-top: 20px;
        }
        .action-buttons button {
            padding: 8px 16px;
            margin-right: 10px;
            background-color: #f44336;
            color: white;
            border: none;
            cursor: pointer;
        }
        .action-buttons button:hover {
            opacity: 0.8;
        }
        .print-link {
            color: blue;
            text-decoration: underline;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <h1>SO/DD Registration</h1>
        </div>

        <div class="filter-section">
            <div class="filter-item">
                <label>Forename</label>
                <asp:TextBox ID="txtForename" runat="server"></asp:TextBox>
            </div>
            <div class="filter-item">
                <label>Surname</label>
                <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
            </div>
            <div class="filter-item">
                <label>Date of Birth</label>
                <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
            </div>
            <div class="filter-item">
                <label>Postcode</label>
                <asp:TextBox ID="txtPostcode" runat="server"></asp:TextBox>
            </div>
            <div class="filter-item">
                <label>SO/DD Reference Number</label>
                <asp:TextBox ID="txtRefNumber" runat="server"></asp:TextBox>
            </div>
            <div class="filter-item">
                <label>Phone</label>
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="practice-section">
            <label>Practice:</label>
            <asp:DropDownList ID="ddlPractice" runat="server">
                <asp:ListItem Text="Avenue Eyewear" Value="Avenue Eyewear" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="grid-container">
            <asp:GridView ID="gvCustomers" runat="server" CssClass="grid-view" 
    AutoGenerateColumns="False" AllowPaging="True" PageSize="20" 
    OnPageIndexChanging="gvCustomers_PageIndexChanging"
    DataKeyNames="scheduledPaymentID">
    <Columns>
        <asp:TemplateField HeaderText="Select">
            <ItemTemplate>
                <asp:CheckBox ID="chkSelect" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="scheduledPaymentID" HeaderText="SI No" />
        <asp:BoundField DataField="Title" HeaderText="Title" />
        <asp:BoundField DataField="Forename" HeaderText="Forename" />
        <asp:BoundField DataField="Surname" HeaderText="Surname" />
        <asp:BoundField DataField="DOB" HeaderText="DOB" /> 
        <asp:BoundField DataField="PostCode" HeaderText="PostCode" /> 
        <asp:BoundField DataField="Mobile" HeaderText="Mobile" /> 
        <asp:BoundField DataField="SO_Ref_No" HeaderText="SO Ref No" />
        <asp:BoundField DataField="Regular_Amount" HeaderText="Regular Amount" DataFormatString="{0:F2}" HtmlEncode="False" />
        <asp:BoundField DataField="Item_Next_Collection_Date" HeaderText="Next Collection Date" />
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="lnkPrint" runat="server" Text="Print" CssClass="print-link" 
                    OnClick="lnkPrint_Click" CommandArgument='<%# Eval("scheduledPaymentID") %>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
        </div>

        <div class="action-buttons">
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
            <asp:Button ID="btnDeleteSelected" runat="server" Text="Delete Selected" OnClick="btnDeleteSelected_Click" />
        </div>
    </form>
</body>
</html>