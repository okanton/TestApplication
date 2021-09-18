using System;
using System.Threading.Tasks;

namespace TestApplication.Utility
{
    public class ConsoleSpinner
    {
        private static string[,] sequence = null;

        public int Delay { get; set; } = 500;

        private readonly int totalSequences = 0;
        private int counter;

        public ConsoleSpinner(int delay)
        {
            Delay = delay;

            counter = 0;
            sequence = new string[,] {
                { ".   ", "..  ", "... ", "...." },
            };

            totalSequences = sequence.GetLength(0);
        }

        public async Task Turn(string displayMsg = "", int sequenceCode = 0)
        {
            counter++;

            await Task.Delay(Delay);

            sequenceCode = sequenceCode > totalSequences - 1 ? 0 : sequenceCode;

            int counterValue = counter % 4;

            string fullMessage = displayMsg + sequence[sequenceCode, counterValue];
            int msglength = fullMessage.Length;

            Console.Write(fullMessage);

            Console.SetCursorPosition(Console.CursorLeft - msglength, Console.CursorTop);
        }
    }
}