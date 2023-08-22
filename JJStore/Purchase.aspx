<%@ Page Title="" Language="C#" MasterPageFile="~/JJStoreMaster.Master" AutoEventWireup="true" CodeFile="Purchase.aspx.cs" Inherits="JJStore.Purchase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .right-div {
            position: fixed;
            top: 0;
            right: 0;
            width: 20%;
            height: 100%;
            background-color: #f2f2f2;
            box-shadow: -3px 0px 10px rgba(0, 0, 0, 0.1);
            z-index: 9999;
        }
    </style>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.0/jquery.min.js"></script>
    <script>

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Purchase" runat="server">
       <span>Your voucher Code <asp:Label ID="lblVoucherCode" runat="server" ></asp:Label></span>
           <div style="margin-top: 10px;">
            <span>Supplier Name</span>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <span>Date</span>
            <asp:TextBox  ID="TextBox3" runat="server"></asp:TextBox>
         
            </div>
     <div style="float: left">
            <p>Remark</p>
            <asp:DropDownList ID="ddlRemark" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlRemark_SelectedIndexChanged">
                <asp:ListItem Text="new" Value="new"></asp:ListItem>
                <asp:ListItem Text="none" Value="none"></asp:ListItem>
                <asp:ListItem Text="change" Value="change"></asp:ListItem>
             </asp:DropDownList>
        </div>
        <div style="float: left;margin-left:20px">
            <p>Stock ID</p>
            <asp:TextBox ID="ID" runat="server"></asp:TextBox>
        </div>
        <div style="float: left;margin-left:20px">
            <p>Name</p>
            <asp:TextBox ID="Name" runat="server"></asp:TextBox>
        </div>
        <div style="float: left;margin-left:20px">
            <p>Qunatity</p>
            <asp:TextBox ID="Quanty" runat="server"></asp:TextBox>
              <asp:RegularExpressionValidator ID="NumberValidator" runat="server"
            ControlToValidate="Quanty"
            ValidationExpression="^\d+$"
            ErrorMessage="Only number."
            Display="Dynamic" />
        </div>
        <div style="float: left;margin-left:20px">
            <p>Price</p>
            <asp:TextBox ID="Price" runat="server"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
            ControlToValidate="Price"
            ValidationExpression="^\d+$"
            ErrorMessage="Only number."
            Display="Dynamic" />
        </div>
        <div style="float: left;margin-left:20px">
            <p>Total Price</p>
            <asp:TextBox ID="Total_Price" runat="server"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
            ControlToValidate="Total_Price"
            ValidationExpression="^\d+$"
            ErrorMessage="Only number."
            Display="Dynamic" />
        </div>
      
   
        <div style="float: left;margin-left:20px;margin-top:20px">
            <asp:Button ID="Save" runat="server" Text="Save/Add" OnClick="Save_Click" />
        </div>
        <div style="clear:both" ></div>


        <h3>Items</h3>
        <asp:GridView ID="GridView1" runat="server" CssClass="table"
            width="40%" ShowHeaderWhenEmpty="True" >
        </asp:GridView>

     <div class="right-div">
            <h2>Purchase Summary </h2>
            <div>
                <span>Voucher Code <asp:Label ID="voucherNo" runat="server"></asp:Label></span> 
            </div>
           <div>
                <span>Supplier Name <asp:Label ID="supplierName" runat="server"></asp:Label></span> 
            </div>
           <div>
                <span>Date <asp:Label ID="purchaseDate" runat="server"></asp:Label></span> 
            </div>
           <div>
                <span>Total Item <asp:Label ID="totalItem" runat="server"></asp:Label></span> 
            </div>
           <div>
                <span>Total Amount <asp:Label ID="totalAmount" runat="server"></asp:Label></span> 
            </div>
          <div>
               <asp:Button ID="saveVoucher" runat="server"></asp:Button>
            </div>
        </div>
   
</asp:Content>

