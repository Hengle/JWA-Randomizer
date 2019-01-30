using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using JWA_Randomizer.Entities;
using JWA_Randomizer.Enums;
using JWA_Randomizer.Utilities;

namespace JWA_Randomizer
{
    internal class Program
    {
        /// <summary>
        /// The main workhorse
        /// </summary>
        private static void Main()
        {
            Console.WriteLine("Running JWA-Randomizer.");
            Console.WriteLine();

            // Get the manager
            var manager = GetDinosaurManager();

            // While the User still wishes to randomize their team, we keep going
            while (IsUserReplyAffirmative("Would you like to randomize your team?"))
            {
                GetTeam(manager);
            }

            Exit("JWA-Randomizer completed", false);
        }

        /// <summary>
        /// Checks whether the input from the User is a valid option for selecting a randomizer
        /// </summary>
        /// <param name="question">Question or statement to display to the User</param>
        /// <param name="type">The type of randomizer chosen by the User</param>
        /// <returns>Returns true if the User replies with a valid randomizer choice</returns>
        private static bool IsUserReplyValidRandomizer(string question, out RandomizerType type)
        {
            // Display the question/statement and its options
            Console.WriteLine(question);
            Console.WriteLine("1 - Standard");
            Console.WriteLine("2 - Weighted");
            Console.WriteLine("3 - Balanced");

            // Read input from the User
            var reply = Console.ReadLine();
            Console.WriteLine();

            // Attempt to get the selected randomizer based on input
            type = Utils.GetRandomizerType(reply);

            // Return whether a valid choice was given
            return type != RandomizerType.Invalid;
        }

        /// <summary>
        /// Asks a question in the console and returns whether the User replied with affirmation
        /// </summary>
        /// <param name="question">The question to be asked to the User</param>
        /// <returns>Returns true if the User replies with either YES or Y (case insensitive)</returns>
        private static bool IsUserReplyAffirmative(string question)
        {
            // Pose the question and choices to the User
            Console.WriteLine(question);
            Console.WriteLine("Please enter either YES or NO (y/n).");

            // Read input from the User
            var reply = Console.ReadLine();
            Console.WriteLine();

            // Return whether or not the User responded affirmatively
            return reply != null &&
                   (reply.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                    reply.Equals("yes", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Initializes the DinosaurManager
        /// </summary>
        /// <returns>Returns a DinosaurManager with populated dinosaur list</returns>
        private static DinosaurManager GetDinosaurManager()
        {
            Console.WriteLine("Loading dinosaurs from XML file...");

            // Get the App.config settings
            var appSettingsReader = new AppSettingsReader();
            var dinosaurFileLocation = (string) appSettingsReader.GetValue("DinosaurFileLocation", typeof(string));

            // Create the manager
            var manager = new DinosaurManager(dinosaurFileLocation);

            try
            {
                // Attempt to read the dinosaurs from the specified file
                manager.LoadDinosaurs();
            }
            catch (Exception e)
            {
                // If there's an error, alert the User and abort the application
                Exit($"Unable to load dinosaurs:  {e.Message}", true);
                return null;
            }

            // Success! Let the User know how many dinosaurs were loaded
            Console.WriteLine(
                $"{manager.DinosaurCount} dinosaur{(manager.DinosaurCount == 1 ? string.Empty : "s")} loaded.");
            Console.WriteLine();

            return manager;
        }

        /// <summary>
        /// Method that, should the User decide, selects 8 dinosaurs from those available to be used in
        /// a team.  User can select standard, weighted, or balanced randomization.
        /// </summary>
        /// <param name="manager">The DinosaurManager used to generate the team</param>
        private static void GetTeam(DinosaurManager manager)
        {
            RandomizerType type;

            // Prompt the User for their randomizer choice, giving them 3 chances to get it right
            if (!IsUserReplyValidRandomizer("How would you like to randomize your team?", out type))
            {
                if (
                    !IsUserReplyValidRandomizer(
                        "Incorrect option.  Please select a valid randomizer option from those below.", out type))
                {
                    if (!IsUserReplyValidRandomizer("This is your last chance.  Please get it right.", out type))
                    {
                        Exit("Welp, you done mucked it up now.", true);
                        return;
                    }
                }
            }

            // Let the user know what is going on
            Console.WriteLine($"Randomizer set to {type}.");
            Console.WriteLine("Getting your team...");
            Console.WriteLine();

            List<Dinosaur> team;
            try
            {
                // Attempt to get the team using the specified randomizer
                team = manager.GetTeam(type);
            }
            catch (Exception e)
            {
                // If an error occurs, alert the User, and abort the application
                Exit($"An error occurred when trying to get your team: {e.Message}", true);
                return;
            }

            // Display the randomized team to the User
            WriteTeam(team);
        }

        /// <summary>
        /// Displays a team of dinosaurs to the console
        /// </summary>
        /// <param name="team">The team of dinosaurs to display</param>
        private static void WriteTeam(List<Dinosaur> team)
        {
            // Iterate through each dinosaur, displaying its level, name, rarity, and type(s)
            Console.WriteLine("Your team lineup:");
            team.ForEach(d =>
            {
                Console.WriteLine(d.ToString());
            });

            Console.WriteLine();
        }

        /// <summary>
        /// End the process and display a message to the User
        /// </summary>
        /// <param name="message">Message to be displayed to the User before exiting</param>
        /// <param name="waitToExit">When true, pauses exiting until User presses a key</param>
        private static void Exit(string message, bool waitToExit)
        {
            // Display the message
            Console.WriteLine(message);
            Console.WriteLine();

            // If we are to wait for the user, then we read for input
            if (waitToExit)
            {
                Console.WriteLine("Press ENTER to exit.");
                Console.ReadLine();
            }

            // Wait half a second, say "Bye!", and close the application
            Thread.Sleep(500);
            Console.WriteLine("Bye!");
            Thread.Sleep(500);

            Environment.Exit(0);
        }
    }
}