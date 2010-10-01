namespace Netduino.Core
{
	public class OutputGpioEventArgs
	{
	    /// <summary>
        /// A simple DTO that represents the state of an output pin on the netduino
        /// </summary>
        /// <param name="pin"></param>
        /// <param name="edge"></param>
		public OutputGpioEventArgs(int pin, bool edge)
		{
			Pin = pin;
			Edge = edge;

		}

	    /// <summary>
	    /// The pin this event belongs to
	    /// </summary>
	    public int Pin { get; set; }

	    /// <summary>
	    /// The state the pin should transition to
	    /// </summary>
	    public bool Edge { get; set; }
	}
}
