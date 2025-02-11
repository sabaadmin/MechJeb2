/*
 * Copyright Lamont Granquist (lamont@scriptkiddie.org)
 * Dual licensed under the MIT (MIT-LICENSE) license
 * and GPLv2 (GPLv2-LICENSE) license or any later version.
 */

#nullable enable

using System;

namespace MechJebLib.Utils
{
    /// <summary>
    /// Singleton logger with some minimum viable dependency injection.  This is necesary
    /// to keep Debug.Log from pulling Unity into MechJebLib.  The logger passed into
    /// Register should probably be thread safe.
    /// </summary>
    public class Logger
    {
        private Logger() { }
        
        private static Logger _instance { get; } = new Logger();

        private Action<object> _logger = o => { };

        private void LogImpl(string message)
        {
            _logger(message);
        }

        public static void Register(Action<object> logger)
        {
            _instance._logger = logger;
        }
        
        public static void Log(string message)
        {
            _instance.LogImpl(message);
        }
    }
}
