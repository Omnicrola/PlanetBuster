using System;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public class Logging
    {
        private static Logging _instance;
        private LogLevel _currentLogLevel;

        public static Logging Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logging();
                }
                return _instance;
            }
        }

        private Logging()
        {
            _currentLogLevel = GameConstants.LoggingLevel;
        }

        public void SetLogLevel(LogLevel logLevel)
        {
            _currentLogLevel = logLevel;
        }

        public void Log(LogLevel level, string message)
        {
            if (_currentLogLevel <= level)
            {
                var gameTime = Time.time;
                var worldTime = DateTime.Now.ToString("s");
                var formattedMessage = string.Format("{0}|{1}|{2}", worldTime, gameTime, message);
                Debug.Log(formattedMessage);
            }
        }
    }

    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error
    }
}