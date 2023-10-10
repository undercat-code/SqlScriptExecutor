using System.Text;

namespace SqlScriptExecutor.Core

{
    public class EmailMessageBuilder
    {
        public string Build(List<string> errorCollection)
        {
            var mailText = new StringBuilder("During the script executing the following errors occured:\r\n<table cellspacing=\"2\" border=\"1\" cellpadding=\"5\" width=\"600\">" + "<tr>\r\n    <th>Script Path</th>\r\n    <th>Error</th>\r\n  </tr>\r\n");
            //var mailText = $"During the script executing the following errors occured:\r\n<table cellspacing=\"2\" border=\"1\" cellpadding=\"5\" width=\"600\">" + "<tr>\r\n    <th>Script Path</th>\r\n    <th>Error</th>\r\n  </tr>\r\n";
            for (var i = 0; i < errorCollection.Count; i += 2)
            {
                mailText.Append($"<tr>\r\n    <th align=\"left\">{errorCollection[i]}</th>\r\n    <th align=\"left\">{errorCollection[i + 1]}</th>\r\n  </tr>\r\n");
            }
            mailText.Append($"</table>");
            return mailText.ToString();
        }

    }
}
