using System;

namespace ReminderBot.App.Exceptions;

public class FavoriteException : Exception
{
    public FavoriteException(string name) : base($"Favorite {name} is not set.") { }
}