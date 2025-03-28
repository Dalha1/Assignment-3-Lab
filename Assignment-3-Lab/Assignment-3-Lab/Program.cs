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
                           count = EnterDailyValues(dates, minutes, count);
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
            // Display main menu heading 
            Console.WriteLine("Main Menu");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            // Display Main menu options 
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
            // Display Analysis sub menu heading and options 
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
            // Display prompt and return user input
            Console.Write(promptMessage);
            return Console.ReadLine();
        }
        // TODO: create the PromptDouble() method
        static double PromptDouble(string promptMessage)
        {
            // Continue prompting until valid double is entered
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
            // Continue prompting until valid integer is entered
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
            // Initialize largest with first value
            double largest = values[0];
            // Compare with remaining values
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
            // Initialize smallest with first value
            double smallest = values[0];
            // Compare with remaining values
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
            // Calculate sum of all values
            double sum = 0;
            for (int i = 0; i < countOfEntries; i++)
            {
                sum += values[i];
            }
            // Return average (sum divided by count)
            return sum / countOfEntries;
        }

        // ++++++++++++++++++++++++++++++++++++ Difficulty 2 ++++++++++++++++++++++++++++++++++++

        // TODO: create the EnterDailyValues method
        static int EnterDailyValues(string[] dates, double[] values, int count)
        {
            // Initialize variables
            string month = string.Empty;
            int year = 0;
            double value = 0.0;
            // Get and validate month input
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
                // Get and validate year input
                Console.Write("Enter the year (yyyy): ");
            } while (!int.TryParse(Console.ReadLine(), out year) || year < 2000 || year >= 2025);
            Console.WriteLine();

            Console.WriteLine("Hint: Enter -1 to cancel and exit.");
            // Main input loop
            do
            {
                // Process each day of the month
                for (int i = 1; i <= 31; i++)
                {
                    Console.WriteLine();
                    Console.Write($"Enter the minutes for day {i}: ");
                    // Validate numeric input
                    if (!double.TryParse(Console.ReadLine(), out value))
                    {
                        Console.WriteLine("Please enter a valid input.");
                        i--; // Retry same day
                    }
                    // Check for exit condition
                    if (value == -1)
                    {
                       
                        break;
                    }
                    // Process valid input
                    if (value >= 0)
                    {
                        string currentDate = $"{year}-{month}-{i:D2}";
                        dates[count] = currentDate;
                        values[count] = value;
                        count++;
                        
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid input.");
                        i--;  // Retry same day
                        value = -1;
                    }

                    Console.WriteLine("Hint: Enter -1 to cancel and exit.");
                }
            } while (value != -1);

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
            // Initialize counter for number of records loaded
            int count = 0;
            try
            {
                // Attempt to open and read the file using StreamReader
                using (StreamReader reader = new StreamReader(filename))
                {
                    // Read the first line which should be the heading
                    string header = reader.ReadLine();
                    // Verify the file has the correct header format
                    if (header != "Date,Minutes")
                    {
                        throw new Exception("Invalid file format. Expected 'Date,Minutes' header.");
                    }
                    // Read the file line by line
                    string line;
                    while ((line = reader.ReadLine()) != null && count < dates.Length)
                    {
                        // Split each line into date and minutes parts
                        string[] parts = line.Split(',');
                        // Verify line has both date and minutes
                        if (parts.Length == 2 && double.TryParse(parts[1], out double value))
                        {
                            // Validate the date format (YYYY-MMM-DD)
                            if (!IsValidDateFormat(parts[0]))
                            {
                                throw new Exception($"Invalid date format: {parts[0]}. Expected YYYY-MMM-DD");
                            }
                            // Store the valid date and minutes in arrays
                            dates[count] = parts[0];
                            minutes[count] = value;
                            // Increment counter for successful record
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors (file not found, format issues, etc.)
                Console.WriteLine($"Error loading file: {ex.Message}");
                return 0;// Return 0 to indicate no records were loaded
            }
            // Return the number of records successfully loaded
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
                // Create or open file for writing using StreamWriter
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    // Write the required header line
                    writer.WriteLine("Date,Minutes");
                    // Write each record from the arrays
                    for (int i = 0; i < count; i++)
                    {
                        // Format each line as: date,minutes
                        // Minutes formatted to 2 decimal places
                        writer.WriteLine($"{dates[i]},{minutes[i]:F2}");
                    }
                }
                // Confirm successful save to user
                Console.WriteLine($"Data saved successfully to {filename}");
            }
            catch (Exception ex)
            {
                // Handle any errors (permission issues, disk full, etc.)
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }
        // TODO: create the DisplayEntries method
        static void DisplayEntries(string[] dates, double[] minutes, int count)
        {
            // Display header
            Console.WriteLine("Current Entries");
            Console.WriteLine("=====================\n");
            // Display column headers
            Console.WriteLine("Date        Minutes");
            Console.WriteLine("----------- -----------");
            // Display each entry
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{dates[i]}  {minutes[i],7:F2}");
            }
            Console.WriteLine("=====================");
        }
        // ++++++++++++++++++++++++++++++++++++ Difficulty 3 ++++++++++++++++++++++++++++++++++++

        // TODO: create the EditEntries method
        static void EditEntries(string[] dates, double[] minutes, int count)
        {
            // Check for data to edit
            if (count == 0)
            {
                Console.WriteLine("Sorry, LOAD data or enter NEW data before EDITING.");
                return;
            }
            // Show current data
            DisplayEntries(dates, minutes, count);
            // Get month and year from first entry
            string month = dates[0].Substring(5, 3);
            string year = dates[0].Substring(0, 4);
            Console.WriteLine($"You are currently editing data for: {month}-{year}");

            bool continueEditing = true;
            while (continueEditing)
            {
                // Get day to edit
                Console.Write("Enter the day of the month (e.g. 31) for which you want to edit the minutes value: ");
                if (!int.TryParse(Console.ReadLine(), out int day) || day < 1 || day > 31)
                {
                    Console.WriteLine("Invalid day. Please enter a number between 1 and 31.");
                    continue;
                }
                // Find entry to edit
                string dateToEdit = $"{year}-{month}-{day:D2}";
                int index = Array.IndexOf(dates, dateToEdit, 0, count);

                if (index == -1)
                {
                    Console.WriteLine($"No entry found for day {day}.");
                    continue;
                }
                // Get new value
                Console.Write($"Enter the NEW # of minutes for day {day}: ");
                if (!double.TryParse(Console.ReadLine(), out double newValue) || newValue < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a non-negative number.");
                    continue;
                }
                // Update and confirm
                minutes[index] = newValue;
                Console.WriteLine($"Successfully changed value for day {day} to {newValue}.");
                Console.WriteLine();
                // Check if user wants to continue
                Console.Write("Do you wish to edit another value? (y/n) ");
                continueEditing = Console.ReadLine().ToLower().StartsWith("y");

                if (!continueEditing)
                {
                    Console.WriteLine("Hint: SAVE your changes to a file!");
                    Console.WriteLine("Returning to MAIN MENU.");
                }
            }
        }
        // ++++++++++++++++++++++++++++++++++++ Difficulty 4 ++++++++++++++++++++++++++++++++++++

        // TODO: create the DisplayChart method
        static void DisplayChart(string[] dates, double[] minutes, int count)
        {
            // Exit if no data to display
            if (count == 0) return;
            // Display the chart title with the month from the first date
            Console.WriteLine($"\n=== Study Time for the month of {dates[0].Substring(5, 3)} ===\n");
            // Find the maximum value for scaling the chart
            double maxValue = CalculateLargest(minutes, count);
            // Set up chart parameters
            int maxBars = 40;
            int yAxisSteps = 7;
            double stepSize = maxValue / yAxisSteps;// Calculate size of each step
            // Draw Y-axis scale and bars
            for (int step = yAxisSteps; step >= 0; step--)
            {
                // Calculate and display the current scale value
                double currentValue = step * stepSize;
                Console.Write($"{currentValue,6:F0}|");
                // Draw horizontal bars for each data point at this level
                for (int i = 0; i < count; i++)
                {
                    // If value meets or exceeds current level, draw bar segment
                    if (minutes[i] >= currentValue)
                        Console.Write(" ||");
                    else
                        Console.Write("   "); // Empty space if value is below current level
                }
                Console.WriteLine();// Move to next line after drawing all points
            }
            // Draw X-axis base line
            Console.Write("      |");// Align with scale values
            for (int i = 0; i < count; i++)
            {
                Console.Write("---"); // Draw base line segments
            }
            Console.WriteLine();
            // Draw X-axis labels (days)
            Console.Write(" Days |");
            for (int i = 0; i < count; i++)
            {
                // Display day number from date string
                Console.Write($" {dates[i].Substring(9, 2)}");
            }
            Console.WriteLine("\n");// Add extra line for spacing
        }
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