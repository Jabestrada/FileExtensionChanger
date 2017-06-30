<%@ Page Async="true" Language="c#" Debug="true" MaintainScrollPositionOnPostback="true" %>

<%@ Import Namespace=" FileExtensionChanger.Lib" %>
<%@ Import Namespace="System.Threading.Tasks" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title runat="server">File Extension Changer v.1</title>
    <link rel="stylesheet" href="/Content/bootstrap.css" />
    <style>
        .row {
            margin-top: 10px;
            margin-bottom: 15px;
        }

        .read-only {
            background-color: cornsilk;
        }

        textarea[readonly="readonly"] {
            background-color: cornsilk;
        }

        .radio-with-caption {
            display: block;
        }

            .radio-with-caption input {
                margin-right: 5px;
            }

        input[type="radio"] + label {
            font-weight: normal;
        }
    </style>
</head>

<body>
    <div class="container">
        <h1 class="text-center">File Extension Changer v.1</h1>
        <form id="Form1" runat="server">
            <div class="row">
                <label>Input filenames</label>
                <asp:TextBox ID="filenamesToChange" runat="server"
                    TextMode="MultiLine"
                    Rows="10"
                    Width="100%" />
            </div>
            <div class="row">
                <label>New file extension</label>
                <asp:TextBox ID="newExtension" runat="server"
                    Width="100%" />
            </div>
            <div class="row">
                <asp:Button ID="GoButton" CssClass="btn btn-primary" Text="Go" OnClick="ChangeExtension" runat="server" Width="100%" />
            </div>
            <asp:Label ID="status" runat="server" />
            <div id="resultsContainer" runat="server" visible="false">
                <br />
                <div class="row">
                    <label>Results</label>
                    <asp:TextBox ID="results" runat="server"
                        TextMode="MultiLine"
                        ReadOnly="true"
                        Rows="10"
                        Width="100%" />
                </div>
            </div>
        </form>
    </div>
    <script src="/Scripts/jquery-1.10.2.js"></script>
    <script src="/Scripts/bootstrap.js"></script>
</body>
</html>

<script runat="server">

    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
        base.OnLoad(e);
        Server.ScriptTimeout = int.MaxValue;
    }

    private async Task<FileExtensionChangeResult> Start()
    {
        var options = new FileExtensionChangeOptions
        {
            NewExtension = newExtension.Text,
            Strategy = ChangeExtensionStrategy.ChangeExistingExtension,
            Filenames = filenamesToChange.Text
                             .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                             .Where(s => !string.IsNullOrWhiteSpace(s))
                             .ToArray()
        };
        var fileExtensionChanger = new FileExtensionChange(options);
        return await fileExtensionChanger.Start();
    }

    private async void ChangeExtension(object sender, EventArgs e)
    {
        try
        {
            var result = await Start();
            // Display results.
            var output = new StringBuilder();
            foreach (var itemResult in result.ResultItems) {
                if (itemResult.Succeeded)
                {
                    output.AppendLine(string.Format("File {0} renamed to {1}",
                        itemResult.Filename, itemResult.NewFilename));
                }
                else {
                    output.AppendLine(string.Format("Failed to rename file {0} to {1}. Reason: {2}",
                        itemResult.Filename, itemResult.NewFilename, itemResult.Reason));
                }
            }
            results.Text = output.ToString();
        }
        catch (Exception exc)
        {
            results.Text = "FAILED: " + exc.Message;
        }
        resultsContainer.Visible = true;
    }
</script>
