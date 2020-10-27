using System;
using System.Collections.Generic;
using System.Linq;

namespace SecCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Title = "Crypt Game Bot";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Title();
            AllAbout();

            Console.ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Title();

            Console.WriteLine("\n\n THE GAME starts...");

            again:
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n Enter the level (1 / 2 / 3) : ");

            int level, i;
            string lev = Console.ReadLine();
            if(lev.Equals("1") || lev.Equals("2") || lev.Equals("3"))
            {
                level = Convert.ToInt32(lev);
                level+=3;
            }
            else
            {
                Console.WriteLine(" Be aware of your inputs! ;P");
                goto again;
            }
            
            var code = new List<int>();
            var input = new List<int>();
            int[] x = new int[2];

            code = CodeGenerator(level);
            Console.WriteLine(" Code Generated !");

            Console.WriteLine(" Now, Guess the Code!");
            i = 8;
            do
            {
                input = PlayerInput(level);
                x = CheckCode(code, input);
                Console.WriteLine(" Game-Machine (" + x[0] + ", " + x[1] + ")");
                GameMachineDisplay(x, level);
                if(Console.ForegroundColor == ConsoleColor.DarkYellow)
                    break;
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n You have " + (i - 1) + " attempt(s) remaining");
                i--;
                if(i == 0)
                {
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(" Oops!.. GAME OVER!");
                }
            }while(i > 0);

            PrintCode(code);
            
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n Do you want to play once more (yes(y)/no(n)) ? ");
            string again = Console.ReadLine();
            again = again.ToLower();
            if(again == "yes" || again == "y")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Title();
                goto again;
            }
            else
            {
                Console.WriteLine("\n Press any key to exit..");
                Console.ReadKey();
                System.Environment.Exit(0);
            }
        }

        static void Title()
        {
            //Console.SetWindowSize(126, 30);

            string title;
            title = "CRYPT!";
            Console.SetCursorPosition(Console.WindowWidth/2 - title.Length/2, 1);
            Console.WriteLine(title);
            Console.SetCursorPosition(Console.WindowWidth/2 - 35, 3);
            Console.WriteLine("______________________________________________________________________");
        }

        static void AllAbout()
        {
            Console.WriteLine("\n About CRYPT! : ");
            Console.WriteLine(" A logical game where the player has to crack the code that is generated, by guessing the code digit by digit as per the output of the Game-Machine, for each attempt.");
            Console.WriteLine(" Attempts are limited for each of the three levels of the game.");
            Console.WriteLine(" And no repeated digits are there in the code (0 - 9).");
            Console.WriteLine("\n More: ");
            Console.WriteLine(" * There are 3 levels for the game: \n  1. Easy - 4 digit code\n  2. Medium - 5 digit code\n  3. Hard - 6 digit code");
            Console.WriteLine(" * Player can attempt to crack the code 8 times.");
            Console.WriteLine(" * Guess the code digit by digit. After entering your code, the Game-Machine prints \n    O - White Circle: O means that  of the digits entered is present in the code, but not in the correct position.\n    0 - Black Circle: 0 means that  of the digits entered is present in the code and is in the correct position.");
            Console.WriteLine("    For eg, Let the Code be '4836' and let your entry is '1234', then the Game-Machine generates \"0O\"\n     0 represents the digit 3 which is present and also in correct position in the code.\n     O represents the digit 4 which is present in the code, but not in the correct position.");
            Console.WriteLine("\n ThAt's All About THE GAME!..");
            Console.WriteLine("\n Press any key to play the game..");
        }

        static List<int> CodeGenerator(int lvl)
        {
            var code = new List<int>();
            Random digGen = new Random();
            int num, i;
            for(i = 0; i < lvl; i++)
            {
                label:
                num = digGen.Next(0, 10);
                if(!code.Contains(num))
                    code.Add(num);
                else
                    goto label;
                //Console.Write(code[i]);
            }
            //Console.Writeline();
            return code;
        }

        static void PrintCode(List<int> code)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n The Secret Code is ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            foreach(int digit in code)
                Console.Write(digit);
            Console.ResetColor();
            Console.Write("\n\n");
        }
        
        static List<int> PlayerInput(int lvl)
        {
            char[] dig = new char[10] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

            input:
            Console.Write("\n Enter the " + lvl + " digit code: ");
            string input = Convert.ToString(Console.ReadLine());
            int k;
            for(int i = 0; i < input.Length; i++)
            {
                k = 0;
                foreach(char x in dig)
                {
                    if(input[i] == x)
                        k++;
                }
                if(k == 0)
                {
                    Console.WriteLine(" Oops!.. Enter a whole number code ;)");
                    goto input;
                }
            }

            var _input = input.Select(digit => int.Parse(digit.ToString()));
            List<int> inp = new List<int>();
            foreach(var obj in _input)
                inp.Add(obj);
            
            if(inp.Count == lvl)
                return inp;
            else
            {
                Console.WriteLine(" Invalid Entry! Once more!");
                return PlayerInput(lvl);
            }
        }
        
        static int[] CheckCode(List<int> code, List<int> input)
        {
            int[] x = new int[2] {0, 0};
            var fm = new List<int>();
            var nm = new List<int>();
            for(int i = 0; i < input.Count; i++)
            {
                for(int j = 0; j < input.Count; j++)
                {
                    if(input.ElementAt(i) == code.ElementAt(j))
                    {
                        if(i == j)
                        {
                            fm.Add(input.ElementAt(i));
                            x[0]++;
                        }
                    }
                }
            }
            for(int i = 0; i < input.Count; i++)
            {
                for(int j = 0; j < input.Count; j++)
                {
                    if(fm.Contains(input.ElementAt(i)) || nm.Contains(input.ElementAt(i)))
                        continue;
                    if(input.ElementAt(i) == code.ElementAt(j))
                    {
                        nm.Add(input.ElementAt(i));
                        x[1]++;
                    }
                }
            }
            return x;
        }

        static void GameMachineDisplay(int[] x, int lvl)
        {
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            switch(x[0])
            {
                case 0: Console.Write(" ");
                        if(x[1] == 0)
                            for(int i = 0; i < lvl; i++)
                                Console.Write(" ");
                        else
                            Checkx1(x[1]);
                        break;
                case 1: Console.Write(" 0");
                        Checkx1(x[1]);
                        break;
                case 2: Console.Write(" 00");
                        Checkx1(x[1]);
                        break;
                case 3: Console.Write(" 000");
                        Checkx1(x[1]);
                        break;
                case 4: Console.Write(" 0000");
                        if(lvl == 4)
                        {
                            Won();
                            break;
                        }
                        else
                        {
                            Checkx1(x[1]);
                            break;
                        }
                case 5: Console.Write(" 00000");
                        if(lvl == 5)
                        {
                            Won();
                            break;
                        }
                        else
                        {
                            Checkx1(x[1]);
                            break;
                        }
                case 6: Console.Write(" 000000");
                        if(lvl == 6)
                        {
                            Won();
                            break;
                        }
                        else
                        {
                            Checkx1(x[1]);
                            break;
                        }
                default: Console.ForegroundColor = ConsoleColor.Red;
                         Console.Write(" Error!");
                         break;
            }
        }

        static void Checkx1(int x1)
        {
            switch(x1)
            {
                case 0: Console.Write(" ");
                        break;
                case 1: Console.Write("O ");
                        break;
                case 2: Console.Write("OO ");
                        break;
                case 3: Console.Write("OOO ");
                        break;
                case 4: Console.Write("OOOO ");
                        break;
                case 5: Console.Write("OOOOO ");
                        break;
                case 6: Console.Write("OOOOOO ");
                        break;
                default: Console.ForegroundColor = ConsoleColor.Red;
                         Console.Write(" Error!");
                         break;
            }
        }

        static void Won()
            {
                Console.Write(" ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("\n Yeyy!.. You Won!\n CODE DECRYPTED!\n");
            }
    }
}
