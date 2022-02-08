using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlohyUtility;

namespace Day3___the_dayining.Cviko
{
    class ConsoleCopier
    {
        private const int printLenght = 10;
        private const char escapeKey = 'q';
        private const bool deleteOnPrint = true;

        private string controlString = "";
        private event InputHandler onWrite;
        private delegate void InputHandler(char c);

        public void Start()
        {
            char c = ' ';

            onWrite += (char c) =>
            {
                //on backspace
                if (c == (char)8)
                {
                    controlString.Trim(controlString[controlString.Length - 1]);
                    return;
                }

                //anything else gets appended
                controlString += c;
                if (controlString.Length > printLenght - 1)
                {
                    Console.WriteLine($"\n{controlString}\n");
                    if (deleteOnPrint) controlString = "";
                }
            };

            while ((c = (Convert.ToChar(Console.ReadKey().KeyChar))) != escapeKey)
            {
                onWrite(c);
            }
        }
    }

    class Uloha1 : IUloha
    {
        //while - read a pres event plnit string ? pokud string > 20 znaku udela write

        public void Execute()
        {
            new ConsoleCopier().Start();
        }
    }
}
