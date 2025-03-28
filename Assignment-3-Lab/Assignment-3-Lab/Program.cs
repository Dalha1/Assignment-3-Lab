namespace Assignment3
{
    internal class Program
    {
        /// <summary>
        /// Main method for Assignment 3.
        /// Program allows the user to enter/save/load/edit/view daily time tracking values from a file.
        /// Allows simple data analysis for a given month.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool continueProgram = true;

            // TODO: 
            // declare a constant to represent the maximum size of the arrays
            // arrays must be large enough to store data for an entire month 
            const int MAX_DAYS_IN_MONTH = 31;

            // TODO:
            // create a string array named dates, using the max size constant you created above to specify the physical size of the array
            string[] dates = new string[MAX_DAYS_IN_MONTH];
            // TODO:
            // create a double array named minutes, using the max size constant you created above to specify the physical size of the array
            double[] minutes = new double[MAX_DAYS_IN_MONTH];
            int count = 0;
            DisplayProgramIntro();
            // TODO: call DisplayMainMenu()
            DisplayMainMenu();
            while (continueProgram)
            {
                string mainMenuChoice = Prompt("Enter MAIN MENU option ('D' to display menu): ").ToUpper();
                Console.WriteLine();

                //MAIN MENU Switch statement
                switch (mainMenuChoice)
                {
                    case "N": //[N]ew Daily Entries

                        if (AcceptNewEntryDisclaimer())
                        {
                            // TODO: call EnterDailyValues & assign its return value
                            EnterDailyValues(dates, minutes, count);
                            Console.WriteLine($"\nEntries completed. {count} records in temporary memory.\n");
                        }
                        else
                        {
                            Console.WriteLine("Cancelling new data entry. Returning to MAIN MENU.");
                        }
                        break;
                    case "S": //[S]ave Entries to File
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before SAVING.");
                        }
                        else if (AcceptSaveEntryDisclaimer())
                        {
                            string filename = PromptForFilename();
                            // TODO: call SaveToFile()
                            SaveToFile(filename, dates, minutes, count);
                        }
                        else
                        {
                            Console.WriteLine("Cancelling save operation. Returning to MAIN MENU.");
                        }

                        break;
                    case "E": //[E]dit Entries
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before EDITING.");
                        }
                        else if (AcceptEditEntryDisclaimer())
                        {
                            //TODO: call EditEntries()
                            EditEntries(dates, minutes, count);
                        }
                        else
                        {
                            Console.WriteLine("Cancelling EDIT operation. Returning to MAIN MENU.");
                        }
                        break;
                    case "L": //[L]oad  File
                        if (AcceptLoadEntryDisclaimer())
                        {
                            string filename = Prompt("Enter name of file to load: ");
                            // TODO: call LoadFromFile() and assign its return value
                            LoadFromFile(dates, minutes, filename);
                            Console.WriteLine($"{count} records were loaded.\n");
                        }
                        else
                        {
                            Console.WriteLine("Cancelling LOAD operation. Returning to MAIN MENU.");
                        }
                        break;
                    case "V":
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before VIEWING.");
                        }
                        else
                        {
                            // TODO: call DisplayEntries()
                            DisplayEntries(dates, minutes, count);
                        }
                        break;
                    case "M": //[M]onthly Statistics
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before ANALYSIS.");
                        }
                        else
                        {
                            RunAnalysisMenu(dates, minutes, count);
                        }
                        break;
                    case "D": //[D]isplay Main Menu
                        //TODO: call DisplayMainMenu()
                        DisplayMainMenu();
                        break;
                    case "Q": //[Q]uit Program
                        bool quit = Prompt("Are you sure you want to quit (y/n)? ").ToLower().Equals("y");
                        Console.WriteLine();
                        if (quit)
                        {
                            continueProgram = false;
                        }
                        break;
                    default: //invalid entry. Reprompt.
                        Console.WriteLine("Invalid reponse. Enter one of the letters to choose a menu option.");
                        break;
                }
            }

            DisplayProgramOutro();
        }

        /// <summary>
        /// Runs the analysis sub-menu to display summary metrics.
        /// </summary>
        /// <param name="dates">an array containing dates in YYYY-MM-DD format</param>
        /// <param name="numbers">an array containing numeric values</param>
        /// <param name="count">logical count of elements</param>
        static void RunAnalysisMenu(string[] dates, double[] numbers, int count)
        {
            bool runAnalysis = true;
            string year = dates[0].Substring(0, 4),
                month = dates[0].Substring(5, 3);

            while (runAnalysis)
            {
                string analysisMenuChoice;

                // TODO: call DisplayAnalysisMenu()
                DisplayAnalysisMenu();

                analysisMenuChoice = Prompt("Enter ANALYSIS sub-menu option: ").ToUpper();
                Console.WriteLine();

                switch (analysisMenuChoice)
                {
                    case "A": //[A]verage 
                        // TODO: uncomment the next 2 lines & call CalculateMean();
                        double mean = CalculateMean(numbers, count);
                        Console.WriteLine($"The mean value for {month} {year} is: {mean:N2}.\n");
                        break;
                    case "H": //[H]ighest 
                        // TODO: uncomment the next 2 lines & call CalculateLargest();
                        double largest = CalculateLargest(numbers, count);
                        Console.WriteLine($"The largest value for {month} {year} is: {largest:N2}.\n");
                        break;
                    case "L": //[L]owest 
                        //TODO: uncomment the next 2 lines & call CalculateSmallest();
                        double smallest = CalculateSmallest(numbers, count);
                        Console.WriteLine($"The smallest value for {month} {year} is: {smallest:N2}.\n");
                        break;
                    case "G": //[G]raph 
                        //TODO: call DisplayChart()
                        DisplayChart(dates, numbers, count);
                        Prompt("Press <enter> to continue...");
                        break;
                    case "R": //[R]eturn to MAIN MENU
                        runAnalysis = false;
                        break;
                    default: //invalid entry. Reprompt.
                        Console.WriteLine("Invalid reponse. Enter one of the letters to choose a submenu option.");
                        break;
                }
            }
        }

        // ================================================================================================ //
        //                                                                                                  //
        //                                              METHODS                                             //
        //                                                                                                  //
        // ================================================================================================ //

        // ++++++++++++++++++++++++++++++++++++ Difficulty 1 ++++++++++++++++++++++++++++++++++++

        // TODO: create the DisplayMainMenu() method
        static void DisplayMainMenu()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            Console.WriteLine("[N]ew Daily Entries");
            Console.WriteLine("[S]ave Entries to File");
            Console.WriteLine("[E]dit Entries");
            Console.WriteLine("[L]oad File");
            Console.WriteLine("[V]iew Entered/Loaded Data");
            Console.WriteLine("[M]onthly Statistics");
            Console.WriteLine("[D]isplay Main Menu");
            Console.WriteLine("[Q]uit Program");
            Console.WriteLine();
        }


        // TODO: create the DisplayAnalysisMenu() method
        static void DisplayAnalysisMenu()
        {
            Console.WriteLine("ANALYSIS SUB-MENU");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("[A]verage");
            Console.WriteLine("[H]ighest");
            Console.WriteLine("[L]owest");
            Console.WriteLine("[G]raph");
            Console.WriteLine("[R]eturn to Main Menu");



        }
        // TODO: create the Prompt method
        static string Prompt(string promptMessage)
        {
            Console.Write(promptMessage);
            return Console.ReadLine();
        }
        // TODO: create the PromptDouble() method
        static double PromptDouble(string promptMessage)
        {
            double value;
            while (!double.TryParse(Prompt(promptMessage), out value))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            return value;
        }
        // optional TODO: create the PromptInt() method
        static int PromptInt(string promptMessage)
        {
            int value;
            while (!int.TryParse(Prompt(promptMessage), out value))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
            return value;
        }
        // TODO: create the CalculateLargest() method
        static double CalculateLargest(double[] values, int countOfEntries)
        {
            double largest = values[0];
            for (int i = 1; i < countOfEntries; i++)
            {
                if (values[i] > largest)
                    largest = values[i];
            }
            return largest;
        }
        // TODO: create the CalculateSmallest() method
        static double CalculateSmallest(double[] values, int countOfEntries)
        {
            double smallest = values[0];
            for (int i = 1; i < countOfEntries; i++)
            {
                if (values[i] < smallest)
                    smallest = values[i];
            }
            return smallest;
        }
        // TODO: create the CalculateMean() method
        static double CalculateMean(double[] values, int countOfEntries)
        {
            double sum = 0;
            for (int i = 0; i < countOfEntries; i++)
            {
                sum += values[i];
            }
            return sum / countOfEntries;
        }

        // ++++++++++++++++++++++++++++++++++++ Difficulty 2 ++++++++++++++++++++++++++++++++++++

        // TODO: create the EnterDailyValues method
        static int EnterDailyValues(string[] dates, double[] values, int count)
        {
            string month = string.Empty;
            int year = 0;
            double value = 0.0;

            Console.Write("Enter the month (e.g. JAN): ");
            month = Console.ReadLine().ToUpper();
            while (!IsValidMonth(month))
            {
                Console.WriteLine("Invalid month format. Please use three letters (e.g., JAN, FEB)");
                Console.Write("Enter the month (e.g. JAN): ");
                month = Console.ReadLine().ToUpper();
            }
            Console.WriteLine();

            do
            {
                Console.Write("Enter the year (yyyy): ");
            } while (!int.TryParse(Console.ReadLine(), out year) || year < 2000 || year >= 2025);
            Console.WriteLine();

            Console.WriteLine("Hint: Enter -1 to cancel and exit.");

            for (int i = 0; i < 31; i++)
            {
                string currentDate = $"{year}-{month}-{(i + 1):d2}";


                if (Array.IndexOf(dates, currentDate, 0, count) != -1)
                {
                    Console.WriteLine($"Date {currentDate} already exists. Skipping...");
                    continue;
                }

                Console.Write($"Enter the minutes for day {i + 1}: ");
                if (!double.TryParse(Console.ReadLine(), out value))
                {
                    Console.WriteLine("Please enter a valid number.");
                    i--;
                    continue;
                }

                if (value == -1)
                {
                    break;
                }

                if (value >= 0)
                {
                    dates[count] = currentDate;
                    values[count] = value;
                    count++;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input (must be non-negative).");
                    i--;
                }
            }

            return count;
        }
        private static bool IsValidMonth(string month)
        {
            string[] validMonths = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN",
                                   "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
            return Array.IndexOf(validMonths, month) != -1;
        }

        // TODO: create the LoadFromFile method
        static int LoadFromFile(string[] dates, double[] minutes, string filename)
        {
            int count = 0;
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string header = reader.ReadLine();
                    if (header != "Date,Minutes")
                    {
                        throw new Exception("Invalid file format. Expected 'Date,Minutes' header.");
                    }

                    string line;
                    while ((line = reader.ReadLine()) != null && count < dates.Length)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2 && double.TryParse(parts[1], out double value))
                        {
                            if (!IsValidDateFormat(parts[0]))
                            {
                                throw new Exception($"Invalid date format: {parts[0]}. Expected YYYY-MMM-DD");
                            }
                            dates[count] = parts[0];
                            minutes[count] = value;
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
                return 0;
            }
            return count;
        }

        private static bool IsValidDateFormat(string date)
        {
            try
            {
                if (date.Length != 11) return false;
                if (date[4] != '-' || date[8] != '-') return false;

                string year = date.Substring(0, 4);
                string month = date.Substring(5, 3);
                string day = date.Substring(9, 2);

                if (!int.TryParse(year, out int y) || y < 2000 || y >= 2025) return false;
                if (!int.TryParse(day, out int d) || d < 1 || d > 31) return false;

                string[] validMonths = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN",
                                      "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
                return Array.IndexOf(validMonths, month.ToUpper()) != -1;
            }
            catch
            {
                return false;
            }
        }
        // TODO: create the SaveToFile method
        static void SaveToFile(string filename, string[] dates, double[] minutes, int count)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine("Date,Minutes");
                    for (int i = 0; i < count; i++)
                    {
                        writer.WriteLine($"{dates[i]},{minutes[i]:F2}");
                    }
                }
                Console.WriteLine($"Data saved successfully to {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }
        // TODO: create the DisplayEntries method
        static void DisplayEntries(string[] dates, double[] minutes, int count)
        {
            Console.WriteLine("\n=== Daily Entries ===");
            Console.WriteLine("No. | Date       | Minutes");
            Console.WriteLine("----|-----------|---------");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{i + 1,3} | {dates[i]} | {minutes[i],7:F2}");
            }
            Console.WriteLine("=====================");
        }
        // ++++++++++++++++++++++++++++++++++++ Difficulty 3 ++++++++++++++++++++++++++++++++++++

        // TODO: create the EditEntries method
        static void EditEntries(string[] dates, double[] minutes, int count)
        {
            DisplayEntries(dates, minutes, count);

            do
            {
                int index = PromptInt("Enter the number of the entry to edit (1-" + count + ") or 0 to finish: ");
                if (index == 0) break;

                index--; 
                if (index >= 0 && index < count)
                {
                    Console.WriteLine($"Current value for {dates[index]}: {minutes[index]}");
                    double newValue = PromptDouble("Enter new minutes value: ");
                    if (newValue >= 0)
                    {
                        minutes[index] = newValue;
                        Console.WriteLine("Entry updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid value. Minutes must be non-negative.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid entry number!");
                }
                Console.WriteLine();
            } while (true);
        }
        // ++++++++++++++++++++++++++++++++++++ Difficulty 4 ++++++++++++++++++++++++++++++++++++

        // TODO: create the DisplayChart method
        static void DisplayChart(string[] dates, double[] minutes, int count)
        {
            if (count == 0) return;

            Console.WriteLine($"\n=== Study Time for the month of {dates[0].Substring(5, 3)} ===\n");
            double maxValue = CalculateLargest(minutes, count);
            int maxBars = 40;
            int yAxisSteps = 7;
            double stepSize = maxValue / yAxisSteps;

            for (int step = yAxisSteps; step >= 0; step--)
            {
                double currentValue = step * stepSize;
                Console.Write($"{currentValue,6:F0}|");

                for (int i = 0; i < count; i++)
                {
                    if (minutes[i] >= currentValue)
                        Console.Write(" ||");
                    else
                        Console.Write("   ");
                }
                Console.WriteLine();
            }

            Console.Write("      |");
            for (int i = 0; i < count; i++)
            {
                Console.Write("---");
            }
            Console.WriteLine();

            Console.Write(" Days |");
            for (int i = 0; i < count; i++)
            {
                Console.Write($" {dates[i].Substring(9, 2)}");
            }
            Console.WriteLine("\n");
        }
        // ********************************* Helper methods *********************************

        /// <summary>
        /// Displays the Program intro.
        /// </summary>
        static void DisplayProgramIntro()
        {
            Console.WriteLine("****************************************\n" +
                "*                                      *\n" +
                "*            Monthly  Sales            *\n" +
                "*                                      *\n" +
                "****************************************\n");
        }

        /// <summary>
        /// Displays the Program outro.
        /// </summary>
        static void DisplayProgramOutro()
        {
            Console.Write("Program terminated. Press ENTER to exit program...");
            Console.ReadLine();
        }

        /// <summary>
        /// Displays a disclaimer for NEW entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool AcceptNewEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.\n" +
                "Hint: Select EDIT from the main menu instead, to change individual days.\n");
            response = Prompt("Do you wish to proceed anyway? (y/n) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        /// <summary>
        /// Displays a disclaimer for SAVE entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool AcceptSaveEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: saving to an EXISTING file will overwrite data currently on that file.\n" +
                "Hint: Files will be saved to this program's directory by default.\n" +
                "Hint: If the file does not yet exist, it will be created.\n");
            response = Prompt("Do you wish to proceed anyway? (y/n) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        /// <summary>
        /// Displays a disclaimer for EDIT entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool AcceptEditEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: editing will overwrite unsaved sales values.\n" +
                "Hint: Save to a file before editing.\n");
            response = Prompt("Do you wish to proceed anyway? (y/n ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        /// <summary>
        /// Displays a disclaimer for LOAD entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool AcceptLoadEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.\n" +
                "Hint: If you entered New Daily sales entries, save them first!\n");
            response = Prompt("Do you wish to proceed anyway? (y/n) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        /// <summary>
        /// Displays prompt for a filename, and returns a valid filename. 
        /// Includes exception handling.
        /// </summary>
        /// <returns>User-entered string, representing valid filename (.txt or .csv)</returns>
        static string PromptForFilename()
        {
            string filename = "";
            bool isValidFilename = true;
            const string CSV_FILE_EXTENSION = ".csv";
            const string TXT_FILE_EXTENSION = ".txt";

            do
            {
                filename = Prompt("Enter name of .csv or .txt file to save to (e.g. JAN-2025-data.csv): ");
                if (filename == "")
                {
                    isValidFilename = false;
                    Console.WriteLine("Please try again. The filename cannot be blank or just spaces.");
                }
                else
                {
                    if (!filename.EndsWith(CSV_FILE_EXTENSION) && !filename.EndsWith(TXT_FILE_EXTENSION)) //if filename does not end with .txt or .csv.
                    {
                        filename = filename + CSV_FILE_EXTENSION; //append .csv to filename
                        Console.WriteLine("It looks like your filename does not end in .csv or .txt, so it will be treated as a .csv file.");
                        isValidFilename = true;
                    }
                    else
                    {
                        Console.WriteLine("It looks like your filename ends in .csv or .txt, which is good!");
                        isValidFilename = true;
                    }
                }
            } while (!isValidFilename);
            return filename;
        }

    }
}