namespace JUtil
{
    /// <summary>
    /// LoggingMode
    /// </summary>
    public enum LoggingMode
    {
        None = 0,

        /// <summary>
        /// log message to Nlog
        /// </summary>
        Nlog = 1,

        /// <summary>
        /// log message to SILog
        /// </summary>
        SILog = 2,

        /// <summary>
        /// log message to Console
        /// </summary>
        Console = 3,

        /// <summary>
        /// log message to EventLog
        /// </summary>
        EventLog = 4,

        /// <summary>
        /// log message to File
        /// </summary>
        FileLog = 5

    } // end of LoggingMode
}
