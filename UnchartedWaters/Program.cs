using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnchartedWaters {
    internal class Program {
        static void Main(string[] args) {

            int[,,] data = new int[10, 10, 3];
            data = GetSatelliteData();
            GetLayerPercentage(data);
            Console.WriteLine($"Ally to enemy sub ratio - {GetRatio(data)}\n");
            AttackDecider(data);
            OceanDisplay(data);

            

        }//END MAIN

        static int[,,] GetSatelliteData() {
            Random rng = new Random();
            int[,,] data = new int[10, 10, 3];
            

            for (int z = 0; z < data.GetLength(2); z++ ) {
                for (int y = 0;  y < data.GetLength(1);y++) {
                    for (int x = 0; x < data.GetLength(0); x++) {

                        if (rng.Next(0,101) < 25) {
                            data[x, y, z] = rng.Next(1, 3);
                            
                        } else {
                            data[x, y, z] = 0;
                        }//END IF ELSE
                    }//END FOR
                }//END FOR
            }//END FOR
            return data;
        }//END GET SATELLITE DATE FUNCTION

        static double GetLayerPercentage(int[,,] data) {
            double percentage = 0.0;
            string depth = "";
            double subs = 0;
            for (int z = 0; z < data.GetLength(2); z++) {
                for (int y = 0; y < data.GetLength(1); y++) {
                    for (int x = 0; x < data.GetLength(0); x++) {
                        if (data[x,y,z] == 1 || data[x, y, z] == 2) {
                            subs++;
                        }//END IF
                    }//END FOR
                }//END FOR
                switch (z) {
                    case 0:
                        depth = "Surface";
                            break;
                    case 1:
                        depth = "Under water";
                        break;
                    case 2:
                        depth = "Deep ocean";
                        break;
                }//END CASES
                            percentage = subs / 100;
                            Console.WriteLine($"{depth} sub percentage - %{percentage}\n");
                            subs = 0;
            }//END FOR
            return percentage;
        }//END GET LAYER PERCENTAGE FUNCTION

        static double GetRatio(int[,,] data) {
            double ratio        = 0.0;
            double enemySubs    = 0.0;
            double friendlySubs = 0.0;

            for (int z = 0; z < data.GetLength(2); z++) {
                for (int y = 0; y < data.GetLength(1); y++) {
                    for (int x = 0; x < data.GetLength(0); x++) {
                        if (data[x, y, z] == 1) {
                            friendlySubs++;                            
                        } else if (data[x, y, z] == 2) {
                            enemySubs++;
                        }//END ELSE IF
                    }//END FOR
                }//END FOR
            }//END FOR
            ratio = friendlySubs / enemySubs;

            return ratio;
        }//END GET LAYER FUNCTION

        static bool AttackDecider(int[,,] data) {
            bool shouldAttack = false;
            int friendlySubs = 0;
            int enemySubs = 0;
            string depth = "";

            for (int z = 0; z < data.GetLength(2); z++) {
                for (int y = 0; y < data.GetLength(1); y++) {
                    for (int x = 0; x < data.GetLength(0); x++) {
                        if (data[x, y, z] == 1) {
                            friendlySubs++;
                        } else if (data[x, y, z] == 2) {
                            enemySubs++;
                        }//END ELSE IF
                    }//END FOR
                }//END FOR
                        if (friendlySubs > enemySubs) {
                            shouldAttack = true;
                        }
                switch (z) {
                    case 0:
                        depth = "surface";
                        break;
                    case 2:
                        depth = "under water";
                        break;
                    case 3:
                        depth = "deep ocean";
                        break;
                }

                Console.WriteLine($"Attack on {depth} layer? - {shouldAttack}\n");

                friendlySubs = 0; 
                enemySubs = 0;
            }//END FOR
            return shouldAttack;
        }//END ATTACK DECIDER

        static void OceanDisplay(int[,,] data) {

            string depth = "";
            for (int z = 0; z < data.GetLength(2); z++) {
                switch (z) {
                    case 0: 
                        depth = "Surface";
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case 1:
                        depth = "Under Water";
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case 2:
                        depth = "Deep Ocean";
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                }//END SWITCH
            Console.WriteLine($"\n{depth}");
                for (int y = 0; y < data.GetLength(1); y++) {
                    for (int x = 0; x < data.GetLength(0); x++) {
                        
                        Console.Write($"{data[x,y,z]} ");
                    }//END FOR
                        Console.Write("\n");
                }//END FOR
            }//END FOR
            Console.ResetColor();
        }//END OCEAN DISPLAY
        

        #region HELPER FUNCTIONS
        static void Fancify(string text) {

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("**##=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=##**");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\t--->>\\{text}/<<---");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("**##=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=##**");

            Console.ResetColor();

        }//END FANCIFY
        static string Prompt(string request) {
            //Variables
            string userInput = "";

            //Request Information From User
            Console.Write(request);

            //Receive Response
            userInput = Console.ReadLine();

            return userInput;

        }//END PROMPT HELPER
        static int PromptInt(string request) {

            int userInput = 0;
            bool isValid = false;

            Console.Write(request);
            isValid = int.TryParse(Console.ReadLine(), out userInput);

            while (isValid == false) {

                Console.WriteLine("ERROR: NO REAL NUMBER");
                Console.Write(request);
                isValid = int.TryParse(Console.ReadLine(), out userInput);
            }//END WHILE

            return userInput;

        }//END PROMPT TRY INT
        static double PromptDouble(string request) {

            double userInput = 0;
            bool isValid = false;

            Console.Write(request);
            isValid = double.TryParse(Console.ReadLine(), out userInput);

            while (isValid == false) {

                Console.WriteLine("ERROR: NO REAL NUMBER");
                Console.Write(request);
                isValid = double.TryParse(Console.ReadLine(), out userInput);
            }

            return userInput;

        }//END PROMPT DOUBLE
        #endregion
    }//END CLASS
}//END NAMESPACE
