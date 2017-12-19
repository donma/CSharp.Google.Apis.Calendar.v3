<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="refresh_token.aspx.cs" Inherits="TestGoogleApi.refresh_token" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnRefreshToken" runat="server" Text="重換 User Token" OnClick="btnRefreshToken_Click" />
        </div>
    </form>
</body>
</html>
