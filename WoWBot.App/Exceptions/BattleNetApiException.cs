using System;

namespace ReminderBot.App.Exceptions;

public class BattleNetApiException : Exception
{
    public BattleNetApiException(string message) : base(message) { }
}