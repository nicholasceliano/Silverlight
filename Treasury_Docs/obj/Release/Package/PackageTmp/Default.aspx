<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="TDocsDataService.Default1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Treasury Docs</title>
        <link rel="Stylesheet" type="text/css" href="App_Themes/Hess/Styles/Default.css" />
        <script type="text/javascript" src="App_Themes/Hess/JavaScript/Silverlight.js"></script>
        <script type="text/javascript" src="App_Themes/Hess/JavaScript/SilverlightCustom.js"></script>
    </head>
    <body>
        <form id="form1" runat="server" class="form">
            <div id="silverlightControlHost">
                <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" class="silverLightFrame">
                    <param name="source" value="/ClientBin/RadControlsSilverlightClient.xap"/>
                    <param name="onError" value="onSilverlightError" />
                    <param name="background" value="white" />
                    <param name="minRuntimeVersion" value="4.0.50826.0" />
                    <param name="autoUpgrade" value="true" />
                    <param id="paramSilverLight" runat="server" name="InitParams" />
                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50826.0" class="unformatted">
                        <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight" class="noBorder"/>
                    </a>
                </object>
                <iframe id="_sl_historyFrame" class="iframe" />
            </div>
        </form>
    </body>
</html>
