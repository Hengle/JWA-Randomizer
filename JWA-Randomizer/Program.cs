using System;
using System.Collections.Generic;
using System.Threading;
using JWA_Randomizer.Entities;
using JWA_Randomizer.Enums;
using JWA_Randomizer.Utilities;
using JWA_Randomizer.Utilities.Extensions;

namespace JWA_Randomizer
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Running JWA-Randomizer.");
            Console.WriteLine();

            var manager = GetDinosaurManager();

            while (IsUserReplyAffirmative("Would you like to randomize your team?"))
            {
                GetTeam(manager);
            }

            Exit("JWA-Randomizer completed", false);
        }

        private static bool IsUserReplyValidRandomizer(string question, out RandomizerType type)
        {
            Console.WriteLine(question);
            Console.WriteLine("1 - Standard");
            Console.WriteLine("2 - Weighted");
            Console.WriteLine("3 - Balanced");

            var reply = Console.ReadLine();
            Console.WriteLine();

            type = Utils.GetRandomizerType(reply);
            return type != RandomizerType.Invalid;
        }

        /// <summary>
        /// Asks a question in the console and returns whether the user replied with affirmation
        /// </summary>
        /// <param name="question">The question to be asked to the user</param>
        /// <returns>Returns true if the user replies with either YES or Y (case insensitive)</returns>
        private static bool IsUserReplyAffirmative(string question)
        {
            Console.WriteLine(question);
            Console.WriteLine("Please enter either YES or NO (y/n).");
            var reply = Console.ReadLine();
            Console.WriteLine();

            return reply != null &&
                   (reply.Equals("y", StringComparison.InvariantCultureIgnoreCase) ||
                    reply.Equals("yes", StringComparison.InvariantCultureIgnoreCase));
        }

        private static DinosaurManager GetDinosaurManager()
        {
            Console.WriteLine("Loading dinosaurs from XML file...");

            var manager = new DinosaurManager(@"C:/Git/JWA-Randomizer/JWA-Randomizer/Dinosaurs.xml");
            manager.LoadDinosaurs();

            Console.WriteLine($"{manager.DinosaurCount} dinosaur{(manager.DinosaurCount == 1 ? string.Empty : "s")} loaded.");
            Console.WriteLine();

            return manager;
        }

        private static void GetTeam(DinosaurManager manager)
        {
            RandomizerType type;
            if (!IsUserReplyValidRandomizer("How would you like to randomize your team?", out type))
            {
                if (
                    !IsUserReplyValidRandomizer(
                        "Incorrect option.  Please select a valid randomizer option from those below.", out type))
                {
                    if (!IsUserReplyValidRandomizer("This is your last chance.  Please get it right.", out type))
                    {
                        Exit("Welp, you done mucked it up now.", true);
                    }
                }
            }

            Console.WriteLine($"Randomizer set to {type}.");

            Console.WriteLine("Getting your team...");
            Console.WriteLine();

            var team = manager.GetTeam(type);
            WriteTeam(team);
        }

        private static void WriteTeam(List<Dinosaur> team)
        {
            if (team.IsNullOrEmpty())
            {
                Console.WriteLine("Unable to create a full dinosaur team.");
                Console.WriteLine();
                return;
            }

            Console.WriteLine("Your team lineup:");
            team.ForEach(d =>
            {
                Console.WriteLine(d.ToString());
            });

            Console.WriteLine();
        }

        /// <summary>
        /// End the process and display a message to the user
        /// </summary>
        /// <param name="message">Message to be displayed to the user before exiting</param>
        /// <param name="waitToExit">When true, pauses exiting until User presses a key</param>
        private static void Exit(string message, bool waitToExit)
        {
            Console.WriteLine(message);
            Console.WriteLine();

            if (waitToExit)
            {
                Console.WriteLine("Press ENTER to exit.");
                Console.ReadLine();
            }

            Thread.Sleep(500);
            Console.WriteLine("Bye!");
            Thread.Sleep(500);

            Environment.Exit(0);
        }
    }
}
