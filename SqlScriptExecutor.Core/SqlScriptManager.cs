using Serilog;

namespace SqlScriptExecutor.Core
{
    public class SqlScriptManager
    {
        public readonly List<string> LogCollection;
        public readonly LogCollectionReformator LogCollectionReformator;
        public readonly EmailMessageBuilder EmailMessageBuilder;
        public readonly IMessageSender MessageSender;
        public readonly string EmailRecipient;


        public SqlScriptManager(List<string> logCollection, LogCollectionReformator logCollectionReformator, EmailMessageBuilder emailMessageBuilder, IMessageSender messageSender, string emailRecipient)
        {
            LogCollection = logCollection;
            LogCollectionReformator = logCollectionReformator;
            EmailMessageBuilder = emailMessageBuilder;
            MessageSender = messageSender;
            EmailRecipient = emailRecipient;
        }

        public void SendMessage()
        {
            var errorList = LogCollectionReformator.ReformatErrorText(LogCollection);

            if (errorList.Count > 0)
            {
                var emailBody = EmailMessageBuilder.BuildEmailMessage(errorList);
                MessageSender.Send(EmailRecipient, emailBody);
                Log.Information($"Scripts Executor got errors in progress, message was sent to recipient");

            }

        }

    }
}
