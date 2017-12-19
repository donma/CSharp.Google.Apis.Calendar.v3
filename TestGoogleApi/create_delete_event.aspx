<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="create_delete_event.aspx.cs" Inherits="TestGoogleApi.create_delete_event" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnCreateEvent" runat="server" Text="建立活動" OnClick="btnCreateEvent_Click" />
            <br />
            <br />
            建立後的 Event Id :
            <asp:TextBox ID="txtEventId" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnDelEvent" runat="server" Text="刪除活動" OnClick="btnDelEvent_Click" />
        </div>
    </form>
</body>
</html>
