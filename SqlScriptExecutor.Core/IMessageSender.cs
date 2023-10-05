﻿namespace SqlScriptExecutor.Core
{
    public interface IMessageSender
    {
        void Send(string to, string body, string subject = "Errors from SQL Executor process:");
    }
}
