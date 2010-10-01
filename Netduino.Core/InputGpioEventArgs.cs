namespace Netduino.Core
{
    /// <summary>
    /// A simple DTO that represents the state of an input pin on the netduino
    /// </summary>
	public class InputGpioEventArgs
	{
        public InputGpioEventArgs(int pin, bool edge)
		{
            Pin = pin;
			Edge = edge;

		}

        /// <summary>
        /// What pin this event is for
        /// </summary>
        public int Pin { get; set; }

        /// <summary>
        /// What state the pin transitioned to
        /// </summary>
        public bool Edge { get; set; }
	}
}
